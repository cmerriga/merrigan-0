//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace Merrigan0 {
//    public class CastingEnumerator<T, TTo> : Enumerator<TTo> {
//        private IEnumerator<T> baseEnumerator;
//        private bool started;

//        public override TTo UnsafeCurrent { get { return (TTo)(object)baseEnumerator.Current; } }
//        protected override Boolean Started { get { return started; } }

//        protected CastingEnumerator(IEnumerable<T> s) { this.baseEnumerator = s.GetEnumerator(); }

//        public override Boolean MoveNext() { return baseEnumerator.MoveNext(); }

//        public override void Reset() {
//            started = false;
//            baseEnumerator.Reset();
//        }
//    }
//}
