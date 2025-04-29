using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A way to recognize a range of consecutive characters, like a-z.")]
    [Untested]
    public class CharacterRangeRecognizer : CharacterRecognizer {
        protected char chLow;
        protected char chHigh;

        public CharacterRangeRecognizer(char chLow, char chHigh) :
            this(chLow + "-" + chHigh, chLow, chHigh)
        {
        }

        public CharacterRangeRecognizer(String name, char chLow, char chHigh)
            : this(name, chLow, chHigh, null) {
        }

        public CharacterRangeRecognizer(String name, char chLow, char chHigh, Func<RecognitionTree, object> getDataFunction)
            : base(name, getDataFunction) {
            this.chLow = chLow;
            this.chHigh = chHigh;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            CharacterRangeRecognizer recognizer = new CharacterRangeRecognizer('a', 'z');
            Testing.Test(recognizer.Name, () => {
                recognizer.TestTryRead(
                    "1",
                    new object[] {
                    "a", 'a',
                    "z", 'z'
                });
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new CharacterRangeRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('[');
            regexSoFar.Append(TextUtilities.RegexEscape(chLow));
            regexSoFar.Append('-');
            regexSoFar.Append(TextUtilities.RegexEscape(chHigh));
            regexSoFar.Append(']');
        }

        protected override bool Recognized(char ch) {
            return (ch >= chLow && ch <= chHigh);
        }

        ////protected class CharacterRangeRecognizerExecution : RecognizerExecution<CharacterRangeRecognizer> {
        ////    public CharacterRangeRecognizerExecution(CharacterRangeRecognizer characterRangeRecognizer, String s, long i)
        ////        : base(characterRangeRecognizer, s, i) {
        ////    }

        ////    //public override void ConsiderNextCharacter(char ch) {
        ////    //    if (Finished) { return; }
        ////    //    if (ch >= TypedRecognizer.chLow && ch <= TypedRecognizer.chHigh) {
        ////    //        Recognized = true;
        ////    //        Length = 1;
        ////    //    } else {
        ////    //        Rejected = true;
        ////    //    }
        ////    //    Finished = true;
        ////    //}

        ////    public override void Run() {
        ////        if (Index >= baseString.Length) {
        ////            Rejected = true;
        ////            return;
        ////        }
        ////        char ch = baseString[Index];
        ////        if (ch >= TypedRecognizer.chLow && ch <= TypedRecognizer.chHigh) {
        ////            Recognized = true;
        ////            Length = 1;
        ////            return;
        ////        }
        ////        Rejected = true;
        ////    }
        ////}
    }
}
