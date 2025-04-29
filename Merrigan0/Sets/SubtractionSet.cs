using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class SubtractionSet<T> : CoalescingSet<T> {
        private Set<T> set1;
        private Set<T> set2;
        private IEnumerator<T> set1Enumerator;

        public override Set<T> Complement { get { return new UnionSet<T>(set1.Complement, set2); } }

        public SubtractionSet(Set<T> set1, Set<T> set2) {
            this.set1 = set1;
            this.set2 = set2;
            set1Enumerator = set1.GetEnumerator();
        }

        public override bool Contains(T value) { return set1.Contains(value) && !set2.Contains(value); }

        // Returns the next element in set 1 that is not in set 2.
        protected override bool TryGetNextUncoalescedItem(out T item) {
            while (true) {
                if (!set1Enumerator.MoveNext()) {
                    break;
                }
                T itemToTry = set1Enumerator.Current;
                if (!set2.Contains(itemToTry)) {
                    item = itemToTry;
                    return true;
                }
            }
            item = default(T);
            return false;
        }
    }
}
