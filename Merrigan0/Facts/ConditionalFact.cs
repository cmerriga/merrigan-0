using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    // A fact where the second is true iff the first is true.
    [Untested]
    public class ConditionalFact : Fact {
        public Fact Condition { get; private set; }
        //public Fact Fact { get; private set; }

        // Could be a POCO, Fact, or Glom
        public object Value { get; private set; }

        // value: could be a POCO, Fact, or Glom
        public ConditionalFact(Fact condition, /*Fact fact*/object value) {
            Condition = condition;
            Value = value;
        }

        public override bool Disimplies(Fact fact) {
            if (!Condition.Implies(/* ? */fact)) {
                return false;
            }
            //// base on the nature of Value
            return base.Disimplies(fact);
        }

        public override bool Implies(Fact fact) {
            if (!Condition.Implies(/* ? */fact)) {
                return false;
            }
            //// base on the nature of Value
            return base.Disimplies(fact);
        }
    }
}
