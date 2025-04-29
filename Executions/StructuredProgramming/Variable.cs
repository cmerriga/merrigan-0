using System;

namespace Executions.StructuredProgramming {
    public abstract class Variable {
        public string Description { get; private set; }
        public string Name { get; private set; }
        public abstract Type Type { get; }

        public Variable(string name, string description) {
            Description = description;
            Name = name;
        }
    }

    public class Variable<T> : Variable {
        public override Type Type { get { return typeof(T); } }

        public Variable(string name, string description) : base(name, description) { }
    }
}
