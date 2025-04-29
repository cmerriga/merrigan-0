using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.Matrixes {
    //// Wrapper for a 2-D block. Use when you have a 2-D block but need an Matrix.
    //[Untested]
    //internal class BlockWrapperMatrix<T> : Matrix<T> {
    //    private T[,] block;

    //    public override long Height { get { return block.GetLongLength(0); } }
    //    public override long Width { get { return block.GetLongLength(1); } }

    //    public BlockWrapperMatrix(T[,] block) {
    //        this.block = block;
    //        ////block = null;
    //    }

    //    /// <summary>
    //    /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
    //    /// </summary>
    //    /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
    //    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    //    /// <exception cref="ArgumentNullException">array is null.</exception>
    //    /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
    //    public override void CopyTo(T[] array, int arrayIndex) { block.CopyTo(array, arrayIndex); }

    //    public override T[,] ToBlock() { return block; }

    //    public override bool TryGetItem(long r, long c, out T item) {
    //        if (r >= Height || c >= Width) {
    //            item = default(T);
    //            return false;
    //        }
    //        item = block[r, c];
    //        return true;
    //    }
    //}
}
