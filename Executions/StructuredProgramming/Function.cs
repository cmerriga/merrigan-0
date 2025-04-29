using System;

namespace Executions.StructuredProgramming {
    public class Function : Block {
        public string Name { get; private set; }
        public Variable[] Parameters { get; private set; }
        public string WhatItDoes { get; private set; }

        public Function(string name, Variable[] parameters, string whatItDoes, Statement[] statements/*, Variable[] variables*/)
            : base(statements/*, variables*/) {
            Name = name;
            Parameters = parameters;
            WhatItDoes = whatItDoes;
        }
    }

    public class Function<T> : Expression<T> {
        private Block block;

        public string Name { get; private set; }
        public Variable[] Parameters { get; private set; }
        public string WhatItDoes { get; private set; }

        public Function(string name, Variable[] parameters, string whatItDoes, Statement[] statements/*, Variable[] variables*/) {
            block = new Block(statements/*, variables*/);
            Name = name;
            Parameters = parameters;
            WhatItDoes = whatItDoes;
        }
    }
}
