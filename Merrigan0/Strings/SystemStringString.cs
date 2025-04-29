using System;
using System.Collections.Generic;

namespace Merrigan0 {
    // Use when you have a System.String but need a String.
    [Untested]
    internal class SystemStringString : String {
        private string s;

        /// <summary>
        /// Gets the number of distinctElements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <value>The number of distinctElements contained in the <see cref="ICollection{T}"/>.</value>
        public override long Length { get { return s.Length; } }

        public SystemStringString(System.String s) : base() { this.s = s; }

        public override char[] ToBlock() { return s.ToCharArray(); }

        public override IEnumerable<char> ToIEnumerable() { return s; }

        public override string ToString() { return s; }

        public override bool TryGetCharacter(long i, out char ch) {
            if (i >= s.Length) {
                ch = default(char);
                return false;
            }
            ch = s[(int)i];
            return true;
        }

        //protected override char GetCharacter(long i) { return s[(int)i]; }
    }
}
