using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class ObjectGlom : ValueGlom<object> {
        private static Array<Fact> facts = Array<Fact>.From(
            DataTypeFact.Object,
            Comparable.Only);

        //public static String ValueName = "value";

        //private object o;

        public override Array<Fact> DirectImplications { get { return facts; } }

        //public object Value { get { return o; } }

        public ObjectGlom(object o) : base(o) { }

        //public override object As(Type type) {
        //    return Reflection.Cast(o, type);
        //}

        //public override string ToString() {
        //    return o == null ? String.NullRepresentation.ToString() : o.ToString();
        //}

        //protected override bool TryGetAtThisLevel(String name, out object o) {
        //    if (name == ValueName) {
        //        o = Value;
        //        return true;
        //    }

        //    // Look in properties for this name
        //    return Reflection.TryGet(this.o, name, out o);
        //    ////o = Reflection.Get(this.o, name); //// create try & change to try
        //    ////return true;
        //}
    }
}
