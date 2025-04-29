using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    //// Use when you have a matrix and want its inverse.
    //[Untested]
    //internal class InverseMatrix<T> : Matrix<T> {
    //    private Matrix<T> baseMatrix;
    //    private Matrix<T> inverseMatrix;

    //    public override long Height { get { return baseMatrix.Height; } }
    //    public override long Length { get { return baseMatrix.Length; } }
    //    public override long Width { get { return baseMatrix.Width; } }

    //    public InverseMatrix(Matrix<T> baseMatrix) { this.baseMatrix = baseMatrix; }

    //    public override Matrix<T> Inverse() {
    //        return baseMatrix;
    //    }

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
