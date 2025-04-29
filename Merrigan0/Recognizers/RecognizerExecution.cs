using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.Internal.Text {
//    [DebuggerDisplay("{DebuggerDisplay}")]
//    [Untested]
//    public abstract class RecognizerExecution : Execution.Execution /*: RecognitionTree*/ {
//        protected MutableArray<long> recognitionLengthsSoFar;

//        [DiagnosticOnly]
//        public string DebuggerDisplay {
//            get {
//                return Recognizer.ToString() + " " +
//                    (Recognized ? (String)"rec " : String.Empty) +
//                    (Rejected ? (String)"rej " : String.Empty) +
//                    (Finished ? (String)"fin" : String.Empty);
//            }
//        }

//        public bool Finished { get; protected set; }

//        [WhatItIs("The lengths after the begin index of all the possible recognitions. For instance, a * or + operator may offer many possibilities.")]
//        public virtual Array<long> RecognitionLengths { get { return recognitionLengthsSoFar == null ? Array<long>.Empty : recognitionLengthsSoFar.Current; } }

//        public RecognitionTree RecognitionTree { get; protected set; }
//        public bool Recognized { get; protected set; }

//        protected long BeginIndex { get; protected set; }
//        protected long ChunkLength { get; protected set; }
//        protected long Index { get; protected set; }
//        protected Recognizer Recognizer { get; protected set; }
//        protected bool Rejected { get; protected set; }
//        protected String String { get; protected set; }

//        public RecognizerExecution(Recognizer recognizer, String s, long i, long chunkLength) {
//            BeginIndex = i;
//            ChunkLength = chunkLength;
//            Index = i;
//            Recognizer = recognizer;
//            String = s;
//        }

//        //// Considers the character. After this method, all statuses should be up to date:
//        //// Recognized, Rejected, Finished.
//        //public abstract void ConsiderNextCharacter(char ch);

//        //// Consider the characters one block at a time
//        //public virtual void ConsiderNextCharacters(Array<char> a) {
//        //    long length = a.Length;
//        //    for (long i = 0; i < length; ++i) {
//        //        ConsiderNextCharacter(a[i]);
//        //        if (Recognized || Finished) {
//        //            Length = i + 1;
//        //            break;
//        //        }
//        //        if (Rejected) {
//        //            Length = i;
//        //            break;
//        //        }
//        //    }
//        //}

//        //// Change any status that might need changing when exact character count is needed. Implementors
//        //// must propagate this call to its children if any
//        //public virtual void ReportOutOfCharacters() {
//        //    if (!Recognized) {
//        //        Rejected = true;
//        //    }
//        //    Finished = true;
//        //}

//        //// Runs the recognition until recognized and finished, or rejected. The status is available on the
//        //// object after this is run
//        public abstract void Run();

//        //[ImplementationNote("Does the entire read, as defined by the recognizer.")]
//        protected virtual bool ReallyDoChunk() {
//            return ReallyReallyDoChunk();
//        }

//        //[ImplementationNote("Does the entire read, as defined by the recognizer.")]
//        protected virtual bool ReallyReallyDoChunk() {
//            if (Finished) {
//                return true;
//            }

//            RecognitionTree tree;
//            long i = Index;
//            bool succeeded = Recognizer.TryRead(String, ref i, out tree);
//            RecognitionTree = tree;
//            if (succeeded) {
//                Recognized = true;
//            } else {
//                Rejected = true;
//            }
//            Finished = true;
//            return true;
//        }
//    }

//    public abstract class RecognizerExecution<T> : RecognizerExecution where T : Recognizer {
//        public T TypedRecognizer { get { return (T)Recognizer; } }

//        public RecognizerExecution(T typedRecognizer, String s, long i, long chunkLength) : base(typedRecognizer, s, i, chunkLength) { }
//    }
//}
