using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class InsertCharacterString : String {
        private String baseString;
        private long i;
        private char ch;

        public override long Length { get { return baseString.Length + 1; } }

        public InsertCharacterString(String baseString, long i, char ch)
        { 
            this.baseString = baseString;
            this.i = i;
            this.ch = ch;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(char[] array, int arrayIndex) {
            baseString.Substring(0, i).CopyTo(array, arrayIndex);
            array[arrayIndex + i] = ch;
            baseString.Substring(i, baseString.Length - i).CopyTo(array, arrayIndex + (int)i + 1);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            if (i < this.i) {
                return baseString.TryGetCharacter(i, out ch);
            }
            if (i > this.i) {
                return baseString.TryGetCharacter(i - 1, out ch);
            }
            ch = this.ch;
            return true;
        }

        //protected override char GetCharacter(long i) {
        //    if (i < this.i) {
        //        return baseString[i];
        //    }
        //    if (i > this.i) {
        //        return baseString[i - 1];
        //    }
        //    return ch;
        //}
    }
}
