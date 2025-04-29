using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class DigitRecognizer : CharacterRangeRecognizer {
        public DigitRecognizer() : this(null) { }

        public DigitRecognizer(String name) : base(name, '0', '9') { }

        [DiagnosticOnly]
        [Test]
        public static new void Test() {
            DigitRecognizer recognizer = new DigitRecognizer();
            recognizer.TestTryRead(
                "a",
                new object[] {
                    "0", 0,
                    "9", 9
                });
        }

        protected override object Data(char ch) {
            return (int)(ch - '0');
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append("\\d");
        }
    }
}
