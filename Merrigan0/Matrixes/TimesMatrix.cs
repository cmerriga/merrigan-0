using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    /*
     * Start with basic: no efficiency, but correct algorithm.
     * Then make big version.
     * Then see if something else needed.
     */

    // Use when you have two matrixes to multiply.
    //[Untested]
    //internal class TimesMatrix<T> : Matrix<T> {
    //    private long h;
    //    private long w;
    //    private T[,] block;

    //    public override long Height { get { return h; } }
    //    public override long Width { get { return w; } }

    //    public TimesMatrix(Matrix<T> left, Matrix<T> right) {
    //        h = left.Height;
    //        w = right.Width;

    //        // The new block will be the number of rows of the left, and the number of columns of the right
    //        T[,] block = new T[h, w];
    //        Func<object, object, object> calculateFunction;
    //        if (!TimesOperator.Only.TryGetCalculateFunction(Reflection.TypeParameter(left.GetType()), Reflection.TypeParameter(right.GetType()), out calculateFunction)) {
    //            throw new ArgumentException();
    //        }
    //        for (long r = 0L; r < h; ++r) {
    //            Array<T> row = left.Row(r);
    //            for (long c = 0L; c < w; ++c) {
    //                block[r, c] = row.Dot(right.Column(c), (item1, item2) => (T)calculateFunction(item1, item2));
    //            }
    //        }
    //    }

    //    public override Matrix<T> Multiply(Matrix<T> m2) {
    //        return baseMatrix;
    //    }

    //    public override bool TryGetItem(long r, long c, out T item) {
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
