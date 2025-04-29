using System;
using System.Collections.Generic;

namespace Executions {
    public static partial class Utilities {
        public static bool Equal<T>(IEnumerable<T> items1, IEnumerable<T> items2) {
            if (Object.ReferenceEquals(items1, items2)) {
                return true;
            }
            IEnumerator<T> enumerator1 = items1.GetEnumerator();
            IEnumerator<T> enumerator2 = items2.GetEnumerator();
            while (true) {
                if (enumerator1.MoveNext()) {
                    if (enumerator2.MoveNext()) {
                        if (!Object.Equals(enumerator1.Current, enumerator2.Current)) {
                            return false;
                        }
                    } else {
                        return false;
                    }
                } else if (enumerator2.MoveNext()) {
                    return false;
                } else {
                    break;
                }
            }
            return true;
        }

        public static T Random<T>() {
            return (T)Random(typeof(T));
        }

        public static object Random(Type type) {
            if (type == typeof(string)) {
                return Guid.NewGuid().ToString();
            }
            if (type == typeof(object)) {
                return new Object();
            }
            //// !
            return null;
        }

        public static IList<KeyValuePair<T1, T2>> ToPairs<T1, T2>(object[] values) {
            List<KeyValuePair<T1, T2>> pairsSoFar = new List<KeyValuePair<T1, T2>>();
            if (values.LongLength % 2 != 0) {
                throw new Exception("Had odd number of arguments.");
            }
            for (long i = 0; i < values.LongLength; i += 2) {
                KeyValuePair<T1, T2> pair = new KeyValuePair<T1, T2>((T1)values[i], (T2)values[i + 1]);
                pairsSoFar.Add(pair);
            }
            return pairsSoFar;
        }
    }
}
