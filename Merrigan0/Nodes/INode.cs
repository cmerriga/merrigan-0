//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using Merrigan0.Internal.DotNet.Extensions;

//namespace Merrigan0 {
//    [WhatItIs("One way to divide a node into children, e.g. file system dividing into directories or files, or string dividing into characters")]
//    public abstract class Distinctor {
//    }

//    public class ObjectDistinctor {
//    }

//    public class ArrayDistinctor {
//    }

//    public class CustomDistinctor : Distinctor {
//    }

//    public class DirectoryNode {
//    }

//    [WhatItIs("The facilities that must be offered so that an object can be treated as a node within a " +
//        "tree of objects that can navigate out to other objects via an integer index or a name.")]
//    public abstract class Node {
//        private static Dictionary<Type, Func<object, Node>> createFunctionsByType = new Dictionary<Type, Func<object, Node>>();

//        public static void RegisterType(Type type, Func<object, Node> createFunction) {
//            createFunctionsByType[type] = createFunction;
//        }

//        public static Node From(object o) {
//            Conversion.Convert<Node>(o);
//            if (o == null) {
//                return null;
//            }
//            if (!
//            //Type type = o.GetType();
//            //if (type.IsValueType) {
//            //    return new ValueNode<object>(o);
//            //}
//            Node node = o as Node;
//            if (node != null) {
//                return node;
//            }
//            String s = o as String;
//            if (s != null) {
//                return new StringNode(s);
//            }
//            string systemString = o as string;
//            if (systemString != null) {
//                return new StringNode(s);
//            }

//            IEnumerable enumerable = o as IEnumerable;
//            if (enumerable != null) {
//                return new ArrayArrayNode<object>(Array<object>.From(enumerable));
//            }

//            throw new Exception();
//        }

//        [MayBeNull(When = "if no Node is present with that index")]
//        public virtual Node this[long i] { get { return null; } }

//        [MayBeNull(When = "if no Node is present with that name")]
//        public virtual Node this[String name] { get { return null; } }

//        //public Node this[StringCode code] { get; set; }
//        [WhatItIs("All the indexed, but not named, nodes, in order.")]
//        public virtual Array<Node> Children { get { return Array<Node>.Empty; } }

//        public virtual bool Leaf { get { return false; } }

//        [WhatItIs("All the named values.")]
//        public virtual Array<KeyValuePair<String, Node>> NamedValues { get { return Array<KeyValuePair<String, Node>>.Empty; } }

//        //[WhatItIs("If the Node is a wrapper for a POCO value, that value. Otherwise, null.")]
//        //public virtual object PocoValue { get { return null; } }

//        public virtual object Value { get { return null; } }

//        static Node() {
//            RegisterType(typeof(Node), o => (Node)o);
//            RegisterType(typeof(String), o => new StringNode((String)o));
//            RegisterType(typeof(string), o => new SystemStringNode((string)o));
//            RegisterType(typeof(IEnumerable), o => new ArrayNode((IEnumerable)o));
//        }

//        [WhatItIs("The children, if any, of this node, as would be defined by the given distinctor.")]
//        public Array<Node> Children(Distinctor distinctor) {

//        }
//    }

//    [WhatItIs("A Node that functions as a hash map from String to Node only, with no indexed values.")]
//    public class HashNode : Node {
//        private Dictionary<String, Node> nodesByName = new Dictionary<String, Node>();

//        public override Node this[long i] { get { return null; } }

//        public override Node this[String name] {
//            get {
//                Node node;
//                if (!nodesByName.TryGetValue(name, out node)) {
//                    return null;
//                }
//                return node;
//            }
//        }

//        //public Node this[StringCode code] { get; set; }
//        public abstract Array<Node> Children { get; }
//        public abstract Array<KeyValuePair<String, Node>> NamedValues { get; }

//        [WhatItIs("If the Node is a wrapper for a POCO value, that value. Otherwise, null.")]
//        public abstract object PocoValue { get; }
//    }

//    public class LeafNode<T> : Node {
//        private T value;

//        //[WhatItIs("If the Node is a wrapper for a POCO value, that value. Otherwise, null.")]
//        //public override object PocoValue { get { return Value; } }

//        public override bool Leaf { get { return true; } }

//        public T TypedValue { get { return value; } }

//        public override object Value { get { return value; } }

//        public LeafNode(T value) {
//            this.value = value;
//        }
//    }

//    public class StringNode : Node {
//        [MayBeNull(When = "if no Node is present with that index")]
//        public override Node this[long i] { get { return new ValueNode<char>(Value[i]); } }

//        [MayBeNull(When = "if no Node is present with that name")]
//        public override Node this[String name] {
//            get {
//                if (name == "Length") {
//                    return new ValueNode<long>(s.Length);
//                }
//                return null;
//            }
//        }

//        //public Node this[StringCode code] { get; set; }
//        [WhatItIs("All the indexed, but not named, nodes, in order.")]
//        public override Array<Node> Children { get { return new ValueNodeArray<char>(Value.ToArray()); } }

//        public StringNode(String s) : base(s) { }
//    }

//    public class SystemStringNode : Node {
//        private string s;

//        [MayBeNull(When = "if no Node is present with that index")]
//        public override Node this[long i] { get { return new ValueNode<char>(Value[i]); } }

//        [MayBeNull(When = "if no Node is present with that name")]
//        public override Node this[String name] {
//            get {
//                if (name == "Length") {
//                    return new ValueNode<long>(s.Length);
//                }
//                return null;
//            }
//        }

//        //public Node this[StringCode code] { get; set; }
//        [WhatItIs("All the indexed, but not named, nodes, in order.")]
//        public override Array<Node> Children { get { return new ValueNodeArray<char>(Value.ToArray()); } }

//        public SystemStringNode(string s) {
//            this.s = s;
//        }
//    }

//    public class ValueNodeArray<T> : Array<Node> {
//        private Array<T> a;

//        public ValueNodeArray(Array<T> a) {
//            this.a = a;
//        }

//        public override bool TryGetItem(long i, out Node item) {
//            //// Doesn't check i
//            item = Node.From(a[i]);
//            return true;
//        }
//    }

//    public class ArrayArrayNode<T> : ValueNode<Array<T>> {
//        [MayBeNull(When = "if no Node is present with that index")]
//        public override Node this[long i] { get { return Node.From(Value[i]); } }

//        [MayBeNull(When = "if no Node is present with that name")]
//        public virtual Node this[String name] {
//            get {
//                if (name == "Length") {
//                    return new ValueNode<long>(Value.Length);
//                }
//                return null;
//            }
//        }

//        //public Node this[StringCode code] { get; set; }
//        [WhatItIs("All the indexed, but not named, nodes, in order.")]
//        public virtual Array<Node> Children { get { return Array<Node>.Empty; } }

//        [WhatItIs("All the named values.")]
//        public virtual Array<KeyValuePair<String, Node>> NamedValues { get { return Array<KeyValuePair<String, Node>>.Empty; } }

//        public ArrayArrayNode(Array<T> a) : base(a) { }

//        [WhatItIs("If the Node is a wrapper for a POCO value, that value. Otherwise, null.")]
//        public virtual object PocoValue { get { return null; } }
//    }

//    //[Untested]
//    //public abstract partial class Node : Map<String, Node> {
//    //    public static readonly Node Empty = new EmptyNode();
//    //    public static readonly Node EmptyArray = new EmptyArrayNode();
//    //    public static readonly Node Null = new NullNode();

//    //    public static Node From(object o) {
//    //        if (o == null) {
//    //            return NullNode.Only;
//    //        }

//    //        Node map = o as Node;
//    //        if (map != null) {
//    //            return map;
//    //        }

//    //        // If it's a dictionary of string or String, turn it into a map
//    //        Type type = o.GetType();
//    //        foreach (Type @interface in type.GetInterfaces()) {
//    //            if (@interface.IsGenericType &&
//    //                @interface.GetGenericTypeDefinition() == typeof(IDictionary<,>))
//    //            {
//    //                Type keyType = @interface.GetGenericArguments()[0];
//    //                if (keyType == typeof(string) || typeof(String).IsAssignableFrom(keyType)) {
//    //                    MutableMap<String, Node> nodesByNameSoFar = new MutableMap<String, Node>();
//    //                    //IEnumerator enumerator = source.Call<IEnumerator>("GetEnumerator");
//    //                    foreach (object pair in (IEnumerable)o) {
//    //                        nodesByNameSoFar[pair.GetProperty("Key")] = pair.GetProperty("Value").ToNode();
//    //                    }
//    //                    return new MapNode(nodesByNameSoFar);
//    //                }
//    //            }
//    //        }

//    //        // If it's an sourceEnumerable, turn it into an array map
//    //        IEnumerable enumerable = o as IEnumerable;
//    //        if (enumerable != null && !(o is string))
//    //            return new ArrayNode(enumerable.ToArray<Node>());

//    //        return new ObjectNode(o);
//    //    }

//    //    public static ArrayNode From(IEnumerable values) {
//    //        return new ArrayNode();
//    //    }

//    //    public static Node From(object o) {
//    //        if (o == null) {
//    //            return NullNode.Only;
//    //        }
//    //        Node map = o as Node;
//    //        if (map != null) {
//    //            return map;
//    //        }
//    //        return new ObjectNode(o);
//    //    }

//    //    //public virtual NodeType Type { get; private set; }

//    //    public virtual object AsObject() { return this; }

//    //    public virtual Node Dice(Array<String> properties) {
//    //        MutableMap<String, Node> nodesSoFar = new MutableMap<String, Node>();
//    //        foreach (String property in properties) {
//    //            nodesSoFar.Add(property, this[property]);
//    //        }
//    //        return new MapNode(nodesSoFar);
//    //    }

//    //    public virtual object To(Type type) {
//    //        return null; //////
//    //    }

//    //    public T To<T>() {
//    //        return (T)To(typeof(T));
//    //    }
//    //}
//}
