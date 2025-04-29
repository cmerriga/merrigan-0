using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0 {
    [Untested]
    public class CustomSet<T> : Set<T> {
        private Func<T, bool> containsFunction;
        private Func<Set<T>, IEnumerator<T>> createEnumerator;
        private long length;

        public override long Length { get { return length; } }

        public CustomSet(long length, Func<T, bool> containsFunction, Func<Set<T>, IEnumerator<T>> createEnumerator) {
            this.containsFunction = containsFunction;
            this.createEnumerator = createEnumerator;
            this.length = length;
        }

        /// <summary>
        /// Determines whether the ICollection<T> contains a specific value.
        /// </summary>
        /// <param name="map">The object to locate in the ICollection<T>.</param>
        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
        /// implementation to use for comparing keys.
        /// </remarks>
        public override bool Contains(T item) {
            return containsFunction(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public override IEnumerator<T> GetEnumerator() {
            return createEnumerator(this);
        }
    }
}
