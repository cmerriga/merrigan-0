using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("a named value that is recognized by the glom")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Untested]
    public class PropertyAttribute : Attribute {
        public String Name { get; private set; }
        public Type Type { get; private set; }

        public PropertyAttribute(string name) : this(name, typeof(object)) { }
        public PropertyAttribute(string name, Type type) {
            Name = name;
            Type = type;
        }
    }
}
