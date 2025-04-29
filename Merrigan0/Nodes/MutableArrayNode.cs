using System;
using System.Collections.Generic;
using System.Diagnostics;
////using Merrigan0.Internal.DotNet;
using Merrigan0.Internal.Nodes;

namespace Merrigan0.Internal.Nodes {
////    [Untested]
////    public class MutableArrayNode : ArrayNode {
////        public ArrayNode Current { get; protected set; }

////        public override long Length { get { return Current.Length; } }

////        public MutableArrayNode(ArrayNode baseItems) { Current = baseItems; }
////        public MutableArrayNode() : this(ArrayNode.Empty) { }

////        public void Append(Node item) { Current = new AppendArrayNode(Current, item); }
////        public void Append(ArrayNode s) { Current = new ConcatenateArrayNode(Current, s); }
////        public void Insert(long i, Node item) { Current = new InsertItemArrayNode(Current, i, item); }
////        public void Insert(long i, ArrayNode s) { Current = new InsertArrayNode(Current, i, s); }
////        public void Prepend(Node item) { Current = new PrependArrayNode(Current, item); }
////        public void Prepend(ArrayNode s) { Current = new ConcatenateArrayNode(s, Current); }
////        public void Remove(long i, long length) { Current = new RemoveArrayNode(Current, i, length); }
////        public void Sort(Func<Node, Node, int> compare) { Current = new ArrayNodeSortedArrayNode(Current, compare); }
////        public ArrayNode ToArray() { return Current; }

////        protected override Node GetCharacter(long i) { return Current[i]; }
////    }
}
