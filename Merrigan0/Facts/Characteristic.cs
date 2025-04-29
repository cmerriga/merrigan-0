using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    [Untested]
    public abstract class Characteristic : Fact {
        private Guid id;

        protected Characteristic() {
            id = Guid.NewGuid();
        }

        public override string ToString() {
            return this.GetType().Name;
        }
    }

    //public class Bitwise : Characteristic {
    //    public static readonly Bitwise Only = new Bitwise();

    //    protected Bitwise() { }

    //    public override bool Implies(Fact fact) {
    //        if (fact == Integral.Only) {
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    //public class Integral : Characteristic {
    //    public static readonly Integral Only = new Integral();

    //    protected Integral() { }

    //    public override bool Implies(Fact fact) {
    //        if (fact == Numeric.Only) {
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    //public class NumericData : DataTypeFact {
    //    public static readonly Numeric Only = new Numeric();

    //    protected Numeric() { }

    //    public override bool Implies(Fact fact) {
    //        if (fact == Comparable.Only) {
    //            return true;
    //        }
    //        return false;
    //    }
    //}
}
