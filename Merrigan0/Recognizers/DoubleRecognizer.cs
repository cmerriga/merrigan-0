using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A recognizer that tries to read a floating-point number from text, per the rules of IEEE-754.")]
    [Concept("IEEE-754", "https://en.wikipedia.org/wiki/IEEE_754")]
    [Untested]
    public class DoubleRecognizer : Recognizer {
        public static readonly DoubleRecognizer Only = new DoubleRecognizer();

        protected DoubleRecognizer() : base("double") { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            DoubleRecognizer recognizer = new DoubleRecognizer();
            recognizer.TestTryRead(
                "a",
                new object[] {
                    "0", 0.0,
                    "1", 1.0,
                    "9.1", 9.1
                });
        }

        ////public override object GetData(RecognitionTree tree) { return Double.Parse(tree.String); }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long iToTry = i;
            double r;
            if (!String.TryReadNumber(s, ref iToTry, out r)) {
                tree = null;
                return false;
            }
            tree = new RecognitionTree(this, s, i, iToTry - i, r);
            i = iToTry;
            return true;
        }
    }
}
