using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public abstract class BinaryExpression<T> : Expression<T> {
        public object Left { get; private set; }
        public object Right { get; private set; }

        public BinaryExpression(object left, object right) : base(left, right) {
            Left = left;
            Right = right;
        }
    }
}
