using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.GlomsInternal;

namespace Merrigan0.MetaInternal {
    [Untested]
    public class NamespacesGlom : Glom {
        public static readonly NamespacesGlom Only = new NamespacesGlom();

        // Gets the value from the class/static level
        protected override bool TryGetAtThisLevel(String name, out object o) {
            NamespaceGlom namespaceGlom = NamespaceGlom.From(name);
            if (namespaceGlom == null) {
                o = null;
                return false;
            }
            o = namespaceGlom;
            return true;
        }
    }
}
