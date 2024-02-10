using EzeeCee.Linking;
using EzeeCee.Typing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Linking
{
    public sealed partial class Linker : SymbolMarshaller
    {
        public Linker()
        {

        }
    }
}

namespace EzeeCee.Syntax
{
    partial interface IDeclaration
    {
        public void ForwardDeclare(Linker linker);
        public void Link(Linker linker);
    }

    public partial interface IExecutable
    {
        public IntermediateRepresentation.IElement GenerateIR(Linker linker);
    }

    partial interface IValue : IExecutable
    {

    }

    partial interface IStatement : IExecutable
    {

    }
}

namespace EzeeCee.IntermediateRepresentation
{
    partial interface IElement
    {
        public IElement Link(Linker linker);
    }

    public sealed partial class UnlinkedFunctionCall : IValue, IStatement
    {
        public bool IsValidTypeArgument => false;

        public ForwardDeclaredSymbol SymbolToCall { get; private set; }
        public List<IValue> Arguments { get; private set; }
        
        public UnlinkedFunctionCall(ForwardDeclaredSymbol symbolToCall, List<IValue> arguments)
        {
            SymbolToCall = symbolToCall;
            Arguments = arguments;
        }

        public IValue SubstituteValueWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => new UnlinkedFunctionCall(SymbolToCall, Arguments.SubstituteWithTypeargs(typeargs));

        public IStatement SubstituteStatementWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => new UnlinkedFunctionCall(SymbolToCall, Arguments.SubstituteWithTypeargs(typeargs));

        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => throw new InvalidOperationException();

        public bool IsCompatibleWith(ITypeArgument typeArgument) => throw new InvalidOperationException();
    }

    public sealed partial class UnlinkedSymbolReference : IValue
    {
        public bool IsValidTypeArgument => false;

        public ForwardDeclaredSymbol SymbolToRead { get; private set; }

        public UnlinkedSymbolReference(ForwardDeclaredSymbol symbolToRead)
        {
            SymbolToRead = symbolToRead;
        }

        public IValue SubstituteValueWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => this;

        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typeargs) => this;

        public bool IsCompatibleWith(ITypeArgument typeArgument) => throw new InvalidOperationException();
    }
}

namespace EzeeCee.Typing
{
    partial interface IType
    {
        public IType Link(Linker linker);
    }

    public sealed partial class UnlinkedType : IType
    {
        public bool IsEmpty => throw new InvalidOperationException();
        public bool IsValidTypeArgument => false;

        public ForwardDeclaredSymbol Symbol { get; private set; }
        public List<ITypeArgument> TypeArguments { get; private set; }
        
        public UnlinkedType(ForwardDeclaredSymbol symbol, List<ITypeArgument> typeArguments)
        {
            Symbol = symbol;
            TypeArguments = typeArguments;
        }

        public IType SubstituteTypeWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typearg) => new UnlinkedType(Symbol, TypeArguments.SubstituteWithTypeargs(typearg));

        public ITypeArgument SubstituteTypeargWithTypeargs(Dictionary<TypeParameter, ITypeArgument> typearg) => new UnlinkedType(Symbol, TypeArguments.SubstituteWithTypeargs(typearg));

        public bool IsCompatibleWith(IType type)
        {
            if (type is UnlinkedType unlinkedType)
                return Symbol == unlinkedType.Symbol && TypeArguments.IsCompatibleWith(unlinkedType.TypeArguments);
            return false;
        }

        public bool IsCompatibleWith(ITypeArgument typeArgument)
        {
            if (typeArgument is IType type)
                return IsCompatibleWith(type);
            return false;
        }
    }
}