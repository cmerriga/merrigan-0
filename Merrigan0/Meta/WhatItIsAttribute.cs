using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.All)]
    [Untested]
    public class WhatItIsAttribute : Attribute {
        public String Notes { get; private set; }
        public String Summary { get; private set; }

        public WhatItIsAttribute(string summary) {
            Summary = summary;
        }

        public WhatItIsAttribute(string summary, string notes) {
            Notes = notes;
            Summary = summary;
        }
    }
}
