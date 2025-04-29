using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal { 
//    [WhatItIs("A way to recognize a single character as in or out of a class like letters or digits.")]
//    [Untested]
//    public abstract class CharacterClassRecognizer : Recognizer {
//        protected CharacterClassRecognizer(String name) : base(name) { }

//        public abstract bool InClass(char ch);

//        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
//            if (i >= s.Length) {
//                goto fail;
//            }
//            if (InClass(s[i])) {
//                tree = new RecognitionTree(this, s, i, 1);
//                ++i;
//                return true;
//            }

//        fail:
//            tree = null;
//            return false;
//        }

//        ////public override RecognizerExecution BeginExecution(String s, long i) {
//        ////    return new CharacterClassRecognizerExecution<DotNetRecognizer>(this, s, i);
//        ////}

//        ////protected class CharacterClassRecognizerExecution<T> : RecognizerExecution<T> where T : DotNetRecognizer {
//        ////    public CharacterClassRecognizerExecution(T recognizer, String s, long i)
//        ////        : base(recognizer, s, i) {
//        ////    }

//        ////    public override void Run() {
//        ////        long iToTry = Index;
//        ////        if (iToTry < baseString.Length && TypedRecognizer.InClass(baseString[iToTry])) {
//        ////            Length = 1;
//        ////            Recognized = true;
//        ////        } else {
//        ////            Recognized = false;
//        ////        }
//        ////    }
//        ////}
//    }
}
