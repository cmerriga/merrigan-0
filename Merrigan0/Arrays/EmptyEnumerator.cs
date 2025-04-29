using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    internal class EmptyEnumerator<T> : IEnumerator<T> {
        public static EmptyEnumerator<T> Only = new EmptyEnumerator<T>();

        public T Current {
            get {
                throw new InvalidOperationException();
            }
        }

        Object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public void Dispose() { }
        public bool MoveNext() { return false; }
        public void Reset() { }
    }
}
