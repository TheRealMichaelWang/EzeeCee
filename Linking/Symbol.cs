using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Linking
{
    public partial interface ISymbol
    {
        public string Name { get; }
        public string SymbolType { get; }
        public bool IsPrivate { get; }
    }

    public partial class SymbolMarshaller
    {
        public class SymbolContainer
        {
            private Dictionary<string, ISymbol> symbols;

            public SymbolContainer()
            {
                symbols = new Dictionary<string, ISymbol>();
            }

            public bool DeclareSymbol(ISymbol symbol)
            {
                if (symbols.ContainsKey($"{symbol.Name}{symbol.SymbolType}"))
                    return false;
                symbols.Add($"{symbol.Name}{symbol.SymbolType}", symbol);
                return true;
            }

            public ISymbol? FindSymbol(string name, params string[] types)
            {
                foreach(string type in types)
                {
                    ISymbol? toFind;
                    if (symbols.TryGetValue($"{name}{type}", out toFind))
                        return toFind;
                }
                return null;
            }
        }

        public sealed class Module : SymbolContainer, ISymbol
        {
            public string Name { get; private set; }
            public string SymbolType => "module";
            public bool IsPrivate => false;

            public Module(string name) : base()
            {
                Name = name;
            }
        }

        private SymbolContainer topLevelSymbols;
        private Stack<SymbolContainer> currentWorkingContainers;

        public SymbolMarshaller()
        {
            topLevelSymbols = new SymbolContainer();
            currentWorkingContainers = new Stack<SymbolContainer>();
            currentWorkingContainers.Push(topLevelSymbols);
        }

        public ISymbol? FindSymbol(string[] symbolPath, params string[] expectedTypes)
        {
            Stack<SymbolContainer> currentWorking = new(currentWorkingContainers);
            while (currentWorking.Count > 0)
            {
                SymbolContainer currentContainer = currentWorking.Pop();
                for (int i = 0; i < symbolPath.Length - 1; i++)
                {
                    ISymbol? container = currentContainer.FindSymbol(symbolPath[i], "module");

                    if (container == null || container is not SymbolContainer module || container.IsPrivate)
                        goto next_current_working;
                    else
                        currentContainer = module;
                }
                ISymbol? result = currentContainer.FindSymbol(symbolPath.Last(), expectedTypes);
                if (result != null)
                {
                    if (symbolPath.Length > 1 && result.IsPrivate)
                        return null;
                    return result;
                }

                next_current_working:
                continue;
            }
            return null;
        }

        public ISymbol? FindSymbol(string symbolPath, params string[] expectedTypes) => FindSymbol(new string[] { symbolPath }, expectedTypes);

        public bool DeclareSymbol(ISymbol symbol) => currentWorkingContainers.Peek().DeclareSymbol(symbol);

        public void WithWorkingSymbolContainer(SymbolContainer symbolContainer, Action action)
        {
            currentWorkingContainers.Push(symbolContainer);
            action();
            currentWorkingContainers.Pop();
        }
    }

    //represents empty forward declared symbol that will later be set
    public sealed class ForwardDeclaredSymbol: ISymbol
    {
        public string Name { get; private set; }
        public string SymbolType { get; private set; }
        public bool IsPrivate => false;

        private ISymbol? symbol = null;
        public bool IsLinked => symbol != null;

        public ForwardDeclaredSymbol(string name, string symbolType)
        {
            Name = name;
            SymbolType = symbolType;
        }

        public ISymbol Value
        {
            get
            {
                if (symbol == null)
                    throw new InvalidOperationException($"Unlinked symbol {Name} of type {SymbolType} hasn't been linked yet!");
                return symbol;
            }
            set
            {
                if (value.Name != Name)
                    throw new InvalidOperationException($"Expected symbol of name {Name}, but got {value.Name} instead.");
                if (value.SymbolType != SymbolType)
                    throw new InvalidOperationException($"Expected symbol of type {SymbolType}, but got {value.SymbolType} instead.");
                symbol = value;
                if (symbol.IsPrivate)
                    throw new InvalidOperationException($"Expected public symbol, but got a private one.");
            }
        }
    }

    public static class ISymbolExtensions
    {
        public static T? As<T>(this ISymbol symbol) where T : ISymbol
        {
            if (symbol is T elem)
                return elem;
            return null;
        }
    }
}
