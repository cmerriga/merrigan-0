using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Merrigan0.RecognizersInternal {
    [Untested]
    public class RegexRecognizer : Recognizer {
        private Regex regex;

        public RegexRecognizer(String regex) : this(regex, regex, null) { }

        public RegexRecognizer(String name, String regex) : this(name, regex, null) { }

        public RegexRecognizer(String name, String regex, Func<RecognitionTree, object> getDataFunction)
            : base(name, null, getDataFunction) 
        {
            this.regex = new Regex(regex);
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            RegexRecognizer recognizer = new RegexRecognizer("a+b+");
            Testing.Test(recognizer.Name, () => {
                recognizer.TestTryRecognize("a", false);
                recognizer.TestTryRead("a", false);
                recognizer.TestTryRecognize("ab", true);
                recognizer.TestTryRead("ab", true);
                recognizer.TestTryRecognize("abad", false);
                recognizer.TestTryRead("abad", true);
                recognizer.TestTryRead("1", new object[] { "aabb", "aabb" });
            });
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            string systemString = s.ToString();
            Match match = regex.Match(systemString, (int)i);
            if (match.Success && (match.Index == i)) {
                tree = new RecognitionTree(
                    this,
                    s,
                    i,
                    match.Length);
                i += match.Length;
                return true;
            }
            tree = null;
            return false;
        }
    }
}
