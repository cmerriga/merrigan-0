using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    // An ambient Glom that gets its information from the namespaces from all types loaded in the executable.
    // The implied children are the namespaces underneath this namespace.
    [Untested]
    internal class NamespaceGlom : Glom {
        private static MutableMap<String, NamespaceGlom> namespaceGlomsByTypeSoFar = new MutableMap<String, NamespaceGlom>();

        private String namespaceName;

        protected NamespaceGlom(String namespaceName) {
            this.namespaceName = namespaceName;
        }

        // Returns the interned glom for a namespace.
        public new static NamespaceGlom From(String namespaceName) {
            NamespaceGlom g;
            if (!namespaceGlomsByTypeSoFar.Current.TryGetValue(namespaceName, out g)) {
                g = new NamespaceGlom(namespaceName);
                namespaceGlomsByTypeSoFar.Add(namespaceName, g);
            }
            return g;
        }

        public static Glom Parent(String namespaceName) {
            String parentNamespaceName = namespaceName.BeforeLast('.');
            if (parentNamespaceName.Length == 0) {
                return Glom.Global;
            }
            return new NamespaceGlom(parentNamespaceName);
        }

        protected override bool TryGetAtThisLevel(String name, out object o) {
            // Could be a subnamespace or a type. Check for type first
            String namespacePath = namespaceName + "." + name;
            Type type = System.Type.GetType(namespacePath);
            if (type == null) {
                type = System.Type.GetType(namespacePath + "`1");
            }
            if (type != null) {
                o = type;
                return true;
            }
            o = new NamespaceGlom(namespaceName + "." + name);
            return true;
        }

        //protected override Glom GetContainer() {
        //    return GetParent(namespaceName);
        //}
    }
}
