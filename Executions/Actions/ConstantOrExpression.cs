namespace Executions.Actions {
    public class ConstantOrExpression<T> {
        public bool IsExpression {
            get { 
                return Expression != null;
            }
        }

        public ConstantOrExpression(object value) {
            Expression<T> expression = value as Expression<T>;
            if (expression != null) {
                Expression = expression;
            }
            Constant = (T)value;
        }

        public T Constant;
        public Expression<T> Expression;
    }
}
