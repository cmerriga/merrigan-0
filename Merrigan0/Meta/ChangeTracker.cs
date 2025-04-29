using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [Untested]
    internal class ChangeTracker<T> {
        protected static Change<T> Dummy = new Change<T>();

        public T Current { get { return History.Value; } }
        public Change<T> History { get; private set; }

        public ChangeTracker() : this(Dummy) { }

        public ChangeTracker(Change<T> root) {
            History = root;
        }

        public void Change(T current) {
            History = new Change<T> {
                Context = ExecutionContext.Ambient,
                Previous = History,
                Value = current
            };
        }
    }
}
