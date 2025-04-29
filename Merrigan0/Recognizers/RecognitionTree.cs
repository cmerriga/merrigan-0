using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    //// Add greedy & not-greedy options to the ConsiderNextCharacters
    [DebuggerDisplay("{String}")]
    [Untested]
    public class RecognitionTree {
        protected String baseString;
        private object data;

        public Array<RecognitionTree> Children { get; protected set; }

        [MayBeNull("Recognizer.GetDataFunction == null")]
        public object Data {
            get {
                if (data != null) {
                    return data;
                }
                data = Recognizer.GetData(this);
                return data;
            }
            set {
                data = value; //// should refactor so tree is bud of RecognitionExecution
            }
        }

        public long Index { get; private set; }

        // May be set at the end of execution
        public long Length { get; protected set; }

        public Recognizer Recognizer { get; private set; }
        public String String { get { return baseString.Substring(Index, Length); } }

        // May be null, if type unknown
        public Type Type { get; private set; }

        public RecognitionTree(Recognizer recognizer, String baseString, long index, long length, params RecognitionTree[] children) :
            this(recognizer, baseString, index, length, null, children) {
        }

        public RecognitionTree(
            Recognizer recognizer, 
            String baseString, 
            long index, 
            long length, 
            [MayBeNull] object data = null, 
            [MayBeNull] Array<RecognitionTree> children = null) 
        {
            this.baseString = baseString;
            Children = children;
            this.data = data;
            Index = index;
            Length = length;
            Recognizer = recognizer;
        }

        public override string ToString() { return String; }
    }
}
