using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [WhatItIs("A glom containing all context required to execute a parameterized action.")]
    [Untested]
    internal class GlobalCallGlom : CallGlom { //// internal?
        public override Executor Executor { get { return Executor.Ambient; } }
        public override Execution ParentExecution { get { return null; } }

        public GlobalCallGlom() : base(null) { }
    }
}
