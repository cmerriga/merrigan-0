using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    internal class CrossArrayNode : ArrayNode {
////        private ArrayNode arrayNode1;
////        private ArrayNode arrayNode2;

////        public override long Length { get { return arrayNode1.Length * arrayNode2.Length; } }

////        public CrossArrayNode(ArrayNode arrayNode1, ArrayNode arrayNode2) { 
////            this.arrayNode1 = arrayNode1; 
////            this.arrayNode2 = arrayNode2;
////        }

////        /// <summary>
////        /// Determines whether the ICollection<T> contains a specific value.
////        /// </summary>
////        /// <param name="map">The object to locate in the ICollection<T>.</param>
////        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
////        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
////        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
////        /// implementation to use for comparing keys.
////        /// </remarks>
////        public override bool Contains(ArrayNode pair) {
////            return arrayNode1.Contains(pair[0]) && arrayNode2.Contains(pair[1]);
////        }

////        /// <summary>
////        /// Returns an enumerator that iterates through the collection.
////        /// </summary>
////        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
////        public override IEnumerator<ArrayNode> GetEnumerator() {
////            foreach (Node node1 in arrayNode1) {
////                foreach (Node node2 in arrayNode2) {
////                    yield return new BlockArrayNode(node1, node2);
////                }
////            }
////        }

////        /// <summary>
////        /// Determines the index of a specific map in the IList<T>.
////        /// </summary>
////        /// <param name="map">The object to locate in the IList<T>.</param>
////        /// <returns>The index of map if found in the loadedItems; otherwise, -1.</returns>
////        /// <remarks>
////        /// If an object occurs multiple times in the loadedItems, the IndexOf method always returns the first instance found.
////        /// </remarks>
////        public override bool TryGetIndex(ArrayNode pair, long iBegin, out long i) {
////            long i1;
////            if (!arrayNode1.TryGetIndex(pair[0], iBegin / arrayNode2.Length, out i1)) {
////                goto notfound;
////            }
////            long i2;
////            if (!arrayNode2.TryGetIndex(pair[1], iBegin % arrayNode2.Length, out i2)) {
////                goto notfound;
////            }
////            i = i1 * arrayNode2.Length + i2;
////            return true;

////        notfound:
////            i = -1;
////            return false;
////        }

////        protected override ArrayNode GetCharacter(int i) { return new BlockArrayNode(arrayNode1[i / arrayNode2.Length], arrayNode2[i % arrayNode2.Length]); }
////    }
////}
