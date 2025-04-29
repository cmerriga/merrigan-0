namespace Merrigan0 {
    [Untested]
    public static class TextUtilities {
        private static Set<char> regexCharactersNeedingEscape = Set<char>.From(
            '\\',
            '^',
            '$',
            '.',
            '|',
            '?',
            '*',
            '+',
            '(',
            ')',
            '[',
            '{');

        //private static Set<char> unprintableCharacters = Set<char>.From(
        //    '\r',
        //    '\n',
        //    '\b',
        //    '\t');

        //[Example("a", "a")]
        //[Example("(", "\\(")]
        //[return: Or("Length == 0", "Length == 1")]
        //[return: Contains("ch")]
        public static String RegexEscape(char ch) {
            if (regexCharactersNeedingEscape.Contains(ch)) {
                return String.From('\\', ch);
            }
            //if (unprintableCharacters.Contains(ch)) {
            //    return String.From('\\', ch);
            //}
            return String.From(ch);
        }

        //[Example("abc", "abc")]
        //[Example("abc(def", "abc\\(def")]
        //[return: Greater("Length", "s.Length")]
        public static String RegexEscape(String s) {
            MutableString escapedSoFar = new MutableString();
            long i = 0;
            long iNonescapedSegment = 0;
            while (i < s.Length) {
                char ch = s[i];
                if (regexCharactersNeedingEscape.Contains(ch)) {
                    if (i > iNonescapedSegment) {
                        escapedSoFar.Append(s.Substring(iNonescapedSegment, i - iNonescapedSegment));
                    }
                    escapedSoFar.Append(RegexEscape(ch));
                    iNonescapedSegment = i + 1;
                }
                ++i;
            }

            // Add last segment if there is one
            if (i > iNonescapedSegment) {
                escapedSoFar.Append(s.Substring(iNonescapedSegment, i - iNonescapedSegment));
            }

            return escapedSoFar.Current;
        }
    }
}
