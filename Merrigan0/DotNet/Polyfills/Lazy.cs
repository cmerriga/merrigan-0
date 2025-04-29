using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.DotNet.Polyfills.System {
    [Untested]
    public class Lazy<T> {
        private bool isValueCreated;
        private T value;
        private Func<T> valueFactory;
        private object lockObject = new object();

        public bool IsValueCreated { get { return isValueCreated; } }

        public T Value {
            get {
                if (!IsValueCreated) {
                    lock (lockObject) {
                        if (!IsValueCreated) {
                            value = valueFactory();
                            isValueCreated = true;
                        }
                    }
                }
                return value;
            }
        }

        public Lazy(Func<T> valueFactory) {
            this.valueFactory = valueFactory;
        }
    }
}
