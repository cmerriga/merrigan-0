using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    /// <summary>
////    /// Equivalent to a JavaScript object. Could be an object, a loadedItems, or an loadedItems.
////    /// </summary>
////    [Untested]
////    internal class NodeArrayArrayNode : ArrayNode {
////        private Array<Node> s;

////        public override long Length { get { return s.Length; } }

////        public NodeArrayArrayNode(Array<Node> s) { this.s = s; }

////        public override Array<Node> AsArray() { return s; }
////        public override object AsObject() { return s; }

////        //public override ArrayNode Distinct(Func<Node, Node, int> compare) { return new SortedArraySet<T>(Sorted(compare)); }

////        public override IEnumerator<Node> GetEnumerator() { return s.GetEnumerator(); }

////        //public virtual ArrayNode Sorted() { return Sorted(Comparers.CompareFunction<Node>()); }
////        //public virtual ArrayNode Sorted(Func<Node, Node, int> compare) { return new ArraySortedArray<Node>(this, compare); }

////        protected override Node GetCharacter(long i) { resitems[i]; }
////    }
////}
