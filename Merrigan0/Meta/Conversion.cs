using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Merrigan0.Internal.DotNet; // DoubleDictionary

namespace Merrigan0 {
    [Note("To add a conversion, call RegisterConvertFunction.")]
    [Untested]
    public static class Conversion {
        private static DoubleDictionary<Type, Type, Func<object, object>> convertFunctionsByToTypeByFromType = new DoubleDictionary<Type, Type, Func<object, object>>();
        private static object convertFunctionsByToTypeByFromTypeLockObject = new object();

        private static DoubleDictionary<Type, Type, Func<object, object>> convertFunctionsByFromTypeByToType = new DoubleDictionary<Type, Type, Func<object, object>>();
        private static object convertFunctionsByFromTypeByToTypeLockObject = new object();

        private static Dictionary<Type, Type> preferredConcreteTypesByInterface = new Dictionary<Type, Type>() {
            { typeof(IList), typeof(Array<object>) },
            { typeof(IEnumerable), typeof(Array<object>) },
            { typeof(ICollection), typeof(Array<object>) }
        };

        private static Dictionary<Type, Type> preferredGenericTypesByGenericInterface = new Dictionary<Type, Type>() {
            { typeof(IList<>), typeof(Array<>) },
            { typeof(IEnumerable<>), typeof(Array<>) },
            { typeof(ICollection<>), typeof(Array<>) }
        };

        private static Func<object, object> returnNullFunction = o => null;

        public static TTo Convert<TFrom, TTo>(TFrom from) {
            return (TTo)Convert(from, typeof(TFrom));
        }

        public static object Convert<T>(T value, Type toType) {
            object o;
            if (!TryConvert(value, toType, out o)) {
                throw new Exception();
            }
            return o;
        }

        public static T Convert<T>(object value) {
            object o;
            if (!TryConvert(value, typeof(T), out o)) {
                throw new Exception();
            }
            return (T)o;
        }

        public static Func<object, object> ConvertFunction(Type fromType, Type toType) {
            Func<object, object> convertFunction;
            if (!TryGetConvertFunction(fromType, toType, true, out convertFunction)) {
                throw new Exception();
            }
            return convertFunction;
        }

        [WhatItIs("Whether a value is convertible to the given type.")]
        public static bool Convertible(object fromObject, Type toType) {
            Func<object, object> dummyConvertFunction;
            return TryGetConvertFunction(fromObject, toType, out dummyConvertFunction);
        }

        public static object Identity(object o) {
            return o;
        }

        [WhatItIs("Whether a value is convertible to the given type.")]
        public static bool TryGetConvertFunction(object fromObject, Type toType, out Func<object, object> convertFunction) {
            if (fromObject == null) {
                if (toType.IsValueType) {
                    convertFunction = null;
                    return false;
                }
                convertFunction = returnNullFunction;
                return true;
            }
            Type fromType = fromObject.GetType();
            return TryGetConvertFunction(fromType, toType, out convertFunction);
        }

        [WhatItIs("Whether a value is convertible to the given type.")]
        public static bool Convertible(Type fromType, Type toType) {
            Func<object, object> dummyConvertFunction;
            return TryGetConvertFunction(fromType, toType, out dummyConvertFunction);
        }

        //[WhatItIs("Whether some values are convertible to some types.")]
        //public static bool TryGetConvertFunctions(IEnumerable fromObjects, IEnumerable<Type> toTypes, out Array<Func<object, object>> convertFunctions) {
        //    Array<object> fromObjectsArray = Array<object>.From(fromObjects);
        //    Array<Type> toTypesArray = Array<Type>.From(toTypes);
        //    MutableArray<Func<object, object>> convertFunctionsSoFar = new MutableArray<Func<object,object>>();
        //    for (long i = 0; i < fromObjectsArray.Length; ++i) {
        //        Func<object, object> conversionFunction;
        //        if (!TryGetConvertFunction(fromObjectsArray[i], toTypesArray[i], out conversionFunction)) {
        //            convertFunctions = null;
        //            return false;
        //        }
        //        convertFunctionsSoFar.Append(conversionFunction);
        //    }
        //    convertFunctions = convertFunctionsSoFar.Current;
        //    return true;
        //}

        public static void RegisterConvertFunction(Type fromType, Type toType, Func<object, object> convertFunction) {
            lock (convertFunctionsByToTypeByFromTypeLockObject) {
                convertFunctionsByToTypeByFromType[fromType, toType] = convertFunction;
                ////convertFunctionsByToTypeByFromType.Add(fromType, toType, convertFunction);
            }
        }

        public static void Test() {
            Testing.Test(typeof(Conversion).Name, () => {
                Testing.Test("TryConvert", () => {
                    Testing.Test("cast 'a' to 'a'", () => {
                        char from = 'a';
                        object result;
                        if (!TryConvert(from, typeof(char), out result)) {
                            return false;
                        }
                        if ((char)result != 'a') {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("cast 'a' to 97", () => {
                        char from = 'a';
                        object result;
                        if (!TryConvert(from, typeof(int), out result)) {
                            return false;
                        }
                        if ((int)result != 97) {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("implicit string \"A\" to String \"A\"", () => {
                        string from = "A";
                        object result;
                        if (!TryConvert(from, typeof(String), out result)) {
                            return false;
                        }
                        if ((String)result != (String)"A") {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("Array ('a', 'b', 'c') to Array (97, 98, 99)", () => {
                        Array<char> from = Array<char>.From('a', 'b', 'c');
                        object result;
                        if (!TryConvert(from, typeof(Array<int>), out result)) {
                            return false;
                        }
                        Array<int> resultArray = (Array<int>)result;
                        if (resultArray[0] != 97 && resultArray[1] != 98 && resultArray[2] != 99) {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("Array ('a', 'b', 'c') to int[] (97, 98, 99)", () => {
                        Array<char> from = Array<char>.From('a', 'b', 'c');
                        object result;
                        if (!TryConvert(from, typeof(int[]), out result)) {
                            return false;
                        }
                        int[] resultArray = (int[])result;
                        if (resultArray[0] != 97 && resultArray[1] != 98 && resultArray[2] != 99) {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("Array ('a', 'b', 'c') to List<int> (97, 98, 99)", () => {
                        Array<char> from = Array<char>.From('a', 'b', 'c');
                        object result;
                        if (!TryConvert(from, typeof(List<int>), out result)) {
                            return false;
                        }
                        List<int> resultArray = (List<int>)result;
                        if (resultArray[0] != 97 && resultArray[1] != 98 && resultArray[2] != 99) {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("IEnumerable ('a', 'b', 'c') to List<int> (97, 98, 99)", () => {
                        ArrayList from = new ArrayList(new char[] { 'a', 'b', 'c' });
                        object result;
                        if (!TryConvert(from, typeof(List<int>), out result)) {
                            return false;
                        }
                        List<int> resultArray = (List<int>)result;
                        if (resultArray[0] != 97 && resultArray[1] != 98 && resultArray[2] != 99) {
                            return false;
                        }
                        return true;
                    });
                    Testing.Test("ArrayGlom ('a', 'b', 'c') to List<int> (97, 98, 99)", () => {
                        Glom from = Glom.From(Array<char>.From('a', 'b', 'c'));
                        object result;
                        if (!TryConvert(from, typeof(List<int>), out result)) {
                            return false;
                        }
                        List<int> resultArray = (List<int>)result;
                        if (resultArray[0] != 97 && resultArray[1] != 98 && resultArray[2] != 99) {
                            return false;
                        }
                        return true;
                    });
                });
            });
        }

        public static bool TryConvert<TFrom, TTo>(TFrom from, out TTo to) {
            object toObject;
            bool succeeded = TryConvert(from, typeof(TTo), out toObject);
            to = (TTo)toObject;
            return succeeded;
        }

        public static bool TryConvert([MayBeNull] object value, Type toType, out object o) {
            // Handle null
            if (value == null) {
                o = null;
                if (toType.IsValueType) {
                    return false;
                }
                return true;
            }
            return TryConvert(value, value.GetType(), toType, out o);
        }

        public static bool TryConvert(object original, Type fromType, Type toType, out object converted) {
            // If it's directly castable, do that (includes same type, native cast, assignable cast, and custom casts)
            if (Reflection.TryCast(original, fromType, toType, out converted)) {
                return true;
            }

            Func<object, object> convertFunction;
            if (TryGetConvertFunction(fromType, toType, out convertFunction)) {
                converted = convertFunction(original);
                return true;
            }

            // That didn't work out. Try to find a constructor that takes this value as an IEnumerable
            IEnumerable enumerableOriginal = original as IEnumerable;
            ConstructorInfo constructor;
            Array<object> arguments;
            if (enumerableOriginal != null && Reflection.TryGetConstructor(Array<object>.From(enumerableOriginal), toType, out constructor, out arguments)) {
                converted = constructor.Invoke(arguments.Block);
                return true;
            }

            converted = null;
            return false;
        }

        public static bool TryGetConvertFunction(Type fromType, Type toType, out Func<object, object> convertFunction) {
            return TryGetConvertFunction(fromType, toType, true, out convertFunction);
        }

        public static bool TryGetConvertFunction(Type fromType, Type toType, bool allowCustomConversion, out Func<object, object> convertFunction) {
            if (TryGetConvertFunctionForExactTypes(fromType, toType, allowCustomConversion, out convertFunction)) {
                return true;
            }

            // Go through all possiblities of types implemented by fromType, to types that are either toType or the favorite
            // implementation of toType
            Array<Type> closestTypesImplementedByFromType = Reflection.ImplementedTypes(fromType).Subarray(1);
            Type toClass;
            if (!TryGetPreferredImplementingClass(toType, out toClass)) {
                goto fail;
            }

            ///// figure out if fromType is IEnumerable<> and send that to each call of TryGetConvertFunctionForExactTypes 

            foreach (Type fromTypeToTry in closestTypesImplementedByFromType) {
                if (TryGetConvertFunctionForExactTypes(fromTypeToTry, toClass, allowCustomConversion, out convertFunction)) {
                    return true;
                }
            }

            // If the from type is a Map or Dictionary from names to values, try to create a new instance with that
            //////

        fail:
            // Remember that this one didn't exist
            RegisterConvertFunction(fromType, toType, null);
            convertFunction = null;
            return false;
        }

        private static Dictionary<Type, Func<object, object>> ConvertFunctionsByType(Type fromType) {
            Dictionary<Type, Func<object, object>> convertFunctionsByType;
            if (!convertFunctionsByToTypeByFromType.TryGetDictionary(fromType, out convertFunctionsByType)) {
                convertFunctionsByType = new Dictionary<Type,Func<object,object>>();
                /////
                convertFunctionsByToTypeByFromType.Add(fromType, convertFunctionsByType);
            }
            return convertFunctionsByType;
        }

        private static bool TryGetPreferredImplementingClass(Type type, out Type implementingClass) {
            // If type is already a class, that's the one
            if (type.IsValueType || type.IsClass) {
                implementingClass = type;
                return true;
            }

            // Type is an interface
            // If exact interface is represented in preferred list, use that
            if (preferredConcreteTypesByInterface.TryGetValue(type, out implementingClass)) {
                return true;
            }

            // If it's a specific generic like IExample<int> but not represented in exact list, might be represented in <> list
            if (type.IsGenericType && !type.IsGenericTypeDefinition) {
                Array<Type> typeArguments;
                Type genericType = Reflection.Components(type, out typeArguments);
                if (typeArguments.Length == 1) {
                    Type preferredGenericType;
                    if (preferredGenericTypesByGenericInterface.TryGetValue(genericType, out preferredGenericType)) {
                        implementingClass = preferredGenericType.MakeGenericType(typeArguments[0]);
                        return true;
                    }
                }
            }

            implementingClass = null;
            return false;
        }

        [return: Note("False if definitely doesn't exist.")]
        private static bool TryGetConvertFunctionForExactTypes(Type fromType, Type toType, bool allowCustomConversion, out Func<object, object> convertFunction) {
            // Conversions found or not found should be registered here

            // Conversions to the same type are trivial and never stored
            if (Object.ReferenceEquals(fromType, toType)) {
                convertFunction = Identity;
                return true;
            }

            // If we're lucky the function is already stored
            if (convertFunctionsByToTypeByFromType.TryGetValue(fromType, toType, out convertFunction)) {
                // Could have been stored as not existing
                bool found = (convertFunction != null);
                return found;
            }

            // Otherwise maybe there's a known cast? This includes same type, native cast, downcast, and implicit cast operators
            if (Reflection.TryGetCastFunction(fromType, toType, out convertFunction)) {
                RegisterConvertFunction(fromType, toType, convertFunction);
                return true;
            }

            // From and constructor functions to the from type
            if (Reflection.TryFindInitializer(toType, fromType, out convertFunction)) {
                RegisterConvertFunction(fromType, toType, convertFunction);
                return true;
            }

            // Nothing existed. Before moving on to enumerable to enumerable...are there any initializers from IEnumerable or IEnumerable<>?
            if (allowCustomConversion) {
                // Maybe the types are enumerable. See if the left one is exactly IEnumerable or IEnumerable<T>
                Type fromItemType = null;
                bool fromTypedEnumerable = Reflection.TypedEnumerable(fromType, out fromItemType);
                bool useFromType = fromTypedEnumerable;

                // If this type is typed enumerable, then use that. Otherwise...
                if (!useFromType) {
                    bool fromEnumerable = fromType == typeof(IEnumerable);
                    if (fromEnumerable) {
                        bool fromImplementsTypedEnumerable = Reflection.ImplementsTypedEnumerable(fromType, out fromItemType);
                        if (!fromImplementsTypedEnumerable) {
                            useFromType = true;
                        }
                    }
                }

                // See if the right one implements IEnumerable or IEnumerable<T>
                //bool fromTypedEnumerable = Reflection.TypedEnumerable(fromType, out fromItemType);
                bool toEnumerable = Reflection.HasInterface(toType, typeof(IEnumerable));
                Type toItemType = null;
                bool toTypedEnumerable = Reflection.ImplementsTypedEnumerable(toType, out toItemType);

                // If IEnumerable on both sides, convert to Array<fromType> or Array<object>, then maybe to Array<toType>, then to the target type
                if (useFromType && (toEnumerable || toTypedEnumerable)) {
                    FullyTypedEnumerableConversion conversion;
                    if (FullyTypedEnumerableConversion.TryCreate(fromType, fromItemType, toItemType, toType, out conversion)) {
                        convertFunction = conversion.Convert;
                        RegisterConvertFunction(fromType, toType, convertFunction);
                        return true;
                    }
                }
            }

            // Otherwise try to map names to names
            /////

            convertFunction = null;
            return false;
        }

        private class FullyTypedEnumerableConversion {
            private Func<object, object> convertToFirstArrayFunction;
            private Func<object, object> convertFromSecondArrayFunction;
            private MethodInfo convertArrayMethod;

            public static bool TryCreate(Type fromType, Type fromItemType, Type toItemType, Type toType, out FullyTypedEnumerableConversion conversion) {
                conversion = new FullyTypedEnumerableConversion();

                //// needs to deal with null/non-typed enumerables
                Type firstArrayType = fromItemType != null ?
                    Reflection.SpecificType(typeof(Array<>), fromItemType) :
                    typeof(Array<object>);
                if (!Conversion.TryGetConvertFunction(fromType, firstArrayType, out conversion.convertToFirstArrayFunction)) {
                    conversion = null;
                    return false;
                }

                // If there is no second IEnumerable<T> type, make one
                if (toItemType == null) {
                    ////
                    throw new NotImplementedException();
                } else {
                    Type secondArrayType = Reflection.SpecificType(typeof(Array<>), toItemType);
                    if (!Conversion.TryGetConvertFunction(secondArrayType, toType, false, out conversion.convertFromSecondArrayFunction)) {
                        conversion = null;
                        return false;
                    }
                    //// Should try shoring up with a type-parametered Array.Convert(Type) function
                    MethodInfo genericArrayConvertMethod = Reflection.GetMethod(firstArrayType, BindingFlags.Public | BindingFlags.Instance, "Convert");
                    conversion.convertArrayMethod = genericArrayConvertMethod.MakeGenericMethod(new Type[] { toItemType });
                }
                return true;
            }

            public object Convert(object fromValue) {
                object firstArrayObject = convertToFirstArrayFunction(fromValue);
                object secondArrayObject = convertArrayMethod.Invoke(firstArrayObject, null);
                return convertFromSecondArrayFunction(secondArrayObject);
            }
        }
    }
}

////// Need to find o through all possibilities: all implemented types of fromType to all supported types that implement toType
////Array<Type> fromTypes = Reflection.ImplementedTypes(fromType);
////foreach (Type fromTypeToTry in fromTypes) {
////    Dictionary<Type, Func<object, object>> convertFunctionsByToType = ConvertFunctionsFromType(fromTypeToTry);
////    foreach (KeyValuePair<Type, Func<object, object>> pair in convertFunctionsByToType) {
////        // See if any of this type's implemented classes is the desired to type
////        Array<Type> toTypeImplementedTypes = Reflection.ImplementedTypes(pair.Key);
////        if (toTypeImplementedTypes.Contains(toType)) {
////            convertFunction = pair.Value;
////            RegisterConvertFunction(fromType, toType, convertFunction);
////            return true;
////        }
////    }
////}