using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [WhatItIs("All possible pairs of two arrays, in major-minor order.")]
    [Untested]
    internal class CrossArray<T1, T2> : Array<Tuple<T1, T2>> {
        private Array<T1> a1;
        private Array<T2> a2;

        public override long Length { get { return a1.Length * a2.Length; } }

        public CrossArray(Array<T1> a1, Array<T2> a2) { 
            this.a1 = a1; 
            this.a2 = a2;
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
        public override bool Contains(Tuple<T1, T2> item) {
            return a1.Contains(item.Item1) && a2.Contains(item.Item2);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public override IEnumerator<Tuple<T1, T2>> GetEnumerator() {
            foreach (T1 t1 in a1) {
                foreach (T2 t2 in a2) {
                    yield return new Tuple<T1, T2>(t1, t2);
                }
            }
        }

        /// <summary>
        /// Determines the index of a specific map in the IList<T>.
        /// </summary>
        /// <param name="map">The object to locate in the IList<T>.</param>
        /// <returns>The index of map if found in the array; otherwise, -1.</returns>
        /// <remarks>
        /// If an object occurs multiple times in the array, the IndexOf method always returns the first instance found.
        /// </remarks>
        public override bool TryGetIndex(Tuple<T1, T2> item, long iBegin, out long i) {
            long i1;
            long a2Length = a2.Length;
            if (!a1.TryGetIndex(item.Item1, iBegin / a2Length, out i1)) {
                goto notfound;
            }
            long i2;
            if (!a2.TryGetIndex(item.Item2, iBegin % a2Length, out i2)) {
                goto notfound;
            }
            i = i1 * a2.Length + i2;
            return true;

        notfound:
            i = -1;
            return false;
        }

        public override bool TryGetItem(long i, out Tuple<T1, T2> item) {
            T1 item1;
            long a2Length = a2.Length;
            if (!a1.TryGetItem(i / a2Length, out item1)) {
                item = default(Tuple<T1, T2>);
                return false;
            }
            item = new Tuple<T1, T2>(item1, a2[i % a2Length]);
            return true;
        }

        //protected override Tuple<T1, T2> GetItem(long i) { return new Tuple<T1, T2>(array1[i / array2.Length], array2[i % array2.Length]); }
    }
}
