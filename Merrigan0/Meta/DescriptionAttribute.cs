using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.All)]
    [Untested]
    public class DescriptionAttribute : Attribute {
        private string description;

        public String Description { get { return description; } }

        public DescriptionAttribute(string description) {
            this.description = description;
        }
    }
}
