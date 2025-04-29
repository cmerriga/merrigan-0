using System;
using Executions.StructuredProgramming.Expressions;

namespace Executions.StructuredProgramming {
    public class If : Block {
        public Condition Condition { get; private set; }

        public If(Condition condition, Statement[] statements/*, Variable[] variables*/) : base(statements/*, variables*/) {
            Condition = condition;
        }
    }
}
