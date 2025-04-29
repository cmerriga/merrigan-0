using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class WithoutSet<T> : Set<T> {
        private Set<T> baseElements;
        private T element;
        private bool inBase;

        public override long Length {
            get {
                if (inBase) {
                    return baseElements.Length - 1;
                }
                return baseElements.Length;
            }
        }

        public WithoutSet(Set<T> baseElements, T element) {
            this.baseElements = baseElements;
            this.element = element;
            inBase = baseElements.Contains(this.element);
        }

        public override IEnumerator<T> GetEnumerator() {
            foreach (T element in this) {
                if (!Object.Equals(element, this.element)) {
                    yield return element;
                }
            }
        }

        public override bool Contains(T value) { return baseElements.Contains(value) && !Object.Equals(value, this.element); }
    }
}
