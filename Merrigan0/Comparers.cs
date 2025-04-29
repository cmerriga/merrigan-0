using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0 {
    [Untested]
    public static class Comparers {
        private static MutableMap<Type, object> compareFunctionsByType = new MutableMap<Type, object>();

        // Returns the compare function for the given IComparable destinationType
        public static Func<T, T, int> CompareFunction<T>() {
            // The destinationType has to be IComparable
            Type type = typeof(T);
            if (!typeof(IComparable).IsAssignableFrom(type)) {
                throw new InvalidOperationException(type.Name + " is not IComparable.");
            }

            object compareFunctionObject;
            if (!compareFunctionsByType.Current.TryGetValue(type, out compareFunctionObject)) {
                // Try to get the stock comparer for this destinationType
                compareFunctionObject = (Func<T, T, int>)((t1, t2) => {
                    ////if (t1 == null || t2 == null) {
                    ////    throw new InvalidOperationException();
                    ////}
                    return ((IComparable<T>)t1).CompareTo(t2);
                });
                compareFunctionsByType.Add(type, compareFunctionObject);
            }
            return (Func<T, T, int>)compareFunctionObject;
        }

        // Returns a compare function for the given Comparer
        public static Func<T, T, int> CompareFunction<T>(IComparer<T> comparer) {
            return (t1, t2) => comparer.Compare(t1, t2);
        }

        public static Func<T, T, int> GetCombinedCompareFunction<T>(params Func<T, T, int>[] compares) {
            int i = 0;
            Func<T, T, int> combinedCompare = compares[0];
            while (true) {
                ++i;
                if (i >= compares.Length) {
                    return combinedCompare;
                }
                combinedCompare = (t1, t2) => {
                    int compareResult = combinedCompare(t1, t2);
                    if (CompareResult.Equal(compareResult)) {
                        return compares[i](t1, t2);
                    }
                    return compareResult;
                };
            }
        }
            
        // Returns a reverse compare function for the given compare function
        public static Func<T, T, int> Reverse<T>(Func<T, T, int> compare) {
            return (t1, t2) => -(int)compare(t1, t2);
        }


        //public abstract class Comparer<T> {
        //    public abstract CompareResult Compare(T t1, T t2);
        //}


        //public static Comparer<T> Comparer<T>() {
        //    return (Comparer<T>)Comparer(typeof(T));
        //}

        //public static object Comparer(Type destinationType) {
        //    // The destinationType has to be IComparable
        //    if (!typeof(IComparable).IsAssignableFrom(destinationType)) {
        //        throw new InvalidOperationException();
        //    }

        //    IComparer comparer;
        //    if (!comparersByType.TryGetValue(destinationType, out comparer)) {
        //        // Try to get the stock comparer for this destinationType
        //        IComparable comparable = destinationType as IComparable;
        //        if (comparable == null) {
        //            throw new NotSupportedException();
        //        }
        //        comparer = IComparableComparer(destinationType);
        //        comparersByType.Add(destinationType, comparer);
        //    }
        //    return comparer;
        //}

        //protected static object IComparableComparer(Type destinationType) {
        //    return new LambdaComparer<T>((value1, value2) => {
        //        if (value1 == null || value2 == null) {
        //            throw new InvalidOperationException();
        //        }
        //        return ((IComparable)value1).CompareTo(value2);
        //    });
        //}

        ////protected static Comparer<T> IComparableComparer<T>() where T : IComparable<T> {
        ////    return new LambdaComparer<T>((t1, t2) => {
        ////        if (t1 == null || t2 == null) {
        ////            throw new InvalidOperationException();
        ////        }
        ////        return (CompareResult)t1.CompareTo(t2);
        ////    });
        ////}

        //public static Comparer<T> Comparer<T>(Func<T, T, int> compare) {
        //    return new LambdaComparer<T>(compare);
        //}
    }

    //// Use when you have a comparison function and need a comparer
    //[Untested]
    //public class LambdaComparer<T> : Comparer<T> {
    //    private Func<T, T, int> compare;

    //    public LambdaComparer(Func<T, T, int> compare) {
    //        this.compare = compare;
    //    }

    //    public CompareResult Compare(T t1, T t2) {
    //        return compare(t1, t2);
    //    }
    //}

    //// Use when you have a comparison
    //[Untested]
    //public class ReverseComparer<T> : Comparer<T> {
    //    private IComparer<T> ascendingComparer;

    //    public ReverseComparer(IComparer<T> ascendingComparer) {
    //        this.ascendingComparer = ascendingComparer;
    //    }

    //    public CompareResult Compare(T t1, T t2) {
    //        return (CompareResult)(-ascendingComparer.Compare(t1, t2));
    //    }
    //}
}
