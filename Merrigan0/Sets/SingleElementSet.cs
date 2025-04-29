using System;
using System.Collections.Generic;
using Merrigan0.ArraysInternal;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class SingleElementSet<T> : Set<T> {
        private T element;

        public override long Length { get { return 1; } }

        public SingleElementSet(T element) { this.element = element; }

        public override Set<T> And(Set<T> set2) {
            if (set2.Contains(element)) {
                return this;
            }
            return EmptySet<T>.Only;
        }

        public override bool Contains(T value) { return Object.Equals(value, this.element); }

        public override IEnumerator<T> GetEnumerator() { return new SingleItemEnumerator<T>(element); }

        public override Set<T> Or(Set<T> set2) {
            if (set2.Contains(element)) {
                return set2;
            }
            return set2.With(element);
        }

        public override Set<T> Without(T value) {
            if (!Object.Equals(value, this.element)) {
                return this;
            }
            return base.Without(value);
        }
    }
}
