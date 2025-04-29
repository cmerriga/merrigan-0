using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////[Untested]
    ////internal class SortedIntersectionSet<T> : CoalescingSet<T> {
    ////    private Set<T> set1;
    ////    private Set<T> set2;

    ////    public override Set<T> Complement { get { return new UnionSet<T>(set1.Complement, set2.Complement); } }

    ////    public SortedIntersectionSet(Set<T> set1, Set<T> set2) : base(set1.Compare) {
    ////        this.set1 = set1;
    ////        this.set2 = set2;
    ////    }

    ////    public override bool In(T value) { return set1.In(value) && set2.In(value); }

    ////    protected override void CoalesceUntil(long i) {
    ////        T item1 = default(T);
    ////        bool set1Done = iSet1CoalescedUntil >= set1.Length;
    ////        if (!set1Done) {
    ////            item1 = set1[iSet1CoalescedUntil];
    ////        }
    ////        bool set2Done = iSet2CoalescedUntil >= set2.Length;
    ////        while (coalescedItems.Current.Length <= i) {
    ////            if (set1Done || set2Done) {
    ////                fullyCoalesced = true;
    ////                break;
    ////            }

    ////            int compareResult = Compare(item1, set2[iSet2CoalescedUntil]);
    ////            if (CompareResult.LeftBigger(compareResult)) {
    ////                ++iSet1CoalescedUntil;
    ////                if (iSet1CoalescedUntil >= set1.Length) {
    ////                    set1Done = true;
    ////                }
    ////            } else if (CompareResult.Equal(compareResult)) {
    ////                coalescedItems.Append(item1);
    ////                ++iSet1CoalescedUntil;
    ////                if (iSet1CoalescedUntil >= set1.Length) {
    ////                    set1Done = true;
    ////                }
    ////                ++iSet2CoalescedUntil;
    ////                if (iSet2CoalescedUntil >= set2.Length) {
    ////                    set2Done = true;
    ////                }
    ////            } else {
    ////                ++iSet2CoalescedUntil;
    ////                if (iSet2CoalescedUntil >= set2.Length) {
    ////                    set2Done = true;
    ////                }
    ////            }
    ////        }
    ////    }
    ////}
}
