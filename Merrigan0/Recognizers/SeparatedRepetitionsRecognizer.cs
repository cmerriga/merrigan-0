using System;
using System.Collections.Generic;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class SeparatedRepetitionsRecognizer : Recognizer {
        private Recognizer itemRecognizer;
        private Recognizer separatorRecognizer;

        public SeparatedRepetitionsRecognizer(Recognizer itemRecognizer, Recognizer separatorRecognizer) : this(null, itemRecognizer, separatorRecognizer, null) { }
        public SeparatedRepetitionsRecognizer(String name, Recognizer itemRecognizer, Recognizer separatorRecognizer) : this(name, itemRecognizer, separatorRecognizer, null) { }

        public SeparatedRepetitionsRecognizer(String name, Recognizer itemRecognizer, Recognizer separatorRecognizer, Array<Recognizer> namedLocalRecognizers) : 
            base(name)
                ////Array<Recognizer>.From(new OptionalRecognizer(
                ////    new SequenceRecognizer(child, new AnyRepetitionsRecognizer(new SequenceRecognizer(separator, child))))), 
                ////namedLocalRecognizers) 
        {
            this.itemRecognizer = itemRecognizer;
            this.separatorRecognizer = separatorRecognizer;
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Recognizer recognizer = new SeparatedRepetitionsRecognizer(
                new NamedRecognizer("-digit"),
                new SequenceRecognizer(
                    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
                    new SpecificCharacterRecognizer(','),
                    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))));
            recognizer.TestTryRecognize("", true);
            recognizer.TestTryRecognize("a", true);
            recognizer.TestTryRead("as", true);
            recognizer.TestTryRecognize("as", false);
            recognizer.TestTryRecognize("a,1", true);
            recognizer.TestTryRead("a,1s", true);
            recognizer.TestTryRecognize("a,1s", false);
            recognizer.TestTryRecognize("a, 1 , B,0", true);
        }

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('(');
            itemRecognizer.AppendToRegex(regexSoFar);
            regexSoFar.Append('(');
            itemRecognizer.AppendToRegex(regexSoFar);
            separatorRecognizer.AppendToRegex(regexSoFar);
            regexSoFar.Append(")*)?");
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
                if (iToTry > i && !separatorRecognizer.TryRead(s, ref iToTry, out childTree)) {
                    break;
                }
                if (!itemRecognizer.TryRead(s, ref iToTry, out childTree)) {
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

        protected virtual bool RepetitionsOkay(long nRepetitions) {
            return true;
        }
    }
}
