using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ArraysInternal; // ListWrapperArray

namespace Merrigan0.Internal.DotNet.Extensions {
    [Untested]
    public static class IListExtensions {
        //public static bool Contains<T>(this T map) {
        //    long dummy;
        //    return TryGetIndex(map, 0, out dummy);
        //}

        //public virtual bool Contains(Array<T> s) {
        //    long dummy;
        //    return TryGetIndex(s, 0, out dummy);
        //}

        public static Array<T> ToArray<T>(this IList<T> list) {
            return new ListWrapperArray<T>(list);
        }
    }
}

//namespace System.Linq {
//    public static class IListExtensions {

//    }
//}
