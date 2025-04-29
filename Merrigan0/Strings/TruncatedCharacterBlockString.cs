using System;
using System.IO;

namespace Merrigan0.StringsInternal {
    // When you've got a character characters but need a String.
    [Untested]
    internal class TruncatedCharacterBlockString : String {
        private Array<char> characters;
        
        public override long Length { get { return characters.Length; } }

        public TruncatedCharacterBlockString(char[] block, long length) {
            characters = Array<char>.From(block, length);
        }

        public override bool TryGetCharacter(long i, out char ch) {
            return characters.TryGetItem(i, out ch);
        }

        //protected override char GetCharacter(long i) { return characters[i]; }
    }
}
