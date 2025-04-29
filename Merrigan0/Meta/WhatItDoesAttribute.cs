using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Method)]
    [Untested]
    //// combine with WhatItIs, maybe others
    public class WhatItDoesAttribute : Attribute {
        public String Notes { get; private set; }
        public String Summary { get; private set; }

        public WhatItDoesAttribute(string summary) { Summary = summary; }

        public WhatItDoesAttribute(string summary, string notes) {
            Notes = notes;
            Summary = summary;
        }
    }
}
