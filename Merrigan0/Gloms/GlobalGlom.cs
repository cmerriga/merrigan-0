using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.MetaInternal;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class GlobalGlom : Glom {
        protected override bool TryGetAtThisLevel(String name, out object o) {
            if (Reflection.NamespaceNames().Contains(name)) {
                o = NamespaceGlom.From(name);
                return true;
            }
            o = null;
            return false;
        }
    }
}
