using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class SpecificStringRecognizer : Recognizer {
        private String sToFind;

        public SpecificStringRecognizer(String sToFind) : this(null, sToFind, null) { }

        public SpecificStringRecognizer(String name, String sToFind) : this(name, sToFind, null) { }

        public SpecificStringRecognizer(String name, String sToFind, Func<RecognitionTree, object> getDataFunction)
            : base(name, null, getDataFunction) {
            this.sToFind = sToFind;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test(typeof(SpecificStringRecognizer).Name, () => {
                SpecificStringRecognizer recognizer = new SpecificStringRecognizer("abc");
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize("abc", true);
                recognizer.TestTryRead("abc", true);
                recognizer.TestTryRecognize("abcd", false);
                recognizer.TestTryRead("abcd", true);
            });
        }

        //public override RecognizerExecution BeginExecution(String s, long i) {
        //    return new StringRecognizerExecution(this, s, i);
        //}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('(');
            regexSoFar.Append(TextUtilities.RegexEscape(sToFind));
            regexSoFar.Append(')');
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            // If the string is too long anyway, give up now
            long length = s.Length;
            long lengthToFind = sToFind.Length;
            if (i + lengthToFind > length) {
                goto fail;
            }

            // Match every character
            for (long iToFind = 0; iToFind < lengthToFind; ++iToFind) {
                if (s[i + iToFind] != sToFind[iToFind]) {
                    goto fail;
                }
            }
            tree = new RecognitionTree(this, s, i, lengthToFind);
            i += lengthToFind;
            return true;

        fail:
            tree = null;
            return false;
        }

        ////protected class StringRecognizerExecution : RecognizerExecution<StringRecognizer> {
        ////    public StringRecognizerExecution(StringRecognizer characterRecognizer, String s, long i)
        ////        : base(characterRecognizer, s, i) {
        ////    }

        ////    //public override void ConsiderNextCharacter(char ch) {
        ////    //    if (Finished) { return; } /////

        ////    //    if (TypedRecognizer.s[i] != ch) {
        ////    //        Rejected = true;
        ////    //        Finished = true;
        ////    //        return;
        ////    //    }

        ////    //    ++i;

        ////    //    if (i >= TypedRecognizer.s.Length) {
        ////    //        Recognized = true;
        ////    //        Length = i;
        ////    //        Finished = true;
        ////    //    }
        ////    //}

        ////    public override void Run() {
        ////        if (Index + TypedRecognizer.sToFind.Length > baseString.Length) {
        ////            Rejected = true;
        ////            return;
        ////        }

        ////        long lengthToFind = TypedRecognizer.sToFind.Length;
        ////        for (long iToFind = 0; iToFind < lengthToFind; ++iToFind) {
        ////            if (baseString[Index + iToFind] != TypedRecognizer.sToFind[iToFind]) {
        ////                Rejected = true;
        ////                return;
        ////            }
        ////        }
        ////        Recognized = true;
        ////        Length = lengthToFind;
        ////    }
        ////}
    }
}
