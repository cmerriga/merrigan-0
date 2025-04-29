using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.ArraysInternal {
//    // Use when you have an array of one type but you want to pretend it's an array of another type.
//    [Untested]
//    internal class Subarray2D<T> : Array2D<T> {
//        private Array2D<T> baseItems;
//        private long h;
//        private long r;

//        public override long Height { get { return h; } }

//        public Subarray2D(Array2D<T> baseItems, long r, long h) {
//            this.h = h;
//            this.r = r;
//        }

//        public override Array<T> Row([NotNegative]/*[Less("this.r + h")]*/ long r) {
//            return baseItems.Row(this.r + r);
//        }

//        public override long RowLength(long r) {
//            return baseItems.RowLength(r - this.r);
//        }

//        public override T Item(long r, long c) {
//            return baseItems[this.r + r, c];
//        }

//        protected override Array<T> AsArray() {
//            MutableArray<T> resultSoFar = new MutableArray<T>();
//            long iEnd = r + h;
//            for (long i = 0L; i < iEnd; ++i) {
//                resultSoFar.Append(Row(i));
//            }
//            return resultSoFar.Current;
//        }
//    }
//}
