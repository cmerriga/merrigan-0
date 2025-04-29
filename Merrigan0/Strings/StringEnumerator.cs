using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class StringEnumerator : Enumerator<char> {
        private long i;
        private String s;
        private char current;

        protected override char UnsafeCurrent { get { return current; } }

        public StringEnumerator(String s) {
            this.s = s;
        }

        public override Boolean MoveNext() {
            if (i >= s.Length - 1) {
                return false;
            }
            ++i;
            current = s[i];
            return true;
        }

        protected override void UnsafeReset() { i = -1; }
    }
}
