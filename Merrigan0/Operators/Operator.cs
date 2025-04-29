using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public abstract class Operator {
        public static readonly Operator BitwiseAnd = BitwiseAndOperator.Only;
        public static readonly Operator BitwiseNot = BitwiseNotOperator.Only;
    }
}
