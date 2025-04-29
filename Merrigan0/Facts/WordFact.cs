using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    // Just a label like "preserve" or any other arbitrary word.
    [Untested]
    public class WordFact : Fact {
        public String Word { get; private set; }

        public WordFact(String word) {
            Word = word;
        }

        public override bool Disimplies(String s) {
            if (Word != s) {
                return true;
            }
            return Disimplies(new WordFact(s));
        }

        public override bool Implies(String s) {
            if (Word == s) {
                return true;
            }
            return Implies(new WordFact(s));
        }
    }
}
