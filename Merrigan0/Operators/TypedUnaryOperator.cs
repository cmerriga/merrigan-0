using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    /*
     * add
     * subtract
     * multiply
     * divide
     * equal, g, ge, l, le
     * transform, transform with parameters
     * constant: number, string, bool
     * and, or
     */
    [WhatItIs("An operator that takes two non-null, unknown type objects, and returns a value of a known type.")]
    [Untested]
    public abstract class TypedUnaryOperator<T> : Operator {
        protected Dictionary<Type, Func<object, object>> calculateFunctionsByType =
            new Dictionary<Type, Func<object, object>>();

        protected String Symbol { get; private set; }

        protected TypedUnaryOperator(String symbol) {
            Symbol = symbol;
        }

        public virtual bool TryGetCalculateFunction(Type type, out Func<object, object> calculateFunction) {
            // If there's a function that matches these types exactly, use that
            if (calculateFunctionsByType.TryGetValue(type, out calculateFunction)) {
                return (calculateFunction != null);
            }

            // Go through all available and see if a cast to that type is available\
            Func<object, object> castFunction = null;
            Func<object, object> calculateAfterCastFunction = null;
            foreach (KeyValuePair<Type, Func<object, object>> pair in calculateFunctionsByType) {
                Type functionType = pair.Key;
                if (Reflection.TryGetCastFunction(type, functionType, out castFunction)) {
                    calculateAfterCastFunction = pair.Value;
                    break;
                }
            }

            if (castFunction == null) {
                goto fail;
            }

            CastAndCalculate castAndCalculate = new CastAndCalculate(castFunction, calculateAfterCastFunction);
            calculateFunction = castAndCalculate.Execute;

            // Remember the new function
            RegisterCalculationFunction(type, calculateFunction);
            return true;

        fail:
            // Remember that there weren't any
            calculateFunction = null;
            RegisterCalculationFunction(type, calculateFunction);
            return false;
        }

        public T Run(object o) {
            Type type = o.GetType();
            Func<object, object> calculateFunction;
            if (!TryGetCalculateFunction(type, out calculateFunction)) {
                throw new ArgumentException();
            }
            T result = (T)calculateFunction(o);
            return result;
        }

        public T Run<V>(V value) {
            Func<object, object> calculateFunction;
            if (!TryGetCalculateFunction(typeof(V), out calculateFunction)) {
                throw new ArgumentException();
            }
            T result = (T)calculateFunction(value);
            return result;
        }

        public void RegisterCalculationFunction(Type type, Func<object, object> calculateFunction) {
            calculateFunctionsByType[type] = calculateFunction; //// threads!
        }

        ////public void RegisterTypedCalculationFunction<P>(Func<object, object> calculateFunction) {
        ////    calculateFunctionsByType[typeof(P), typeof(P)] = calculateFunction; //// threads!
        ////}

        public override string ToString() {
            return Symbol;
        }

        protected class CastAndCalculate {
            private Func<object, object> castFunction;
            private Func<object, object> calculateFunction;

            public CastAndCalculate(Func<object, object> castFunction, Func<object, object> calculateFunction) {
                this.castFunction = castFunction;
                this.calculateFunction = calculateFunction;
            }

            public object Execute(object o) {
                object castObject = castFunction(o);
                return calculateFunction(castObject);
            }
        }
    }
}
