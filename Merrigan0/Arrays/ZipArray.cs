using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // A just-in-time generator of tuples.
    [Untested]
    internal class ZipArray<T1, T2> : Array<Tuple<T1, T2>> {
        private Array<T1> array1;
        private Array<T2> array2;

        public override long Length { get { return array1.Length; } }

        // The arrays must be the same length
        public ZipArray(Array<T1> array1, Array<T2> array2) {
            this.array1 = array1; 
            this.array2 = array2;
        }

        public override bool TryGetItem(long i, out Tuple<T1, T2> item) {
            T1 item1;
            if (!array1.TryGetItem(i, out item1)) {
                item = default(Tuple<T1, T2>);
                return false;
            }
            item = new Tuple<T1, T2>(item1, array2[i]);
            return true;
        }

        //protected override Tuple<T1, T2> GetItem(long i) { return new Tuple<T1, T2>(array1[i], array2[i]); }
    }
}
