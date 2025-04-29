using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Taken over by no-<T> EnumerableWrapperArray
    ////// Use when you have an IEnumerable but need an Array.
    ////[Untested]
    ////internal class UntypedEnumerableWrapperArray : CoalescingArray<object> {
    ////    private IEnumerator enumerator;

    ////    public UntypedEnumerableWrapperArray(IEnumerable enumerable) : this(enumerable, null, false) { }

    ////    public UntypedEnumerableWrapperArray(IEnumerable enumerable, Func<object, object, int> compare, bool distinct) :
    ////        base(compare, distinct)
    ////    {
    ////        enumerator = enumerable.GetEnumerator();
    ////    }

    ////    protected override bool TryGetNextUncoalescedItem(out object item) {
    ////        if (!enumerator.MoveNext()) {
    ////            item = null;
    ////            return false;
    ////        }
    ////        item = enumerator.Current;
    ////        return true;
    ////    }
    ////}
}
