using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.FactsInternal;

namespace Merrigan0 {
    [Untested]
    public abstract class Fact : IParent<Fact> {
        private Array<Fact> directImplications;

        public virtual Array<Fact> Children { get { return DirectImplications; } }

        public virtual Array<Fact> DirectImplications { get { return directImplications; } }

        public Fact(params Fact[] directImplications) : this(Array<Fact>.From(directImplications)) { }

        public Fact(Array<Fact> directImplications) {
            this.directImplications = directImplications;
        }

        public virtual bool Disimplies(String s) {
            return Disimplies(new WordFact(s));
        }

        public virtual bool Disimplies(Fact fact) {
            bool disimplies;
            if (TryDisimplies(fact, 10, out disimplies)) {
                return disimplies;
            }
            foreach (Fact factToTry in DirectImplications) {
                if (factToTry.Disimplies(fact)) {
                    return true;
                }
            }
            return false;
            ////foreach (Fact factToTry in DirectImplications) {
            ////    if (factToTry.Disimplies(fact)) {
            ////        return true;
            ////    }
            ////}
            ////return false;
        }

        public virtual bool Implies(String s) {
            return Implies(new WordFact(s));
        }

        public virtual bool Implies(Fact fact) {
            bool implies;
            if (TryImplies(fact, 10, out implies)) {
                return implies;
            }
            foreach (Fact factToTry in DirectImplications) {
                if (factToTry.Implies(fact)) {
                    return true;
                }
            }
            return false;
            //foreach (Fact factToTry in DirectImplications) {
            //    if (factToTry.Implies(fact)) {
            //        return true;
            //    }
            //}
            //return false;
        }

        // Things like < 1 implies < 2, or the fact IS this fact.
        public virtual bool InherentlyImplies(Fact fact) {
            return Equals(fact);
        }

        // Breadth-first
        protected bool TryDisimplies(Fact fact, out bool implies) {
            int depth = 0;
            while (depth < 10) {
                if (TryDisimplies(fact, depth, out implies)) {
                    return true;
                }
                ++depth;
            }

            // Not determined
            implies = false;
            return false;
        }

        // Breadth-first. Will actually test items when depth is 0.
        protected bool TryDisimplies(Fact fact, int depth, out bool implies) {
            if (depth == 0) {
                if (InherentlyImplies(fact)) {
                    implies = true;
                    return true;
                }
            }

            int childDepth = depth - 1;
            foreach (Fact factToTry in DirectImplications) {
                if (factToTry.TryDisimplies(fact, childDepth, out implies)) {
                    return true;
                }
            }

            // Not determined
            implies = false;
            return false;
        }

        // Breadth-first
        protected bool TryImplies(Fact fact, out bool implies) {
            int depth = 0;
            while (depth < 10) {
                if (TryImplies(fact, depth, out implies)) {
                    return true;
                }
                ++depth;
            }

            // Not determined
            implies = false;
            return false;
        }

        // Breadth-first. Will actually test items when depth is 0.
        protected bool TryImplies(Fact fact, int depth, out bool implies) {
            if (depth == 0) {
                if (InherentlyImplies(fact)) {
                    implies = true;
                    return true;
                }
            }

            int childDepth = depth - 1;
            foreach (Fact factToTry in DirectImplications) {
                if (factToTry.TryImplies(fact, childDepth, out implies)) {
                    return true;
                }
            }

            // Not determined
            implies = false;
            return false;
        }
    }
}
