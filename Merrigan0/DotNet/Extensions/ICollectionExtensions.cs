using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ArraysInternal; // CollectionWrapperArray

namespace Merrigan0.Internal.DotNet.Extensions {
    [Untested]
    public static class ICollectionExtensions {
        public static Array<T> Sorted<T>(this ICollection<T> collection) {
            return new CollectionWrapperArray<T>(collection).Sorted();
        }

        public static Array<T> ToArray<T>(this ICollection<T> collection) {
            return new CollectionWrapperArray<T>(collection);
        }
    }
}
