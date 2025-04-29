using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class CustomRecognizer : Recognizer {
        private Func<String, long, CustomRecognizerResult> runFunction;

        public CustomRecognizer(Func<String, long, CustomRecognizerResult> runFunction) : 
            this(null, runFunction) 
        {
        }

        public CustomRecognizer(String name, Func<String, long, CustomRecognizerResult> runFunction) : 
            base(name) 
        {
            this.runFunction = runFunction;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            CustomRecognizer recognizer = new CustomRecognizer("punctuation block", (s, i) => {
                long iToTry = i;
                while (iToTry < s.Length) {
                    if (!Char.IsPunctuation(s[iToTry])) {
                        break;
                    }
                    ++iToTry;
                }
                if (iToTry == i) {
                    return new CustomRecognizerResult() {
                        Succeeded = false
                    };
                }
                return new CustomRecognizerResult() {
                    Length = iToTry - i,
                    Succeeded = true
                };
            });
            Testing.Test("punctuation block", () => {
                recognizer.TestTryRecognize("", false);
                recognizer.TestTryRead("", false);
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize(",*", true);
                recognizer.TestTryRead(",*", true);
                recognizer.TestTryRecognize(",*a", false);
                recognizer.TestTryRead(",*a", true);
            });
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append("<[custom]>");
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new CustomRecognizerExecution(this, s, i);
        ////}

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            CustomRecognizerResult result = runFunction(s, i);
            if (result.Succeeded) {
                tree = new RecognitionTree(this, s, i, result.Length);
                i += result.Length;
                return true;
            }

            tree = null;
            return false;
        }

        ////protected class CustomRecognizerExecution : RecognizerExecution<CustomRecognizer> {
        ////    public CustomRecognizerExecution(CustomRecognizer customRecognizer, String s, long i)
        ////        : base(customRecognizer, s, i) {
        ////    }

        ////    public override void Run() {
        ////        CustomRecognizerResult result = TypedRecognizer.runFunction(baseString, Index);
        ////        Length = result.Length;
        ////        Recognized = result.Recognized;
        ////        Rejected = result.Rejected;
        ////    }
        ////}

        public struct CustomRecognizerResult {
            public long Length;
            public bool Succeeded;
        }
    }
}
