using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An expression that has a list expression or simple expression on the left, and an expression on the right.")]
    [Untested]
    public class PairExpression : BinaryExpression {
        static PairExpression() {
            RegisterCalculationFunction(
                typeof(PairExpression),
                typeof(object),
                typeof(object),
                (l, r) => Array<object>.From(l, r));
        }

        protected override string Separator { get { return ","; } }

        public PairExpression(Expression left, Expression right) : base(left, right) { }

        public PairExpression(Expression left, Expression right, Type type) : base(left, right, type) { }
    }
}
