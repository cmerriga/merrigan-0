using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    using Merrigan0.Internal.DotNet;

////    // A lazily-populated array based on a filter over another array.
////    [Untested]
////    internal class FilteredArrayNode : ArrayNode {
////        private ArrayNode baseItems;
////        private Func<Node, bool> condition;
////        private MutableArrayNode filteredItems = new MutableArrayNode();
////        private int iFilteredUntil;

////        public override long Length {
////            get {
////                if (iFilteredUntil < baseItems.Length) {
////                    FilterUntil(long.MaxValue);
////                }
////                return filteredItems.Length;
////            }
////        }

////        public FilteredArrayNode(ArrayNode baseItems, Func<Node, bool> condition) {
////            this.baseItems = baseItems;
////            this.condition = condition;
////        }

////        protected override Node GetCharacter(long i) {
////            if (i >= baseItems.Length) {
////                FilterUntil(i);
////            }
////            return baseItems[i];
////        }

////        private void FilterUntil(long i) {
////            long length = Length;
////            while (iFilteredUntil < length) {
////                Node item = baseItems[iFilteredUntil];
////                if (condition(item)) {
////                    filteredItems.Append(item);
////                    if (i < filteredItems.Length) {
////                        return;
////                    }
////                }
////                ++iFilteredUntil;
////            }
////        }
////    }
////}
