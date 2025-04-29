using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class SetGlom : Glom {
        private Glom baseGlom;
        private Dictionary<String, object> valuesByName; //// make a Map

        public SetGlom(Glom baseGlom, String name, object value) {
            this.baseGlom = baseGlom;
            valuesByName = new Dictionary<String, object>() { { name, value } };
        }

        public SetGlom(Glom baseGlom, params object[] namesAndValues) {
            this.baseGlom = baseGlom;
            valuesByName = new Dictionary<String, object>();
            Utilities.ToPairs<String, object>(namesAndValues).Each(p => { valuesByName.Add(p.Item1, p.Item2); });
        }

        // Returns an amorphous value that can be used by other epxressions
        protected override bool TryGetAtThisLevel(String name, out object o) {
            if (valuesByName.TryGetValue(name, out o)) {
                return true;
            }

            o = baseGlom[name];
            return o != null;
        }
    }
}
