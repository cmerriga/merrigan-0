using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class StringGlom : ValueGlom<String> {
        private static Array<Fact> facts = Array<Fact>.From(
            DataTypeFact.String,
            Comparable.Only);

        public override Array<Fact> DirectImplications { get { return facts; } }

        //public String Value { get; private set; }

        public override GlomType Type { get { return GlomType.String; } }

        public StringGlom(String value) : base(value) { }

        public override object As(Type type) {
            if (Object.ReferenceEquals(type, typeof(String))) {
                return Value;
            }
            return Reflection.Cast(Value, type);
        }

        public override bool Disimplies(Fact fact) {
            StringGlom glom = fact as StringGlom;
            if (glom != null) {
                return Value != glom.As<String>();
            }
            return false;
        }

        public override bool Implies(Fact fact) {
            StringGlom glom = fact as StringGlom;
            if (glom != null) {
                return Value == glom.As<String>();
            }
            return false;
        }
    }
}
