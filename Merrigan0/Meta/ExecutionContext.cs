using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    ///// Make a Node, not a MutableMap
    [Untested]
    public class ExecutionContext : MutableMap<String, object> {
        public static ExecutionContext Ambient { get; private set; }
    }
}
