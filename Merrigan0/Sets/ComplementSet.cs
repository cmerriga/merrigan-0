using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    // Like an "anti-set". Stores only things that are not in it.
    [Untested]
    internal class ComplementSet<T> : Set<T> {
        private Set<T> baseSet;

        public override Set<T> Complement { get { return baseSet; } }
        public override bool Inverse { get { return !baseSet.Inverse; } }

        public override long Length { get { throw new NotSupportedException(); } }

        public ComplementSet(Set<T> baseSet) { this.baseSet = baseSet; }

        public override bool Contains(T value) { return !baseSet.Contains(value); }

        public override IEnumerator<T> GetEnumerator() { throw new NotSupportedException(); }
    }
}
