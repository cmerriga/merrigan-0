using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class IntersectionSet<T> : CoalescingSet<T> {
        private Set<T> bigSet;
        private Set<T> smallSet;
        ////private long nTriedFromSmallSet;
        ////private long smallSetLength;
        private IEnumerator<T> smallSetEnumerator;

        public override Set<T> Complement { get { return new UnionSet<T>(smallSet.Complement, bigSet.Complement); } }

        public IntersectionSet(Set<T> set1, Set<T> set2) : base() {
            if (set1.Length <= set2.Length) {
                smallSet = set1;
                bigSet = set2;
            } else {
                smallSet = set2;
                bigSet = set1;
            }
            ////smallSetLength = smallSet.Length;
            smallSetEnumerator = smallSet.GetEnumerator();
        }

        public override bool Contains(T value) { return smallSet.Contains(value) && bigSet.Contains(value); }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            while (smallSetEnumerator.MoveNext()) {
                T itemToTry = smallSetEnumerator.Current;
                if (bigSet.Contains(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
            }
            item = default(T);
            return false;
        }
    }
}
