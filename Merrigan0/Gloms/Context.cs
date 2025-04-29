using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An agnostic structure to link one existing glom to a proxy glom, but also with its own parent.")]
    [Note("Contains no properties or facts of its own.")]
    [Untested]
    public class Context : Glom {
        [WhatItIs("a tree of gloms, all of which are contexts")]
        protected class ContextTree {
            private Dictionary<Glom, ContextTree> childrenByParent = new Dictionary<Glom, ContextTree>();

            [WhatItIs("could be context, could be simple glom")]
            public Glom Chain { get; private set; }

            public ContextTree(Glom chain) {
                Chain = chain;
            }

            public void Add(Glom childGlom, [WhatItIs("could be context, could be simple glom")] ContextTree child) {
                childrenByParent.Add(childGlom, child);
            }

            public bool TryGetChild(Glom childGlom, out ContextTree child) {
                return childrenByParent.TryGetValue(childGlom, out child);
            }
        }

        protected static ContextTree KnownChains = new ContextTree(null);
        protected static object knownChainsLock = new object();

        private Glom local;
        private String localName;

        ////protected Glom Container { 
        ////    get {
        ////        if (container == null) {
        ////            container = GetContainer();
        ////        }
        ////        return container;
        ////    }
        ////}

        ////// Returns null if not found. null means no such named item exists.
        ////public override object this[String name] {
        ////    get {
        ////        object o;
        ////        if (TryGetAtThisLevel(name, out o)) {
        ////            return o;
        ////        }
        ////        if (container == null) {
        ////            return null;
        ////        }
        ////        return container[name];
        ////    }
        ////}

        public Context(Glom local)
            : base((Glom)null) {
            this.local = local;
        }

        public Context(Glom parent, Glom local)
            : base(parent) {
            this.local = local;
        }

        ////// change to take any object and convert to glom
        //public Context(params object[] chain) : this(((Array<object>)chain).Transform(o => Glom.From(o))) { }

        //public Context(Array<Glom> chain)
        //    : base(InternedChain(chain.Subarray(0, chain.Length - 1))) {
        //    this.local = chain.Last();
        //}

        //// change to take any object and convert to glom
        // namesAndParents should be something like ("class", ClassGlom, "instance", Glom, null, ValueGlom)
        public Context(params object[] namesAndParents) : this(Utilities.ToPairs<String, Glom>(namesAndParents)) { }

        public Context(Array<Tuple<String, Glom>> nameParentPairs) : base((Glom)null) {
            Context chainSoFar = null;
            foreach (Tuple<String, Glom> pair in nameParentPairs.Left(nameParentPairs.Length - 1)) {
                Context newChild = new Context(chainSoFar, pair.Item1, pair.Item2);
                chainSoFar = newChild;
            }
            local = nameParentPairs.Last().Item2;
            localName = nameParentPairs.Last().Item1;
            prototype = chainSoFar;
        }

        public Context(Glom parent, String parentName, Glom local) : base(parent) {
            this.local = local;
            this.localName = parentName;
        }

        //public ContainedGlom([WhatItIs("Parents in order of decreasing generality.")] params Glom[] containers) {
        //    ////Glom accumulatedContained = null;
        //    ////foreach (Glom container in containers) {
        //    ////    if (accumulatedContained == null) {
        //    ////        accumulatedContained = container;
        //    ////    } else {
        //    ////        accumulatedContained = new ContainedGlom(accumulatedContained, container);
        //    ////    }
        //    ////}
        //    this.container = Chain(Array<Glom>.From(containers));
        //}

        // Gets or creates the container chain that represents the given gloms.
        [WhatItIs("a glom or context that includes each of the given gloms, in parent-to-child order")]
        [return: MayBeNull("gloms.Length == 0")]
        [return: Note("Format will be glom (-> context ^ glom)* where context ^ glom means a context whose local is glom")]
        protected static Glom InternedChain(Array<Glom> gloms) {
            // If there aren't any, return null
            if (gloms.Length == 0) {
                return null;
            }

            // If there's only one, just return that
            if (gloms.Length == 1) {
                return gloms[0];
            }

            // If this sequence is already in the known chains, use that. If not, ensure that such a chain is
            // added to the known chains
            ////bool sequenceInKnownChains = true;
            ContextTree currentTree = KnownChains;
            Glom glom = null;
            Glom chainSoFar = null;
            lock (knownChainsLock) {
                for (long i = 0; i < gloms.Length; ++i) {
                    // If the glom is already represented in the current tree, use that. If not, create
                    // a new one and add it
                    glom = gloms[i];
                    ContextTree childTree;
                    if (currentTree.TryGetChild(glom, out childTree)) {
                        // Already here, use this
                        chainSoFar = childTree.Chain;
                        continue;
                    }

                    // Make a new chain and store it in the tree
                    // If it is the first one, the plain glom will do
                    if (i == 0) {
                        chainSoFar = glom;
                    } else {
                        // Otherwise do a context for it
                        chainSoFar = new Context(chainSoFar, glom);
                    }

                    ContextTree newChildTree = new ContextTree(chainSoFar);
                    currentTree.Add(glom, newChildTree);
                    currentTree = newChildTree;
                }

                ////if (sequenceInKnownChains) {
                ////    // Use the one we found
                ////    chainSoFar = currentTree.Chain;
                ////} else {
                ////    // Add a new one from the second-to-last, plus this
                ////    Glom lastGlom = gloms.Last();
                ////    if (gloms.Length == 2) {
                ////        chainSoFar = new Context(gloms[0], lastGlom);
                ////    } else {
                ////        chainSoFar = new Context(new Context(gloms.Subarray(0, gloms.Length - 1)), lastGlom);
                ////    }
                ////    currentTree.Add(lastGlom, new ContextTree(chainSoFar));
                ////}
            }
            return chainSoFar;
        }

        //// define overrides for implies/disimplies

        public override bool TryGet(String name, out object o) {
            if (local.TryGet(name, out o)) {
                return true;
            }

            if (name != null && name == localName) {
                o = local;
                return true;
            }

            if (prototype == null) {
                o = null;
                return false;
            }

            return prototype.TryGet(name, out o);
        }

        //protected virtual Glom GetContainer() {
        //    return null;
        //}
    }
}
