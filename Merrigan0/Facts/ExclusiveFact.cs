using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    [Untested]
    public class ExclusiveFact : WordFact {
        public Distinctor Distinctor { get; private set; }

        public ExclusiveFact(Distinctor distinctor, String name)
            : base(name) {
            Distinctor = distinctor;
        }

        public override bool Disimplies(Fact fact) {
            ExclusiveFact exclusiveFact = fact as ExclusiveFact;
            if (exclusiveFact != null) {
                if (Object.ReferenceEquals(exclusiveFact.Distinctor, Distinctor)) {
                    if (fact == this) {
                        return false;
                    }
                    return true;
                }
            }
            return base.Disimplies(fact);
        }

        public override bool Implies(Fact fact) {
            ExclusiveFact exclusiveFact = fact as ExclusiveFact;
            if (exclusiveFact != null) {
                if (Object.ReferenceEquals(exclusiveFact.Distinctor, Distinctor)) {
                    if (fact != this) {
                        return false;
                    }
                    return true;
                }
            }
            return base.Implies(fact);
        }
    }
}
