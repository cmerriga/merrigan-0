using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0 {
    [Untested]
    public static class Utilities {
        ///// <summary>
        ///// Creates an appropriately typed adapter, or returns the original value.
        ///// </summary>
        ///// <typeparam name="T">Any destinationType.</typeparam>
        ///// <param name="value">Any value.</param>
        ///// <returns>An appropriately typed copy or adapter, or the original value.</returns>
        ///// <exception cref="InvalidCastException">If the value cannot be converted.</exception>
        //public static T As<T>(object value) {
        //    // Null begets a default only if the value's destinationType is nullable
        //    if (value == null) {
        //        if (typeof(T).IsValueType) {
        //            return default(T);
        //        } else {
        //            return (T)value;
        //        }
        //    }

        //    // If a direct cast can be made, return that
        //    Type valueType = value.GetType();
        //    if (typeof(T).IsAssignableFrom(valueType)) {
        //        return (T)value;
        //    }

        //    // If a direct adapter can be found, use that

        //    // If an indirect adapter can be found, use that

        //    // Otherwise fail
        //    throw new InvalidCastException(string.Format("Value of type {0} could not be adapted as type {1}.", valueType.Name, typeof(T).Name));
        //}

        [WhatItDoes("Constructs an object of the given type, initialized with the given values.")]
        public static T Create<T>(params object[] values) {
            Type type = typeof(T);
            foreach (ConstructorInfo constructor in type.GetConstructors()) {
                ParameterInfo[] parameters = constructor.GetParameters();
                if (parameters.Length == values.Length) {
                    Boolean matches = true;
                    for (Int32 i = 0; i < parameters.Length; ++i) {
                        if (!parameters[i].ParameterType.IsAssignableFrom(values[i].GetType())) {
                            matches = false;
                            break;
                        }
                    }

                    if (matches) {
                        return (T)constructor.Invoke(values);
                    }
                }
            }

            throw new ArgumentException("No constructor exists for these values.");
        }

        private static Dictionary<Type, Dictionary<Type, Func<object, object>>> conversionFunctionsByFromTypeByToType = new Dictionary<Type, Dictionary<Type, Func<object, object>>>() {
            {
                typeof(int),
                new Dictionary<Type, Func<object, object>>() {
                    { typeof(char), ch => (int)ch }
                }
            }
        };

        public static object ConvertEnum(object source, Type destinationType) {
            // If it's a string, just use that, case-insensitively
            string sourceString = source as string;
            if (sourceString != null) {
                return Enum.Parse(destinationType, sourceString, true);
            }

            // If it's an int, cast sort-of directly
            if (source is int) {
                return Enum.ToObject(destinationType, (int)source);
            }

            // Convert it to a string and try to use that, case-insensitively
            Type sourceType = source.GetType();
            string stringifiedEnum = source.ToString();
            return Enum.Parse(destinationType, stringifiedEnum, true);
        }

        // Might be an ArrayGlom, might be an ObjectGlom, might be a NumberGlom, etc.
        public static Glom ConvertToGlom(object o, Type type = null) {
            Glom g;
            if (type == null) {
                type = o.GetType();
            }

            //// Dictionaries
            //if (Reflection.Implements(type, typeof(IDictionary))) {
            //    // Make sure the object being copied is actually a dictionary
            //    IDictionary existingDictionary = o as IDictionary;
            //    if (existingDictionary == null) {
            //        throw new Exception(); ////
            //    }
            //    IDictionary dictionary = (IDictionary)Reflection.New(type);
            //    Type keyType;
            //    Type valueType;
            //    if (type.ContainsGenericParameters) {
            //        Type[] genericParameterTypes = type.GetGenericArguments();
            //        keyType = genericParameterTypes[0];
            //        valueType = genericParameterTypes[1];
            //    } else {
            //        keyType = typeof(object);
            //        valueType = typeof(object);
            //    }
            //    foreach (DictionaryEntry entry in existingDictionary) {
            //        dictionary.Add(entry.Key, entry.Value);
            //    }
            //    return dictionary;
            //}

            //// Maps
            //if (Reflection.Implements(type, typeof(Map<,>))) {
            //    // Make sure the object being copied is actually a dictionary
            //    IDictionary existingDictionary = o as IDictionary;
            //    if (existingDictionary == null) {
            //        throw new Exception(); ////
            //    }
            //    IDictionary dictionary = (IDictionary)Reflection.New(type);
            //    Type keyType;
            //    Type valueType;
            //    if (type.ContainsGenericParameters) {
            //        Type[] genericParameterTypes = type.GetGenericArguments();
            //        keyType = genericParameterTypes[0];
            //        valueType = genericParameterTypes[1];
            //    } else {
            //        keyType = typeof(object);
            //        valueType = typeof(object);
            //    }
            //    foreach (DictionaryEntry entry in existingDictionary) {
            //        dictionary.Add(entry.Key, entry.Value);
            //    }
            //    return dictionary;
            //}

            // Maps

            // IEnumerables that weren't dictionaries of maps are made into array gloms

            // Regular objects
            g = Glom.From(o);
            return g;
        }

        ////public static T ConvertFromGlom<T>(Glom g) {
        ////    return (T)ConvertFromGlom(g, typeof(T));
        ////}

        ////public static object ConvertFromGlom(Glom g, Type type) {
        ////    if (!type.HasInterface(typeof(IEnumerable))) {
        ////        //// The glom also has to be an array
        ////        //ArrayGlom arrayGlom = g as ArrayGlom;
        ////        //if (g != null) {
        ////        //    throw new Exception(); ////
        ////        //}

        ////        //long length = arrayGlom.Length;
                
        ////        //// New object is block?
        ////        //if (type == 

        ////        //// New object is Array?
        ////    }
        ////    return null; ////
        ////}

        [WhatItDoes("Returns all combinations, ordered by distance from the beginning of each array.")]
        public static Array<Tuple<T1, T2>> ClosestPairs<T1, T2>(Array<T1> a1, Array<T2> a2) {
            // Make two parallel arrays
            Array<Tuple<T1, T2>> combinations = a1.Cross(a2);
            long a2Length = a2.Length;
            Array<long> distances = combinations.Transform((i, item) => {
                long leftDistance = i / a2Length;
                long rightDistance = i % a2Length;
                return leftDistance * leftDistance + rightDistance * rightDistance;
            });
            ////IndentedConsole.Ambient.AppendLine(combinations);
            ////IndentedConsole.Ambient.AppendLine(distances);

            // Sort them by distance using the distance array as keys
            Array<Tuple<T1, T2>> result;
            Array<long> dummySortedDistances;
            Utilities.SortWithKey(combinations, distances, out result, out dummySortedDistances);
            ////IndentedConsole.Ambient.AppendLine(result);
            ////IndentedConsole.Ambient.AppendLine(dummySortedDistances);
            return result;
        }

        public static void Each(object o, Action<object> function) {
            if (Reflection.Enumerable(o.GetType())) {
                foreach (object item in (IEnumerable)o) {
                    function(item);
                }
                return;
            }
            function(o);
        }

        public static bool EnumerablesEqual(IEnumerable e1, IEnumerable e2) {
            if (e1 == null) {
                return (e2 == null);
            }
            if (e2 == null) {
                return (e1 == null);
            }
            if (Object.ReferenceEquals(e1, e2)) {
                return true;
            }
            IEnumerator enumerator1 = e1.GetEnumerator();
            IEnumerator enumerator2 = e2.GetEnumerator();
            while (true) {
                bool hasNext1 = enumerator1.MoveNext();
                bool hasNext2 = enumerator2.MoveNext();
                if (hasNext1 != hasNext2) {
                    return false;
                }
                if (!hasNext1) {
                    break;
                }
                if (!EqualsOperator.Only.Run(enumerator2.Current, enumerator1.Current)) {
                    return false;
                }
            }
            return true;
        }

        [WhatItDoes("Gets the file name of the running application.")]
        [return: NotEmpty]
        public static String GetApplicationName() {
            // Different options here: https://stackoverflow.com/questions/616584/how-do-i-get-the-name-of-the-current-executable-in-c
            // File name with extension
            // return System.AppDomain.CurrentDomain.FriendlyName;
            // Returns the filename without extension (e.g. MyApp).
            //return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            String name;
            using (Process process = Process.GetCurrentProcess()) {
                name = Path.GetFileName(process.MainModule.FileName);
            }
            return name;
        }

        public static String GetMachineFolderPath() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), GetApplicationName());
        }

        public static object GetNullableValue(object nullable) {
            bool hasValue = nullable.Property<bool>("HasValue");
            if (!hasValue) {
                return null;
            }
            return nullable.Property("Value");
        }

        public static String GetUserFolderPath() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetApplicationName());
        }

        public static void Insist<T>(ref T t, params Func<T, String>[] conditions) {
            foreach (Func<T, String> condition in conditions) {
                string conditionFailure = condition(t);
                if (!Interactive) {
                    if (conditionFailure != null) {
                        throw new ArgumentException(conditionFailure);
                    }
                    continue;
                }

                while (conditionFailure != null) {
                    T response;
                    bool succeeded = TryAskUser(string.Format("{0}. Do you have a different suggestion?", conditionFailure), out response);
                    if (!succeeded) {
                        throw new ArgumentException(conditionFailure);
                    }
                    conditionFailure = condition(t);
                }
            }
        }

        public static bool Interactive { get; set; }

        [WhatItIs("The Map<String, object> that represents the public properties of the object.")]
        public static Map<String, object> MapFromObject(object o, Type type = null) {
            if (type == null) {
                type = o.GetType();
            }

            MutableMap<String, object> mapSoFar = new MutableMap<String, object>();

            // Add public properties that are settable and gettable
            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                if (propertyInfo.GetGetMethod() != null && propertyInfo.GetSetMethod() != null) {
                    mapSoFar.Add(propertyInfo.Name, propertyInfo.GetValue(o, null));
                }
            }

            // Add public fields that aren't calculated
            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)) {
                mapSoFar.Add(fieldInfo.Name, fieldInfo.GetValue(o));
            }

            return mapSoFar.Current;
        }

        public static object Max([NotEmpty] IEnumerable enumerable) {
            return Max(Array<object>.From(enumerable));
        }

        public static object Max([NotEmpty] Array<object> a) {
            object maxItem = null;
            Type maxItemType = null;
            bool first = true;
            Func<object, object, object> mostRecentGreaterFunction = null;
            Type previousItemType = null;
            foreach (object item in a) {
                if (first) {
                    first = false;
                    maxItem = item;
                    maxItemType = item.GetType();
                    continue;
                }

                Type itemType = item.GetType();

                // Try to find a calculation function that takes these two types
                if (itemType != previousItemType) {
                    if (!GreaterOperator.Only.TryGetCalculateFunction(itemType, maxItemType, out mostRecentGreaterFunction)) {
                        throw new Exception();
                    }
                }

                if ((bool)mostRecentGreaterFunction(item, maxItem)) {
                    maxItem = item;
                    maxItemType = itemType;
                }

                previousItemType = itemType;
            }
            return maxItem;
        }

        public static object Min([NotEmpty] IEnumerable enumerable) {
            return Min(Array<object>.From(enumerable));
        }

        public static object Min([NotEmpty] Array<object> a) {
            object minItem = null;
            Type minItemType = null;
            bool first = true;
            Func<object, object, object> mostRecentLessThanFunction = null;
            Type previousItemType = null;
            foreach (object item in a) {
                if (first) {
                    first = false;
                    minItem = item;
                    minItemType = item.GetType();
                    continue;
                }

                Type itemType = item.GetType();

                // Try to find a calculation function that takes these two types
                if (itemType != previousItemType) {
                    if (!LessOperator.Only.TryGetCalculateFunction(itemType, minItemType, out mostRecentLessThanFunction)) {
                        throw new Exception();
                    }
                }

                if ((bool)mostRecentLessThanFunction(item, minItem)) {
                    minItem = item;
                    minItemType = itemType;
                }

                previousItemType = itemType;
            }
            return minItem;
        }

        public static object Resolve(object o, object valueOrNml) {
            String nml = valueOrNml as String;
            if (nml != null) {
                return Glom.From(o).Evaluate(nml);
            }
            string nmlSystemString = valueOrNml as string;
            if (nmlSystemString != null) {
                return Glom.From(o).Evaluate(nmlSystemString);
            }
            return valueOrNml;
        }

        [WhatItIs("an array with all items properly before the items than which they are less")]
        public static Array<T> SortedTransitive<T>(Array<T> items, Func<T, T, bool> lessFunction) {
            long iterationsAllowed = items.Length;
            Array<T> mostSortedItems = items;
            long length = items.Length;
            while (iterationsAllowed > 0) {
                ////IndentedConsole.Ambient.AppendLine(mostSortedItems);
                bool inserted = false;
                MutableArray<T> sortedItemsSoFar = new MutableArray<T>();
                foreach (T item in mostSortedItems) {
                    for (long i = 0L; i < sortedItemsSoFar.Current.Length; ++i) {
                        if (lessFunction(item, sortedItemsSoFar.Current[i])) {
                            sortedItemsSoFar.Insert(i, item);
                            inserted = true;
                            break;
                        }
                    }

                    if (!inserted) {
                        sortedItemsSoFar.Append(item);
                    }
                }
                if (!inserted) {
                    break;
                }
                mostSortedItems = sortedItemsSoFar.Current;
                --iterationsAllowed;
            }
            return mostSortedItems;
        }

        //[Equal("toSort.Length, keys.Length")]
        //[Warn("toSort.Length > 100")]
        public static void SortWithKey<T, K>(Array<T> toSort, Array<K> keys, out Array<T> sorted, out Array<K> sortedKeys) where K : IComparable {
            T[] sortedSoFar = toSort.ToBlock();
            K[] sortedKeysSoFar = keys.ToBlock();

            // Sort via insertion sort
            long length = keys.Length;
            for (long iToInsert = 0; iToInsert < length; ++iToInsert) {
                // Find the lowest of all the keys
                long iLowestKeySoFar = iToInsert;
                K lowestKeySoFar = sortedKeysSoFar[iToInsert];
                for (long iToCheck = iToInsert + 1; iToCheck < length; ++iToCheck) {
                    K keyToCheck = sortedKeysSoFar[iToCheck];
                    if (lowestKeySoFar.CompareTo(keyToCheck) > 0) {
                        iLowestKeySoFar = iToCheck;
                        lowestKeySoFar = keyToCheck;
                    }
                }

                // Switch the values in the new arrays if necessary
                if (iLowestKeySoFar != iToInsert) {
                    K keyToMove = sortedKeysSoFar[iToInsert];
                    T itemToMove = sortedSoFar[iToInsert];
                    sortedKeysSoFar[iToInsert] = lowestKeySoFar;
                    sortedSoFar[iToInsert] = sortedSoFar[iLowestKeySoFar];
                    sortedKeysSoFar[iLowestKeySoFar] = keyToMove;
                    sortedSoFar[iLowestKeySoFar] = itemToMove;
                }
            }

            sorted = Array<T>.From(sortedSoFar);
            sortedKeys = Array<K>.From(sortedKeysSoFar);
        }

        public static void ThrowIfIndexOutOfRange(long i, long length) {
            if (i >= length || i < 0) {
                ThrowIndexOutOfRangeException(i, length);
            }
        }

        public static void ThrowIndexOutOfRangeException(long i, long length) {
            throw new IndexOutOfRangeException(string.Format("Index must be in range [0, {0}), but was {1}.", length, i));
        }

        public static Array<Tuple<T1, T2>> ToPairs<T1, T2>(object[] values) {
            MutableArray<Tuple<T1, T2>> tuplesSoFar = new MutableArray<Tuple<T1, T2>>();
            if (values.LongLength % 2 != 0) {
                throw new Exception("Had odd number of arguments.");
            }
            for (long i = 0; i < values.LongLength; i += 2) {
                Tuple<T1, T2> tuple = new Tuple<T1, T2>(Reflection.Cast<T1>(values[i]), Reflection.Cast<T2>(values[i + 1]));
                tuplesSoFar.Append(tuple);
            }
            return tuplesSoFar.Current;
        }

        public static bool TryAskUser<T>(String prompt, out T value) {
            System.Console.Write(prompt + " ");
            String response = System.Console.ReadLine();
            if (response == null) {
                value = default(T);
                return false;
            }
            value = default(T); ////Json.Read(new StringReader(response));
            return true;
        }

        public static void Wait(Func<bool> condition, TimeSpan maxDelay) {
            // Start with a short wait and double until equal to max delay
            TimeSpan nextDelay = TimeSpan.FromMilliseconds(1);
            while (!condition()) {
                Thread.Sleep(nextDelay);
                if (nextDelay < maxDelay) {
                    nextDelay = nextDelay + nextDelay;
                    if (nextDelay > maxDelay) {
                        nextDelay = maxDelay;
                    }
                }
            }
        }
    }
}
