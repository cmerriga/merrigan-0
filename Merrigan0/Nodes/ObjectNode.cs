using System;
using System.Collections.Generic;
using System.Diagnostics;

////namespace Merrigan0.Internal.Nodes {
////    /// <summary>
////    /// When you have an object but need a Node.
////    /// </summary>
////    [Untested]
////    internal partial class ObjectNode : Node {
////        public static ObjectNode False = new ObjectNode(false);
////        public static ObjectNode True = new ObjectNode(true);
////        public object Value { get; private set; }

////        public ObjectNode(object o) {
////            Value = o;
////        }

////        public override object To(Type type) {
////            if (type.IsAssignableFrom(Value.GetType())) {
////                return Value;
////            }
////            return Value.To(type);
////        }
////    }
////}
