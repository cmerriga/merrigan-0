using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class TimeIntervalGlom : Glom {
        static TimeIntervalGlom() {
            // Register this in expressions
            // Plus
            PlusExpression.RegisterCalculationFunction(typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), PlusTimeIntervalTimeInterval);

            // Minus
            MinusExpression.RegisterCalculationFunction(typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), MinusTimeIntervalTimeInterval);

            // Less
            LessExpression.RegisterCalculationFunction(typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), typeof(TimeIntervalGlom), LessTimeIntervalGlomTimeIntervalGlom);
        }

        private TimeSpan timeSpan;

        public TimeIntervalGlom(TimeSpan timeSpan) {
            this.timeSpan = timeSpan;
        }

        public override object As(Type type) {
            if (type == typeof(TimeSpan)) {
                return timeSpan;
            }
            throw new InvalidCastException();
        }

        // interval1 and interval2 must be castable to TimeSpan. Return value is TimeSpan.
        private static object PlusTimeIntervalTimeInterval(object interval1, object interval2) {
            TimeSpan sum = Reflection.Cast<TimeSpan>(interval1) + Reflection.Cast<TimeSpan>(interval2);
            return sum;
        }

        // interval1 and interval2 must be castable to TimeSpan. Return value is TimeSpan.
        private static object MinusTimeIntervalTimeInterval(object interval1, object interval2) {
            TimeSpan difference = Reflection.Cast<TimeSpan>(interval1) - Reflection.Cast<TimeSpan>(interval2);
            return difference;
        }

        // interval1 and interval2 must be castable to TimeSpan. Return value is bool.
        private static object LessTimeIntervalGlomTimeIntervalGlom(object interval1, object interval2) {
            return Reflection.Cast<TimeSpan>(interval1) < Reflection.Cast<TimeSpan>(interval2);
        }
    }
}
