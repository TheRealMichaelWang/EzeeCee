using EzeeCee.Typing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.IntermediateRepresentation
{
    public abstract partial class FunctionCall : IValue
    {
        public abstract bool IsValidTypeArgument { get; }

        public List<ITypeArgument> TypeArguments { get; private set; }
        public List<IValue> Arguments { get; private set; }

        public FunctionCall(List<IValue> arguments, List<ITypeArgument> typeArguments)
        {
            Arguments = arguments;
            TypeArguments = typeArguments;
        }
        
        public abstract IValue SubstituteValueWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs);
        public abstract bool IsCompatibleWith(ITypeArgument typeArgument);
    }

    public static class ArgumentExtensions
    {
        public static List<IValue> SubstituteWithTypeargs(this List<IValue> arguments, Dictionary<TypeParameter, ITypeArgument> typeargs) => arguments.ConvertAll(argument => argument.SubstituteValueWithTypeargs(typeargs));

        public static bool IsCompatibleWith(this List<IValue> arguments, List<IValue> otherArguments)
        {
            if (arguments.Count != otherArguments.Count)
                return false;
            for (int i = 0; i < arguments.Count; i++)
                if (!arguments[i].IsCompatibleWith(otherArguments[i]))
                    return false;
            return true;
        }
    }
}
