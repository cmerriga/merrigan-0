using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [Untested]
    public class TypesGlom : Glom {
        // Gets the value from the class/static level
        protected override bool TryGetAtThisLevel(String name, out object o) {
            Type type = System.Type.GetType(name);
            if (type == null) {
                o = null;
                return false;
            }
            o = Glom.From(type);
            return true;
        }
    }
}
