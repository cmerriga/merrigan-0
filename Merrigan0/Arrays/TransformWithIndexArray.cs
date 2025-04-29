using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class TransformWithIndexArray<T1, T2> : CoalescingArray<T2> {
        private Array<T1> baseItems;
        private long iBase;
        private Func<long, T1, T2> transformWithIndex;

        public override long Length { get { return baseItems.Length; } }

        public TransformWithIndexArray(Array<T1> baseItems, Func<long, T1, T2> transform) {
            this.baseItems = baseItems;
            this.transformWithIndex = transform;
        }

        protected override bool TryGetNextUncoalescedItem(out T2 item) {
            if (iBase >= baseItems.Length) {
                item = default(T2);
                return false;
            }
            item = transformWithIndex(iBase, baseItems[iBase]);
            ++iBase;
            return true;
        }

        ////protected override T2 GetItem(long i) { return transformWithIndex(i, baseItems[i]); }
    }
}
