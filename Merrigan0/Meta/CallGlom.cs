using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [WhatItIs("A glom containing all context required to execute a parameterized action.")]
    [Untested]
    public class CallGlom : Glom { //// internal?
        public virtual Executor Executor { get; private set; }
        public virtual Execution ParentExecution { get; private set; }

        public CallGlom(Execution parent) : base() {
            ParentExecution = parent;
            if (parent != null) {
                Executor = parent.Executor;
            }
        }
    }
}
