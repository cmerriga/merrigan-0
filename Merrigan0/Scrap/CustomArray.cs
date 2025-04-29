//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;

//namespace Merrigan0 {
//    [Untested]
//    public class CustomArray<T> : Array<T> {
//        // The index input will never be out of range
//        private Func<long, T> getItemFunction;

//        private long length;

//        public override long Length { get { return length; } }

//        public CustomArray(long length, Func<long, T> getItemFunction) {
//            this.getItemFunction = getItemFunction;
//            this.length = length;
//        }

//        public override bool TryGetItem(long i, out T item) {
//            if (i >= length) {
//                item = default(T);
//                return false;
//            }
//            item = getItemFunction(i);
//            return true;
//        }
//    }
//}
