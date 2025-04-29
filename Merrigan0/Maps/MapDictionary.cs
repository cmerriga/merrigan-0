using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    // Use when you have a Map but you need an IDictionary
    [Untested]
    internal class MapDictionary<K, V> : IDictionary<K, V> {
        private Map<K, V> map;

        public V this[K key] {
            get { return map[key]; }
            set { throw new NotSupportedException(); }
        }

        public int Count { get { return (int)map.Domain.Length; } }
        public bool IsReadOnly { get { return true; } }
        public ICollection<K> Keys { get { return map.Domain.ICollection; } }
        public ICollection<V> Values { get { return map.Transform(pair => pair.Value).ICollection; } }

        public MapDictionary(Map<K, V> map) { this.map = map; }

        public void Add(K key, V value) { throw new NotSupportedException(); }

        /// <summary>
        /// Adds an map to the ICollection<T>.
        /// </summary>
        /// <param name="map">The object to add to the ICollection<T>.</param>
        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        public void Add(KeyValuePair<K, V> pair) { throw new NotSupportedException(); }

        public void Clear() { throw new NotSupportedException(); }

        public bool Contains(V value) {
            foreach (K key in map.Domain) {
                if (object.Equals(map[key], value)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether the ICollection<T> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the ICollection<T>.</param>
        /// <returns>true if item is found in the ICollection<T>; otherwise, false.</returns>
        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
        /// implementation to use for comparing keys.
        /// </remarks>
        public bool Contains(KeyValuePair<K, V> pair) { return map.Contains(pair); }
        ////public bool Contains(KeyValuePair<K, V> pair) { return ContainsKey(pair.Key) && object.Equals(this[pair.Key], pair.Value); } // if the KeyValuePair equality operator doesn't cut it

        /// <summary>
        /// Determines whether the System.Collections.Generic.IDictionary<TKey,TValue>
        /// contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the System.Collections.Generic.IDictionary<TKey,TValue>.</param>
        /// <returns>true if the System.Collections.Generic.IDictionary<TKey,TValue> contains an element with the key; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public bool ContainsKey(K key) { return map.Domain.Contains(key); }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
            int i = arrayIndex;
            foreach (KeyValuePair<K, V> pair in this) {
                array[i] = pair;
                ++i;
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() {
            foreach (K key in map.Domain) {
                yield return new KeyValuePair<K, V>(key, map[key]);
            }
        }

        public bool Remove(K key) { throw new NotSupportedException(); }

        /// <summary>
        /// Removes the first occurrence of a specific object from the ICollection<T>.
        /// </summary>
        /// <param name="map">The object to remove from the ICollection<T>.</param>
        /// <returns>
        /// true if map was successfully removed from the ICollection<T>; otherwise, false. 
        /// This method also returns false if map is not found in the original ICollection<T>.
        /// </returns>
        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        /// <remarks>
        /// Implementations can vary in how they determine equality of objects; for example, List<T> uses Comparer<T>.Default, whereas, Dictionary<TKey,TValue>
        /// allows the user to specify the IComparer<T> implementation to use for comparing keys.<para/>
        /// In collections of contiguous elements, such as lists, the elements that follow the removed element move up to occupy the vacated spot. If the 
        /// collection is indexed, the indexes of the elements that are moved are also updated. This behavior does not apply to 
        /// collections where elements are conceptually grouped into buckets, such as a map table.
        /// </remarks>
        public bool Remove(KeyValuePair<K, V> pair) { throw new NotSupportedException(); }

        public bool TryGetValue(K key, out V value) { return map.TryGetValue(key, out value); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
