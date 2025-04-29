using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class SpecificCharacterRecognizer : CharacterRecognizer {
        private char ch;

        public SpecificCharacterRecognizer(char ch) : this(ch, null) { }

        public SpecificCharacterRecognizer(String name, char ch) : this(name, ch, null) { }

        public SpecificCharacterRecognizer(char ch, Func<RecognitionTree, object> getDataFunction) :
            this(ch, ch, getDataFunction) {
        }

        public SpecificCharacterRecognizer(String name, char ch, Func<RecognitionTree, object> getDataFunction)
            : base(name, getDataFunction) {
            this.ch = ch;
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

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append(TextUtilities.RegexEscape(ch));
        }

        protected override bool Recognized(char ch) {
            return ch == this.ch;
        }
    }
}
