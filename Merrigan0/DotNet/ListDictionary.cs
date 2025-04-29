using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // WithSubsitution with Map
    [Untested]
    public class ListDictionary<K, V> {
        private Dictionary<K, List<V>> valueListsByKey;

        public List<V> this[K key] {
            get {
                List<V> values;
                if (valueListsByKey != null && valueListsByKey.TryGetValue(key, out values)) {
                    return values;
                }
                return new List<V>();
            }
            set {
                if (valueListsByKey == null) {
                    valueListsByKey = new Dictionary<K, List<V>>();
                }
                valueListsByKey[key] = value;
            }
        }

        public void Add(K key, V value) {
            GetList(key).Add(value);
        }

        public void Add(K key, IEnumerable<V> values) {
            GetList(key).AddRange(values);
        }

        public void Clear() {
            valueListsByKey.Clear();
        }

        // Removes at most one, the first, occurrence of the value.
        // Returns true iff something got removed.
        public bool Remove(K key, V value) {
            if (valueListsByKey == null) {
                return false;
            }
            List<V> values;
            if (!valueListsByKey.TryGetValue(key, out values)) {
                return false;
            }

            return values.Remove(value);
        }

        private List<V> GetList(K key) {
            if (valueListsByKey == null) {
                valueListsByKey = new Dictionary<K, List<V>>();
            }

            List<V> existingValues;
            if (!valueListsByKey.TryGetValue(key, out existingValues)) {
                existingValues = new List<V>();
                valueListsByKey.Add(key, existingValues);
            }
            return existingValues;
        }
    }
}
