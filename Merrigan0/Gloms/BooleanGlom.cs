using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class BooleanGlom : ValueGlom<bool> {
        public static implicit operator Boolean(BooleanGlom bg) {
            return bg.Value;
        }

        private static Array<Fact> facts = Array<Fact>.From(
            Comparable.Only);

        public override Array<Fact> DirectImplications { get { return facts; } }

        public override GlomType Type { get { return GlomType.Boolean; } }

        //public bool Value { get; private set; }

        public BooleanGlom(bool value) : base(value) { }

        public override object As(Type type) {
            if (Object.ReferenceEquals(type, typeof(bool))) {
                return Value;
            }
            return Reflection.Cast(Value, type);
        }

        public override bool Disimplies(Fact fact) {
            BooleanGlom glom = fact as BooleanGlom;
            if (glom != null) {
                return Value != glom.As<bool>();
            }
            return false;
        }

        public override bool Implies(Fact fact) {
            BooleanGlom glom = fact as BooleanGlom;
            if (glom != null) {
                return Value == glom.As<bool>();
            }
            return false;
        }
    }
}
