using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [WhatItIs("A way to recognize one or more of a certain character class in a row.")]
    [Untested]
    public abstract class CharacterClassGroupRecognizer : Recognizer {
        protected CharacterClassGroupRecognizer(String name) : base(name) { }

        public abstract bool InClass(char ch);

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long iToTry = i;
            long length = s.Length;
            while (iToTry < length) {
                if (!InClass(s[iToTry])) {
                    break;
                }
                ++iToTry;
            }
            long groupLength = iToTry - i;
            if (groupLength == 0) {
                tree = null;
                return false;
            }

            tree = new RecognitionTree(this, s, i, groupLength);
            i = iToTry;
            return true;
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new CharacterClassGroupRecognizerExecution<CharacterClassGroupRecognizer>(this, s, i);
        ////}

        ////protected class CharacterClassGroupRecognizerExecution<T> : RecognizerExecution<T> where T : CharacterClassGroupRecognizer {
        ////    public CharacterClassGroupRecognizerExecution(T recognizer, String s, long i)
        ////        : base(recognizer, s, i) {
        ////    }

        ////    public override void Run() {
        ////        long iToTry = Index;
        ////        long length = baseString.Length;
        ////        while (iToTry < length) {
        ////            if (!TypedRecognizer.InClass(baseString[iToTry])) {
        ////                break;
        ////            }
        ////            ++iToTry;
        ////        }
        ////        long nFound = iToTry - Index;
        ////        if (nFound > 0) {
        ////            Length = nFound;
        ////            Recognized = true;
        ////        } else {
        ////            Rejected = true;
        ////        }
        ////    }
        ////}
    }
}
