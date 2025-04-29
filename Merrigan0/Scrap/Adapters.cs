//using System;
//using System.Collections.Generic;

//namespace Merrigan0 {
//    public static class Adapters {
//        private static ListDictionary<Type, Adapter> adaptersFromType = new ListDictionary<Type, Adapter>();
//        private static ListDictionary<Type, Adapter> adaptersToType = new ListDictionary<Type, Adapter>();

//        /// <summary>
//        /// Reset after any addition/removal of adapters.
//        /// </summary>
//        private static Dictionary<TypePair, Adapter> rememberedAdapters = new Dictionary<TypePair, Adapter>();

//        public static TTo As<TFrom, TTo>(TFrom value) {
//            // Null begets a default only if the value's destinationType is nullable
//            if (value == null) {
//                if (typeof(TFrom).IsValueType) {
//                    return default(TTo);
//                } else {
//                    return (TTo)(object)value;
//                }
//            }

//            Adapter<TFrom, TTo> adapter;
//            if (TryFind<TFrom, TTo>(out adapter)) {
//                return adapter.To(value);
//            }
//            throw new InvalidCastException(string.Format("Could not adapt a value of type {0} to type {1}.", typeof(TFrom).Name, typeof(TTo).Name));
//        }

//        public static T As<T>(object value) {
//            Type type = (value == null) ? typeof(object) : value.GetType();
//            Adapter adapter;
//            if (TryFind(type, typeof(T), out adapter)) {
//                return (T)adapter.To(value);
//            }
//            throw new InvalidCastException(string.Format("Could not adapt a value of type {0} to type {1}.", type.Name, typeof(T).Name));
//        }

//        public static bool TryFind(Type fromType, Type toType, out Adapter adapter) {
//            int dummy;
//            return TryFind(fromType, toType, null, out adapter, out dummy);
//        }

//        public static bool TryFind<TFrom, TTo>(out Adapter<TFrom, TTo> adapter) {
//            Adapter untypedAdapter;
//            if (TryFind(typeof(TFrom), typeof(TTo), out untypedAdapter)) {
//                adapter = (Adapter<TFrom, TTo>)untypedAdapter;
//                return true;
//            }
//            adapter = null;
//            return false;
//        }

//        /// <summary>
//        /// Tries to find a chain of adapters that will convert a value from one destinationType to another.
//        /// </summary>
//        /// <param name="fromType"></param>
//        /// <param name="toType"></param>
//        /// <param name="adapter"></param>
//        /// <param name="chainLength">One or more.</param>
//        /// <returns></returns>
//        private static bool TryFind(Type fromType, Type toType, int? maxChainLength, out Adapter adapter, out int chainLength) {
//            // If there's a remembered one for this pair, or it is remembered that this pair is unsupported, return it
//            TypePair typePair = new TypePair(fromType, toType);
//            if (rememberedAdapters.TryGetValue(typePair, out adapter)) {
//                // If it was remembered not to have an adapter for this pair, return false
//                if (adapter == null) {
//                    chainLength = 0;
//                    return false;
//                }

//                // Return success
//                ChainAdapter chainAdapter = adapter as ChainAdapter;
//                chainLength = chainAdapter == null ? 1 : chainAdapter.Length;
//                return true;
//            }

//            // See if one of those adapters converts to the desired destinationType
//            List<Adapter> fromAdapters = adaptersFromType[fromType];
//            foreach (Adapter fromAdapter in fromAdapters) {
//                // If this adapter converts to exactly the right destinationType, use it
//                if (fromAdapter.ToType == toType) {
//                    adapter = fromAdapter;
//                    rememberedAdapters.Add(typePair, adapter);
//                    chainLength = 1;
//                    return true;
//                }
//            }

//            // If a direct cast can be made, return that
//            if (toType.IsAssignableFrom(fromType)) {
//                adapter = new CastAdapter(fromType, toType);
//                rememberedAdapters.Add(typePair, adapter);
//                chainLength = 1;
//                return true;
//            }

//            // Get a chain if we are allowed to have more than one
//            int? bestChainLength = null;
//            if (!maxChainLength.HasValue || maxChainLength > 1)
//            {
//                // Assume we start with one of the from adapters available, and try to find an adapter that goes from that one's
//                // output to our goal output
//                foreach (Adapter fromAdapter in fromAdapters) {
//                    int returnedChainLength;
//                    if (TryFind(fromAdapter.ToType, toType, bestChainLength.HasValue ? bestChainLength - 1 : bestChainLength, out adapter, out returnedChainLength)) {
//                        // If chain length is 1, then we can't do better than this
//                        if (returnedChainLength == 1) {
//                            adapter = new ChainAdapter(fromAdapter, adapter);
//                            chainLength = returnedChainLength + 1;
//                            return true;
//                        }

//                        // Remember this chain length as the best we've found so far
//                        bestChainLength = returnedChainLength;
//                    }
//                }
//            }

//            // If none were ever found, bestChainLength will have remained unset
//            if (!bestChainLength.HasValue) {
//                // Remember the adapter if it was for sure not a conscious choice not to continue searching
//                if (!maxChainLength.HasValue) {
//                    rememberedAdapters.Add(typePair, null);
//                }

//                // Return false
//                adapter = null;
//                chainLength = 0;
//                return false;
//            }

//            // Return true
//            rememberedAdapters.Add(typePair, adapter);
//            chainLength = bestChainLength.Value;
//            return true;
//        }

//        private struct TypePair {
//            public Type FromType;
//            public Type ToType;

//            public TypePair(Type fromType, Type toType) {
//                FromType = fromType;
//                ToType = toType;
//            }
//        }
//    }
//}
