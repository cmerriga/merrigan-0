using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    // Use when you have an array of bytes but need a String.
    [Untested]
    internal class CharacterArrayString : String {
        private Array<char> characters;

        public override long Length { get { return characters.Length; } }

        public CharacterArrayString(Array<char> characters) { this.characters = characters; }


        public override bool TryGetCharacter(long i, out char ch) {
            return characters.TryGetItem(i, out ch);
        }
        
        //protected override char GetCharacter(long i) { return characters[i]; }
    }
}
