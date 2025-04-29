using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A recognizer for the end of a strig.")]
    [Untested]
    public class EndRecognizer : Recognizer {
        public static readonly EndRecognizer Only = new EndRecognizer();

        public EndRecognizer() : base("end") { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test("abc", () => {
                EndRecognizer recognizer = new EndRecognizer();
                String s = "abc";
                long i = 0L;
                Testing.Test("0", !recognizer.TryRead(s, ref i));
                i = 3L;
                Testing.Test("3", recognizer.TryRead(s, ref i));
            });
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append("$");
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            if (i >= s.Length) {
                tree = new RecognitionTree(this, s, i, 0);
                return true;
            }
            tree = null;
            return false;
        }
    }
}
