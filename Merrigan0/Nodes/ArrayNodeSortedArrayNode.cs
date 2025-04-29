using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    // When you have an ArrayNode but you needed a sorted one
////    [Untested]
////    internal class ArrayNodeSortedArrayNode : SortedArrayNode {
////        private Heap<Node> heap;
////        private ArrayNode nodes;

////        public override long Length { get { return nodes.Length; } }

////        public ArrayNodeSortedArrayNode(ArrayNode nodes, Func<Node, Node, int> compare) :
////            base(compare)
////        {
////            heap = new Heap<Node>(nodes, compare);
////        }

////        //protected void SortUntil(long i) {
////        //    //////
////        //}

////        //protected override Node GetCharacter(long i) {
////        //    SortUntil(i);
////        //    return s[i];
////        //}
////    }
////}
