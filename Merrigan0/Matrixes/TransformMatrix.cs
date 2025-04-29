using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    //[Untested]
    //internal class TransformMatrix<T1, T2> : Matrix<T2> {
    //    private Matrix<T1> baseMatrix;
    //    private Func<T1, T2> transform;

    //    public override long Height { get { return baseMatrix.Height; } }
    //    public override long Width { get { return baseMatrix.Width; } }

    //    public TransformMatrix(Matrix<T1> baseMatrix, Func<T1, T2> transform) {
    //        this.baseMatrix = baseMatrix;
    //        this.transform = transform;
    //    }

    //    public override bool TryGetItem(long r, long c, out T2 item) {
    //        if (!(0 <= r && r < Height && 0 <= c && c < Width)) {
    //            item = default(T2);
    //            return false;
    //        }
    //        item = transform(baseMatrix[r, c]);
    //        return true;
    //    }
    //}
}
