using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    internal class TypedEnumerator : IEnumerator<object> {
        private IEnumerator baseEnumerator;

        public object Current {
            get {
                return baseEnumerator.Current;
            }
        }

        Object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public TypedEnumerator(IEnumerator baseEnumerator) {
            this.baseEnumerator = baseEnumerator;
        }

        public void Dispose() { }
        public bool MoveNext() { return baseEnumerator.MoveNext(); }
        public void Reset() { baseEnumerator.Reset(); }
    }
}
