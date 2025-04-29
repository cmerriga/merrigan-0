using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    ////[WhatItIs("An expression that wraps a list (or a single object) and exposes it as a single object.")]
    ////[Untested]
    ////public class ScalarExpression : Expression {
    ////    private Expression baseExpression;

    ////    public ScalarExpression(Expression baseExpression) { }

    ////    public override bool TryEvaluate(Glom context, out object value) {
    ////        return baseExpression.TryEvaluate(context, out value);
    ////    }
    ////    //public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
    ////    //    return LessOrEqualsOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
    ////    //}
    ////}
}
