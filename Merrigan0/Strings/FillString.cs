using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // A string containing repetitions of a single character.
    [Untested]
    internal class FillString : String {
        private char ch;
        private long length;

        public override long Length { get { return length; } }

        public FillString(char ch, long length) {
            this.ch = ch;
            this.length = length;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= length) {
                ch = default(char);
                return false;
            }
            ch = this.ch;
            return true;
        }

        //protected override char GetCharacter(long i) {
        //    if (i >= length) { throw new IndexOutOfRangeException(); }
        //    return ch;
        //}
    }
}
