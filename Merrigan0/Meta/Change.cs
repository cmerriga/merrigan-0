using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [Untested]
    public class Change<T> {
        public ExecutionContext Context;
        public Change<T> Previous;
        public T Value;
    }
}
