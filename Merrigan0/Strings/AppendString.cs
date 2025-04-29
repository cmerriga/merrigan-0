using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // When you have an array plus an map to put on the end
    [Untested]
    internal class AppendString : String {
        private String s;
        private char ch;

        public override long Length { get { return s.Length + 1; } }

        public AppendString(String s, char ch)
        { 
            this.s = s;
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
            s.CopyTo(array, arrayIndex);
            array[arrayIndex + s.Length] = ch;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i < s.Length) {
                return s.TryGetCharacter(i, out ch);
            }
            if (i > s.Length) {
                ch = default(char);
                return false;
            }
            ch = this.ch;
            return true;
        }

        //protected override char GetCharacter(long i) {
        //    if (i < s.Length) {
        //        return s[i];
        //    }
        //    if (i > s.Length) {
        //        throw new IndexOutOfRangeException();
        //    }
        //    return ch;
        //}
    }
}
