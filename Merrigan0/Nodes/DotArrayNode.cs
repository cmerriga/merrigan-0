using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    // A just-in-time generator of tuples.
////    [Untested]
////    internal class DotArrayNode : ArrayNode {
////        private ArrayNode arrayNode1;
////        private ArrayNode arrayNode2;

////        public override long Length { get { return arrayNode1.Length; } }

////        // The arrays must be the same length
////        public DotArrayNode(ArrayNode arrayNode1, ArrayNode arrayNode2) { 
////            this.arrayNode1 = arrayNode1; 
////            this.arrayNode2 = arrayNode2;
////        }

////        protected override Tuple<Node, Node> GetCharacter(int i) {
////            return new Tuple<Node, Node>(arrayNode1[i / arrayNode2.Length], arrayNode2[i % arrayNode2.Length]);
////        }
////    }
////}
