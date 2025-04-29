using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class UnionSet<T> : CoalescingSet<T> {
        private Set<T> set1;
        private Set<T> set2;
        private bool onSet2;
        private IEnumerator<T> currentSetEnumerator;

        public override Set<T> Complement { get { return new IntersectionSet<T>(set1.Complement, set2.Complement); } }

        public UnionSet(Set<T> set1, Set<T> set2) {
            this.set1 = set1;
            this.set2 = set2;
            currentSetEnumerator = set1.GetEnumerator();
        }

        public override bool Contains(T value) { return set1.Contains(value) || set2.Contains(value); }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            while (true) {
                bool succeeded = currentSetEnumerator.MoveNext();
                if (!succeeded) {
                    if (onSet2) {
                        break;
                    }
                    onSet2 = true;
                    currentSetEnumerator = set2.GetEnumerator();
                    continue;
                }
                T itemToTry = currentSetEnumerator.Current;
                if (!coalescedItems.Current.Domain.Contains(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
            }
            item = default(T);
            return false;
        }
    }
}
