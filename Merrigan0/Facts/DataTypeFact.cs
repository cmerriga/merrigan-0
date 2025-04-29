using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    [Untested]
    public class DataTypeFact : ExclusiveFact {
        public static Fact Array;
        public static Fact Boolean;
        public static Fact Number;
        public static Fact Object;
        public static Fact String;

        private static Distinctor DataTypeDistinctor = Distinctor.From("data-type");

        static DataTypeFact() {
            DataTypeDistinctor = Distinctor.From("data-type");
            
            Array = DataTypeDistinctor.Fact("array");
            Boolean = DataTypeDistinctor.Fact("boolean");
            Number = DataTypeDistinctor.Fact("number");
            Object = DataTypeDistinctor.Fact("object");
            String = DataTypeDistinctor.Fact("string");
        }

        protected DataTypeFact(String name) : base(DataTypeDistinctor, name) { }
    }
}
