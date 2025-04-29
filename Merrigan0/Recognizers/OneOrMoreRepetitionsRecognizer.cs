using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class OneOrMoreRepetitionsRecognizer : RepetitionsRecognizer {
        public OneOrMoreRepetitionsRecognizer(Recognizer child) : this(null, child, null) { }
        public OneOrMoreRepetitionsRecognizer(String name, Recognizer child) : this(name, child, null) { }

        public OneOrMoreRepetitionsRecognizer(String name, Recognizer child, Array<Recognizer> localRecognizersByName) : 
            base(name, child, localRecognizersByName) { 
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            OneOrMoreRepetitionsRecognizer recognizer = new OneOrMoreRepetitionsRecognizer(new SpecificStringRecognizer("abc"));
            Testing.Test(recognizer.Regex, () => {
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize("ab", false);
                recognizer.TestTryRead("ab", false);
                recognizer.TestTryRecognize("abc", true);
                recognizer.TestTryRead("abc", true);
                recognizer.TestTryRecognize("abca", false);
                recognizer.TestTryRead("abca", true);
                recognizer.TestTryRecognize("abcabc", true);
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

        protected override bool RepetitionsOkay(long nRepetitions) { return nRepetitions >= 1L; }

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
