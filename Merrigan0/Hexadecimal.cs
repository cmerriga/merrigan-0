using System;

namespace Merrigan0 {
    public static class Hexadecimal {
        [Example("f", 15)]
        [Example("FA3", 4003)]
        public static int From(String hex) {
            int resultSoFar = 0;
            for (int i = 0; i < hex.Length; ++i) {
                int value = Number(hex[i]);
                resultSoFar *= 16;
                resultSoFar += value;
            }
            return resultSoFar;
        }

        [Example(0, false, '0')]
        [Example(13, false, 'd')]
        [Example(13, true, 'D')]
        [Throws(-1, true)]
        public static char HexDigit(int n, bool capital = false) {
            if (n < 0) { goto unrecognized; }
            if (n < 10) {
                return (char)('0' + n);
            }
            if (n < 16) {
                return (char)((capital ? 'A' : 'a') + n - 10);
            }

        unrecognized:
            throw new Exception("No hex digit for " + n);
        }

        [Example(10, null, "a")]
        [Example(100, null, "64")]
        [Example(100, 4, "0064")]
        public static String Hex(int n, int? nDigits = null) {
            MutableString ms = new MutableString();
            int nRemaining = n;
            int nDigitsSoFar = 0;
            while (nRemaining > 0 || (nDigits.HasValue && nDigitsSoFar < nDigits.Value)) {
                int nextDigit = nRemaining & 0xF;
                ms.Append(HexDigit(nextDigit));
                nRemaining >>= 4;
                ++nDigitsSoFar;
            }
            return ms.Current.Reverse();
        }

        [Example('0', 0)]
        [Example('9', 9)]
        [Example('a', 10)]
        [Example('f', 15)]
        [Example('A', 10)]
        [Example('F', 15)]
        [Throws('0' - 1)]
        [Throws('9' + 1)]
        [Throws('a' - 1)]
        [Throws('f' + 1)]
        [Throws('A' - 1)]
        [Throws('F' + 1)]
        public static int Number(char ch) {
            if (ch <= '9') {
                if (ch < '0') { goto unrecognized; }
                return ch - '0';
            }
            if (ch <= 'F') {
                if (ch < 'A') { goto unrecognized; }
                return ch - 55; // 55 = 'A' - 10
            }
            if (ch <= 'f') {
                if (ch < 'a') { goto unrecognized; }
                return ch - 87; // 87 = 'a' - 10
            }

        unrecognized:
            throw new Exception("Unrecognized  character.");
        }
    }
}
