using System;
using System.Reflection;

namespace Executions.Diagnostics {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class MemberDiagnosticAttribute : Attribute {
        ////public abstract void Check(MemberInfo memberInfo, object value);
    }
}
