using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public abstract class Expression : Action {
        protected Expression(object[] namesAndPrerequisites) : base(namesAndPrerequisites) { }
    }

    public abstract class Expression<T> : Expression {
        protected Expression(params object[] namesAndPrerequisites) : base(namesAndPrerequisites) { }
    }
}
