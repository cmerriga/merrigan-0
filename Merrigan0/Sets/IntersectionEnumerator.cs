using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    //// Maybe not needed. Maybe an IntersectionSet might as well be created?
    ////[Untested]
    ////internal class IntersectionEnumerator<T> : Enumerator<T> {
    ////    protected T current;
    ////    protected bool done;
    ////    private Set<T> set1;
    ////    private Set<T> set2;
    ////    protected IEnumerator<T> lowEnumerator;
    ////    protected IEnumerator<T> highEnumerator;

    ////    protected override T UnsafeCurrent { get { return current; } }

    ////    protected IntersectionEnumerator(Set<T> set1, Set<T> set2) {
    ////        this.set1 = set1;
    ////        this.set2 = set2;
    ////    }

    ////    public override Boolean MoveNext() {
    ////        if (done) {
    ////            return false;
    ////        }

    ////        //if (!started) {
    ////        //    started = true;
    ////        //}

    ////        // Move up low enumerator until a match or greater is found
    ////        int comparison;
    ////        while (true) {
    ////            comparison = set1.Compare(lowEnumerator.Current, highEnumerator.Current);
    ////            if (comparison >= 0) {
    ////                break;
    ////            }
    ////            if (!lowEnumerator.MoveNext()) {
    ////                done = true;
    ////                return false;
    ////            }
    ////        }

    ////        // If we found an equal one, move both enumerators up if possible
    ////        if (comparison == 0) {
    ////            if (!lowEnumerator.MoveNext()) {
    ////                done = true;
    ////                return false;
    ////            }
    ////            if (!highEnumerator.MoveNext()) {
    ////                done = true;
    ////                return false;
    ////            }
    ////        } else { // comparison < 0
    ////            // High one was lower so switch them
    ////            IEnumerator<T> temp;
    ////            temp = lowEnumerator;
    ////            lowEnumerator = highEnumerator;
    ////            highEnumerator = temp;
    ////        }
    ////        return true;
    ////    }

    ////    protected override void UnsafeReset() {
    ////        done = false;
    ////        lowEnumerator = set1.GetEnumerator();
    ////        highEnumerator = set2.GetEnumerator();
    ////        if (!lowEnumerator.MoveNext()) {
    ////            done = true;
    ////            return;
    ////        }
    ////        if (!highEnumerator.MoveNext()) {
    ////            done = true;
    ////            return;
    ////        }
    ////    }
    ////}
}
