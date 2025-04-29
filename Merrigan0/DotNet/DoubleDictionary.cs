using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ArraysInternal; // EmptyEnumerator

namespace Merrigan0 {
    //// Replace uses with DoubleMap or HyperMap and delete this
    [Untested]
    public class DoubleDictionary<K1, K2, V> : IEnumerable<KeyValuePair<K1, Dictionary<K2, V>>> {
        private Dictionary<K1, Dictionary<K2, V>> valuesByKey2ByKey1;

        public Dictionary<K2, V> this[K1 key1] {
            get {
                Dictionary<K2, V> dictionary;
                if (!TryGetDictionary(key1, out dictionary)) {
                    throw new KeyNotFoundException();
                }
                return dictionary;
            }

            set {
                if (valuesByKey2ByKey1 == null) {
                    valuesByKey2ByKey1 = new Dictionary<K1, Dictionary<K2, V>>();
                }
                valuesByKey2ByKey1[key1] = value;
            }
        }

        public V this[K1 key1, K2 key2] {
            get {
                V value;
                if (!TryGetValue(key1, key2, out value)) {
                    throw new KeyNotFoundException();
                }
                return value;
            }

            set {
                Dictionary<K2, V> valuesByKey2 = EnsureDictionaryExists(key1);
                valuesByKey2[key2] = value;
            }
        }

        public long Count {
            get {
                long nValuesSoFar = 0L;
                foreach (Dictionary<K2, V> valuesByKey2 in valuesByKey2ByKey1.Values) {
                    nValuesSoFar += valuesByKey2.Count;
                }
                return nValuesSoFar;
            }
        }

        public DoubleDictionary() { }

        //public DoubleDictionary(params object[] keyKeyValueTree) {
        //    ////Utilities.ToTrees<K1, K2, V>
        //}

        public void Add(K1 key1, K2 key2, V value) {
            Dictionary<K2, V> valuesByKey2 = EnsureDictionaryExists(key1);
            valuesByKey2.Add(key2, value);
        }

        public void Add(K1 key1, Dictionary<K2, V> valuesByKey2) {
            EnsureDictionaryExists();
            valuesByKey2ByKey1.Add(key1, valuesByKey2);
        }

        public void Clear() {
            if (valuesByKey2ByKey1 != null) {
                valuesByKey2ByKey1.Clear();
            }
        }

        public IEnumerator<KeyValuePair<K1, Dictionary<K2, V>>> GetEnumerator() {
            if (valuesByKey2ByKey1 != null) {
                return valuesByKey2ByKey1.GetEnumerator();
            }
            return EmptyEnumerator<KeyValuePair<K1, Dictionary<K2, V>>>.Only;
        }

        public bool TryGetDictionary(K1 key1, out Dictionary<K2, V> valuesByKey2) {
            if (valuesByKey2ByKey1 == null) {
                valuesByKey2 = null;
                return false;
            }
            return valuesByKey2ByKey1.TryGetValue(key1, out valuesByKey2);
        }

        public bool TryGetValue(K1 key1, K2 key2, out V value) {
            Dictionary<K2, V> valuesByKey2;
            if (!TryGetDictionary(key1, out valuesByKey2)) {
                value = default(V);
                return false;
            }
            return valuesByKey2.TryGetValue(key2, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        private void EnsureDictionaryExists() {
            if (valuesByKey2ByKey1 == null) {
                valuesByKey2ByKey1 = new Dictionary<K1, Dictionary<K2, V>>();
            }
        }

        private Dictionary<K2, V> EnsureDictionaryExists(K1 key1) {
            EnsureDictionaryExists();
            Dictionary<K2, V> valuesByKey2;
            if (!valuesByKey2ByKey1.TryGetValue(key1, out valuesByKey2)) {
                valuesByKey2 = new Dictionary<K2,V>();
                valuesByKey2ByKey1.Add(key1, valuesByKey2);
            }
            return valuesByKey2;
        }
    }
}
