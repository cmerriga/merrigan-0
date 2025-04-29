using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // A just-in-time generator of tuples.
    [Untested]
    internal class DotArray<T1, T2, T3> : CoalescingArray<T3> {
        private Array<T1> array1;
        private Array<T2> array2;
        private Func<T1, T2, T3> combine;
        private long iNextToCoalesce;

        public override long Length { get { return array1.Length; } }

        // The arrays must be the same length
        public DotArray(/*[Equal("Length", "array2.Length")] */Array<T1> array1, /*[Equal("Length", "array1.Length")] */Array<T2> array2, Func<T1, T2, T3> combine) {
            this.array1 = array1; 
            this.array2 = array2;
            this.combine = combine;
        }

        protected override bool TryGetNextUncoalescedItem(out T3 item) {
            if (iNextToCoalesce >= Length) {
                item = default(T3);
                return false;
            }
            item = combine(array1[iNextToCoalesce], array2[iNextToCoalesce]);
            ++iNextToCoalesce;
            return true;
        }
    }
}
