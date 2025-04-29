using System;
using Executions.Diagnostics;
using Executions.Diagnostics.MemberAttributes;

namespace Executions.StructuredProgramming {
    public partial class Assignment<T> : Statement {
        [Create("expression")]
        public Expression<T> Expression { get; private set; }

        public Type Type { get { return typeof(T); } }

        [Create("variableName")]
        public string VariableName { get; private set; }
        ////public Variable<T> Variable { get; private set; }

        public Assignment(string variableName, Expression<T> expression) {
            DebugOnly.CheckParameters(variableName, expression);
            Expression = expression;
            VariableName = variableName;
            ////Variable = variable;
        }
    }

    //// possibly Assignment<T> for Expression<T> and Variable<T>
}
