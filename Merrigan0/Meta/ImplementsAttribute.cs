using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("an attribute that marks a variable's value (a type) as being required to implement a particular type")]
    [Untested]
    public class ImplementsAttribute : ConstraintAttribute {
        private Expression typeExpression;

        public ImplementsAttribute(Type type) {
            typeExpression = new ConstantExpression<Type>(type);
        }
        public ImplementsAttribute(string nml) {
            typeExpression = Nml.Expression(nml);
        }

        protected override Expression CreateConstraint() {
            Expression newExpression = new ImplementsExpression(
                SubjectExpression,
                typeExpression);
            return newExpression;
        }
    }
}
