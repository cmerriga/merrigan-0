using System;
using System.Collections.Generic;

namespace Merrigan0 {
    // Not thread-safe
    public class LruCache<K, V> {
        private Func<K, V> createItemFunction;
        private LinkedList<KeyValuePair<K, V>> keysFromLeastRecent = new LinkedList<KeyValuePair<K, V>>();
        private Dictionary<K, LinkedListNode<KeyValuePair<K, V>>> valueListNodesByKey = new Dictionary<K, LinkedListNode<KeyValuePair<K, V>>>();
        private int maxSize;

        public V this[K key] {
            get {
                LinkedListNode<KeyValuePair<K, V>> valueListNode;
                if (!valueListNodesByKey.TryGetValue(key, out valueListNode)) {
                    V value = createItemFunction(key);
                    valueListNode = keysFromLeastRecent.AddLast(new KeyValuePair<K, V>(key, value));
                    valueListNodesByKey.Add(key, valueListNode);
                    if (keysFromLeastRecent.Count > maxSize) {
                        LinkedListNode<KeyValuePair<K, V>> nodeToRemove = keysFromLeastRecent.First;
                        keysFromLeastRecent.RemoveFirst();
                        valueListNodesByKey.Remove(nodeToRemove.Value.Key);
                    }
                }
                return valueListNode.Value.Value;
            }
        }

        public LruCache(Func<K, V> createItemFunction, int maxSize = 100) {
            this.createItemFunction = createItemFunction;
            this.maxSize = maxSize;
        }
    }
}
