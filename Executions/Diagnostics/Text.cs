using System;
using System.Collections.Generic;
using ExecutionsTest = Executions.Test;

namespace Executions {
    public static partial class Text {
        public static void Test() {
            TestGetLowerCasePartsFromMixedCase();
            TestCombineAsPascalCase();
            TestCapitalizeLowerCase();
        }

        public static void TestCapitalizeLowerCase() {
            TestCapitalizeLowerCase("", "");
            TestCapitalizeLowerCase("a", "A");
            TestCapitalizeLowerCase("A", "A");
            TestCapitalizeLowerCase("ab", "Ab");
            TestCapitalizeLowerCase("1b", "1b");
        }

        public static void TestCapitalizeLowerCase(string lowerCaseString, string expectedResult) {
            string result = CapitalizeLowerCase(lowerCaseString);
            if (!string.Equals(result, expectedResult)) {
                Console.WriteLine("CapitalizeLowerCase failed on \"{0}\". Result: \"{1}\"", lowerCaseString, result);
            }
        }

        public static void TestCombineAsPascalCase() {
            TestCombineAsPascalCase(new string[] { "", "" }, "");
            TestCombineAsPascalCase(new string[] { "a", "" }, "A");
            TestCombineAsPascalCase(new string[] { "", "a" }, "A");
            TestCombineAsPascalCase(new string[] { "ab", "ab" }, "AbAb");
            TestCombineAsPascalCase(new string[] { "1b", "ab" }, "1bAb");
            TestCombineAsPascalCase(new string[] { "Ab", "Ab" }, "AbAb");
        }

        public static void TestCombineAsPascalCase(IList<string> lowerCaseStrings, string expectedResult) {
            string result = CombineAsPascalCase(lowerCaseStrings);
            if (!string.Equals(result, expectedResult)) {
                Console.WriteLine("CombineAsPascalCase failed on \"{0}\". Result: \"{1}\"", String.Join(", ", lowerCaseStrings), result);
            }
        }

        public static void TestGetLowerCasePartsFromMixedCase() {
            TestGetLowerCasePartsFromMixedCase("AbcDef", new string[] { "abc", "def" });
            TestGetLowerCasePartsFromMixedCase("abcDef", new string[] { "abc", "def" });
            TestGetLowerCasePartsFromMixedCase("ABCDef", new string[] { "abc", "def" });
            TestGetLowerCasePartsFromMixedCase("ABcdef", new string[] { "a", "bcdef" });
            TestGetLowerCasePartsFromMixedCase("abc12de", new string[] { "abc", "12", "de" });
            TestGetLowerCasePartsFromMixedCase("abc12De", new string[] { "abc", "12", "de" });
            TestGetLowerCasePartsFromMixedCase("ABC12de", new string[] { "abc", "12", "de" });
            TestGetLowerCasePartsFromMixedCase("AbcDEf", new string[] { "abc", "d", "ef" });
            TestGetLowerCasePartsFromMixedCase("AbcDEF", new string[] { "abc", "def" });
            TestGetLowerCasePartsFromMixedCase("", new string[] { });
            TestGetLowerCasePartsFromMixedCase("A", new string[] { "a" });
            TestGetLowerCasePartsFromMixedCase("a", new string[] { "a" });
            TestGetLowerCasePartsFromMixedCase("1", new string[] { "1" });
            TestGetLowerCasePartsFromMixedCase("Aa", new string[] { "aa" });
            TestGetLowerCasePartsFromMixedCase("AA", new string[] { "aa" });
            TestGetLowerCasePartsFromMixedCase("12", new string[] { "12" });
        }

        public static void TestGetLowerCasePartsFromMixedCase(string mixedCaseString, IList<string> expectedResult) {
            IList<string> result = GetLowerCasePartsFromMixedCase(mixedCaseString);
            if (!ExecutionsTest.Equal(result, expectedResult)) {
                Console.WriteLine("GetLowerCasePartsFromMixedCase failed on \"{0}\". Result: \"{1}\"", mixedCaseString, String.Join(", ", result));
            }
        }
    }
}
