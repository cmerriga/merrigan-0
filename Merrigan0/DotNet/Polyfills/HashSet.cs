using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.DotNet.Polyfills.System.Collections.Generic {
    [Untested]
    public class HashSet<T> {
        private Dictionary<T, bool> dummiesByValue;

        public long Count {
            get {
                if (dummiesByValue == null) {
                    return 0;
                }
                return dummiesByValue.Count;
            }
        }

        public void Add(T value) {
            if (dummiesByValue == null) {
                dummiesByValue = new Dictionary<T, bool>();
            }
            dummiesByValue[value] = true;
        }

        public void Clear() {
            dummiesByValue = null;
        }

        public bool Contains(T value) {
            bool dummy;
            if (dummiesByValue != null && dummiesByValue.TryGetValue(value, out dummy)) {
                return true;
            }
            return false;
        }
    }
}
