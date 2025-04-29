using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet;

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
    public abstract class TypedBinaryOperator<T> : Operator {
        // Type 1: left expression type
        // Type 2: right expression type
        protected DoubleDictionary<Type, Type, Func<object, object, object>> calculateFunctionsByTypes =
            new DoubleDictionary<Type, Type, Func<object, object, object>>();

        protected String Symbol { get; private set; }

        protected TypedBinaryOperator(String symbol) {
            Symbol = symbol;
        }

        public virtual bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            // If there's a function that matches these types exactly, use that
            if (calculateFunctionsByTypes.TryGetValue(leftType, rightType, out calculateFunction)) {
                return (calculateFunction != null);
            }

            // Try to find one for the closest common cast
            bool found = false;
            Type commonCastType;
            Func<object, object> leftCastFunction;
            Func<object, object> rightCastFunction;
            Func<object, object, object> calculateAfterCastsFunction = null;
            if (Reflection.TryGetUpcastFunctions(leftType, rightType, out commonCastType, out leftCastFunction, out rightCastFunction)) {
                if (calculateFunctionsByTypes.TryGetValue(commonCastType, commonCastType, out calculateAfterCastsFunction)) {
                    found = true;
                }
            }

            // Otherwise, create a new one that maps the types to good values
            if (!found) {
                // Go through all available and see if casts to those left and right types are available
                foreach (KeyValuePair<Type, Dictionary<Type, Func<object, object, object>>> leftTypePair in calculateFunctionsByTypes) {
                    Type functionLeftType = leftTypePair.Key;
                    if (!Reflection.TryGetCastFunction(leftType, functionLeftType, out leftCastFunction)) {
                        continue;
                    }

                    foreach (KeyValuePair<Type, Func<object, object, object>> rightTypePair in leftTypePair.Value) {
                        // Okay we've got a function that can take this combo of left type and right type
                        if (Reflection.TryGetCastFunction(rightType, rightTypePair.Key, out rightCastFunction)) {
                            calculateAfterCastsFunction = rightTypePair.Value;
                            found = true;
                            break;
                        }
                    }

                    if (found) { break; }
                }
            }

            if (found) {
                CastAndCalculate castAndCalculate = new CastAndCalculate(leftCastFunction, rightCastFunction, calculateAfterCastsFunction);
                calculateFunction = castAndCalculate.Execute;

                // Remember the new function
                RegisterCalculationFunction(leftType, rightType, calculateFunction);
                return true;
            }

            // Remember that there weren't any
            calculateFunction = null;
            RegisterCalculationFunction(leftType, rightType, calculateFunction);
            return false;
        }

        public T Run(object left, object right) {
            Type leftType = left.GetType();
            Type rightType = right.GetType();
            Func<object, object, object> calculateFunction;
            if (!TryGetCalculateFunction(leftType, rightType, out calculateFunction)) {
                throw new ArgumentException();
            }
            T result = (T)calculateFunction(left, right);
            return result;
        }

        public T Run<L, R>(L left, R right) {
            Func<object, object, object> calculateFunction;
            if (!TryGetCalculateFunction(typeof(L), typeof(R), out calculateFunction)) {
                throw new ArgumentException();
            }
            T result = (T)calculateFunction(left, right);
            return result;
        }

        public void RegisterCalculationFunction(Type leftType, Type rightType, Func<object, object, object> calculateFunction) {
            calculateFunctionsByTypes[leftType, rightType] = calculateFunction; //// threads!
        }

        public void RegisterTypedCalculationFunction<P>(Func<object, object, object> calculateFunction) {
            calculateFunctionsByTypes[typeof(P), typeof(P)] = calculateFunction; //// threads!
        }

        public override string ToString() {
            return Symbol;
        }

        protected class CastAndCalculate {
            private Func<object, object> leftCastFunction;
            private Func<object, object> rightCastFunction;
            private Func<object, object, object> calculateFunction;

            public CastAndCalculate(Func<object, object> leftCastFunction, Func<object, object> rightCastFunction, Func<object, object, object> calculateFunction) {
                this.leftCastFunction = leftCastFunction;
                this.rightCastFunction = rightCastFunction;
                this.calculateFunction = calculateFunction;
            }

            public object Execute(object left, object right) {
                object castLeft;
                if (leftCastFunction == null) {
                    castLeft = left;
                } else {
                    castLeft = leftCastFunction(left);
                }
                object castRight;
                if (rightCastFunction == null) {
                    castRight = right;
                } else {
                    castRight = rightCastFunction(right);
                }
                return calculateFunction(castLeft, castRight);
            }
        }
    }
}
