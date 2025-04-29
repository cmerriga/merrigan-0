using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    // A set with no members.
    [Untested]
    internal class EmptySet<T> : Set<T> {
        public static readonly EmptySet<T> Only = new EmptySet<T>();

        public override long Length { get { return 0L; } }

        public override Set<T> And(Set<T> set2) { return this; }
        public override bool Contains(T item) { return false; }

        public override IEnumerator<T> GetEnumerator() { yield break; }

        public override Set<T> Or(Set<T> set2) { return set2; }
        public override Set<T> Without(T element) { return this; }
        public override Set<T> With(T element) { return new SingleElementSet<T>(element); }
    }
}
