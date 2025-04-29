using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0 {
    // An attribute that defines restrictions that will cause invalid-call failures if not
    // met.
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Untested]
    public abstract class CommitmentAttribute : Attribute {
        public abstract void Test(object instance, MethodInfo methodInfo);
        public void Test(MethodInfo methodInfo) { Test(null, methodInfo); }
    }
}
