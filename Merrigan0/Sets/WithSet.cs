using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class WithSet<T> : Set<T> {
        private Set<T> baseElements;
        private T element;
        private bool inBase;

        public override long Length {
            get {
                if (inBase) {
                    return baseElements.Length;
                }
                return baseElements.Length + 1; 
            } 
        }

        public WithSet(Set<T> baseElements, T element) {
            this.baseElements = baseElements;
            this.element = element;
            inBase = baseElements.Contains(element);
        }

        public override bool Contains(T value) { return baseElements.Contains(value) || Object.Equals(value, this.element); }

        public override IEnumerator<T> GetEnumerator() {
            foreach (T element in baseElements) {
                yield return element;
            }
            yield return this.element;
        }
    }
}
