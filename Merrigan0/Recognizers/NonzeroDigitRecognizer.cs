using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class NonzeroDigitRecognizer : CharacterRangeRecognizer {
        public NonzeroDigitRecognizer() : this(null) { }

        public NonzeroDigitRecognizer(String name) : base(name, '1', '9') { }

        [DiagnosticOnly]
        [Test]
        public static new void Test() {
            NonzeroDigitRecognizer recognizer = new NonzeroDigitRecognizer();
            recognizer.TestTryRead(
                "0",
                new object[] {
                    "1", 1,
                    "9", 9
                });
        }

        protected override object Data(char ch) {
            return (int)(ch - '0');
        }
    }
}
