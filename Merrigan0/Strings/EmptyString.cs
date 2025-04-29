using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // A zero-length string.
    [Untested]
    internal class EmptyString : String {
        public static readonly EmptyString Only = new EmptyString();

        public override long Length { get { return 0; } }

        public override bool TryGetCharacter(long i, out char ch) {
            ch = default(char);
            return false;
        }

        //protected override char GetCharacter(long i) { throw new IndexOutOfRangeException(); }
    }
}
