using System;
using System.Collections.Generic;
using ExecutionsTest = Executions.Test;

namespace Executions {
    public static partial class Utilities {
        public static void Test() {
            TestEqual();
            ////TestToPairs();
        }

        public static void TestEqual() {
            TestEqual(null, null, true);
            int[] empty = new int[0];
            TestEqual(null, empty, false);
            TestEqual(empty, null, false);
            TestEqual(empty, empty, true);
            TestEqual(empty, new int[0], true);
            int[] one = new int[] { 1 };
            TestEqual(empty, one, false);
            TestEqual(one, one, true);
            int[] two = new int[] { 1, 2 };
            TestEqual(one, two, false);
            TestEqual(two, two, true);
        }

        public static void TestEqual(IList<int> results1, IList<int> results2, bool expectedResult) {
            if (Object.ReferenceEquals(results1, results2)) {
                if (!expectedResult) {
                    Console.WriteLine("Result was expected to be false.");
                }
                return;
            }
            if (results1 == null && results2 != null) {
                if (expectedResult) {
                    Console.WriteLine("Result was null but should have been non-null.");
                }
                return;
            }
            if (results1 != null && results2 == null) {
                if (expectedResult) {
                    Console.WriteLine("Result was non-null but should have been null.");
                }
                return;
            }
            if (results1.Count != results2.Count) {
                if (expectedResult) {
                    Console.WriteLine("Result was different length ({0}) than expected ({1}).", results1.Count, results2.Count);
                }
                return;
            }

            List<int> resultsList1 = new List<int>(results1);
            List<int> resultsList2 = new List<int>(results2);
            for (int i = 0; i < resultsList1.Count; ++i) {
                if (!Object.Equals(resultsList1[i], resultsList2[i])) {
                    if (expectedResult) {
                        Console.WriteLine("Result {0} was not expected.", i);
                        return;
                    }
                }
            }

            if (!expectedResult) {
                Console.WriteLine("Result was expected to be false.");
            }
        }
    }
}
