using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [WhatItIs("Allows access to the values of static/class properties and fields.")]
    [Property("TypeArgument")]
    [Untested]
    internal class ClassGlom : Glom {
        private static MutableMap<Type, ClassGlom> classGlomsByTypeSoFar = new MutableMap<Type, ClassGlom>();

        private Type type;

        public Glom Namespace {
            get {
                return NamespaceGlom.From(type.Namespace);
            }
        }

        // Returns the interned scope for a type.
        public static ClassGlom From(Type type) {
            ClassGlom g;
            if (!classGlomsByTypeSoFar.Current.TryGetValue(type, out g)) {
                g = new ClassGlom(type);
                classGlomsByTypeSoFar.Add(type, g);
            }
            return g;
        }

        protected ClassGlom(Type type) {
            this.type = type;
        }

        // Gets the value from the class/static level
        protected override bool TryGetAtThisLevel(String name, out object o) {
            if (Reflection.TryGetClassPropertyOrFieldValue(type, name, out o)) {
                return true;
            }
            if (name == "TypeArgument") {
                if (type.IsGenericType) {
                    o = type.GetGenericArguments()[0];
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }
    }
}
