using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    //[Untested]
    //internal class SingleItemMatrix<T> : Matrix<T> {
    //    private T item;

    //    public override long Height { get { return 1L; } }
    //    public override long Width { get { return 1L; } }

    //    public SingleItemMatrix(T item) { this.item = item; }

    //    public override IEnumerator<T> GetEnumerator() { return new SingleItemEnumerator<T>(item); }

    //    public override bool TryGetItem(long r, long c, out T item) {
    //        if (r == 0 && c == 0) {
    //            item = this.item;
    //            return true;
    //        }
    //        item = default(T);
    //        return false;
    //    }
    //}
}
