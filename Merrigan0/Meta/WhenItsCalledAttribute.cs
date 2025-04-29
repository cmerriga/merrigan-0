using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Method)]
    [Untested]
    public class WhenItsCalledAttribute : Attribute {
        protected string whenItsCalled;

        public WhenItsCalledAttribute(string whenItsCalled) {
            this.whenItsCalled = whenItsCalled;
        }
    }
}
