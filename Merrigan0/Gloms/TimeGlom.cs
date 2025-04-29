using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.GlomsInternal {
    [Untested]
    internal class TimeGlom : Glom {
        static TimeGlom() {
            // Register this in expressions
            // Plus
            PlusExpression.RegisterCalculationFunction(typeof(PlusExpression), typeof(TimeGlom), typeof(TimeIntervalGlom), PlusTimeTimeInterval);

            // Minus
            MinusExpression.RegisterCalculationFunction(typeof(MinusExpression), typeof(TimeGlom), typeof(TimeIntervalGlom), MinusTimeTimeInterval);
            MinusExpression.RegisterCalculationFunction(typeof(MinusExpression), typeof(TimeGlom), typeof(TimeGlom), MinusTimeTime);

            // LessThan
            LessExpression.RegisterCalculationFunction(typeof(LessExpression), typeof(TimeGlom), typeof(TimeGlom), LessTimeTime);
        }

        private DateTimeOffset time;

        public TimeGlom(DateTimeOffset time) {
            this.time = time;
        }

        public TimeGlom(DateTime time) {
            this.time = new DateTimeOffset(time);
        }

        private static object PlusTimeTimeInterval(object time, object timeInterval) {
            ////
            return null;
        }

        private static object MinusTimeTimeInterval(object time, object timeInterval) {
            ////
            return null;
        }

        private static object MinusTimeTime(object time, object timeInterval) {
            ////
            return null;
        }

        private static object LessTimeTime(object time1, object time2) {
            ////
            return null;
        }
    }
}
