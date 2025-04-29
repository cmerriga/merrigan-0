using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.ArraysInternal {
//    // Use when you have an array of one type but you want to pretend it's an array of another type.
//    [Untested]
//    internal class CastArray2D<TFrom, TTo> : Array2D<TTo> {
//        private Array2D<TFrom> items;

//        public override long Height { get { return items.Height; } }

//        public CastArray2D(Array2D<TFrom> items) { this.items = items; }

//        public override Array<TTo> Row(long r) {
//            return items.Row(r).Cast<TTo>();
//        }

//        public override long RowLength(long r) {
//            Utilities.ThrowIndexOutOfRangeException(r, 0);
//            return default(long);
//        }

//        public override TTo Item(long r, long c) {
//            return (TTo)(object)items[r, c];
//        }

//        protected override Array<TTo> AsArray() {
//            return items.Flattened.Cast<TTo>();
//        }
//    }
//}
