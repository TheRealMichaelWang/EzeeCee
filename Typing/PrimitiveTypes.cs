using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Typing
{
    public abstract partial class Primitive : IType
    {
        public bool IsEmpty => false;
        public bool IsValidTypeArgument => true;

        public Primitive()
        {

        }

        public abstract bool IsCompatibleWith(IType type);

        public bool IsCompatibleWith(ITypeArgument typeArgument)
        {
            if (typeArgument is IType type)
                return IsCompatibleWith(type);
            return false;
        }

        public virtual IType SubstituteTypeWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => this;

        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => SubstituteTypeWithTypeargs(typeargs);
    }

}
