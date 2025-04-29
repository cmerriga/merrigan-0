using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An indication that the given group value must contain at least one item.")]
    [Note("Checks for Length property to be > 0.")]
    [Untested]
    public class NotEmptyAttribute : ConstraintAttribute {
        private static Expression identifierExpression = new IdentifierExpression("Length");
        private static Expression zeroExpression = new ConstantExpression<long>(0L);

        public NotEmptyAttribute() { }
        public NotEmptyAttribute(string nml) : base(nml) { }

        protected override Expression CreateConstraint() {
            Expression newExpression = new NotEqualsExpression(
                new SequenceAppendExpression(SubjectExpression, identifierExpression),
                zeroExpression);
            return newExpression;
        }
    }
}
