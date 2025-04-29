using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A way to recognize the end of a line.")]
    [Untested]
    public class LineEndRecognizer : Recognizer {
        public static readonly LineEndRecognizer Only = new LineEndRecognizer();

        public LineEndRecognizer() : base("line-end") { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            LineEndRecognizer recognizer = new LineEndRecognizer();
            recognizer.TestTryRecognize("\n", true);
            recognizer.TestTryRead("\n", true);
            recognizer.TestTryRecognize("\r", true);
            recognizer.TestTryRead("\r", true);
            recognizer.TestTryRecognize("\r\n", true);
            recognizer.TestTryRead("\r\n", true);
            recognizer.TestTryRecognize("\na", false);
            recognizer.TestTryRead("\na", true);
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append("$"); //// ambiguous with EndRecognizer, depends on multi-line/m specification
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            // Could be \n or \r\n or just \r
            long length = s.Length;
            if (i >= length) { goto fail; }
            long iToTry = i;
            char ch = s[iToTry];
            if (ch == '\n') {
                ++iToTry;
            } else if (ch == '\r') {
                ++iToTry;
                if (iToTry < length && s[iToTry] == '\n') {
                    ++iToTry;
                }
            } else {
                goto fail;
            }
            tree = new RecognitionTree(this, s, i, iToTry - i);
            i = iToTry;
            return true;

        fail:
            tree = null;
            return false;
        }
    }
}
