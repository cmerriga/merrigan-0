using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // Use when you have a characters of bytes but need a String. Any change to the bytes of bytes will be reflected
    // in this object, so it is advisable to assign the character bytes to null immediately after construction.
    [Untested]
    internal class CharacterBlockString : String {
        private char[] characters;

        public override long Length { get { return characters.LongLength; } }

        public CharacterBlockString(char[] characters) { 
            this.characters = characters;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= Length) {
                ch = default(char);
                return false;
            }
            ch = characters[i];
            return true;
        }

        //protected override char GetCharacter(long i) { return characters[i]; }
    }
}
