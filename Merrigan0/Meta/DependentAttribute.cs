using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    [Untested]
    public class DependentAttribute : Attribute {
        private string[] nmlPaths;

        public Array<MethodInfo> DependsOn { get { return null; } } /////

        public DependentAttribute(params string[] nmlPaths) { this.nmlPaths = nmlPaths; }
    }
}
