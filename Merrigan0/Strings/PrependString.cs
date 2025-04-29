using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    class PrependString : String {
        private String previous;
        private char ch;

        public override long Length { get { return previous.Length + 1; } }

        public PrependString(String previous, char item)
        { 
            this.previous = previous;
            this.ch = item;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(char[] array, int arrayIndex) {
            array[0] = ch;
            previous.CopyTo(array, arrayIndex + 1);
        }

        ///// Special prepended enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            if (i == 0) {
                ch = this.ch;
                return true;
            }
            return previous.TryGetCharacter(i + 1, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    if (i == 0) {
        //        return ch;
        //    }
        //    return previous[i + 1];
        //}
    }
}
