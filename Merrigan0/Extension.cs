using System;

namespace Merrigan0 {
    // For objects that may want to cache values that are expensive to calculate and could be reused
    // because the instance they are based on is immutable.
    //
    // 
    [Untested]
    public class Extension /*: IShrinkable*/ {
        // A sentinel value to use when a result has been created but the result was null
        private static object provenNull = new object();

        //private int iMostRecentlyAccessed;
        private Extender extender;
        private object result0;
        private object result1;
        private object result2;
        //private Func<object> createResultFunction0;
        //private Func<object> createResultFunction1;
        //private Func<object> createResultFunction2;

        public object Get(int i) {
            object result;
            if (i == 0) {
                //if (result0 == null) {
                //    result0 = createResultFunction0();
                //    if (result0 == null) {
                //        result0 = provenNull;
                //    }
                //}
                //iMostRecentlyAccessed = 0;
                result = result0;
            } else if (i == 1) {
                //if (result1 == null) {
                //    result1 = createResultFunction1();
                //    if (result1 == null) {
                //        result1 = provenNull;
                //    }
                //}
                //iMostRecentlyAccessed = 1;
                result = result1;
            } else if (i == 2) {
                //if (result2 == null) {
                //    result2 = createResultFunction1();
                //    if (result2 == null) {
                //        result2 = provenNull;
                //    }
                //}
                //iMostRecentlyAccessed = 2;
                result = result2;
            } else {
                result = extender[i - 3];
            }
            return (Object.ReferenceEquals(result, provenNull)) ? null : result;
        }

        //public void RegisterCreateResultFunction(int i, Func<object> createResultFunction) {
        //    if (i == 0) {
        //        createResultFunction0 = createResultFunction;
        //    } else if (i == 1) {
        //        createResultFunction1 = createResultFunction;
        //    } else if (i == 2) {
        //        createResultFunction2 = createResultFunction;
        //    } else {
        //        Child.RegisterCreateResultFunction(i - 3, createResultFunction);
        //        return;
        //    }
        //}

        public void Set(int i, object o) {
            if (o == null) {
                o = provenNull;
            }
            if (i == 0) {
                result0 = o;
            } else if (i == 1) {
                result1 = o;
            } else if (i == 2) {
                result2 = o;
            } else {
                extender[i - 3] = o;
            }
        }

        //// Returns true iff something was forgotten.
        //public bool Shrink() {
        //    if (iMostRecentlyAccessed == 0) {
        //        if (result2 != null) {
        //            result2 = null;
        //        } else if (result1 != null) {
        //            result1 = null;
        //        } else if (result0 != null) {
        //            result0 = null;
        //        } else {
        //            return false;
        //        }
        //    } else if (iMostRecentlyAccessed == 1) {
        //        if (result2 != null) {
        //            result2 = null;
        //        } else if (result0 != null) {
        //            result0 = null;
        //        } else if (result1 != null) {
        //            result1 = null;
        //        } else {
        //            return false;
        //        }
        //    } else {
        //        if (result1 != null) {
        //            result1 = null;
        //        } else if (result0 != null) {
        //            result0 = null;
        //        } else if (result2 != null) {
        //            result2 = null;
        //        } else {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
