using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Merrigan0.ExecutionsInternal;
//using Merrigan0.GlomsInternal;

//namespace Merrigan0.MetaInternal {
    //[Untested]
//    public abstract class Constraint {
//        public bool Acceptable(object o) { return Acceptable(o, null); }
//        public abstract bool Acceptable(object o, Glom context);
//        public abstract bool TryRandom(Glom context, out object o);
//    }

    //[Untested]
//    public abstract class Constraint<T> : Constraint {
//        public virtual bool Acceptable(T value) { return Acceptable(value, null); }

//        public override bool Acceptable(object o, Glom context) {
//            return Acceptable((T)o, context);
//        }

//        public abstract bool Acceptable(T value, Glom context);

//        public override bool TryRandom(Glom context, out object o) {
//            T value;
//            if (!TryRandom(context, out value)) {
//                o = null;
//                return false;
//            }
//            o = (object)value;
//            return true;
//        }

//        public virtual bool TryRandom(Glom context, out T value) {
//            int nAttempts = 0;
//            while (nAttempts < 1000) {
//                T valueToTry = Random.Value<T>();
//                if (Acceptable(valueToTry, context)) {
//                    value = valueToTry;
//                    return true;
//                }
//                ++nAttempts;
//            }
//            throw new Exception();
//        }

//        public Type ValueType { get { return typeof(T); } }
//    }

//    //public class XxxxConstraint<T> : Constraint<T> {
//    //    public override bool Acceptable(T value, Scope context) {
//    //    }

//    //    public override bool TryRandom(Scope context, out object o) {
//    //    }
//    //}

//    public class CompoundConstraint<T> : Constraint<T> {
//        private Array<Constraint<T>> children;

//        public CompoundConstraint(Array<Constraint<T>> children) {
//            this.children = children;
//        }

//        public override bool Acceptable(T value, Glom context) {
//            foreach (Constraint<T> child in children) {
//                if (!child.Acceptable(value, context)) {
//                    return false;
//                }
//            }
//            return true;
//        }

//        public override bool TryRandom(Glom context, out T value) {
//            if (children.Length == 0L) {
//                value = Merrigan0.Random.Value<T>();
//                return true;
//            }
//            int nAttempts = 0;
//            while (nAttempts < 1000) {
//                if (!children[0].TryRandom(context, out value)) {
//                    return false;
//                }
//                bool acceptable = true;
//                foreach (Constraint<T> child in children.Subarray(1)) {
//                    if (!child.Acceptable(value, context)) {
//                        acceptable = false;
//                        break;
//                    }
//                }
//                if (acceptable) {
//                    return true;
//                }
//                ++nAttempts;
//            }
//            throw new Exception();
//        }
//    }

//}
