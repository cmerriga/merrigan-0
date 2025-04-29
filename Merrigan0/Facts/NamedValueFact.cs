using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    // A fact with a name and a value. The child for any glom
    [Untested]
    public class NamedValueFact : Fact {
        public String Name { get; private set; }
        public object Value { get; private set; }

        // value: could be a POCO, Fact, or Glom
        public NamedValueFact(String name, object value) {
            Name = name;
            Value = value;
        }

        public override bool Disimplies(Fact fact) {
            return false;
        }

        public override bool Implies(Fact fact) {
            return false;
        }
    }
}
