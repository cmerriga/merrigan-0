using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    // Use when you have a matrix of one type but you want to pretend it's a matrix of another type.
    //[Untested]
    //internal class CastMatrix<TFrom, TTo> : Matrix<TTo> {
    //    private Matrix<TFrom> baseMatrix;

    //    public override long Height { get { return baseMatrix.Height; } }
    //    public override long Width { get { return baseMatrix.Width; } }

    //    public CastMatrix(Matrix<TFrom> baseMatrix) { this.baseMatrix = baseMatrix; }

    //    public override bool TryGetItem(long r, long c, out TTo item) {
    //        TFrom baseItem;
    //        if (!baseMatrix.TryGetItem(r, c, out baseItem)) {
    //            item = default(TTo);
    //            return false;
    //        }
    //        item = (TTo)(object)baseItem;
    //        return true;
    //    }
    //}
}
