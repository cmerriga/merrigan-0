using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    // When you have a one-dimensional Array but want to treat it like a Matrix.
    //[Untested]
    //internal class ArrayMatrix<T> : Matrix<T> {
    //    private Array<T> baseArray;
    //    private long height;
    //    private long width;

    //    public override long Height { get { return height; } }
    //    public override long Width { get { return width; } }

    //    public ArrayMatrix(Array<T> baseArray, long height, long width) {
    //        if (height * width != baseArray.Length) {
    //            throw new ArgumentException();
    //        }
    //        this.baseArray = baseArray;
    //        this.height = height;
    //        this.width = width;
    //    }

    //    /// <summary>
    //    /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
    //    /// </summary>
    //    /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
    //    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    //    /// <exception cref="ArgumentNullException">array is null.</exception>
    //    /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
    //    public override void CopyTo(T[] array, int arrayIndex) { baseArray.CopyTo(array, arrayIndex); }

    //    public override bool TryGetItem(long r, long c, out T item) {
    //        if (r >= Height || c >= Width) {
    //            item = default(T);
    //            return false;
    //        }
    //        item = baseArray[r * Width + c];
    //        return true;
    //    }

    //    protected override Array<T> AsArray() {
    //        return new MatrixArray<T>(this);
    //    }
    //}
}
