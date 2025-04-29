using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class CustomCharacterClassGroupRecognizer : CharacterClassGroupRecognizer {
        private Func<char, bool> inClassFunction;

        [MayBeNull(When = "there is no appropriate standard regular expression for the character class")]
        private String regex;

        //[Equals("regex", null)]
        //[Equals("name", null)]
        public CustomCharacterClassGroupRecognizer(Func<char, bool> inClassFunction) : this(null, inClassFunction, null) { }

        //[Equals("regex", null)]
        public CustomCharacterClassGroupRecognizer(String name, Func<char, bool> inClassFunction) : this(name, inClassFunction, null) { }

        public CustomCharacterClassGroupRecognizer(String name, Func<char, bool> inClassFunction, String regex) : base(name) {
            this.inClassFunction = inClassFunction;
            this.regex = regex;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Recognizer recognizer = new CustomCharacterClassGroupRecognizer("Char.IsLower", Char.IsLower);
            Testing.Test(recognizer.Name, () => {
                recognizer.TestTryRead("", false);
                recognizer.TestTryRead("a", true);
                recognizer.TestTryRecognize("a", true);
                recognizer.TestTryRead("A", false);
                recognizer.TestTryRead("ab", true);
                recognizer.TestTryRecognize("ab", true);
                recognizer.TestTryRead("aA", true);
                recognizer.TestTryRead("Aa", false);
                recognizer.TestTryRead(" ab", false);
                recognizer.TestTryRead("ab ", true);
            });
        }

        public override void AppendToRegex([Mutable]/* [Greater("Before Length", "After Length")] */ MutableString regexSoFar) {
            String regexToAppend;
            if (regex == null) {
                if (Name == null) {
                    regexToAppend = "[custom]*";
                } else {
                    regexToAppend = "<" + Name + ">";
                }
            } else {
                regexToAppend = regex;
            }
            regexSoFar.Append(regexToAppend);
        }

        public override bool InClass(char ch) { return inClassFunction(ch); }
    }
}
