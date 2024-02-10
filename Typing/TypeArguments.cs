using EzeeCee.Linking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Typing
{
    partial interface IType
    {
        public IType SubstituteTypeWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
    }

    public partial interface ITypeArgument
    {
        public bool IsValidTypeArgument { get; }

        public bool IsCompatibleWith(ITypeArgument typeArgument);
        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
    }

    public partial interface ITypeArgumentRestriction
    {
        public ITypeArgumentRestriction SubstituteRestrictionWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
        public bool ValidateTypearg(ITypeArgument typeArgument);
    }

    public sealed partial class TypeParameter : ISymbol
    {
        public string Name { get; private set; }
        public string SymbolType => "type";

        public List<ITypeArgumentRestriction> Restrictions { get; private set; }

        public TypeParameter(string name, List<ITypeArgumentRestriction> restrictions)
        {
            Name = name;
            Restrictions = restrictions;
        }
    }

    public static class TypeArgumentsExtension
    {
        public static List<ITypeArgument> SubstituteWithTypeargs(this List<ITypeArgument> arguments, Dictionary<TypeParameter, ITypeArgument> typeargs) => arguments.ConvertAll(argument => argument.SubstituteTypeargWithTypeargs(typeargs));

        public static bool IsCompatibleWith(this List<ITypeArgument> arguments, List<ITypeArgument> otherArguments)
        {
            if (arguments.Count == otherArguments.Count)
                return false;

            for(int i = 0; i < arguments.Count; i++)
                if (!arguments[i].IsCompatibleWith(otherArguments[i]))
                    return false;
            return true;
        }
    }

    public sealed partial class NonEmptyTypeRequirement : ITypeArgumentRestriction
    {
        public NonEmptyTypeRequirement()
        {

        }

        public ITypeArgumentRestriction SubstituteRestrictionWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => this;

        public bool ValidateTypearg(ITypeArgument typeArgument) => typeArgument is IType type && type.IsEmpty;
    }
}
