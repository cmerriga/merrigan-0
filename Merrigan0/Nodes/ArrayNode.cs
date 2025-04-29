using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    /// <summary>
////    /// Equivalent to a JavaScript object. Could be an object, a array, or an array.
////    /// </summary>
////[Untested]
////    public abstract partial class ArrayNode : Node, IArray<Node>, IEnumerable<Node> {
////        public static ArrayNode Empty { get { return EmptyArrayNode.Only; } }

////        //private static bool NodesEqual(Node node1, Node node2) {
////        //    return object.Equals(node1.AsObject(), node2.AsObject());
////        //}

////        public Node this[long i] { get { return GetCharacter(i); } }
////        public abstract long Length { get; }

////        public static ArrayNode FromCsv(String csv) {
////            return null; ////
////        }

////        public virtual bool All(Func<Node, bool> condition) { return Array<Node>.All(this, condition); }
////        public virtual bool Any(Func<Node, bool> condition) { return Array<Node>.Any(this, condition); }
////        public abstract Array<Node> AsArray();
////        public abstract object AsObject();
////        public virtual bool Contains(Node item) { return Array<Node>.Contains(this, item); }
////        public virtual bool Contains(ArrayNode s) { return Array<Node>.Contains(this, (IArray<Node>)s); }

////        // Returns an array with the values just for this property
////        public ArrayNode Column(String property) {
////            MutableArray<Node> nodesSoFar = new MutableArray<Node>();
////            foreach (Node item in this) {
////                nodesSoFar.Append(item);
////            }
////            return new NodeArrayArrayNode(nodesSoFar);
////        }

////        public virtual void CopyTo(Node[] block, long arrayIndex) { Array<Node>.CopyTo(this, block, arrayIndex); }
////        public virtual ArrayNode Cross(ArrayNode arrayNode) { return new CrossArrayNode(this, arrayNode); }

////        public ArrayNode Dice(Array<String> properties) {
////            MutableArray<Node> nodesSoFar = new MutableArray<Node>();
////            foreach (Node item in this) {
////                nodesSoFar.Append(item.Dice(properties));
////            }
////            return new NodeArrayArrayNode(nodesSoFar);
////        }

////        public virtual ArrayNode Distinct() { return Distinct(Comparers.CompareFunction<Node>()); }
////        public virtual ArrayNode Distinct(Func<Node, Node, int> compare) { return new SortedArrayNode(this, compare).Distinct(); }
////        public virtual ArrayNode Dot(ArrayNode arrayNode) { return new DotArrayNode(this, arrayNode); }
////        public virtual IEnumerator<Node> GetEnumerator() { return new ArrayNodeEnumerator(this); }
////        public bool None(Func<Node, bool> predicate) { return !Any(predicate); }

////        public ArrayNode Slice(Func<Node, bool> condition) {
////            MutableArray<Node> nodesSoFar = new MutableArray<Node>();
////            foreach (Node item in this) {
////                if (condition(item)) {
////                    nodesSoFar.Append(item);
////                }
////            }
////            return new NodeArrayArrayNode(nodesSoFar);
////        }

////        ////public ArrayNode Sort(params String[] properties) {
////        ////    // Sort from minor value property to major
////        ////    MutableArray<Node> nodesSoFar = new MutableArray<Node>();
////        ////    int iProperty = properties.Length - 1;
////        ////    while (iProperty >= 0) {
////        ////        nodesSoFar = nodesSoFar.Sort(properties[iProperty]);
////        ////        --iProperty;
////        ////    }
////        ////    return new NodeArrayArrayNode(nodesSoFar);
////        ////}

////        public virtual ArrayNode Sorted() { return Sorted(Comparers.CompareFunction<Node>()); }
////        public virtual ArrayNode Sorted(Func<Node, Node, int> compare) { return new ArraySortedArray<Node>(this, compare); }

////        public virtual ArrayNode Subarray(long i, long length) { return new SubarrayNode(this, i, length); }

////        public virtual Node ToNode(Func<Node, String> getKey) {
////            MutableNode mapSoFar = new MutableNode();
////            foreach (Node item in this) {
////                mapSoFar.Set(getKey(item), item);
////            }
////            return mapSoFar.Current;
////        }

////        public virtual ArrayNode Transform(Func<Node, Node> transform) { return new TransformArrayNode(this, transform); }

////        public virtual bool TryGetIndex(Node item, long iBegin, out long i) {
////            long length = Length;
////            for (long iToTry = iBegin; iToTry < length; ++iToTry) {
////                if (object.Equals(this[iToTry], item)) {
////                    i = iToTry;
////                    return true;
////                }
////            }
////            i = -1;
////            return false;
////        }

////        public virtual bool TryGetIndex(ArrayNode s, long iBegin, out long i) {
////            //// Depending on size, use Boyer-Moore
////            long length = Length;
////            long substringLengtstems.Length;
////            for (long iToTry = iBegin; iToTry < length; ++iToTry) {
////                if (iToTry + substringLength > length) {
////                    substringLength = length - iToTry;
////                }
////                bool found = true;
////                for (int jToTry = 0; jToTry < substringLength; ++jToTry) {
////                    if (!object.Equals(this[iToTry + jToTry], s[jToTry])) {
////                        found = false;
////                        break;
////                    }
////                }
////                if (found) {
////                    i = iToTry;
////                    return true;
////                }
////            }
////            i = -1;
////            return false;
////        }

////        public virtual ArrayNode Where(Func<Node, bool> condition) { return new FilteredArrayNode(this, condition); }

////        public virtual ArrayNode WithSubstitution(ArrayNode unwantedItems, ArrayNode wantedItems) {
////            long iLastInspected = 0;
////            long iFoundItems;
////            if (!TryGetIndex(unwantedItems, iLastInspected, out iFoundItems)) {
////                return Current;
////            }
////            MutableArrayNode withSubstitutionSoFar = new MutableArrayNode();
////            do {
////                withSubstitutionSoFar.Append(Subarray(iLastInspected, iFoundItems - iLastInspected));
////                iLastInspected = iFoundItems + unwantedItems.Length;
////            } while (TryGetIndex(unwantedItems, iLastInspected, out iFoundItems));
////            return withSubstitutionSoFar.Current;
////        }

////        protected abstract Node GetCharacter(long i);
////    }
////}
