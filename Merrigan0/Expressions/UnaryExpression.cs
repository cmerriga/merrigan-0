using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet;

namespace Merrigan0 {
    /*
     * negate, bitwise not
     */
    [Untested]
    public abstract class UnaryExpression : Expression {
        // This expression type, base expression output type
        private static DoubleDictionary<Type, Type, Func<object, object>> calculateFunctionsByTypes =
            new DoubleDictionary<Type, Type, Func<object, object>>();

        protected Expression baseExpression;

        [MayBeNull(When = "no special prefix was defined")]
        protected virtual String Prefix { get { return null; } }

        private Func<object, object> calculateFunction;
        private Type mostRecentBaseValueType;
        private Func<object, object> mostRecentCalculateFunction;

        public override Array<Expression> Children { get { return Array<Expression>.From(baseExpression); } }

        protected UnaryExpression(Expression baseExpression) : this(baseExpression, null) { }

        protected UnaryExpression(Expression baseExpression, Type type) {
            this.baseExpression = baseExpression;

            // If possible, determine a calculate function that can be used every time
            Type baseExpressionType = baseExpression.Type;
            if (baseExpressionType != null) {
                TryGetCalculateFunction(baseExpressionType, out calculateFunction);
            }
        }

        public static void RegisterCalculationFunction(Type expressionType, Type baseExpressionType, Func<object, object> calculateFunction) {
            calculateFunctionsByTypes[expressionType, baseExpressionType] = calculateFunction;
        }

        //public static void RegisterTypedCalculationFunction<T>(Type expressionType, Func<object, object> calculateFunction) {
        //    calculateFunctionsByTypes[expressionType, typeof(T)] = calculateFunction;
        //}

        public override string ToString() {
            if (Prefix == null) {
                return base.ToString();
            }
            return Prefix;
            //return Prefix + baseExpression.ToString();
        }

        public override bool TryEvaluate(Glom context, out object value) {
            object baseValue;
            if (!baseExpression.TryEvaluate(context, out baseValue)) {
                goto fail;
            }

            // If we had found a consistent function to use, use it
            if (calculateFunction != null) {
                value = calculateFunction(baseValue);
                return true;
            }

            // See if the most recent function still applies. If so, use it 
            Type baseValueType = baseValue.GetType();
            if (baseValueType == mostRecentBaseValueType) {
                value = mostRecentCalculateFunction(baseValue);
                return true;
            }

            // Try to find a calculation function that takes this type
            Func<object, object> newCalculateFunction;
            if (TryGetCalculateFunction(baseValueType, out newCalculateFunction)) {
                mostRecentBaseValueType = baseValueType;
                mostRecentCalculateFunction = newCalculateFunction;
                value = mostRecentCalculateFunction(baseValue);
                return true;
            }

        fail:
            value = null;
            return false;
        }

        ////// All objects have to be type T
        ////protected abstract Func<object, object> ValueFunction(Type upcastType);

        // Try to get a known function that accepts types castable from left and right types
        public virtual bool TryGetCalculateFunction(Type baseType, out Func<object, object> calculateFunction) {
            Type expressionType = this.GetType();

            // If there's a function that matches these types exactly, use that
            if (calculateFunctionsByTypes.TryGetValue(expressionType, baseType, out calculateFunction)) {
                return true;
            }

            // Otherwise, create a new one that maps the types to good values

            // Find the available ones
            Dictionary<Type, Func<object, object>> calculateFunctionsByType;

            // Use the mappings for this expression type
            if (calculateFunctionsByTypes.TryGetDictionary(expressionType, out calculateFunctionsByType)) {
                foreach (KeyValuePair<Type, Func<object, object>> typePair in calculateFunctionsByType) {
                    // Okay we've got a function that can take this type
                    Type functionType = typePair.Key;

                    // Get a function that allows a cast from the expected left type. If none, skip
                    Func<object, object> baseExpressionCastFunction = null;
                    if (!functionType.IsAssignableFrom(expressionType)) {
                        if (!Reflection.TryGetCastFunction(expressionType, functionType, out baseExpressionCastFunction)) {
                            continue;
                        }
                    }

                    CastAndCalculate castAndCalculate = new CastAndCalculate(baseExpressionCastFunction, typePair.Value);
                    calculateFunction = castAndCalculate.Execute;
                    return true;
                }
            }

            // There weren't any
            calculateFunction = null;
            return false;
        }

        protected class CastAndCalculate {
            private Func<object, object> baseExpressionCastFunction;
            private Func<object, object> calculateFunction;

            public CastAndCalculate(Func<object, object> baseExpressionCastFunction, Func<object, object> calculateFunction) {
                this.baseExpressionCastFunction = baseExpressionCastFunction;
                this.calculateFunction = calculateFunction;
            }

            public object Execute(object baseValue) {
                object castBase;
                if (baseExpressionCastFunction == null) {
                    castBase = baseValue;
                } else {
                    castBase = baseExpressionCastFunction(baseValue);
                }
                return calculateFunction(castBase);
            }
        }
    }
}
