using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class RepetitionsRangeRecognizer : RepetitionsRecognizer {
        long? maxRepetitions;
        long? minRepetitions;

        public RepetitionsRangeRecognizer(Recognizer child, long? minRepetitions, long? maxRepetitions) :
            this(null, child, minRepetitions, maxRepetitions, null) {
        }

        public RepetitionsRangeRecognizer(String name, Recognizer child, long? minRepetitions, long? maxRepetitions, Array<Recognizer> localRecognizersByName)
            : base(name, child, localRecognizersByName) {
            this.maxRepetitions = maxRepetitions;
            this.minRepetitions = minRepetitions;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test("(abc, null, null)", () => {
                RepetitionsRangeRecognizer recognizer = new RepetitionsRangeRecognizer(new SpecificStringRecognizer("abc"), null, null);
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
            });

            Testing.Test("(abc, 1, null)", () => {
                RepetitionsRangeRecognizer recognizer = new RepetitionsRangeRecognizer(new SpecificStringRecognizer("abc"), 1, null);
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

            Testing.Test("(abc, 1, 2)", () => {
                RepetitionsRangeRecognizer recognizer = new RepetitionsRangeRecognizer(new SpecificStringRecognizer("abc"), 1, 2);
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize("ab", false);
                recognizer.TestTryRead("ab", false);
                recognizer.TestTryRecognize("abc", true);
                recognizer.TestTryRead("abc", true);
                recognizer.TestTryRecognize("abca", false);
                recognizer.TestTryRead("abca", true);
                recognizer.TestTryRecognize("abcabcabc", false);
                recognizer.TestTryRead("abcabcabc", true);
                recognizer.TestTryRecognize("", false);
                recognizer.TestTryRead("", false);
            });
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            child.AppendToRegex(regexSoFar);
            regexSoFar.Append('{');
            if (minRepetitions.HasValue) {
                regexSoFar.Append(minRepetitions.Value);
            } else {
                regexSoFar.Append('0');
            }
            if (maxRepetitions.HasValue) {
                regexSoFar.Append(',');
                regexSoFar.Append(maxRepetitions.Value);
            }
            regexSoFar.Append('}');
        }

        protected override bool RepetitionsOkay(long nRepetitions) {
            if (minRepetitions.HasValue && nRepetitions < minRepetitions.Value) { return false; }
            if (maxRepetitions.HasValue && nRepetitions > maxRepetitions.Value) { return false; }
            return true;
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new RepetitionsRangeRecognizerExecution<RepetitionsRangeRecognizer>(this, s, i);
        ////}

        ////protected class RepetitionsRangeRecognizerExecution<T> : RepetitionsRecognizerExecution<T> where T : RepetitionsRangeRecognizer {
        ////    public RepetitionsRangeRecognizerExecution(T repetitionsRangeRecognizer, String s, long i)
        ////        : base(repetitionsRangeRecognizer, s, i) {
        ////    }

        ////    // Whether the seek for further repetitions must stop
        ////    protected override bool MustStop(long nRepetitions) {
        ////        return TypedRecognizer.maxRepetitions.HasValue && (nRepetitions >= TypedRecognizer.maxRepetitions.Value);
        ////    }

        ////    // Whether the given number of repetitions is acceptable
        ////    protected override bool RepetitionsOkay(long nRepetitions) {
        ////        return
        ////            (TypedRecognizer.minRepetitions.HasValue && (nRepetitions >= TypedRecognizer.minRepetitions.Value)) &&
        ////            (TypedRecognizer.maxRepetitions.HasValue && (nRepetitions <= TypedRecognizer.maxRepetitions.Value));
        ////    }
        ////}
    }
}
