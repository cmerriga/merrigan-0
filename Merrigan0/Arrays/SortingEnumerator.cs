using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    //[Untested]
    //internal class SortingEnumerator<T> : Enumerator<T> {
    //    private bool done;
    //    private Set<T> set1;
    //    private Set<T> set2;
    //    private IEnumerator<T> set1Enumerator;
    //    private IEnumerator<T> set2Enumerator;
    //    private bool started;

    //    public override T UnsafeCurrent { get { return (TTo)(object)baseEnumerator.Current; } }
    //    protected override Boolean Started { get { return started; } }

    //    protected IntersectionEnumerator(Set<T> set1, Set<T> set2) { this.baseEnumerator = s.GetEnumerator(); }

    //    public override Boolean MoveNext() {
    //        if (set1Enumerator.Started)
    //        T next1 = set1Enumerator.Current;
    //        T next2 = set2Enumerator.Current;
    //        if (current1 < current2) {
    //            set1Enumerator

    //        return baseEnumerator.MoveNext(); 
    //    }

    //    public override void Reset() {
    //        started = false;
    //        set1Enumerator = set1.GetEnumerator();
    //        set2Enumerator = set2.GetEnumerator();
    //        done = false;
    //    }
    //}
}
