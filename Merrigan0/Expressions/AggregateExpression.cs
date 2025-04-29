using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    /*
     * min, max, total, average, median, etc.
     */
    [Untested]
    public abstract class AggregateExpression : Expression {
        private Expression group;

        public Expression Group { get { return group; } }

        ////private Func<object, object, object> cumulativeCalculateFunction;
        ////private Type mostRecentLeftType;
        ////private Type mostRecentRightType;
        ////private Func<object, object, object> mostRecentCalculateFunction;

        protected AggregateExpression(Expression group/*, Func<object, object, object> cumulativeCalculateFunction*/) : this(group/*, cumulativeCalculateFunction*/, null) { }

        protected AggregateExpression(Expression group/*, Func<object, object, object> cumulativeCalculateFunction*/, Type type) :
            base(type) {
            this.group = group;
            ////this.cumulativeCalculateFunction = cumulativeCalculateFunction;
        }

        public override bool TryEvaluate(Glom context, out object value) {
            // Should be IEnumerable
            object group;
            if (!Group.TryEvaluate(context, out group)) { goto fail; }
            IEnumerable enumerable = group as IEnumerable;
            if (enumerable == null) { goto fail; }
            value = Calculate(enumerable);
            return true;

        fail:
            value = null;
            return false;
        }

        [WhatItDoes("Calculates a single value based on the group values.")]
        protected abstract object Calculate(IEnumerable enumerable);
    }

    //[Untested]
    //public class ExampleAggregateExpression : AggregateExpression {

    //    public ExampleAggregateExpression(Expression group) : this(group, null) { }

    //    public ExampleAggregateExpression(Expression group, Type type) : base(group, type) { }

    //    public override string ToString() {
    //        return Left.ToString() + Separator + Right.ToString();
    //    }
    //}
}
