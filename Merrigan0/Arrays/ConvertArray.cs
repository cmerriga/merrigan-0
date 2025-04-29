using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an array of one type but you want to pretend it's an array of another type.
    [Untested]
    internal class ConvertArray<TFrom, TTo> : Array<TTo> {
        //private Func<object, object> convertFunction;
        private Array<TFrom> baseItems;

        public override long Length { get { return baseItems.Length; } }

        public ConvertArray(Array<TFrom> baseItems) {
            //if (!Conversion.TryGetConvertFunction(typeof(TFrom), typeof(TTo), out convertFunction)) {
            //    throw new Exception();
            //}
            this.baseItems = baseItems;
        }

        public override bool TryGetItem(long i, out TTo item) {
            if (i >= baseItems.Length) {
                item = default(TTo);
                return false;
            }
            item = Conversion.Convert<TTo>(baseItems[i]);
            //item = (TTo)convertFunction(baseItems[i]);
            return true;
        }
    }
}
