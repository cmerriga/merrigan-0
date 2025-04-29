using System;
using System.IO;

namespace Merrigan0.StringsInternal {
    // When you've got a character characters but need a String.
    [Untested]
    internal class CharacterBlockSubstring : String {
        private char[] block;
        private long i;

        public override long Length { get { return block.Length; } }

        public CharacterBlockSubstring(char[] block, long i, long length) {
            this.block = block;
            this.i = i;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= Length) {
                ch = default(char);
                return false;
            }
            ch = block[this.i + i];
            return true;
        }

        //protected override char GetCharacter(long i) { return block[this.i + i]; }
    }
}
