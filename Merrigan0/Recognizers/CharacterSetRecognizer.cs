using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class CharacterSetRecognizer : CharacterRecognizer {
        private Set<char> characters;

        public CharacterSetRecognizer(Set<char> characters) :
            this(null, characters)
        {
        }

        public CharacterSetRecognizer(String name, params char[] characters) :
            this(name, Set<char>.From(characters)) 
        { 
        }

        public CharacterSetRecognizer(String name, Set<char> characters) :
            base(name, null)
        {
            this.characters = characters;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            CharacterSetRecognizer recognizer = new CharacterSetRecognizer("{a, b, c}", 'a', 'b', 'c');
            Testing.Test(recognizer.Name, () => {
                recognizer.TestTryRead(
                    "1",
                    new object[] {
                    "a", 'a',
                    "c", 'c'
                });
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new CharacterSetRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('[');
            foreach (char ch in characters) {
                regexSoFar.Append(ch);
            }
            regexSoFar.Append(']');
        }

        protected override bool Recognized(char ch) {
            return characters.Contains(ch);
        }

        ////protected class CharacterSetRecognizerExecution : RecognizerExecution<CharacterSetRecognizer> {
        ////    public CharacterSetRecognizerExecution(CharacterSetRecognizer characterSetRecognizer, String s, long i)
        ////        : base(characterSetRecognizer, s, i) {
        ////    }

        ////    public override void Run() {
        ////        if (Index >= baseString.Length) {
        ////            Rejected = true;
        ////            return;
        ////        }
        ////        char ch = baseString[Index];
        ////        if (TypedRecognizer.characters.Contains(ch)) {
        ////            Recognized = true;
        ////            Length = 1;
        ////            return;
        ////        }
        ////        Rejected = true;
        ////    }
        ////}
    }
}
