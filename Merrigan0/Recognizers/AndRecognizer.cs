using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class AndRecognizer : Recognizer {
        private Array<Recognizer> children;

        public AndRecognizer(params Recognizer[] children) : this(null, children, null) { }
        public AndRecognizer(String name, params Recognizer[] children) : this(name, children, null) { }

        public AndRecognizer(String name, /* [MustNotBeEmpty] */Array<Recognizer> children, Array<Recognizer> localRecognizers) :
            base(name, localRecognizers) {
            this.children = children;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            AndRecognizer andRecognizer = new AndRecognizer(
                new SpecificCharacterRecognizer('a'),
                new SpecificStringRecognizer("a"));
            andRecognizer.TestTryRecognize("a", true);
            andRecognizer.TestTryRecognize("b", false);
            andRecognizer.TestTryRecognize("ab", false);
            andRecognizer.TestTryRead("ab", true);

            andRecognizer = new AndRecognizer(
                            new SpecificStringRecognizer("a"));
            andRecognizer.TestTryRecognize("a", true);
            andRecognizer.TestTryRecognize("b", false);
            andRecognizer.TestTryRecognize("ab", false);
            andRecognizer.TestTryRead("ab", true);
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('(');
            long length = children.Length;
            for (long i = 0; i < length; ++i) {
                if (i > 0) {
                    regexSoFar.Append('&');
                }
                children[i].AppendToRegex(regexSoFar);
            }
            regexSoFar.Append(')');
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new AndRecognizerExecution(this, s, i);
        ////}

        //// needs to allow for multiple possible lengths
        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long iChild = i;
            bool requiredLengthFound = false;
            long requiredLength = -1;
            MutableArray<RecognitionTree> childTreesSoFar = new MutableArray<RecognitionTree>();
            foreach (Recognizer child in children) {
                iChild = i;
                RecognitionTree childTree;
                if (!child.TryRead(s, ref iChild, out childTree)) {
                    tree = null;
                    return false;
                }
                long childLength = iChild - i;
                if (!requiredLengthFound) {
                    requiredLength = childLength;
                    requiredLengthFound = true;
                } else if (childLength != requiredLength) {
                    tree = null;
                    return false;
                }
                childTreesSoFar.Append(childTree);
            }
            tree = new RecognitionTree(this, s, i, iChild - i, childTreesSoFar.Current);
            i = iChild;
            return true;
        }

        ////protected class AndRecognizerExecution : RecognizerExecution<AndRecognizer> {
        ////    private Array<RecognizerExecution> children;
        ////    //private RecognizerExecution mostRecentChildRecognized;

        ////    public RecognizerExecution ChosenExecution { get; private set; }

        ////    public AndRecognizerExecution(AndRecognizer andRecognizer, String s, long i)
        ////        : base(andRecognizer, s, i) {
        ////        children = andRecognizer.children.Transform(r => r.BeginExecution(s, i));
        ////    }

        ////    public override void Run() {
        ////        // Recognize only if all components are exactly the same length
        ////        //// possibly multiply recognized ones could be repolled to try to get to the same length?
        ////        long length = -1;
        ////        foreach (RecognizerExecution child in children) {
        ////            child.Run();
        ////            if (!child.Recognized) {
        ////                Rejected = true;
        ////                return;
        ////            }

        ////            if (length == -1) {
        ////                length = child.Length;
        ////            } else if (child.Length != length) {
        ////                Rejected = true;
        ////                return;
        ////            }
        ////        }
        ////        Recognized = true;
        ////        Length = length;
        ////    }
        ////}
    }
}
