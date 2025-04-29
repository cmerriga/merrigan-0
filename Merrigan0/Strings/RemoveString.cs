using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class RemoveString : String {
        private String baseString;
        private long iRemoved;
        private long lengthRemoved;

        public override long Length { get { return baseString.Length - lengthRemoved; } }

        public RemoveString(String baseString, long iRemoved, long lengthRemoved)
        { 
            this.iRemoved = iRemoved;
            this.lengthRemoved = lengthRemoved;
            this.baseString = baseString;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(char[] array, int arrayIndex) {
            baseString.Substring(0, iRemoved).CopyTo(array, arrayIndex);
            long iBaseAfterRemoved = iRemoved + lengthRemoved;
            baseString.Substring(iBaseAfterRemoved).CopyTo(array, arrayIndex + (int)iRemoved);
        }

        ///// Special removed enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            if (i < iRemoved) {
                return baseString.TryGetCharacter(i, out ch);
            }
            return baseString.TryGetCharacter(i + lengthRemoved, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    if (i < iRemoved) {
        //        return baseString[i];
        //    }
        //    return baseString[i + lengthRemoved];
        //}
    }
}
