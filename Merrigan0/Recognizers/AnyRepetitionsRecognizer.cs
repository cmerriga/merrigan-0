using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class AnyRepetitionsRecognizer : RepetitionsRecognizer {
        public AnyRepetitionsRecognizer(Recognizer child) : this(null, child, null) { }
        public AnyRepetitionsRecognizer(String name, Recognizer child) : this(name, child, null) { }

        public AnyRepetitionsRecognizer(String name, Recognizer child, Array<Recognizer> localRecognizersByName)
            : base(name, child, localRecognizersByName) { 
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            AnyRepetitionsRecognizer recognizer = new AnyRepetitionsRecognizer(new SpecificStringRecognizer("abc"));
            recognizer.TestTryRecognize("a", false);
            recognizer.TestTryRead("a", true);
            recognizer.TestTryRecognize("ab", false);
            recognizer.TestTryRead("ab", true);
            recognizer.TestTryRecognize("abc", true);
            recognizer.TestTryRead("abc", true);
            recognizer.TestTryRecognize("abca", false);
            recognizer.TestTryRead("abca", true);
            recognizer.TestTryRecognize("abcabc", true);
            recognizer.TestTryRead("abcabc", true);
            recognizer.TestTryRecognize("", true);
            recognizer.TestTryRead("", true);
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new AnyRepetitionsRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            child.AppendToRegex(regexSoFar);
            regexSoFar.Append('*');
        }

        protected override bool RepetitionsOkay(long nRepetitions) { return true; }

        ////protected class AnyRepetitionsRecognizerExecution : RepetitionsRecognizerExecution<AnyRepetitionsRecognizer> {
        ////    public AnyRepetitionsRecognizerExecution(AnyRepetitionsRecognizer anyRepetitionsRecognizer, String s, long i)
        ////        : base(anyRepetitionsRecognizer, s, i) {
        ////    }

        ////    // Whether the given number of repetitions is acceptable
        ////    protected override bool RepetitionsOkay([NotNegative] long nRepetitions) {
        ////        return true;
        ////    }
        ////}
    }
}
