using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    // Adapter to use when you have a Matrix but need a flat Array. The items are ordered in row-major order.
    //[Untested]
    //internal class MatrixArray<T> : Array<T> {
    //    private Matrix<T> matrix;

    //    public override long Length { get { return matrix.Height * matrix.Width; } }

    //    public MatrixArray(Matrix<T> matrix) {
    //        this.matrix = matrix;
    //    }

    //    public override bool TryGetItem(long i, out T item) {
    //        long r = i / matrix.Width;
    //        long c = i % matrix.Width;
    //        return matrix.TryGetItem(r, c, out item);
    //    }
    //}
}
