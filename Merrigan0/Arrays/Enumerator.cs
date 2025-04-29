using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    //// Needs to be resolved with documentation
    //// Needs naming convention for Quick or something
    [Untested]
    internal abstract class Enumerator<T> : IEnumerator<T> {
        public T Current {
            get {
                //if (!Started) {
                //    throw new InvalidOperationException("The enumerator was not moved to the first item.");
                //}
                return UnsafeCurrent;
            }
        }

        //public abstract bool Done { get; }
        protected abstract T UnsafeCurrent { get; }
        protected Boolean Started { get; private set; }

        Object IEnumerator.Current {
            get {
                return Current;
            }
        }

        protected Enumerator() { UnsafeReset(); }

        public abstract Boolean MoveNext();

        public void Reset() {
            //Started = false;
            UnsafeReset();
        }

        public virtual void Start() { }

        public virtual void Dispose() { }

        protected virtual void UnsafeReset() { }

        //protected void EnsureStarted() {
        //    if (!Started) {
        //        Start();
        //    }
        //}
    }
}
