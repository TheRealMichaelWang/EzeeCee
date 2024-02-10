using EzeeCee.Linking;
using EzeeCee.Syntax.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    public sealed partial class FunctionCall : IValue, IStatement, IDeclaration, ITypeArgument
    {
        public SourceLocation SourceLocation { get; private set; }

        public string[] SymbolToCall { get; private set; }
        public List<IElement> Arguments { get; private set; }

        public FunctionCall(SourceLocation sourceLocation, string[] symbolToCall, List<IElement> arguments)
        {
            SourceLocation = sourceLocation;
            SymbolToCall = symbolToCall;
            Arguments = arguments;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments)
        {
            MacroDeclaration? macroDeclaration = linker.FindSymbol<MacroDeclaration>()
            return new FunctionCall(SourceLocation, SymbolToCall, Arguments.SubstituteMacros(linker, macroArguments));
        }
    }

    public sealed partial class BinaryOperator : IValue
    {
        public SourceLocation SourceLocation { get; private set; }

        public Token Operator { get; private set; }

        public IValue Left { get; private set; }
        public IValue Right { get; private set; }

        public BinaryOperator(SourceLocation sourceLocation, Token @operator, IValue left, IValue right)
        {
            SourceLocation = sourceLocation;
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new BinaryOperator(SourceLocation, Operator, Left.SubstituteMacros(macroArguments).As<IValue>(), Right.SubstituteMacros(macroArguments).As<IValue>());
    }

    public sealed partial class GetAtIndex : IValue
    {
        public SourceLocation SourceLocation { get; private set; }

        public IValue Array { get; private set; }
        public IValue Index { get; private set; }

        public GetAtIndex(SourceLocation sourceLocation, IValue array, IValue index)
        {
            SourceLocation = sourceLocation;
            Array = array;
            Index = index;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new GetAtIndex(SourceLocation, Array.SubstituteMacros(macroArguments).As<IValue>(), Index.SubstituteMacros(macroArguments).As<IValue>());
    }

    public sealed partial class SetAtIndex : IValue, IStatement
    {
        public SourceLocation SourceLocation { get; private set; }

        public IValue Array { get; private set; }
        public IValue Index { get; private set; }
        public IValue SetValue { get; private set; }

        public SetAtIndex(SourceLocation sourceLocation, IValue array, IValue index, IValue setValue)
        {
            SourceLocation = sourceLocation;
            Array = array;
            Index = index;
            SetValue = setValue;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new SetAtIndex(SourceLocation, Array.SubstituteMacros(macroArguments).As<IValue>(), Index.SubstituteMacros(macroArguments).As<IValue>(), SetValue.SubstituteMacros(macroArguments).As<IValue>());
    }

    public sealed partial class GetProperty : IValue
    {
        public SourceLocation SourceLocation { get; private set; }

        public IValue Struct { get; private set; }
        public string PropertyName { get; private set; }

        public GetProperty(SourceLocation sourceLocation, IValue @struct, string propertyName)
        {
            SourceLocation = sourceLocation;
            Struct = @struct;
            PropertyName = propertyName;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new GetProperty(SourceLocation, Struct.SubstituteMacros(macroArguments).As<IValue>(), PropertyName);
    }

    public sealed partial class SetProperty : IValue, IStatement
    {
        public SourceLocation SourceLocation { get; private set; }

        public IValue Struct { get; private set; }
        public string PropertyName { get; private set; }
        public IValue SetValue { get; private set; }

        public SetProperty(SourceLocation sourceLocation, IValue @struct, string propertyName, IValue setValue)
        {
            SourceLocation = sourceLocation;
            Struct = @struct;
            PropertyName = propertyName;
            SetValue = setValue;
        }

        public IElement SubstituteMacros(Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => new SetProperty(SourceLocation, Struct.SubstituteMacros(linker, macroArguments).As<IValue>(), PropertyName, SetValue.SubstituteMacros(linker, macroArguments).As<IValue>());
    }

    public static class ArgumentExtensions
    {
        public static List<IElement> SubstituteMacros(this List<IElement> arguments, Linker linker, Dictionary<MacroParameter, IElement> macroArguments) => arguments.ConvertAll(argument => argument.SubstituteMacros(linker, macroArguments));
    }
}
