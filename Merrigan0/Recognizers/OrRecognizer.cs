using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class OrRecognizer : Recognizer {
        private Array<Recognizer> children;

        public OrRecognizer(params Recognizer[] children) : this(null, children, null) { }
        public OrRecognizer(String name, params Recognizer[] children) : this(name, children, null) { }

        public OrRecognizer(String name, Array<Recognizer> children, Array<Recognizer> localRecognizers) :
            base(name, localRecognizers) {
            this.children = children;
            //// children.Each(e => e.SetParent(this));
            //// RegisterNamedRecognizers(children); ////
        }

        //[DiagnosticOnly]
        //[Test]
        //public static void Test() {
        //    Testing.Test(typeof(OptionalRecognizer).Name, () => {
        //        Recognizer recognizer = new StringRecognizer("ab");
        //        recognizer.TestTryRecognize("a", false, false);
        //        recognizer.TestTryRecognize("a", true, false);
        //        recognizer.TestTryRecognize("ab", false, true);
        //        recognizer.TestTryRecognize("ab", true, true);
        //        recognizer.TestTryRecognize("abc", false, true);
        //        recognizer.TestTryRecognize("abc", true, false);

        //        recognizer = new AnyRepetitionsRecognizer(new StringRecognizer("ab"));
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, false);
        //        recognizer.TestTryRecognize("ab", false, true);
        //        recognizer.TestTryRecognize("ab", true, true);
        //        recognizer.TestTryRecognize("abc", false, true);
        //        recognizer.TestTryRecognize("abc", true, false);
        //        recognizer.TestTryRecognize("abab", false, true);
        //        recognizer.TestTryRecognize("abab", true, true);
        //        recognizer.TestTryRecognize("", false, true);
        //        recognizer.TestTryRecognize("", true, true);

        //        recognizer = new CharacterRecognizer('a');
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, true);
        //        recognizer.TestTryRecognize("aa", false, true);
        //        recognizer.TestTryRecognize("aa", true, false);
        //        recognizer.TestTryRecognize("", false, false);
        //        recognizer.TestTryRecognize("", true, false);
        //        recognizer.TestTryRecognize("c", false, false);
        //        recognizer.TestTryRecognize("c", true, false);

        //        recognizer = new CharacterRangeRecognizer('a', 'z');
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, true);
        //        recognizer.TestTryRecognize("z", false, true);
        //        recognizer.TestTryRecognize("aa", false, true);
        //        recognizer.TestTryRecognize("aa", true, false);
        //        recognizer.TestTryRecognize("", false, false);
        //        recognizer.TestTryRecognize("", true, false);

        //        //recognizer = new OrRecognizer(
        //        //    new AnyRepetitionsRecognizer(new StringRecognizer("ab")),
        //        //    new CharacterRecognizer('a'),
        //        //    new StringRecognizer("abac"));
        //        //recognizer.TestTryRecognize("a", false, true);
        //        //recognizer.TestTryRecognize("a", true, true);
        //        //recognizer.TestTryRecognize("ab", false, true);
        //        //recognizer.TestTryRecognize("ab", true, true);
        //        //recognizer.TestTryRecognize("abad", false, true);
        //        //recognizer.TestTryRecognize("abad", true, false);
        //        //recognizer.TestTryRecognize("abac", true, true);

        //        recognizer = new OptionalRecognizer(new CharacterRecognizer("minus", '-'));
        //        recognizer.TestTryRecognize("-", true, true);
        //        recognizer.TestTryRecognize("", true, true);
        //        recognizer.TestTryRecognize("--", false, true);
        //        recognizer.TestTryRecognize("--", true, false);

        //        recognizer = new AnyRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9'));
        //        recognizer.TestTryRecognize("", true, true);
        //        recognizer.TestTryRecognize("ab", false, true);
        //        recognizer.TestTryRecognize("ab", true, false);
        //        recognizer.TestTryRecognize("1", true, true);
        //        recognizer.TestTryRecognize("1ab", false, true);
        //        recognizer.TestTryRecognize("1ab", true, false);
        //        recognizer.TestTryRecognize("12", true, true);
        //        recognizer.TestTryRecognize("12ab", false, true);
        //        recognizer.TestTryRecognize("12ab", true, false);
        //        recognizer.TestTryRecognize("123", true, true);

        //        recognizer = new SequenceRecognizer(
        //           new CharacterRangeRecognizer('1', '9'),
        //           new CharacterRangeRecognizer('0', '9'));
        //        recognizer.TestTryRecognize("10", true, true);
        //        recognizer.TestTryRecognize("10", false, true);
        //        recognizer.TestTryRecognize("10a", true, false);
        //        recognizer.TestTryRecognize("10a", false, true);

        //        recognizer = new AnyRepetitionsRecognizer(new CharacterRangeRecognizer('a', 'z'));
        //        recognizer.TestTryRecognize("", false, true);
        //        recognizer.TestTryRecognize("", true, true);
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, true);
        //        recognizer.TestTryRecognize("ab", false, true);
        //        recognizer.TestTryRecognize("ab", true, true);
        //        recognizer.TestTryRecognize("ab1", false, true);
        //        recognizer.TestTryRecognize("ab1", true, false);

        //        recognizer = new SequenceRecognizer(
        //            new CharacterRangeRecognizer('1', '9'),
        //            new AnyRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9')));
        //        recognizer.TestTryRecognize("1", false, true);
        //        recognizer.TestTryRecognize("1", true, true);
        //        recognizer.TestTryRecognize("1a", false, true);
        //        recognizer.TestTryRecognize("1a", true, false);
        //        recognizer.TestTryRecognize("10", false, true);
        //        recognizer.TestTryRecognize("10", true, true);
        //        recognizer.TestTryRecognize("10a", false, true);
        //        recognizer.TestTryRecognize("10a", true, false);
        //        recognizer.TestTryRecognize("101", false, true);
        //        recognizer.TestTryRecognize("101", true, true);
        //        recognizer.TestTryRecognize("101a", false, true);
        //        recognizer.TestTryRecognize("101a", true, false);
        //        recognizer.TestTryRecognize("", true, false);
        //        recognizer.TestTryRecognize("", false, false);

        //        recognizer = new OneOrMoreRepetitionsRecognizer(new StringRecognizer("ab"));
        //        recognizer.TestTryRecognize("a", false, false);
        //        recognizer.TestTryRecognize("a", true, false);
        //        recognizer.TestTryRecognize("ab", false, true);
        //        recognizer.TestTryRecognize("ab", true, true);
        //        recognizer.TestTryRecognize("abc", false, true);
        //        recognizer.TestTryRecognize("abc", true, false);
        //        recognizer.TestTryRecognize("abab", false, true);
        //        recognizer.TestTryRecognize("abab", true, true);
        //        recognizer.TestTryRecognize("", false, false);
        //        recognizer.TestTryRecognize("", true, false);

        //        recognizer = new AndRecognizer(
        //            new StringRecognizer("aa"),
        //            new OneOrMoreRepetitionsRecognizer(new CharacterRecognizer('a')),
        //            new AnyRepetitionsRecognizer(new CharacterRangeRecognizer('a', 'z')));
        //        recognizer.TestTryRecognize("a", false, false);
        //        recognizer.TestTryRecognize("a", true, false);
        //        recognizer.TestTryRecognize("ab", false, false);
        //        recognizer.TestTryRecognize("ab", true, false);
        //        recognizer.TestTryRecognize("aa", false, true);
        //        recognizer.TestTryRecognize("aa", true, true);
        //        recognizer.TestTryRecognize("", false, false);
        //        recognizer.TestTryRecognize("", true, false);

        //        recognizer = new UntilRecognizer(new StringRecognizer("bb"));
        //        recognizer.TestTryRecognize("", false, true);
        //        recognizer.TestTryRecognize("", true, true);
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, true);
        //        recognizer.TestTryRecognize("abb", false, true);
        //        recognizer.TestTryRecognize("abb", true, false);
        //        recognizer.TestTryRecognize("abc", false, true);
        //        recognizer.TestTryRecognize("abc", true, true);

        //        recognizer = new AndRecognizer(
        //            new AnyRepetitionsRecognizer(new CharacterRangeRecognizer('a', 'z')),
        //            new UntilRecognizer(new StringRecognizer("stop")));
        //        recognizer.TestTryRecognize("", false, true);
        //        recognizer.TestTryRecognize("", true, true);
        //        recognizer.TestTryRecognize("a", false, true);
        //        recognizer.TestTryRecognize("a", true, true);
        //        recognizer.TestTryRecognize("astop", false, false);
        //        recognizer.TestTryRecognize("astop", true, false);
        //    });
        //}

        ////public override RecognizerExecution BeginExecution(String s, long i) {
        ////    return new OrRecognizerExecution(this, s, i);
        ////}

        public override void AppendToRegex(MutableString regexSoFar) {
            regexSoFar.Append('(');
            long length = children.Length;
            for (long i = 0; i < length; ++i) {
                if (i > 0) {
                    regexSoFar.Append('|');
                }
                children[i].AppendToRegex(regexSoFar);
            }
            regexSoFar.Append(')');
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test(typeof(OptionalRecognizer).Name, () => {
                Recognizer recognizer = new OrRecognizer(
                    new AnyRepetitionsRecognizer(new SpecificStringRecognizer("ab")),
                    new SpecificCharacterRecognizer('a'),
                    new SpecificStringRecognizer("abac"));
                recognizer.TestTryReadOrRecognize("a", false, true);
                recognizer.TestTryReadOrRecognize("a", true, true);
                recognizer.TestTryReadOrRecognize("ab", false, true);
                recognizer.TestTryReadOrRecognize("ab", true, true);
                recognizer.TestTryReadOrRecognize("abad", false, true);
                recognizer.TestTryReadOrRecognize("abad", true, false);
                recognizer.TestTryReadOrRecognize("abac", true, true);
            });
        }

        ////[ImplementationNote("tree will have the longest matching child as its single child. If there " +
        ////    "is a tie, an arbitrary child is chosen.")]
        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            long longestLengthSoFar = -1;
            RecognitionTree longestChildTreeSoFar = null;
            long iChildBegin;
            foreach (Recognizer child in children) {
                iChildBegin = i;
                RecognitionTree childTree;
                if (child.TryRead(s, ref iChildBegin, out childTree)) {
                    long childLength = iChildBegin - i;
                    if (childLength > longestLengthSoFar) {
                        longestChildTreeSoFar = childTree;
                        longestLengthSoFar = childLength;
                    }
                }
            }
            if (longestChildTreeSoFar != null) {
                tree = new RecognitionTree(this, s, i, longestLengthSoFar, longestChildTreeSoFar);
                i += longestLengthSoFar;
                return true;
            }

            tree = null;
            return false;
        }

        //protected class OrRecognizerExecution : RecognizerExecution<OrRecognizer> {
        //    private Array<RecognizerExecution> children;

        //    //public RecognizerExecution ChosenExecution { get; private set; }

        //    public OrRecognizerExecution(OrRecognizer orRecognizer, String s, long i)
        //        : base(orRecognizer, s, i) {
        //        children = orRecognizer.children.Transform(r => r.BeginExecution(s, i));
        //    }

        //    public override void Run() {
        //        long longestLengthSoFar = -1;
        //        RecognizerExecution longestChildSoFar = null;
        //        foreach (RecognizerExecution child in children) {
        //            child.Run();
        //            if (child.Recognized) {
        //                long childLength = child.Length;
        //                if (childLength > longestLengthSoFar) {
        //                    longestChildSoFar = child;
        //                    longestLengthSoFar = childLength;
        //                }
        //            }
        //        }
        //        if (longestChildSoFar != null) {
        //            Length = longestLengthSoFar;
        //            Recognized = true;
        //            Children = Array<RecognitionTree>.From(longestChildSoFar);
        //        } else {
        //            Rejected = true;
        //        }
        //    }
        //}
    }
}
