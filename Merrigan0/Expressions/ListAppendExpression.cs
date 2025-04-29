using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An expression that has a list expression or simple expression on the left, and an expression on the right.")]
    [Untested]
    public class ListAppendExpression : BinaryExpression {
        static ListAppendExpression() {
            RegisterCalculationFunction(
                typeof(ListAppendExpression),
                typeof(Array<object>),
                typeof(object),
                (l, r) => ((Array<object>)l) + r);
            ////RegisterCalculationFunction(
            ////    typeof(ListAppendExpression),
            ////    typeof(object),
            ////    typeof(object),
            ////    (l, r) => Array<object>.From(l, r));
        }

        protected override string Separator { get { return ","; } }

        public ListAppendExpression(Expression left, Expression right) : base(left, right) { }

        public ListAppendExpression(Expression left, Expression right, Type type) : base(left, right, type) { }
    }
}
