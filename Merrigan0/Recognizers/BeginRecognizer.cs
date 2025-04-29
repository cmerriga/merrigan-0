using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A recognizer for the beginning of a strig.")]
    [Untested]
    public class BeginRecognizer : Recognizer {
        public static readonly BeginRecognizer Only = new BeginRecognizer();

        public BeginRecognizer() : base("begin") { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test("abc", () => {
                BeginRecognizer recognizer = new BeginRecognizer();
                String s = "abc";
                long i = 0L;
                Testing.Test("0", recognizer.TryRead(s, ref i));
                i = 1L;
                Testing.Test("1", !recognizer.TryRead(s, ref i));
            });
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append("^");
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            if (i == 0) {
                tree = new RecognitionTree(this, s, i, 0);
                return true;
            }
            tree = null;
            return false;
        }
    }
}
