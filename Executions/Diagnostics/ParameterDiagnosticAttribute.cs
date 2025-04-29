using System;
using System.Reflection;

namespace Executions.Diagnostics {
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParameterDiagnosticAttribute : Attribute {
        public abstract void Check(ParameterInfo parameterInfo, object value);
    }
}
