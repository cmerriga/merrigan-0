using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class NumberGlom : ValueGlom<object> {
        private static Array<Fact> facts = Array<Fact>.From(
            DataTypeFact.Number,
            Comparable.Only);

        //public object Number { get; private set; }

        public override Array<Fact> DirectImplications { get { return facts; } }

        public NumberGlom(object number) : base(number) { }

        //public override object As(Type type) {
        //    return Reflection.Cast(Number, type);
        //}
    }
}
