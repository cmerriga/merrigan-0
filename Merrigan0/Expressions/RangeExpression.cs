using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class RangeExpression : Expression {
        private Expression expression;
        ////private double max;
        ////private string maxNml;
        ////private bool maxInclusive;
        ////private double min;
        ////private string minNml;
        ////private bool minInclusive;

        public RangeExpression(Expression subjectExpression, object minOrMinNml, bool minInclusive, object maxOrMaxNml, bool maxInclusive) {
            Expression maxExpression;
            string maxNml = maxOrMaxNml as string;
            if (maxNml == null) {
                maxExpression = new ConstantExpression((double)maxOrMaxNml);
            } else {
                maxExpression = Merrigan0.Nml.Expression(maxNml);
            }

            Expression minExpression;
            string minNml = minOrMinNml as string;
            if (minNml == null) {
                minExpression = new ConstantExpression((double)minOrMinNml);
            } else {
                minExpression = Merrigan0.Nml.Expression(minNml);
            }

            expression = new LogicalAndExpression(
                minInclusive ?
                    (Expression)new GreaterOrEqualsExpression(subjectExpression, minExpression) :
                    (Expression)new GreaterExpression(subjectExpression, minExpression),
                maxInclusive ?
                    (Expression)new LessOrEqualsExpression(subjectExpression, maxExpression) :
                    (Expression)new LessExpression(subjectExpression, maxExpression));
        }

        public RangeExpression([Example("(0.0, 1.0]")] string definition) {
            //RecognitionTree tree;
            //RangeRecognizer.Only.TryRecognize(definition, out tree);
            throw new NotImplementedException();
        }

        public override bool TryEvaluate(Glom context, out object value) {
            return expression.TryEvaluate(context, out value);
        }
    }
}
