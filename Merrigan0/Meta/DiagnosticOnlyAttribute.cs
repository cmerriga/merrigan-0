using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // An attribute that marks a method as 
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Struct | AttributeTargets.Property)]
    [Untested]
    public class DiagnosticOnlyAttribute : Attribute { }
}
