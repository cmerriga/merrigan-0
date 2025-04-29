using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    using Merrigan0.Internal.DotNet;

////    [Untested]
////    internal class TransformArrayNode : ArrayNode {
////        private ArrayNode baseItems;
////        private Func<Node, Node> transform;

////        public override long Length { get { return baseItems.Length; } }

////        public TransformArrayNode(ArrayNode baseItems, Func<Node, Node> transform) {
////            this.baseItems = baseItems;
////            this.transform = transform;
////        }

////        protected override Node GetCharacter(long i) { return transform(baseItems[i]); }
////    }
////}
