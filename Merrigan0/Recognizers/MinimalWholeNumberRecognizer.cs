using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class MinimalWholeNumberRecognizer : Recognizer {
        public MinimalWholeNumberRecognizer() : this("minimal-whole-number") { }

        public MinimalWholeNumberRecognizer(String name) : base(name) { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            MinimalWholeNumberRecognizer recognizer = new MinimalWholeNumberRecognizer();
            recognizer.TestTryRead(
                "a",
                new object[] {
                    "0", 0,
                    "910", 910
                });
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            if (i >= s.Length) { goto fail; }
            long iToTry = i;
            char ch = s[iToTry];
            long nSoFar;
            if (ch == '0') {
                nSoFar = 0L;
                ++iToTry;
            } else {
                // First digit must be 1-9
                if (ch < '1' || ch > '9') { goto fail; }
                nSoFar = (long)(ch - '0');
                while (true) {
                    ++iToTry;
                    if (iToTry >= s.Length) { break; }
                    ch = s[iToTry];
                    if (ch < '0' || ch > '9') { break; }
                    nSoFar = nSoFar * 10L + (long)(ch - '0');
                }
            }
            tree = new RecognitionTree(this, s, i, iToTry - i, nSoFar);
            i = iToTry;
            return true;

        fail:
            tree = null;
            return false;
        }
    }
}
