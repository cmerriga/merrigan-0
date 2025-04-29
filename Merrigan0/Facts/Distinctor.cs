using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    // A category of mutually exclusive facts, like alive vs. dead, 2 vs. 3, January vs. September.
    [Untested]
    public class Distinctor {
        ////public static Distinctor DataType { get; private set; }

        //// Change to map and then no need for lock
        private static Dictionary<String, Distinctor> distinctorsByName = new Dictionary<String, Distinctor>();
        private static object distinctorsByNameLock = new object();

        private Dictionary<String, ExclusiveFact> factsByName = new Dictionary<String, ExclusiveFact>();
        private object factsByNameLock = new object();

        public static Distinctor From(String name) {
            Distinctor distinctor;
            lock (distinctorsByNameLock) {
                if (!distinctorsByName.TryGetValue(name, out distinctor)) {
                    distinctor = new Distinctor(name);
                    distinctorsByName.Add(name, distinctor);
                }
            }
            return distinctor;
        }

        public String Name { get; private set; }

        protected Distinctor(String name) {
            Name = name;
        }

        public ExclusiveFact Fact(String name) {
            ExclusiveFact fact;
            lock (factsByNameLock) {
                if (!factsByName.TryGetValue(name, out fact)) {
                    fact = new ExclusiveFact(this, name);
                    factsByName.Add(name, fact);
                }
            }
            return fact;
        }
    }
}
