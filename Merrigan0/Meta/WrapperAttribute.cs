using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // The method is purely a wrapper for another one in the class
    [AttributeUsage(AttributeTargets.Method)]
    [Untested]
    public class WrapperAttribute : Attribute {
        public string WrappedMethodName { get;  private set; }

        public WrapperAttribute() : this(null) { }

        public WrapperAttribute(string wrappedMethodName) {
            WrappedMethodName = wrappedMethodName;
        }
    }
}
