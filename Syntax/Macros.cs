using EzeeCee.Linking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    partial interface IElement
    {
        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments);
    }

    public partial class MacroParameter : ISymbol
    {
        public string Name { get; private set; }
        public string SymbolType => "macro";
        public bool IsPrivate => true;

        public MacroParameter(string name)
        {
            Name = name;
        }
    }

    public sealed partial class ValueMacroParameter : MacroParameter
    {
        public ValueMacroParameter(string name) : base(name)
        {

        }
    }

    public sealed partial class StatementMacroParameter : MacroParameter
    {
        public StatementMacroParameter(string name) : base(name)
        {

        }
    }

    public sealed partial class MacroDeclaration : ISymbol, IDeclaration
    {
        public SourceLocation SourceLocation { get; private set; }
        
        public string Name { get; private set; }
        public string SymbolType => "macro";
        public bool IsPrivate { get; private set; }
        public List<MacroParameter> Parameters { get; private set; }

        public IElement SubstitutionSource { get; private set; }

        private bool IsSubstituting = false;

        public MacroDeclaration(SourceLocation sourceLocation, string name, List<MacroParameter> parameters, bool isPrivate, IElement substitutionSource)
        {
            SourceLocation = sourceLocation;
            Name = name;
            Parameters = parameters;
            SubstitutionSource = substitutionSource;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new MacroDeclaration(SourceLocation, Name, Parameters, IsPrivate, SubstitutionSource.SubstituteMacros(linker, macroArguments));

        public IElement GetSubstituted(List<IElement> arguments)
        {
            Debug.Assert(!IsSubstituting);
            Debug.Assert(Parameters.Count == arguments.Count);

            IsSubstituting = true;
            Dictionary<MacroParameter, IElement> macroArguments = new(arguments.Count);
            for (int i = 0; i < arguments.Count; i++)
                macroArguments.Add(Parameters[i], arguments[i]);
            IElement substituted = SubstitutionSource.SubstituteMacros(macroArguments);
            IsSubstituting = false;
            return substituted;
        }
    }
}
