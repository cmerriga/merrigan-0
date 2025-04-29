using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    //// Replace uses with DoubleSet or HyperSet and delete this
    [Untested]
    public class DoubleHashSet<T1, T2> {
        private DoubleDictionary<T1, T2, bool> dummyByKey2ByKey1;

        public long Count {
            get {
                return dummyByKey2ByKey1.Count;
            }
        }

        public void Add(T1 key1, T2 key2) {
            if (dummyByKey2ByKey1 == null) {
                dummyByKey2ByKey1 = new DoubleDictionary<T1, T2, bool>();
            }
            dummyByKey2ByKey1[key1, key2] = true;
        }

        public void Clear() {
            dummyByKey2ByKey1.Clear();
        }

        public bool Contains(T1 key1, T2 key2) {
            bool dummy;
            if (dummyByKey2ByKey1 != null && dummyByKey2ByKey1.TryGetValue(key1, key2, out dummy)) {
                return true;
            }
            return false;
        }
    }
}
