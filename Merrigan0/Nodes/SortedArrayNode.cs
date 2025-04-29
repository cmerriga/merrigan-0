using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    using Merrigan0.Internal.DotNet;

////    [Untested]
////    internal abstract class SortedArrayNode : ArrayNode, ISortedArray<Node> {
////        public Func<Node, Node, int> Compare { get; private set; }

////        protected SortedArrayNode(Func<Node, Node, int> compare) { Compare = compare; }

////        ////public static SortedArrayNode FromSorted(IEnumerable<Node> sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
////        ////public static SortedArrayNode FromSorted(IEnumerable<Node> sortedItems, Func<Node, Node, int> compare) { return new ArraySortedArrayNode(new EnumerableArrayNode(sortedItems), compare); }
////        ////public static SortedArrayNode FromSorted(Node[] sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
////        ////public static SortedArrayNode FromSorted(Node[] sortedItems, Func<T, T, int> compare) { return new SortedBlockSortedArrayNode(sortedItems, compare); }
////        ////public static SortedArrayNode FromSorted(IList<T> sortedItems) { return FromSorted(sortedItems, Comparers.CompareFunction<T>()); }
////        ////public static SortedArrayNode FromSorted(IList<T> sortedItems, Func<T, T, int> compare) { return new SortedListWrapperArray<T>(sortedItems, compare); }

////        public override Set<Node> Distinct() { return new SortedArraySet<Node>(this); }
////        public override Set<Node> Distinct(Func<Node, Node, int> compare) { return new SortedDistinctArraySet<Node>(this); }
////        public override SortedArrayNode Sorted() { return this; }

////        public override SortedArrayNode Sorted(Func<Node, Node, int> compare) {
////            if (object.ReferenceEquals(compare, Compare)) { return this; }
////            return new ArraySortedArrayNode(this, compare);
////        }

////        public override bool TryGetIndex(Node item, long iBegin, out long i) { return TryBinarySearch(iBegin, Length - 1, item, out i); }
////        public override ArrayNode Where(Func<Node, bool> condition) { return new FilteredSortedArrayNode(this, condition); }

////        protected bool TryBinarySearch(long begin, long end, Node map, out long i) {
////            int compareResult;
////            if (begin < end - 1) {
////                long midway = (begin + end) / 2;
////                compareResult = Compare(GetCharacter(midway), map);
////                if (CompareResult.LeftBigger(compareResult)) {
////                    return TryBinarySearch(midway + 1, end, map, out i);
////                }

////                return TryBinarySearch(begin, midway, map, out i);
////            }

////            // End is just after begin, or the same
////            compareResult = Compare(GetCharacter(begin), map);
////            if (CompareResult.Equal(compareResult)) {
////                i = begin;
////                return true;
////            }
////            if (CompareResult.RightBigger(compareResult) || end == begin) {
////                goto notfound;
////            }
////            compareResult = Compare(GetCharacter(end), map);
////            if (CompareResult.Equal(compareResult)) {
////                goto notfound;
////            }

////        notfound:
////            i = 0;
////            return false;
////        }
////    }
////}
