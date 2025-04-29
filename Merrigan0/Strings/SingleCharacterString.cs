using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // A string containing a single character.
    [Untested]
    internal class SingleCharacterString : String {
        private char ch;

        public override long Length { get { return 1; } }

        public SingleCharacterString(char ch) {
            this.ch = ch;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i == 0) {
                ch = this.ch;
                return true;
            }
            ch = default(char);
            return false;
        }

        //protected override char GetCharacter(long i) {
        //    if (i >= Length) { throw new IndexOutOfRangeException(); } /// promote to debug check
        //    return ch;
        //}
    }
}
