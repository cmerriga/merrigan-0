using System;

namespace Executions.StructuredProgramming.Expressions {
    public class VariableValue {
        public string VariableName { get; private set; }

        public VariableValue(string variableName) {
            VariableName = variableName;
        }
    }
}
