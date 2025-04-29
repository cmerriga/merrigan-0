using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    /// <summary>
////    /// Use when you have a sortedDistinctBlock of nodes but want an ArrayNode.
////    /// </summary>
////    [Untested]
////    internal class BlockArrayNode : ArrayNode {
////        private Node[] s;

////        public override long Length { get { return s.Length; } }

////        public BlockArrayNode(params Node[] s) { this.s = s; }

////        public override Array<Node> AsArray() { return new BlockArray<Node>(s); }
////        public override object AsObject() { return s; }

////        public override void CopyTo(Node[] block, long arrayIndex) { s.CopyTo(block, arrayIndex); }

////        //public virtual ArrayNode Sorted() { return Sorted(Comparers.CompareFunction<Node>()); }
////        //public virtual ArrayNode Sorted(Func<Node, Node, int> compare) { return new ArraySortedArray<Node>(this, compare); }

////        protected override Node GetCharacter(long i) { resitems[i]; }
////    }
////}
