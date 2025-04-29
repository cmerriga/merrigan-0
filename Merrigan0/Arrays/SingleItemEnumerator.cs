using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    [Untested]
    internal class SingleItemEnumerator<T> : FillEnumerator<T> {
        public SingleItemEnumerator(T item) :
            base(1, item) {
        }
    }
}
