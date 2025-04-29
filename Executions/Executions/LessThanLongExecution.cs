using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class LessThanLongExecution : BinaryExpressionExecution<bool> {
        public LessThanLongExpression LessThanLongExpression { get { return (LessThanLongExpression)Action; } }

        public LessThanLongExecution(Execution parent, LessThanLongExpression expression) : base(parent, expression) { }

        protected override bool TryFinish() {
            TypedResult = (long)Left.Value < (long)Right.Value;
            return true;
        }
    }
}
