using System;
using System.Collections.Generic;
using Merrigan0.ExecutionsInternal;

namespace Merrigan0.StringsInternal {
    // When you have an array plus an map to put on the end
    [Untested]
    internal class ReplaceString : String {
        private String baseString;
        private Array<Tuple<Recognizer, String>> replacements;
        private String replacedString;

        public override long Length { 
            get {
                EnsureCoalesced();
                return replacedString.Length;
            }
        }

        public ReplaceString(String s, params object[] recognizersAndReplacementStrings) {
            baseString = s;
            replacements = Utilities.ToPairs<Recognizer, String>(recognizersAndReplacementStrings);
        }

        public override bool TryGetCharacter(long i, out char ch) {
            EnsureCoalesced();
            return replacedString.TryGetCharacter(i, out ch);
        }

        private void EnsureCoalesced() {
            if (replacedString != null) {
                return;
            }
            MutableString replacedStringSoFar = null;
            long i = 0;
            long baseStringLength = baseString.Length;
            long iAfterLastReplacement = 0;
            while (i < baseStringLength) {
                bool replacementMade = false;
                foreach (Tuple<Recognizer, String> replacement in replacements) {
                    RecognitionTree treeToReplace;
                    long iToReplace = i;
                    if (replacement.Item1.TryRead(baseString, ref iToReplace, out treeToReplace)) {
                        // Create mutable string if not there
                        if (replacedStringSoFar == null) {
                            replacedStringSoFar = new MutableString();
                        }

                        // Append all the characters since the last replacement
                        replacedStringSoFar.Append(baseString.Substring(iAfterLastReplacement, i - iAfterLastReplacement));

                        // Append the substitution
                        replacedStringSoFar.Append(replacement.Item2);

                        iAfterLastReplacement = iToReplace;
                        i = iToReplace;
                        replacementMade = true;
                        break;
                    }
                }
                if (!replacementMade) {
                    ++i;
                }
            }
            if (replacedStringSoFar == null) {
                replacedString = baseString;
            } else {
                if (i > iAfterLastReplacement) {
                    replacedStringSoFar.Append(baseString.Substring(iAfterLastReplacement, i - iAfterLastReplacement));
                }
                replacedString = replacedStringSoFar.Current;
            }
        }
    }
}
