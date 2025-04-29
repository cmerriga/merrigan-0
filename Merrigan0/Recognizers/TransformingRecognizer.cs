using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A recognizer that transforms the data from another, to something different.")]
    [Untested]
    public class TransformingRecognizer : Recognizer {
        private Recognizer baseRecognizer;
        private Func<object, object> transformFunction;

        public TransformingRecognizer(Recognizer baseRecognizer, Func<object, object> transformFunction) : this(null, baseRecognizer, transformFunction) { }

        public TransformingRecognizer(String name, Recognizer baseRecognizer, Func<object, object> transformFunction) :
            base(name, null) 
        {
            this.baseRecognizer = baseRecognizer;
            this.transformFunction = transformFunction;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            TransformingRecognizer recognizer = new TransformingRecognizer(new SpecificStringRecognizer("abc"), o => ((String)o).Length);
            recognizer.TestTryRecognize("a", false);
            recognizer.TestTryRead("a", false);
            recognizer.TestTryRecognize("abc", true);
            recognizer.TestTryRead("abc", true);
            recognizer.TestTryRecognize("abcd", false);
            recognizer.TestTryRead("abcd", true);
            Testing.Test("transforms correctly", () => {
                RecognitionTree tree;
                long i = 0;
                if (!recognizer.TryRead("abc", ref i, out tree)) {
                    return false;
                }
                return (long)tree.Data == 3;
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new TransformingRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            baseRecognizer.AppendToRegex(regexSoFar);
        }

        public override object GetData(RecognitionTree tree) { return transformFunction(tree.Children[0].Data); }

        public override string ToString() { return baseRecognizer.ToString(); }

        public override bool TryRead(String s, ref long i, [Note("Child is the output of the base recognizer.")] out RecognitionTree tree) {
            long iToTry = i;
            RecognitionTree baseTree;
            bool succeeded = baseRecognizer.TryRead(s, ref iToTry, out baseTree);
            if (succeeded) {
                tree = new RecognitionTree(this, s, i, iToTry - i, baseTree);
                i = iToTry;
                return true;
            }
            tree = null;
            return false;
        }

        ////protected class TransformingRecognizerExecution : RecognizerExecution<TransformingRecognizer> {
        ////    private RecognizerExecution child;

        ////    public TransformingRecognizerExecution(TransformingRecognizer transformingRecognizer, String s, long i)
        ////        : base(transformingRecognizer, s, i) {
        ////            child = transformingRecognizer.baseRecognizer.BeginExecution(s, i);
        ////        Recognized = true;
        ////    }

        ////    public override void Run() {
        ////        child.Run();
        ////        if (child.Recognized) {
        ////            Recognized = true;
        ////            Length = child.Length;
        ////            return;
        ////        }
        ////        Recognized = false;
        ////        Length = 0;
        ////    }
        ////}
    }
}
