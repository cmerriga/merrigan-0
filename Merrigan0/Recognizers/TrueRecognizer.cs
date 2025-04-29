using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A recognizer that always recognizes the entire string.")]
    [Untested]
    public class TrueRecognizer : Recognizer {
        public TrueRecognizer() : this(null) { }

        public TrueRecognizer(String name) : base(name) { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            TrueRecognizer recognizer = new TrueRecognizer();
            recognizer.TestTryRecognize(String.Empty, true);
            recognizer.TestTryRead(String.Empty, true);
            recognizer.TestTryRecognize("abc", true);
            recognizer.TestTryRead("abc", true);
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            tree = new RecognitionTree(this, s, i, s.Length);
            i += s.Length;
            return true;
        }
    }
}
