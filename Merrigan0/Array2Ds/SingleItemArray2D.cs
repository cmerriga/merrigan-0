using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.Internal.Matrixes {
//    [Untested]
//    internal class SingleItemArray2D<T> : Array2D<T> {
//        private T item;

//        public override long Count { get { return 1L; } }
//        public override long Height { get { return 1L; } }

//        public SingleItemArray2D(T item) { this.item = item; }

//        public override IEnumerator<T> GetEnumerator() { return new SingleItemEnumerator<T>(item); }

//        public override long RowLength(long r) {
//            Utilities.ThrowIfIndexOutOfRange(r, 1);
//            return 1L;
//        }

//        protected override Array<T> AsArray() {
//            //// Not going to work
//            return Array<T>.From(item);
//        }

//        protected override T Item(long r, long c) {
//            if (r != 0) {
//                Utilities.ThrowIndexOutOfRangeException(r, 1);
//            }
//            if (c != 0) {
//                Utilities.ThrowIndexOutOfRangeException(c, 1);
//            }
//            return item;
//        }
//    }
//}
