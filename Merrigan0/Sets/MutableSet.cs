using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.MetaInternal; // ChangeTracker
using Merrigan0.SetsInternal;

namespace Merrigan0 {
    [Untested]
    public class MutableSet<T> /*: Set<T>*/ {
        private ChangeTracker<Set<T>> changeTracker = new ChangeTracker<Set<T>>();

        public virtual Set<T> Current { get { return changeTracker.Current; } }
        public virtual Change<Set<T>> History { get { return changeTracker.History; } }

        public MutableSet() : this(EmptySet<T>.Only) { }

        public MutableSet(Set<T> current) {
            changeTracker.Change(current);
        }

        public void Add(T value) { changeTracker.Change(Current.With(value)); }
        public void Remove(T value) { changeTracker.Change(Current.Without(value)); }
        public override string ToString() { return Current.ToString(); }
    }
}
