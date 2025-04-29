using System;

namespace Executions.StructuredProgramming {
    public partial class Block : Statement {
        public Statement[] Statements { get; private set; }
        ////public Variable[] Variables { get; private set; }

        public Block(Statement[] statements/*, Variable[] variables*/) {
            Statements = statements;
            ////Variables = variables;
        }
    }
}
