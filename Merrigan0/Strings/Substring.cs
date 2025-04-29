using System;

namespace Merrigan0.StringsInternal {
    [Untested]
    internal class Substring : String {
        private String baseString;
        private long index;
        private long length;

        public override long Length { get { return length; } }

        public Substring(String @base, long index, long length) {
            this.baseString = @base;
            this.index = index;
            this.length = length;
        }

        public override string ToString() {
            char[] chars = new char[length];
            for (long i = 0; i < length; ++i) {
                chars[i] = baseString[index + i];
            }
            return new string(chars);
        }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= Length) {
                ch = default(char);
                return false;
            }
            return baseString.TryGetCharacter(index + i, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    if (i >= length) {
        //        throw new IndexOutOfRangeException();
        //    }
        //    return baseString[index + i];
        //}
    }
}
