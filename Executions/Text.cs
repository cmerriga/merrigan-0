using System;
using System.Collections.Generic;
using System.Text;

namespace Executions {
    public static partial class Text {
        public static string CapitalizeLowerCase(string lowerCaseString) {
            if (lowerCaseString.Length > 0) {
                lowerCaseString = Char.ToUpperInvariant(lowerCaseString[0]) + lowerCaseString.Substring(1);
            }
            return lowerCaseString;
        }

        public static string CombineAsPascalCase(IList<string> lowerCaseStrings) {
            StringBuilder builder = new StringBuilder();
            foreach (string lowerCaseString in lowerCaseStrings) {
                builder.Append(CapitalizeLowerCase(lowerCaseString));
            }
            return builder.ToString();
        }

        public static IList<string> GetLowerCasePartsFromMixedCase(string alphanumericString) {
            // Go through string and begin new lowerCasePart either at the beginning, or after a transition from capital to
            // non-capital, or after a transition to or from a number
            if (alphanumericString.Length == 0) {
                return new string[0];
            }

            int iStartOfCurrentPart = 0;
            string lowerCasePart;
            List<string> partsSoFar = new List<string>();
            char currentChar = alphanumericString[0];
            bool previousWasCapital = Char.IsUpper(currentChar);
            bool previousWasDigit = Char.IsDigit(currentChar);
            int i = 1;
            while (i < alphanumericString.Length) {
                currentChar = alphanumericString[i];
                bool capital = Char.IsUpper(currentChar);
                bool digit = Char.IsDigit(currentChar);
                if (capital && !previousWasCapital) {
                    // abcD, 123A, A
                    lowerCasePart = alphanumericString.Substring(iStartOfCurrentPart, i - iStartOfCurrentPart).ToLowerInvariant();
                    partsSoFar.Add(lowerCasePart);
                    iStartOfCurrentPart = i;
                } else if ((digit && !previousWasDigit) || (!digit && previousWasDigit)) {
                    // abc1, 123a, ABC1, 123A
                    lowerCasePart = alphanumericString.Substring(iStartOfCurrentPart, i - iStartOfCurrentPart).ToLowerInvariant();
                    partsSoFar.Add(lowerCasePart);
                    iStartOfCurrentPart = i;
                } else if (!capital && previousWasCapital) {
                    if (iStartOfCurrentPart < i - 1) {
                        lowerCasePart = alphanumericString.Substring(iStartOfCurrentPart, i - 1 - iStartOfCurrentPart).ToLowerInvariant();
                        partsSoFar.Add(lowerCasePart);
                        iStartOfCurrentPart = i - 1;
                    }
                }
                previousWasCapital = capital;
                previousWasDigit = digit;
                ++i;
            }

            // Append last lowerCasePart
            lowerCasePart = alphanumericString.Substring(iStartOfCurrentPart).ToLowerInvariant();
            partsSoFar.Add(lowerCasePart);
            return partsSoFar;
        }

        public static string RemoveNonalphanumeric(string s) {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in s) {
                if (Char.IsLetterOrDigit(ch)) {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }
    }
}
