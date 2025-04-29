using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.ArraysInternal {
//    // Use when you have an array of one type but you want to pretend it's an array of another type.
//    [Untested]
//    internal class ConvertArray2D<TFrom, TTo> : Array2D<TTo> {
//        private Array2D<TFrom> baseItems;

//        public override long Height { get { return baseItems.Height; } }

//        public ConvertArray2D(Array2D<TFrom> items) { this.baseItems = items; }

//        public override Array<TTo> Row(long r) {
//            return baseItems.Row(r).Cast<TTo>();
//        }

//        public override long RowLength(long r) {
//            Utilities.ThrowIndexOutOfRangeException(r, 0);
//            return default(long);
//        }

//        public override TTo Item(long r, long c) {
//            return (TTo)(object)baseItems[r, c];
//        }

//        protected override Array<TTo> AsArray() {
//            return baseItems.Flattened.Convert<TTo>();
//        }
//    }
//}
