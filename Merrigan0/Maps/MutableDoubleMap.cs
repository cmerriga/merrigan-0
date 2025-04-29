using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
////    [Untested]
////    public class MutableDoubleMap<K1, K2, V> {
////        private MutableMap<K1, MutableMap<K2, V>> valuesByKey2ByKey1;

////        public V this[K1 key1, K2 key2] {
////            get {
////                V value;
////                if (!TryGetValue(key1, key2, out value)) {
////                    throw new KeyNotFoundException();
////                }
////                return value;
////            }

////            set {
////                MutableMap<K2, V> valuesByKey2 = EnsureMapExists(key1, key2);
////                valuesByKey2[key2] = value;
////            }
////        }

////        public long Count {
////            get {
////                long nValuesSoFar = 0L;
////                if (valuesByKey2ByKey1 != null) {
////                    foreach (MutableMap<K2, V> valuesByKey2 in valuesByKey2ByKey1.Current.Values) {
////                        nValuesSoFar += valuesByKey2.Current.Count;
////                    }
////                }
////                return nValuesSoFar;
////            }
////        }

////        public MutableDoubleMap() { }

////        //public DoubleMap(params object[] keyKeyValueTree) {
////        //    ////Utilities.ToTrees<K1, K2, V>
////        //}

////        public void Add(K1 key1, K2 key2, V value) {
////            MutableMap<K2, V> valuesByKey2 = EnsureMapExists(key1, key2);
////            valuesByKey2.Add(key2, value);
////        }

////        public void Clear() {
////            if (valuesByKey2ByKey1 != null) {
////                valuesByKey2ByKey1.Clear();
////            }
////        }

////        public bool TryGetMap(K1 key1, out MutableMap<K2, V> valuesByKey2) {
////            if (valuesByKey2ByKey1 == null) {
////                valuesByKey2 = null;
////                return false;
////            }
////            return valuesByKey2ByKey1.Current.TryGetValue(key1, out valuesByKey2);
////        }

////        public bool TryGetValue(K1 key1, K2 key2, out V value) {
////            Map<K2, V> valuesByKey2;
////            if (!TryGetMap(key1, out valuesByKey2)) {
////                value = default(V);
////                return false;
////            }
////            return valuesByKey2.TryGetValue(key2, out value);
////        }

////        private MutableMap<K2, V> EnsureMapExists(K1 key1, K2 key2) {
////            if (valuesByKey2ByKey1 == null) {
////                valuesByKey2ByKey1 = new MutableMap<K1, Map<K2,V>>();
////            }
////            MutableMap<K2, V> valuesByKey2;
////            if (!valuesByKey2ByKey1.TryGetValue(key1, out valuesByKey2)) {
////                valuesByKey2 = new MutableMap<K2,V>();
////                valuesByKey2ByKey1.Add(key1, valuesByKey2);
////            }
////            return valuesByKey2;
////        }
////    }
}
