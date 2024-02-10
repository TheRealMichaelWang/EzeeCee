using EzeeCee.Linking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Typing
{
    public sealed partial class TemplateDeclaration : ISymbol
    {
        public string Name { get; private set; }
        public string SymbolType => "type";

        public List<TypeParameter> TypeParameters { get; private set; }
        public IType BaseType { get; private set; }

        public TemplateDeclaration(string name, List<TypeParameter> typeParameters, IType baseType)
        {
            Name = name;
            TypeParameters = typeParameters;
            BaseType = baseType;
        }
    }

    public sealed partial class TemplateType : IType
    {
        public bool IsEmpty => false;
        public bool IsValidTypeArgument => true;

        public TemplateDeclaration Declaration { get; private set; }
        public List<ITypeArgument> TypeArguments { get; private set; }

        public TemplateType(TemplateDeclaration declaration, List<ITypeArgument> typeArguments)
        {
            Declaration = declaration;
            TypeArguments = typeArguments;
        }

        public bool IsCompatibleWith(IType type) => type is TemplateType templateType && templateType.Declaration == Declaration && templateType.TypeArguments.IsCompatibleWith(templateType.TypeArguments);

        public bool IsCompatibleWith(ITypeArgument typeArgument) => typeArgument is IType type && IsCompatibleWith(type);

        public IType SubstituteTypeWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => new TemplateType(Declaration, TypeArguments.SubstituteWithTypeargs(typeargs));

        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => SubstituteTypeWithTypeargs(typeargs);
    }
}
