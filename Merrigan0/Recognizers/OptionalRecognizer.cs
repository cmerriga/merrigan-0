using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class OptionalRecognizer : Recognizer {
        private Recognizer child;

        public OptionalRecognizer(Recognizer child) : this(null, child, null) { }
        public OptionalRecognizer(String name, Recognizer child) : this(name, child, null) { }

        public OptionalRecognizer(String name, Recognizer child, Array<Recognizer> localRecognizersByName)
            : base(name, localRecognizersByName) {
            this.child = child;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            OptionalRecognizer recognizer = new OptionalRecognizer(new OneOrMoreRepetitionsRecognizer(new SpecificCharacterRecognizer('a')));
            Testing.Test(recognizer.Regex, () => {
                recognizer.TestTryRecognize("a", true);
                recognizer.TestTryRead("a", true);
                recognizer.TestTryRecognize("ab", false);
                recognizer.TestTryRead("ab", true);
                recognizer.TestTryRecognize("d", false);
                recognizer.TestTryRead("d", true);
                recognizer.TestTryRecognize("", true);
                recognizer.TestTryRead("", true);
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new OptionalRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            child.AppendToRegex(regexSoFar);
            regexSoFar.Append('?');
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            RecognitionTree childTree;
            long iToTry = i;
            if (child.TryRead(s, ref iToTry, out childTree)) {
                tree = new RecognitionTree(this, s, i, iToTry - i, childTree);
            } else {
                tree = new RecognitionTree(this, s, i, iToTry - i);
            }
            i = iToTry;
            return true;
        }

        //protected class OptionalRecognizerExecution : RecognizerExecution<OptionalRecognizer> {
        //    private RecognizerExecution child;

        //    public OptionalRecognizerExecution(OptionalRecognizer optionalRecognizer, String s, long i)
        //        : base(optionalRecognizer, s, i) {
        //        child = optionalRecognizer.child.BeginExecution(s, i);
        //        Recognized = true;
        //    }

        //    ////public override void ReportOutOfCharacters() {
        //    ////    child.ReportOutOfCharacters();
        //    ////    base.ReportOutOfCharacters();
        //    ////}

        //    ////// Can't be rejected because this is optional.
        //    ////public override void ConsiderNextCharacter(char ch) {
        //    ////    if (Finished) { return; }
        //    ////    child.ConsiderNextCharacter(ch);
        //    ////    if (child.Rejected) {
        //    ////        Length = 0;
        //    ////        Finished = true;
        //    ////        return;
        //    ////    }
        //    ////    if (child.Finished && child.Recognized) {
        //    ////        Length = child.Length;
        //    ////        Finished = true;
        //    ////    }
        //    ////}

        //    public override void Run() {
        //        child.Run();
        //        if (child.Recognized) {
        //            Recognized = true;
        //            Length = child.Length;
        //            return;
        //        }
        //        Recognized = true;
        //        Length = 0;
        //    }
        //}
    }
}
