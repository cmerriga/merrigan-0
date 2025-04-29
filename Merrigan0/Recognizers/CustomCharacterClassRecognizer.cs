using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A way to recognize a single character as in or out of a class like letters or digits.")]
    [Untested]
    public class CustomCharacterClassRecognizer : CharacterRecognizer {
        private Func<char, bool> inClassFunction;
        private String regex;

        public CustomCharacterClassRecognizer(Func<char, bool> inClassFunction) : this(null, inClassFunction, null) { }

        public CustomCharacterClassRecognizer(String name, Func<char, bool> inClassFunction) : this(name, inClassFunction, null) { }

        public CustomCharacterClassRecognizer(String name, Func<char, bool> inClassFunction, String regex) : base(name, null) {
            this.inClassFunction = inClassFunction;
            this.regex = regex;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            CustomCharacterClassRecognizer recognizer = new CustomCharacterClassRecognizer("Char.IsLower", Char.IsLower);
            Testing.Test(recognizer.Name, () => {
                recognizer.TestTryRecognize("", false);
                recognizer.TestTryRead("", false);
                recognizer.TestTryRecognize("a", true);
                recognizer.TestTryRead("a", true);
                recognizer.TestTryRecognize("A", false);
                recognizer.TestTryRead("A", false);
                recognizer.TestTryRecognize("ab", false);
                recognizer.TestTryRead("ab", true);
            });
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            String regexToAppend;
            if (regex == null) {
                if (Name == null) {
                    regexToAppend = "[custom]";
                } else {
                    regexToAppend = Name;
                }
            } else {
                regexToAppend = regex;
            }
            regexSoFar.Append(regexToAppend);
        }

        protected override bool Recognized(char ch) { return inClassFunction(ch); }
    }
}
