using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0 {
    ///// <summary>
    ///// Equivalent to a JavaScript object. Could be an object, a loadedItems, or an loadedItems.
    ///// </summary>
    //[Untested]
    //public abstract partial class Node : Map<String, Node> {
    //    public static readonly Node Empty = new EmptyNode();
    //    public static readonly Node EmptyArray = new EmptyArrayNode();
    //    public static readonly Node Null = new NullNode();

    //    public static Node From(object o) {
    //        if (o == null) {
    //            return NullNode.Only;
    //        }

    //        Node map = o as Node;
    //        if (map != null) {
    //            return map;
    //        }

    //        // If it's a dictionary of string or String, turn it into a map
    //        Type type = o.GetType();
    //        foreach (Type @interface in type.GetInterfaces()) {
    //            if (@interface.IsGenericType &&
    //                @interface.GetGenericTypeDefinition() == typeof(IDictionary<,>))
    //            {
    //                Type keyType = @interface.GetGenericArguments()[0];
    //                if (keyType == typeof(string) || typeof(String).IsAssignableFrom(keyType)) {
    //                    MutableMap<String, Node> nodesByNameSoFar = new MutableMap<String, Node>();
    //                    //IEnumerator enumerator = source.Call<IEnumerator>("GetEnumerator");
    //                    foreach (object pair in (IEnumerable)o) {
    //                        nodesByNameSoFar[pair.GetProperty("Key")] = pair.GetProperty("Value").ToNode();
    //                    }
    //                    return new MapNode(nodesByNameSoFar);
    //                }
    //            }
    //        }

    //        // If it's an sourceEnumerable, turn it into an array map
    //        IEnumerable enumerable = o as IEnumerable;
    //        if (enumerable != null && !(o is string))
    //            return new ArrayNode(enumerable.ToArray<Node>());

    //        return new ObjectNode(o);
    //    }

    //    public static ArrayNode From(IEnumerable values) {
    //        return new ArrayNode();
    //    }

    //    public static Node From(object o) {
    //        if (o == null) {
    //            return NullNode.Only;
    //        }
    //        Node map = o as Node;
    //        if (map != null) {
    //            return map;
    //        }
    //        return new ObjectNode(o);
    //    }

    //    //public virtual NodeType Type { get; private set; }

    //    public virtual object AsObject() { return this; }

    //    public virtual Node Dice(Array<String> properties) {
    //        MutableMap<String, Node> nodesSoFar = new MutableMap<String, Node>();
    //        foreach (String property in properties) {
    //            nodesSoFar.Add(property, this[property]);
    //        }
    //        return new MapNode(nodesSoFar);
    //    }

    //    public virtual object To(Type type) {
    //        return null; //////
    //    }

    //    public T To<T>() {
    //        return (T)To(typeof(T));
    //    }
    //}
}
