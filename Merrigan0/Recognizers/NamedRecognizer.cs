using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class NamedRecognizer : Recognizer {
        private String recognizerName;

        public NamedRecognizer(String recognizerName) : this(null, recognizerName) { }

        public NamedRecognizer(String name, String recognizerName) : 
            base(name) 
        {
            this.recognizerName = recognizerName;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            NamedRecognizer recognizer = new NamedRecognizer("token");
            Testing.Test(recognizer.recognizerName, () => {
                recognizer.TestTryRecognize("abc123 ", false);
                recognizer.TestTryRead("abc123 ", true);
                recognizer.TestTryRecognize("", false);
                recognizer.TestTryRead("", false);
            });
        }

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new NamedRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('<');
            regexSoFar.Append(TextUtilities.RegexEscape(recognizerName));
            regexSoFar.Append('>');
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            return NamedRecognizer(recognizerName).TryRead(s, ref i, out tree);
        }

        ////protected class NamedRecognizerExecution : RecognizerExecution<NamedRecognizer> {
        ////    public NamedRecognizerExecution(NamedRecognizer namedRecognizer, String s, long i) : 
        ////        base(namedRecognizer, s, i)
        ////    {
        ////    }

        ////    public override void Run() {
        ////        RecognizerExecution child = TypedRecognizer.NamedRecognizer(TypedRecognizer.recognizerName).BeginExecution(baseString, Index);
        ////        child.Run();
        ////        Data = child.Data;
        ////        Length = child.Length;
        ////        Recognized = child.Recognized;
        ////        Rejected = child.Rejected;
        ////    }
        ////}
    }
}
