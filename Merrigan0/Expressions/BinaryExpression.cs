using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet;
using Merrigan0.GlomsInternal;

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
    [Untested]
    [Concept("calculate", "takes two POCOs or gloms and creates another, including native + - * / etc.")]
    public abstract class BinaryExpression : Expression {
        // Type 1: total expression type
        // Type 2: left expression type
        // Type 3: right expression type
        protected static TripleDictionary<Type, Type, Type, Func<object, object, object>> calculateFunctionsByTypes =
            new TripleDictionary<Type, Type, Type, Func<object, object, object>>();

        protected virtual string Separator { get { return null; } }

        private Array<Expression> children;

        public override Array<Expression> Children { get { return children; } }
        public Expression Left { get { return children[0]; } }
        public Expression Right { get { return children[1]; } }

        private Func<object, object, object> calculateFunction;
        private Type mostRecentLeftType;
        private Type mostRecentRightType;
        private Func<object, object, object> mostRecentCalculateFunction;

        protected BinaryExpression(Expression left, Expression right) : this(left, right, null) { }

        protected BinaryExpression(Expression left, Expression right, Type type) : base(type) {
            children = Array<Expression>.From(left, right);

            // Determine a calculate function that can be used every time
            Type leftType = left.Type;
            Type rightType = right.Type;
            if (leftType != null && rightType != null) {
                TryGetCalculateFunction(leftType, rightType, out calculateFunction);
            }
        }

        [WhatItDoes("Registers a function that will perform an operation on two POCOs or gloms, creating a third.")]
        public static void RegisterCalculationFunction(Type expressionType, Type leftType, Type rightType, Func<object, object, object> calculateFunction) {
            calculateFunctionsByTypes[expressionType, leftType, rightType] = calculateFunction;
        }

        [WhatItDoes("Registers a function that will perform an operation on two similarly typed POCOs or gloms, " +
            "creating a third of the same type.")]
        public static void RegisterTypedCalculationFunction<T>(Type expressionType, Func<object, object, object> calculateFunction) {
            calculateFunctionsByTypes[expressionType, typeof(T), typeof(T)] = calculateFunction;
        }

        public override string ToString() {
            if (Separator == null) {
                return base.ToString();
            }
            return Separator;
            //return Left.ToString() + Separator + Right.ToString();
        }

        public override bool TryEvaluate(Glom context, out object value) {
            object left;
            if (!Left.TryEvaluate(context, out left)) {
                goto fail;
            }
            //ObjectGlom leftGlom = left as ObjectGlom;
            //if (leftGlom != null) {
            //    left = leftGlom.Value;
            //}
            Glom leftGlom = left as Glom;
            if (leftGlom != null) {
                left = leftGlom[Glom.ValueName];
            }

            object right;
            if (!Right.TryEvaluate(context, out right)) {
                goto fail;
            }
            //ObjectGlom rightGlom = right as ObjectGlom;
            //if (rightGlom != null) {
            //    right = rightGlom.Value;
            //}
            Glom rightGlom = right as Glom;
            if (rightGlom != null) {
                right = rightGlom[Glom.ValueName];
            }

            // If we had found a consistent function to use, use it
            if (calculateFunction != null) {
                value = calculateFunction(left, right);
                return true;
            }

            // See if the most recent function still applies. If so, use it 
            Type leftType = left.GetType();
            Type rightType = right.GetType();
            if (leftType == mostRecentLeftType && rightType == mostRecentRightType) {
                value = mostRecentCalculateFunction(left, right);
                return true;
            }

            // Try to find a calculation function that takes these two types
            Func<object, object, object> newCalculationFunction;
            if (TryGetCalculateFunction(leftType, rightType, out newCalculationFunction)) {
                mostRecentLeftType = leftType;
                mostRecentRightType = rightType;
                mostRecentCalculateFunction = newCalculationFunction;
                value = mostRecentCalculateFunction(left, right);
                return true;
            }
            
        fail:
            value = null;
            return false;
        }

        //public abstract bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction);

        // Try to get a known function that accepts types castable from left and right types
        [Note("Try not to use. Only used by SequenceAppendExpression and ListAppendExpression currently.")]
        public virtual bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            Type expressionType = this.GetType();
            //Func<object, object, object> calculateFunction;

            // If there's a function that matches these types exactly, use that
            if (calculateFunctionsByTypes.TryGetValue(expressionType, leftType, rightType, out calculateFunction)) {
                return true;
            }

            // Otherwise, create a new one that maps the types to good values

            // Find the available ones
            Dictionary<Type, Dictionary<Type, Func<object, object, object>>> calculateFunctionsByTypesForThisType;

            // Use the mappings for this expression type
            ///// get all available casts for each, especially ObjectGloms
            if (calculateFunctionsByTypes.TryGetDictionary(expressionType, out calculateFunctionsByTypesForThisType)) {
                foreach (KeyValuePair<Type, Dictionary<Type, Func<object, object, object>>> leftTypePair in calculateFunctionsByTypesForThisType) {
                    Type functionLeftType = leftTypePair.Key;

                    // See if there's a cast from this to anything in the known functions
                    foreach (KeyValuePair<Type, Func<object, object, object>> rightTypePair in leftTypePair.Value) {
                        // Okay we've got a function that can take this combo of left type and right type
                        Type functionRightType = rightTypePair.Key;

                        // Get a function that allows a cast from the expected left type. If none, skip
                        Func<object, object> leftCastFunction = null;
                        ////if (!functionLeftType.IsAssignableFrom(leftType)) { //// not what we want: sbyte is assignable from long :-(
                        //if (
                        if (!Reflection.TryGetCastFunction(leftType, functionLeftType, out leftCastFunction)) { //// maybe this is enough?
                            continue;
                        }
                        ////}

                        // Get a function that allows a cast from the expected right type. If none, skip
                        Func<object, object> rightCastFunction = null;
                        ////if (!functionRightType.IsAssignableFrom(rightType)) { //// not what we want: sbyte is assignable from long :-(
                        if (!Reflection.TryGetCastFunction(rightType, functionRightType, out rightCastFunction)) {
                            continue;
                        }
                        ////}

                        CastAndCalculate castAndCalculate = new CastAndCalculate(leftCastFunction, rightCastFunction, rightTypePair.Value);
                        calculateFunction = castAndCalculate.Execute;
                        return true;
                    }
                }
            }

            // There weren't any
            calculateFunction = null;
            return false;
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

    //[Untested]
    //public class ExampleBinaryExpression : BinaryExpression {
    //    static ExampleBinaryExpression() {
    //        RegisterCalculationFunction(
    //            typeof(ExampleBinaryExpression),
    //            typeof(Array<object>),
    //            typeof(object),
    //            (l, r) => ((Array<object>)l) + (object)r);
    //    }

    //    protected virtual string Separator { get { return " "; } }

    //    public ExampleBinaryExpression(Expression left, Expression right) : this(left, right, null) { }

    //    public ExampleBinaryExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

    //    public override string ToString() {
    //        return Left.ToString() + Separator + Right.ToString();
    //    }
    //}
}
