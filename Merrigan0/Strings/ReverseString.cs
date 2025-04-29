using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class ReverseString : String {
        private String baseString;

        public override long Length { get { return baseString.Length; } }

        public ReverseString(String baseString)
        {
            this.baseString = baseString;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            String s = "abc";
            String reversed = new ReverseString(s);
            Testing.TestEquals(reversed, "cba");
        }

        public override String Reverse() {
            return baseString;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= Length) {
                ch = default(char);
                return false;
            }
            return baseString.TryGetCharacter(baseString.Length - i - 1, out ch);
        }
    }
}
