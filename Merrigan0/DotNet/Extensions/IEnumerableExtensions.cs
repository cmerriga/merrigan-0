using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ArraysInternal;

namespace Merrigan0.Internal.DotNet.Extensions {
    [Untested]
    public static class IEnumerableExtensions {
        //public static T GetItem<T>(this IEnumerable<T> items, long i) {
        //    long c = 0;
        //    foreach (T item in items) {
        //        if (c == i) {
        //            return item;
        //        }
        //    }
        //    throw new IndexOutOfRangeException();
        //}

        //public static long GetLength(this IEnumerable items) {
        //    IList list = items as IList;
        //    if (list != null) {
        //        return list.Count;
        //    }

        //    int cObjectsSoFar = 0;
        //    foreach (object o in items) {
        //        ++cObjectsSoFar;
        //    }
        //    return cObjectsSoFar;
        //}

        //public static long GetLength<T>(this IEnumerable<T> items) {
        //    return GetLength((IEnumerable)items);
        //}

        //public static Array<T> ToArray<T>(this IEnumerable<T> items) {
        //    return new EnumerableWrapperArray<T>(items);
        //}

        //public static Array<TTo> ToArray<TFrom, TTo>(this IEnumerable<TFrom> items) {
        //    return new CastArray<TFrom, TTo>(items.ToArray());
        //}

        ////public static ArrayNode ToArrayNode(this IEnumerable<Node> nodes) {
        ////    return new EnumerableArrayNode(nodes);
        ////}

        //public static Array<T> ToSortedArray<T>(this IEnumerable<T> enumerable) {
        //    return new ArraySortedArray<T>(new EnumerableWrapperArray<T>(enumerable));
        //}

        //public static Array<T> ToSortedArray<T>(this IEnumerable<T> items, Func<T, T, int> compare) {
        //    return new EnumerableWrapperArray<T>(items).Sorted(compare);
        //}

        ////public static SortedArrayNode ToSortedArrayNode(this IEnumerable<Node> nodes, Func<Node, Node, int> compare) {
        ////    return new EnumerableArrayNode(nodes).Sorted(compare);
        ////}

        //public static Set<T> ToSet<T>(this IEnumerable<T> items) {
        //    return Set<T>.From(Array<T>.From(items));
        //}

        //public static String ToString<T>(IEnumerable<T> items) {
        //    MutableString stringSoFar = new MutableString();
        //    Boolean first = true;
        //    foreach (T item in items) {
        //        if (first) {
        //            first = false;
        //        } else {
        //            stringSoFar.Append(", ");
        //        }

        //        stringSoFar.Append(item);
        //    }
        //    return stringSoFar.Current;
        //}
    }
}
