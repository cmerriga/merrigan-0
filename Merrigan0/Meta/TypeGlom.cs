using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0.MetaInternal {
    [WhatItIs("Reflected information about the named properties, fields, or methods, for example PropertyInfo, MethodInfo.")]
    [Untested]
    public class TypeGlom : Glom {
        private static MutableMap<Type, TypeGlom> typeGlomsByTypeSoFar = new MutableMap<Type, TypeGlom>();

        private Type type;

        //public NamespaceGlom GetNamespace(String namespaceName) {
        //    String parentNamespaceName = ((String)type.Name).BeforeLast('.');
        //    return NamespaceGlom.From(parentNamespaceName);
        //}

        public static TypeGlom From(Type type) {
            //// needs to use lock
            TypeGlom typeGlom;
            if (!typeGlomsByTypeSoFar.Current.TryGetValue(type, out typeGlom)) {
                typeGlom = new TypeGlom(type);
                typeGlomsByTypeSoFar.Add(type, typeGlom);
            }
            return typeGlom;
        }

        protected TypeGlom(Type type) {
            this.type = type;
        }

        protected override bool TryGetAtThisLevel(String name, out object o) {
            // Try to get this property
            PropertyInfo property = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null) {
                o = property;
                return true;
            }

            // Try to get this field
            FieldInfo field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null) {
                o = field;
                return true;
            }

            // Try to get this method as a delegate
            Array<MethodInfo> methods = Reflection.Methods(type, name);
            if (methods.Length > 0) {
                o = methods;
                return true;
            }

            o = null;
            return false;
        }
    }
}
