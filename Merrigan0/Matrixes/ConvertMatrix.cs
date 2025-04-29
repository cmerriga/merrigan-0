using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    //// Use when you have a Matrix of one type but you want to pretend it's a Matrix of another type.
    //[Untested]
    //internal class ConvertMatrix<TFrom, TTo> : Matrix<TTo> {
    //    //private Func<object, object> convertFunction;
    //    private Matrix<TFrom> baseMatrix;

    //    public override long Height { get { return baseMatrix.Height; } }
    //    public override long Width { get { return baseMatrix.Width; } }

    //    public ConvertMatrix(Matrix<TFrom> baseMatrix) {
    //        //if (!Conversion.TryGetConvertFunction(typeof(TFrom), typeof(TTo), out convertFunction)) {
    //        //    throw new Exception();
    //        //}
    //        this.baseMatrix = baseMatrix;
    //    }

    //    public override bool TryGetItem(long r, long c, out TTo item) {
    //        TFrom baseItem;
    //        if (!baseMatrix.TryGetItem(r, c, out baseItem)) {
    //            item = default(TTo);
    //            return false;
    //        }
    //        item = Conversion.Convert<TTo>(baseItem);
    //        //item = (TTo)convertFunction(baseItem);
    //        return true;
    //    }
    //}
}
