using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class EqualLongExecution : BinaryExpressionExecution<bool> {
        public EqualLongExpression EqualLongExpression { get { return (EqualLongExpression)Action; } }

        public EqualLongExecution(Execution parent, EqualLongExpression expression) : base(parent, expression) { }

        protected override bool TryFinish() {
            TypedResult = (long)Left.Value == (long)Right.Value;
            return true;
        }
    }
}
