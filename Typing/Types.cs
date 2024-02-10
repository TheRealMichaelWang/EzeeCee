using EzeeCee.Linking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Typing
{
    public partial interface IType : ITypeArgument
    {
        public bool IsEmpty { get; }

        public bool IsCompatibleWith(IType type);
    }

    public abstract partial class EmptyType : IType
    {
        public bool IsEmpty => true;
        public bool IsValidTypeArgument => false;

        public abstract bool IsCompatibleWith(IType type);
    }

    public abstract partial class NonEmptyType : IType
    {
        public bool IsEmpty => false;
        public bool IsValidTypeArgument => true;

        public abstract bool IsCompatibleWith(IType type);
    }
}
