
//    using System;
//    using System.Collections.Generic;

//    internal class UnlimitedInteger :
//        Integer {
//        public override Int32 Bits { get { return Int32.MaxValue; } }

//        public UnlimitedInteger(Int64 n) {
//            this.n = n;
//            //unchecked {
//            //    this.components[(Integer)0] = (Int32)(n & 0xFFFFFFFF);
//            //    this.components[(Integer)1] = (Int32)(((UInt64)n & 0xFFFFFFFF00000000) >> 32);
//            //}
//        }

//        public override Int32 AsInt32() {
//            return (Int32)n;
//        }

//        public override Int64 AsInt64() {
//            return n;
//        }

//        public override Integer PlusInt32(Int32 n) {
//            return (Integer)(this.n + (Int64)n);
//        }

//        public override Integer PlusInt64(Int64 n) {
//            return (Int64)this.n + n;
//        }

//        public override Integer Plus(Integer n) {
//            return (Int64)this.n + (Int64)n;
//        }

//        public override Integer MinusInt32(Int32 n) {
//            return this.n + n;
//        }

//        public override Integer MinusInt64(Int64 n) {
//            return (Int64)this.n - n;
//        }

//        public override Integer Minus(Integer n) {
//            return (Int64)this.n - (Int64)n;
//        }

//        // A list of 32-bit parts of the number, from least significant to most.
//        //// private List<Int32> components;

//        //// currently shortcutted to a native int64
//        private Int64 n;
//    }
//}
