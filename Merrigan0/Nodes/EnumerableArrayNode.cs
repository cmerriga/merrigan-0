using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    // Use when you have an IEnumerable of nodes but need an ArrayNode
////    [Untested]
////    internal class EnumerableArrayNode : ArrayNode {
////        private MutableArrayNode loadedItems;
////        private IEnumerator<Node> enumerator;

////        public override long Length {
////            get {
////                if (enumerator != null) {
////                    LoadAll();
////                }
////                return loadedItems.Length;
////            }
////        }

////        public EnumerableArrayNode(IEnumerable<Node> nodes)
////        {
////            enumerator = nodes.GetEnumerator();
////            loadedItems = new MutableArrayNode();
////        }

////        public override IEnumerator<Node> GetEnumerator() {
////            LoadAll();
////            return loadedItems.GetEnumerator();
////        }

////        public override Node[] ToBlock() {
////            LoadAll();
////            return loadedItems.ToBlock();
////        }

////        public override IList<Node> ToIList() {
////            LoadAll();
////            return new ArrayList<T>(loadedItems.Current);
////        }

////        protected override Node GetCharacter(long i) {
////            LoadUntil(i);
////            return loadedItems[i];
////        }

////        protected void LoadAll() {
////            if (enumerator != null) {
////                while (true) {
////                    if (!LoadBlock())
////                        break;
////                }
////            }
////        }

////        protected void LoadUntil(long i) {
////            if (i < loadedItems.Length) {
////                return;
////            }

////            if (enumerator == null) {
////                throw new IndexOutOfRangeException();
////            }

////            while (LoadBlock()) {
////                if (i < loadedItems.Length) {
////                    return;
////                }
////            }

////            throw new IndexOutOfRangeException();
////        }

////        [return:WhatItIs("Whether there are more s remaining")]
////        protected bool LoadBlock() {
////            // Assume enumerator != null
////            lock (enumerator) {
////                int i = 0;
////                while (i < 256) {
////                    if (!enumerator.MoveNext()) {
////                        enumerator = null;
////                        return false;
////                    }
////                    loadedItems.Append(enumerator.Current);
////                }
////                return true;
////            }
////        }
////    }
////}
