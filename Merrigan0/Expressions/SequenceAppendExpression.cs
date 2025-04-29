using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An expression that has a list expression or simple expression on the left, and an expression on the right.")]
    [Untested]
    public class SequenceAppendExpression : BinaryExpression {
        static SequenceAppendExpression() {
            RegisterCalculationFunction(
                typeof(SequenceAppendExpression),
                typeof(Glom),
                typeof(Expression),
                (l, r) => ((Glom)l).Evaluate((Expression)r));
            RegisterCalculationFunction(
                typeof(SequenceAppendExpression),
                typeof(object),
                typeof(Expression),
                (l, r) => Glom.From(l).Evaluate((Expression)r));
        }

        protected override string Separator { get { return "."; } }

        public SequenceAppendExpression(Expression left, Expression right) : base(left, right) { }

        public SequenceAppendExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryEvaluate(Glom context, out object value) {
            object left;
            if (!Left.TryEvaluate(context, out left)) {
                goto fail;
            }
            Glom leftGlom = Glom.From(left);

            value = leftGlom.Evaluate(Right);
            return true;

        fail:
            value = null;
            return false;
        }
    }
}
