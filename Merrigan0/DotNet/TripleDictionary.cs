using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    //// Replace uses with DoubleMap or HyperMap and delete this
    [Untested]
    public class TripleDictionary<K1, K2, K3, V> {
        private Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>> valuesByKey3ByKey2ByKey1;

        public V this[K1 key1, K2 key2, K3 key3] {
            get {
                V value;
                if (!TryGetValue(key1, key2, key3, out value)) {
                    throw new KeyNotFoundException();
                }
                return value;
            }

            set {
                Dictionary<K3, V> valuesByKey3 = EnsureDictionaryExists(key1, key2, key3);
                valuesByKey3[key3] = value;
            }
        }

        public void Add(K1 key1, K2 key2, K3 key3, V value) {
            Dictionary<K3, V> valuesByKey3 = EnsureDictionaryExists(key1, key2, key3);
            valuesByKey3.Add(key3, value);
        }

        public bool TryGetDictionary(K1 key1, out Dictionary<K2, Dictionary<K3, V>> valuesByKey3ByKey2) {
            if (valuesByKey3ByKey2ByKey1 == null) {
                valuesByKey3ByKey2 = null;
                return false;
            }
            return valuesByKey3ByKey2ByKey1.TryGetValue(key1, out valuesByKey3ByKey2);
        }

        public bool TryGetDictionary(K1 key1, K2 key2, out Dictionary<K3, V> valuesByKey3) {
            Dictionary<K2, Dictionary<K3, V>> valuesByKey3ByKey2;
            if (!TryGetDictionary(key1, out valuesByKey3ByKey2)) {
                valuesByKey3 = null;
                return false;
            }
            return valuesByKey3ByKey2.TryGetValue(key2, out valuesByKey3);
        }

        public bool TryGetValue(K1 key1, K2 key2, K3 key3, out V value) {
            Dictionary<K3, V> valuesByKey3;
            if (!TryGetDictionary(key1, key2, out valuesByKey3)) {
                value = default(V);
                return false;
            }
            return valuesByKey3.TryGetValue(key3, out value);
        }

        private Dictionary<K3, V> EnsureDictionaryExists(K1 key1, K2 key2, K3 key3) {
            if (valuesByKey3ByKey2ByKey1 == null) {
                valuesByKey3ByKey2ByKey1 = new Dictionary<K1, Dictionary<K2, Dictionary<K3, V>>>();
            }
            Dictionary<K2, Dictionary<K3, V>> valuesByKey3ByKey2;
            if (!valuesByKey3ByKey2ByKey1.TryGetValue(key1, out valuesByKey3ByKey2)) {
                valuesByKey3ByKey2 = new Dictionary<K2, Dictionary<K3, V>>();
                valuesByKey3ByKey2ByKey1.Add(key1, valuesByKey3ByKey2);
            }
            Dictionary<K3, V> valuesByKey3;
            if (!valuesByKey3ByKey2.TryGetValue(key2, out valuesByKey3)) {
                valuesByKey3 = new Dictionary<K3, V>();
                valuesByKey3ByKey2.Add(key2, valuesByKey3);
            }
            return valuesByKey3;
        }
    }
}
