using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true)]
    [Untested]
    public class ConceptAttribute : Attribute {
        public String Description { get; private set; }
        public String Name { get; private set; }

        public ConceptAttribute(string name, string description) {
            Description = description;
            Name = name;
        }
    }
}
