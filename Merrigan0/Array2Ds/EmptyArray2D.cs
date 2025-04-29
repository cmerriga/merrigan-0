using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.Array2Ds {
//    // A 2-D array with no items.
//    [Untested]
//    internal class EmptyArray2D<T> : Array2D<T> {

//        public override long Count { get { return 0L; } }
//        public override long Height { get { return 0L; } }

//        public override Array<T> Row(long r) {
//            Utilities.ThrowIndexOutOfRangeException(r, 0);
//            return null;
//        }

//        public override long RowLength(long r) {
//            Utilities.ThrowIndexOutOfRangeException(r, 0);
//            return default(long);
//        }

//        public override T Item(long r, long c) {
//            Utilities.ThrowIndexOutOfRangeException(r, 0);
//            return default(T);
//        }

//        protected override Array<T> AsArray() {
//            return Array<T>.Empty;
//        }
//    }
//}
