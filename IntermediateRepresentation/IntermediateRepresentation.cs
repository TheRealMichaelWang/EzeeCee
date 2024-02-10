using EzeeCee.Typing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.IntermediateRepresentation
{
    public partial interface IElement
    {

    }

    public partial interface IValue : IElement, ITypeArgument
    {=
        public IValue SubstituteValueWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
    }

    public partial interface IStatement : IElement
    {
        public IStatement SubstituteStatementWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
    }

    public abstract partial class StaticLiteral : IValue
    {
        public bool IsValidTypeArgument => true;

        public abstract IValue SubstituteValueWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
        public abstract bool IsCompatibleWith(ITypeArgument typeArgument);
    }
}