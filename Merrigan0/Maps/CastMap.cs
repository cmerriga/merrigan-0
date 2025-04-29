using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.ArraysInternal {
//    // Use when you have an array of one type but you want to pretend it's an array of another type.
//    [Untested]
//    internal class CastMap<TFrom, TOriginalTo, TTo> : Map<TFrom, TTo> {
//        private Map<TFrom, TOriginalTo> baseMap;

//        public override long Length { get { return baseMap.Length; } }

//        public CastMap(Map<TFrom, TOriginalTo> baseMap) { this.baseMap = baseMap; }

//        public override bool TryGetItem(long i, out TTo item) {
//            if (i >= items.Length) {
//                item = default(TTo);
//                return false;
//            }
//            item = (TTo)(object)items[i];
//            return true;
//        }
//        //--------------------

//        public override bool TryGetValue(TFrom from, out TTo to) { return dictionary.TryGetValue(from, out to); }


//        protected override Set<TFrom> GetDomain() { return baseMap.Domain; }
//    }
//}
