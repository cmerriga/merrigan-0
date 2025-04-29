using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have a set but need an array.
    [Untested]
    internal class SetArray<T> : EnumerableWrapperArray<T> {
        public SetArray(Set<T> set) : base(set, null, true) { }
    }
}
