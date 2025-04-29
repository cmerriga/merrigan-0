using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal abstract class ValueGlom<T> : Glom {
        private T value;

        public T Value { get { return value; } }

        public ValueGlom(T value) {
            this.value = value;
            ////Type = typeof(T);
        }

        public override object As(Type type) {
            if (Object.ReferenceEquals(type, typeof(T))) {
                return Value;
            }
            return Reflection.Cast(value, type);
        }

        public override string ToString() {
            return value == null ? String.NullRepresentation.ToString() : value.ToString();
        }

        protected override bool TryGetAtThisLevel(String name, out object o) {
            if (name == ValueName) {
                o = value;
                return true;
            }

            // Look in properties for this name
            return Reflection.TryGet(this.value, name, out o);
            ////o = Reflection.Get(this.o, name); //// create try & change to try
            ////return true;
        }
    }
}
