using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.DotNet.Extensions {
    [Untested]
    public static class BlockExtensions {
        ////public static T[] Copy<T>(this T[] block) {
        ////    T[] copy = new T[block.Length];
        ////    block.CopyTo(copy, 0);
        ////    return copy;
        ////}

        public static Array<T> ToArray<T>(this T[] block) {
            return Array<T>.From(block);
        }
    }
}
