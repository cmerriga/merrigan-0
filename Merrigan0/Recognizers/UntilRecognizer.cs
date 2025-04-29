using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A way to collect all characters in a string until either the terminator is found, or the end of the string.")]
    [Untested]
    public class UntilRecognizer : Recognizer {
        private Recognizer terminatorRecognizer;

        public UntilRecognizer(Recognizer terminator) : this(null, terminator, null) { }
        public UntilRecognizer(String name, Recognizer terminator) : this(name, terminator, null) { }

        public UntilRecognizer(String name, Recognizer terminator, Array<Recognizer> localRecognizers)
            : base(name, localRecognizers) 
        {
            this.terminatorRecognizer = terminator;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            UntilRecognizer recognizer = new UntilRecognizer(new SpecificCharacterRecognizer('x'));
            RecognitionTree tree;
            long i;
            Testing.Test(String.Empty, () => {
                i = 0;
                recognizer.TryRead(String.Empty, ref i, out tree);
                return tree != null && tree.Length == 0;
            });
            Testing.Test("x", () => {
                i = 0;
                recognizer.TryRead("x", ref i, out tree);
                return tree != null && tree.Length == 0;
            });
            Testing.Test("abcx", () => {
                i = 0;
                recognizer.TryRead("abcx", ref i, out tree);
                return tree != null && tree.Length == 3;
            });
            Testing.Test("abc", () => {
                i = 0;
                recognizer.TryRead("abc", ref i, out tree);
                return tree != null && tree.Length == 3;
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new UntilRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append(".*");
            terminatorRecognizer.AppendToRegex(regexSoFar);
        }

        //[ImplementationNote("Does not include the terminator in the tree length but adds it as a child.")]
        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long iToTry = i;
            long length = s.Length;
            RecognitionTree childTree = null;
            while (iToTry < length) {
                long iTerminator = iToTry;
                if (terminatorRecognizer.TryRead(s, ref iTerminator, out childTree)) {
                    break;
                }
                ++iToTry;
            }
            tree = childTree == null ?
                new RecognitionTree(this, s, i, iToTry - i) :
                new RecognitionTree(this, s, i, iToTry - i, childTree);
            i = iToTry;
            return true;
        }

        ////protected class UntilRecognizerExecution : RecognizerExecution<UntilRecognizer> {
        ////    private Recognizer sentinelRecognizer;

        ////    public UntilRecognizerExecution(UntilRecognizer untilRecognizer, String s, long i)
        ////        : base(untilRecognizer, s, i) {
        ////        sentinelRecognizer = untilRecognizer.terminatorRecognizer;
        ////    }

        ////    public override void Run() {
        ////        long i = Index;
        ////        long length = baseString.Length;
        ////        while (i < length) {
        ////            RecognizerExecution child = sentinelRecognizer.BeginExecution(baseString, i);
        ////            child.Run();
        ////            if (child.Recognized) {
        ////                break;
        ////            }
        ////            ++i;
        ////        }
        ////        Recognized = true;
        ////        Length = i;
        ////    }
        ////}
    }
}
