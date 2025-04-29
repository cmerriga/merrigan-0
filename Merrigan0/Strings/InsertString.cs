using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class InsertString : String {
        private String baseString;
        private long i;
        private String insertedString;

        public override long Length { get { return baseString.Length + insertedString.Length; } }

        public InsertString(String baseString, long i, String insertedString)
        { 
            this.baseString = baseString;
            this.i = i;
            this.insertedString = insertedString;
        }

        public override void CopyTo(char[] array, int arrayIndex) {
            baseString.Substring(0, i).CopyTo(array, arrayIndex);
            insertedString.CopyTo(array, arrayIndex + (int)i);
            baseString.Substring(i, baseString.Length - i).CopyTo(array, arrayIndex + (int)insertedString.Length);
        }

        ///// Special concatenated enumerator?

        public override bool TryGetCharacter(long i, out char ch) {
            if (i < baseString.Length) {
                return baseString.TryGetCharacter(i, out ch);
            }
            i -= baseString.Length;
            if (i < insertedString.Length) {
                return insertedString.TryGetCharacter(i, out ch);
            }
            return baseString.TryGetCharacter(i - insertedString.Length, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    if (i < baseString.Length) {
        //        return baseString[i];
        //    }
        //    i -= baseString.Length;
        //    if (i < insertedString.Length) {
        //        return insertedString[i];
        //    }
        //    return baseString[i - insertedString.Length];
        //}
    }
}
