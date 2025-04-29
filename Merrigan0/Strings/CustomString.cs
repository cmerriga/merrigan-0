using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0 {
    [Untested]
    public class CustomString : String {
        // The index input will never be out of range
        private Func<long, char> getCharacterFunction;

        private long length;

        public override long Length { get { return length; } }

        public CustomString(long length, Func<long, char> getCharacterFunction) {
            this.getCharacterFunction = getCharacterFunction;
            this.length = length;
        }

        // When changed to use TryGetCharacter
        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= length) {
                ch = default(char);
                return false;
            }
            ch = getCharacterFunction(i);
            return true;
        }

        //protected override char GetCharacter(long i) {
        //    //// Until changed to use TryGetCharacter
        //    if (i >= length) {
        //        Utilities.ThrowIndexOutOfRangeException(i, length);
        //    }

        //    return getCharacterFunction(i);
        //}
    }
}
