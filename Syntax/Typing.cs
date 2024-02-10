using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    public sealed partial class Type : IElement, ITypeArgument
    {
        public SourceLocation SourceLocation { get; private set; }

        public string[] Symbol { get; private set; }
        public List<ITypeArgument> TypeArguments { get; private set; }

        public Type(SourceLocation sourceLocation, string[] symbol, List<ITypeArgument> typeArguments)
        {
            SourceLocation = sourceLocation;
            Symbol = symbol;
            TypeArguments = typeArguments;
        }
    }

    public partial interface ITypeArgument
    {

    }
}