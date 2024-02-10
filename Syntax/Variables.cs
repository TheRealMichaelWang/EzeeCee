using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    public sealed partial class SymbolReference : IValue, IStatement, IDeclaration
    {
        public SourceLocation SourceLocation { get; private set; }
        public string[] SymbolPath { get; private set; }

        public SymbolReference(SourceLocation sourceLocation, string[] symbolPath)
        {
            SourceLocation = sourceLocation;
            SymbolPath = symbolPath;
        }

        public IElement 
    }
}
