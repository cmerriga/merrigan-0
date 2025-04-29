using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class ArrayConcatenateString : String {
        private Array<String> strings;

        // The first index of each component string, in the overall string
        private Array<long> indexes;

        public override long Length { get { return strings.Sum(s => s.Length); } }

        public ArrayConcatenateString(Array<String> strings)
        {
            this.strings = strings;
            MutableArray<long> indexesSoFar = new MutableArray<long>();
            long i = 0;
            long length = strings.Length;
            for (int iString = 0; iString < length; ++iString) {
                indexesSoFar.Append(i);
                i += strings[iString].Length;
            }
            indexes = indexesSoFar.Current;
        }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo(char[] array, int arrayIndex) {
            for (int i = 0; i < strings.Length; ++i) {
                strings[i].CopyTo(array, arrayIndex + (int)indexes[i]);
            }
        }

        ///// Special concatenated enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            for (int iString = 0; iString < strings.Length; ++iString) {
                long iInString = i = indexes[iString];
                if (iInString < strings[iString].Length) {
                    return strings[iString].TryGetCharacter(iInString, out ch);
                }
            }
            ch = default(char);
            return false;
        }

        //protected override char GetCharacter(long i) {
        //    for (int iString = 0; iString < strings.Length; ++iString) {
        //        if (i < indexes[iString] + strings[iString].Length) {
        //            return strings[iString][i];
        //        }
        //    }
        //    throw new IndexOutOfRangeException();
        //}
    }
}
