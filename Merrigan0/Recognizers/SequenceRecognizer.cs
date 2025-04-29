using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class SequenceRecognizer : Recognizer {
        protected Array<Recognizer> children;

        public SequenceRecognizer(params Recognizer[] children) : this(null, children, null) { }
        public SequenceRecognizer(String name, params Recognizer[] children) : this(name, children, null) { }

        public SequenceRecognizer(String name, Array<Recognizer> children, Array<Recognizer> localRecognizers) :
            this(name, children, localRecognizers, null)
        {
        }

        public SequenceRecognizer(String name, Array<Recognizer> children, Array<Recognizer> localRecognizers, Func<RecognitionTree, object> getDataFunction) :
            base(name, localRecognizers, getDataFunction)
        {
            this.children = children;
            //// children.Each(c => c.SetParent(this));
            //// RegisterNamedRecognizers(children); /////
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            SequenceRecognizer recognizer = new SequenceRecognizer(
                new AnyRepetitionsRecognizer(new SpecificStringRecognizer("ab")),
                new SpecificCharacterRecognizer('a'),
                new SpecificStringRecognizer("abac"));
            recognizer.TestTryRecognize("a", false);
            recognizer.TestTryRead("a", false);
            recognizer.TestTryRecognize("ab", false);
            recognizer.TestTryRead("ab", false);
            recognizer.TestTryRecognize("aabac", true);
            recognizer.TestTryRead("aabac", true);
            recognizer.TestTryRecognize("aabacd", false);
            recognizer.TestTryRead("aabacd", true);
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('(');
            long length = children.Length;
            for (long i = 0; i < length; ++i) {
                children[i].AppendToRegex(regexSoFar);
            }
            regexSoFar.Append(')');
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long iToTry = i;
            MutableArray<RecognitionTree> childTreesSoFar = new MutableArray<RecognitionTree>();
            foreach (Recognizer child in children) {
                RecognitionTree childTree;
                if (!child.TryRead(s, ref iToTry, out childTree)) {
                    tree = null;
                    return false;
                }
                childTreesSoFar.Append(childTree);
            }
            tree = new RecognitionTree(this, s, i, iToTry - i, childTreesSoFar.Current);
            i = iToTry;
            return true;
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new SequenceRecognizerExecution(this, s, i);
        ////}

        ////protected class SequenceRecognizerExecution : RecognizerExecution<SequenceRecognizer> {
        ////    //private RecognizerExecution currentChild;
        ////    //private long iChild;
        ////    //long lengthSoFar;

        ////    public SequenceRecognizerExecution(SequenceRecognizer sequenceRecognizer, String s, long i)
        ////        : base(sequenceRecognizer, s, i) {
        ////        //if (!TryAdvanceChild()) {
        ////        //    Recognized = true;
        ////        //    Finished = true;
        ////        //}
        ////    }

        ////    public override void Run() {
        ////        long lengthSoFar = 0;
        ////        for (long iChild = 0; iChild < TypedRecognizer.children.Length; ++iChild) {
        ////            RecognizerExecution child = TypedRecognizer.children[iChild].BeginExecution(baseString, Index + lengthSoFar);
        ////            child.Run();
        ////            if (child.Rejected) {
        ////                Rejected = true;
        ////                return;
        ////            }
        ////            lengthSoFar += child.Length;
        ////        }
        ////        Recognized = true;
        ////        Length = lengthSoFar;
        ////    }
        ////}
    }
}

//// Considers the character. After this method, all statuses should be up to date:
//// Recognized, Rejected, Finished.
//public override void ConsiderNextCharacter(char ch) {
//    if (Finished) { return; }

//    currentChild.ConsiderNextCharacter(ch);

//    // If the child is still in progress, nothing to do
//    if (!currentChild.Finished) {
//        return;
//    }

//    // If the child was rejected, the whole sequence is done
//    if (currentChild.Rejected) {
//        Rejected = true;
//        Finished = true;
//        return;
//    }

//    // The child was recognized so count its length
//    Length += currentChild.Length;

//    // Move on to the next child if there is one
//    if (TryAdvanceChild()) {
//        return;
//    }

//    // If no more children are here, must have been recognized or we would have rejected before
//    Recognized = true;
//    Finished = true;
//}

//// Change any status that might need changing when exact character count is needed. Implementors
//// must propagate this call to its children if any
//public override void ReportOutOfCharacters() {
//    // Shore up the current child and any remaining children to see if they are recognized even with
//    // zero characters
//    while (true) {
//        if (!currentChild.Finished) {
//            currentChild.ReportOutOfCharacters();

//            // If the child was rejected, the whole sequence is done
//            if (currentChild.Rejected) {
//                Rejected = true;
//                Finished = true;
//                return;
//            }

//            // The child was recognized so count its length
//            Length += currentChild.Length;
//        }

//        if (!TryAdvanceChild()) {
//            break;
//        }
//    }

//    Recognized = true;

//    base.ReportOutOfCharacters();
//}

//protected bool TryAdvanceChild() {
//    // Increment but not if we haven't started yet
//    if (currentChild != null) {
//        ++iChild;
//    }

//    // Fail if there are no children left
//    if (iChild >= TypedRecognizer.children.Length) {
//        return false;
//    }

//    // Arrange the new child
//    Recognizer currentChildRecognizer = TypedRecognizer.children[iChild];
//    currentChild = currentChildRecognizer.NewRecognizerExecution(s, Index);
//    return true;
//}
