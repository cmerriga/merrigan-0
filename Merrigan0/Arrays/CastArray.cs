using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an array of one type but you want to pretend it's an array of another type.
    [Untested]
    internal class CastArray<TFrom, TTo> : Array<TTo> {
        private Array<TFrom> items;

        public override long Length { get { return items.Length; } }

        public CastArray(Array<TFrom> items) { this.items = items; }

        public override bool TryGetItem(long i, out TTo item) {
            if (i >= items.Length) {
                item = default(TTo);
                return false;
            }
            item = (TTo)(object)items[i];
            return true;
        }
    }
}
