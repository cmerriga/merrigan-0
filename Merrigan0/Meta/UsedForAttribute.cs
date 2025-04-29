using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Untested]
    public class UsedForAttribute : Attribute {
        private string what;

        public String What { get { return what; } }

        public UsedForAttribute(string what) {
            this.what = what;
        }
    }
}
