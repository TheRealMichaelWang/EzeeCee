using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    public sealed partial class If : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public IValue Condition { get; private set; }

        public List<IStatement> Statements { get; private set; }

        public If(SourceLocation sourceLocation, IValue condition, List<IStatement> statements)
        {
            SourceLocation = sourceLocation;
            Condition = condition;
            Statements = statements;
        }
    }

    public sealed partial class IfElse : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public IValue Condition { get; private set; }

        public List<IStatement> IfTrueStatements { get; private set; }
        public List<IStatement> IfFalseStatements { get; private set; }

        public IfElse(SourceLocation sourceLocation, IValue condition, List<IStatement> ifTrueStatements, List<IStatement> ifFalseStatements)
        {
            SourceLocation = sourceLocation;
            Condition = condition;
            IfTrueStatements = ifTrueStatements;
            IfFalseStatements = ifFalseStatements;
        }
    }

    public sealed partial class While : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public IValue Condition { get; private set; }

        public List<IStatement> Statements { get; private set; }

        public While(SourceLocation sourceLocation, IValue condition, List<IStatement> statements)
        {
            SourceLocation = sourceLocation;
            Condition = condition;
            Statements = statements;
        }
    }

    public sealed partial class DoWhile : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public IValue Condition { get; private set; }

        public List<IStatement> Statements { get; private set; }

        public DoWhile(SourceLocation sourceLocation, IValue condition, List<IStatement> statements)
        {
            SourceLocation = sourceLocation;
            Condition = condition;
            Statements = statements;
        }
    }

    public sealed partial class ForLoop : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public IStatement InitialStatement { get; private set; }
        public IValue ConditionStatement { get; private set; }
        public IStatement IncrementStatement { get; private set; }

        public List<IStatement> Statements { get; private set; }

        public ForLoop(SourceLocation sourceLocation, IStatement initialStatement, IValue conditionStatement, IStatement incrementStatement, List<IStatement> statements)
        {
            SourceLocation = sourceLocation;
            InitialStatement = initialStatement;
            ConditionStatement = conditionStatement;
            IncrementStatement = incrementStatement;
            Statements = statements;
        }
    }

    public sealed partial class StatementExpression : IStatement, IValue
    {
        public SourceLocation SourceLocation { get; private set; }
        public List<IStatement> Statements { get; private set; }
        public IValue ToEvaluate { get; private set; }

        public StatementExpression(SourceLocation sourceLocation, List<IStatement> statements, IValue toEvaluate)
        {
            SourceLocation = sourceLocation;
            Statements = statements;
            ToEvaluate = toEvaluate;
        }
    }

    public sealed partial class CodeBlock : IStatement
    {
        public SourceLocation SourceLocation { get; private set; }
        public List<IStatement> Statements;
        
        public CodeBlock(SourceLocation sourceLocation, List<IStatement> statements)
        {
            SourceLocation = sourceLocation;
            Statements = statements;
        }
    }
}
