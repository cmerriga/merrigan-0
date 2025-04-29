using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.DotNet.Extensions {
    [Untested]
    public static class IComparableExtensions {
        // Returns the compare function for the given Comparer
        public static Func<T, T, int> CompareFunction<T>(this IComparer<T> comparer) {
            return (t1, t2) => comparer.Compare(t1, t2);
        }
    }
}
