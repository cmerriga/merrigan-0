using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class ConcatenateString : String {
        private String s1;
        private String s2;

        public override long Length { get { return s1.Length + s2.Length; } }

        public ConcatenateString(String s1, String s2)
        { 
            // If the value is not compliant with the distinctness and sort compareResult, keep them
            /////

            this.s1 = s1;
            this.s2 = s2;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(char[] array, int arrayIndex) {
            s1.CopyTo(array, arrayIndex);
            s2.CopyTo(array, arrayIndex + (int)s1.Length);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            long s1Length = s1.Length;
            if (i < s1Length) {
                return s1.TryGetCharacter(i, out ch);
            }
            return s2.TryGetCharacter(i - s1Length, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    long s1Length = s1.Length;
        //    if (i < s1Length) {
        //        return s1[i];
        //    }
        //    return s2[i - s1Length];
        //}
    }
}
