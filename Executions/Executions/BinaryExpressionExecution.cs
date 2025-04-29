using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public abstract class BinaryExpressionExecution<T> : ExpressionExecution<T> {
        //public override Action Action { get { return binaryExpression; } }
        public PrerequisiteExecution Left { get { return (PrerequisiteExecutions == null) ? (PrerequisiteExecution)null : PrerequisiteExecutions[0]; } }
        public PrerequisiteExecution Right { get { return (PrerequisiteExecutions == null) ? (PrerequisiteExecution)null : PrerequisiteExecutions[1]; } }

        //// NOTIMPL
        protected BinaryExpressionExecution(Execution parent, BinaryExpression<T> binaryExpression) : base(parent, binaryExpression) { }
    }
}
