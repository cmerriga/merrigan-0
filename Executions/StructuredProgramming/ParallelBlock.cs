using System;

namespace Executions.StructuredProgramming {
    public class ParallelBlock : Block{
        public ParallelBlock(Statement[] statements)
            : base(statements/*, Array.Empty<Variable>()*/) {
        }
    }
}
