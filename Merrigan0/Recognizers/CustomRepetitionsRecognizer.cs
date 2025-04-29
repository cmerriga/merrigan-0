using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class CustomRepetitionsRecognizer : RepetitionsRecognizer {
        private Func<long, bool> repetitionsOkayFunction;

        public CustomRepetitionsRecognizer(Recognizer child, Func<long, bool> repetitionsOkayFunction) : this(null, child, repetitionsOkayFunction, null) { }
        public CustomRepetitionsRecognizer(String name, Recognizer child, Func<long, bool> repetitionsOkayFunction) : this(name, child, repetitionsOkayFunction, null) { }

        public CustomRepetitionsRecognizer(String name, Recognizer child, Func<long, bool> repetitionsOkayFunction, Array<Recognizer> localRecognizersByName) : 
            base(name, child, localRecognizersByName) 
        {
            this.repetitionsOkayFunction = repetitionsOkayFunction;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            CustomRepetitionsRecognizer recognizer = new CustomRepetitionsRecognizer(new SpecificStringRecognizer("abc"), n => n == 1);
            Testing.Test(recognizer.DebuggerDisplay, () => {
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize("ab", false);
                recognizer.TestTryRead("ab", false);
                recognizer.TestTryRecognize("abc", true);
                recognizer.TestTryRead("abc", true);
                recognizer.TestTryRecognize("abca", false);
                recognizer.TestTryRead("abca", true);
                recognizer.TestTryRecognize("abcabc", false);
                recognizer.TestTryRead("abcabc", true);
                recognizer.TestTryRecognize("", false);
                recognizer.TestTryRead("", false);
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new OneOrMoreRepetitionsRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            child.AppendToRegex(regexSoFar);
            regexSoFar.Append('+');
        }

        protected override bool RepetitionsOkay(long nRepetitions) { return repetitionsOkayFunction(nRepetitions); }

        ////protected class OneOrMoreRepetitionsRecognizerExecution : RepetitionsRecognizerExecution<OneOrMoreRepetitionsRecognizer> {
        ////    public OneOrMoreRepetitionsRecognizerExecution(OneOrMoreRepetitionsRecognizer oneOrMoreRepetitionsRecognizer, String s, long i)
        ////        : base(oneOrMoreRepetitionsRecognizer, s, i) {
        ////    }

        ////    // Whether the given number of repetitions is acceptable
        ////    protected override bool RepetitionsOkay([NotNegative] long nRepetitions) {
        ////        return nRepetitions >= 1;
        ////    }
        ////}
    }
}
