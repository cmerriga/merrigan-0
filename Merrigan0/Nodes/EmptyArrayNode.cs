using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    // An array with no s.
////    [Untested]
////    internal class EmptyArrayNode : ArrayNode {
////        public override Node this[long i] { get { throw new IndexOutOfRangeException(); } }
////        public override int Length { get { return 0; } }

////        // Returns an loadedItems with the values just for this property
////        public override ArrayNode Column(String property) {
////            return Only;
////        }

////        public override ArrayNode Dice(Array<String> properties) {
////            return Only;
////        }

////        public override ArrayNode Slice(Func<Node, bool> condition) {
////            return Only;
////        }

////        public override ArrayNode Sort(params Func<Node, bool>[] conditions) {
////            return Only;
////        }
////    }
////}
