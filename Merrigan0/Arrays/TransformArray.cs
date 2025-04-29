using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class TransformArray<T1, T2> : CoalescingArray<T2> {
        private Array<T1> baseItems;
        private long iBase;
        private Func<T1, T2> transform;

        public override long Length { get { return baseItems.Length; } }

        public TransformArray(Array<T1> baseItems, Func<T1, T2> transform) {
            this.baseItems = baseItems;
            this.transform = transform;
        }

        protected override bool TryGetNextUncoalescedItem(out T2 item) {
            if (iBase >= baseItems.Length) {
                item = default(T2);
                return false;
            }
            item = transform(baseItems[iBase]);
            ++iBase;
            return true;
        }

        ////protected override T2 GetItem(long i) { return transform(baseItems[i]); }
    }
}
