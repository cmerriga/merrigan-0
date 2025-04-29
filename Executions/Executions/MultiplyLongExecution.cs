using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class MultiplyLongExecution : BinaryExpressionExecution<long> {
        public MultiplyLongExpression MultiplyLongExpression { get { return (MultiplyLongExpression)Action; } }

        public MultiplyLongExecution(Execution parent, MultiplyLongExpression expression) : base(parent, expression) { }

        protected override bool TryFinish() {
            TypedResult = (long)Left.Value * (long)Right.Value;
            return true;
        }
    }
}
