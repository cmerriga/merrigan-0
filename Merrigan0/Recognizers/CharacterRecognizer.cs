using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("Any recognizer that reads a single character only should implement this.")]
    [Note("It is adequate to merely define the Valid function.")]
    [Untested]
    public abstract class CharacterRecognizer : Recognizer {
        protected CharacterRecognizer(String name, Func<RecognitionTree, object> getDataFunction)
            : base(name, null, getDataFunction) {
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new CharacterRecognizerExecution(this, s, i);
        ////}

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            if (i >= s.Length) { goto fail; }
            char ch = s[i];
            if (Recognized(ch)) {
                tree = new RecognitionTree(this, s, i, 1, Data(ch));
                ++i;
                return true;
            }

        fail:
            tree = null;
            return false;
        }

        protected abstract bool Recognized(char ch);

        protected virtual object Data(char ch) {
            return ch;
        }

        ////protected class CharacterRecognizerExecution : RecognizerExecution<CharacterRecognizer> {
        ////    public CharacterRecognizerExecution(CharacterRecognizer characterRecognizer, String s, long i)
        ////        : base(characterRecognizer, s, i) {
        ////    }

        ////    //public override void ConsiderNextCharacter(char ch) {
        ////    //    if (Finished) { return; }
        ////    //    if (ch == TypedRecognizer.ch) {
        ////    //        Recognized = true;
        ////    //        Length = 1;
        ////    //    } else {
        ////    //        Rejected = true;
        ////    //    }
        ////    //    Finished = true;
        ////    //}

        ////    public override void Run() {
        ////        if (Index >= baseString.Length) {
        ////            Rejected = true;
        ////            return;
        ////        }
        ////        char ch = baseString[Index];
        ////        if (ch == TypedRecognizer.ch) {
        ////            Recognized = true;
        ////            Length = 1;
        ////        } else {
        ////            Rejected = true;
        ////        }
        ////    }
        ////}
    }
}
