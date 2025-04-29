using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class AddLongExecution : BinaryExpressionExecution<long> {
        public AddLongExpression AddLongExpression { get { return (AddLongExpression)Action; } }

        public AddLongExecution(Execution parent, AddLongExpression expression) : base(parent, expression) { }

        protected override bool TryFinish() {
            TypedResult = (long)Left.Value + (long)Right.Value;
            return true;
        }
    }
}
