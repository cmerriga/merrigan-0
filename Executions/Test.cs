using System;
using System.Collections.Generic;
using System.Reflection;
using Executions.Diagnostics.MemberAttributes;

namespace Executions {
    public static class Test {
        public static bool Equal(IList<string> strings1, IList<string> strings2) {
            if (strings1.Count != strings2.Count) {
                return false;
            }

            for (int i = 0; i < strings1.Count; ++i) {
                if (strings1[i] != strings2[i]) {
                    return false;
                }
            }
            return true;
        }

        public static void TestConstructors<T>() {
            TestConstructors(typeof(T));
        }

        public static void TestConstructors(Type type) {
            foreach (ConstructorInfo constructorInfo in type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                TestConstructor(constructorInfo);
            }
        }

        public static void TestConstructor(ConstructorInfo constructorInfo, params object[] values) {
            Dictionary<string, int> valueIndexesByParameterNames = new Dictionary<string, int>();
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();

            // Provide random values if needed
            if (values.Length < parameterInfos.Length) {
                values = new object[parameterInfos.Length];
                for (int i = 0; i < values.Length; ++i) {
                    values[i] = Utilities.Random(parameterInfos[i].ParameterType);
                }
            }

            for (int i = 0; i < values.Length; ++i) {
                valueIndexesByParameterNames.Add(parameterInfos[i].Name, i);
            }
            object o = constructorInfo.Invoke(values);

            // Test that the constructor records Create values correctly
            foreach (PropertyInfo propertyInfo in constructorInfo.DeclaringType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                CreateAttribute createAttribute = propertyInfo.GetCustomAttribute<CreateAttribute>(true);
                if (createAttribute == null) {
                    continue;
                }
                
                // Compare the value of the property with the value of the parameter with the name given in the CreateAttribute
                object parameterValue = values[valueIndexesByParameterNames[createAttribute.Name]];
                if (!Object.Equals(propertyInfo.GetValue(o), parameterValue)) {
                    Console.WriteLine("Constructor failed to set property {0}.", propertyInfo.Name);
                }
            }

            foreach (FieldInfo fieldInfo in constructorInfo.DeclaringType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                CreateAttribute createAttribute = fieldInfo.GetCustomAttribute<CreateAttribute>(true);
                if (createAttribute == null) {
                    continue;
                }

                // Compare the value of the property with the value of the parameter with the name given in the CreateAttribute
                object parameterValue = values[valueIndexesByParameterNames[createAttribute.Name]];
                if (!Object.Equals(fieldInfo.GetValue(o), parameterValue)) {
                    Console.WriteLine("Constructor failed to set property {0}.", fieldInfo.Name);
                }
            }

            TestConstructorRejectsInvalidValues(constructorInfo);
        }

        public static void TestConstructorRejectsInvalidValues(ConstructorInfo constructorInfo) {
            //// use ParameterDiagnosticAttributes
        }

        public static void TestEqual<T>(T result, T expectedResult, string failureMessageTemplate, params object[] values) {
            if (!Equals(result, expectedResult)) {
                Console.WriteLine(String.Format(failureMessageTemplate, values));
            }
        }

        public static void TestEqual<T>(IList<T> results, IList<T> expectedResults, string failureMessageTemplate, params object[] values) {
            if (!Utilities.Equal(results, expectedResults)) {
                Console.WriteLine(String.Format(failureMessageTemplate, values));
            }
        }

        public static void TestMemberEqual<T>(T result, T expectedResult, string memberName) {
            TestEqual(result, expectedResult, "Member {0} was {1} but must be {2}.", result, expectedResult);
        }

        public static void TestMemberEqual<T>(IList<T> results, IList<T> expectedResults, string memberName) {
            TestEqual(results, expectedResults, "Member {0} was {1} but must be {2}.", results, expectedResults);
        }

        public static void TestLessOrEqual<T>(T result, T expectedResult, string failureMessageTemplate, params object[] values) where T : IComparable<T> {
            if (result.CompareTo(expectedResult) > 0) {
                Console.WriteLine(String.Format(failureMessageTemplate, values));
            }
        }

        public static void TestWithinRange(DateTime time, DateTime expectedTime, TimeSpan plusOrMinus, string failureMessageTemplate, params object[] values) {
            if (time < expectedTime.Subtract(plusOrMinus) || time > expectedTime.Add(plusOrMinus)) {
                Console.WriteLine(String.Format(failureMessageTemplate, values));
            }
        }
    }
}
