using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    // Use when you have a matrix of one type but you want to pretend it's a matrix of another type.
    //[Untested]
    //internal class Submatrix<T> : Matrix<T> {
    //    private Matrix<T> baseMatrix;
    //    private long r;
    //    private long c;
    //    private long h;
    //    private long w;

    //    public override long Height { get { return h; } }
    //    public override long Width { get { return w; } }

    //    public Submatrix(Matrix<T> baseMatrix, long r, long c, long h, long w) {
    //        this.baseMatrix = baseMatrix;
    //        this.r = r;
    //        this.c = c;
    //        this.h = h;
    //        this.w = w;
    //    }

    //    public override bool TryGetItem(long r, long c, out T item) {
    //        if (!(0 <= r && r < h && 0 <= c && c < w)) {
    //            item = default(T);
    //            return false;
    //        }
    //        item = baseMatrix[this.r + r, this.c + c];
    //        return true;
    //    }
    //}
}
