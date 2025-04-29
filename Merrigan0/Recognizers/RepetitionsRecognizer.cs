using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public abstract class RepetitionsRecognizer : Recognizer {
        protected Recognizer child;

        public RepetitionsRecognizer(String name, Recognizer child, Array<Recognizer> localRecognizersByName)
            : base(name) {
            this.child = child;
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long nRepetitions = 0;
            long lastOkayRepetitions = -1;
            long lastOkayIndex = -1;
            long iToTry = i;
            MutableArray<RecognitionTree> childTreesSoFar = new MutableArray<RecognitionTree>();
            while (true) {
                if (RepetitionsOkay(nRepetitions)) {
                    lastOkayRepetitions = nRepetitions;
                    lastOkayIndex = iToTry;
                }
                RecognitionTree childTree;
                if (!child.TryRead(s, ref iToTry, out childTree)) {
                    break;
                }
                ++nRepetitions;
            }

            if (lastOkayRepetitions >= 0) {
                tree = new RecognitionTree(this, s, i, lastOkayIndex - i, childTreesSoFar.Current);
                i = lastOkayIndex;
                return true;
            }

            tree = null;
            return false;
        }

        // Whether the given number of repetitions is acceptable
        protected abstract bool RepetitionsOkay(long nRepetitions);

        ////protected abstract class RepetitionsRecognizerExecution<T> : RecognizerExecution<T> where T : RepetitionsRecognizer {
        ////    //long nCharactersSoFar;
        ////    protected RecognizerExecution child;

        ////    public RepetitionsRecognizerExecution(T anyRepetitionsRecognizer, String s, long i)
        ////        : base(anyRepetitionsRecognizer, s, i) {
        ////        child = anyRepetitionsRecognizer.child.BeginExecution(s, i);
        ////        //Recognized = true;
        ////    }

        ////    // Whether the seek for further repetitions must stop
        ////    protected virtual bool MustStop(long nRepetitions) {
        ////        return false;
        ////    }

        ////    // Whether the given number of repetitions is acceptable
        ////    protected abstract bool RepetitionsOkay(long nRepetitions);

        ////    public override void Run() {
        ////        long nRepetitions = 0;
        ////        long lengthSoFar = 0;
        ////        long lastRecognizedLength = 0;
        ////        while (true) {
        ////            child.Run();

        ////            // Handle a rejection. This will stop reading
        ////            if (child.Rejected) {
        ////                break;
        ////                //// No problem, we've satisfied the requirements
        ////                //if (RepetitionsOkay(nRepetitions)) {
        ////                //    break;
        ////                //}

        ////                //// This was not an acceptable number of repetitions. If we had previously recognized it,
        ////                //// leave it at that
        ////                //if (Recognized)
        ////                //Rejected = true;
        ////                //return;
        ////            }

        ////            // Successful recognition. If we've satisfied the requirement for repetitions on this, mark it
        ////            // recognized
        ////            lengthSoFar += child.Length;
        ////            ++nRepetitions;

        ////            if (RepetitionsOkay(nRepetitions)) {
        ////                Recognized = true;
        ////                lastRecognizedLength = lengthSoFar;
        ////            }

        ////            if (MustStop(nRepetitions)) {
        ////                break;
        ////            }

        ////            child = TypedRecognizer.child.BeginExecution(baseString, Index + lengthSoFar);
        ////        }

        ////        if (RepetitionsOkay(nRepetitions)) {
        ////            Recognized = true;
        ////            Length = lastRecognizedLength;
        ////        } else {
        ////            Rejected = true;
        ////        }
        ////    }
        ////}
    }
}
