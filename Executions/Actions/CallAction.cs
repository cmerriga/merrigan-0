using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    ////public class CallAction : Action {
    ////    // The thing that will be called
    ////    public Action Action { get; private set; }

    ////    // The values and expressions that must be resolved
    ////    public IList<KeyValuePair<string, Expression>> NamedExpressions { get; private set; }
    ////    public IList<KeyValuePair<string, object>> NamedValues { get; private set; }

    ////    public CallAction(Action action, params object[] namesAndConstantsOrExpressions) {
    ////        Action = action;

    ////        // Pre-process the parameters as much as possible
    ////        List<KeyValuePair<string, Expression>> namedExpressionsSoFar = new List<KeyValuePair<string,Expression>>();
    ////        List<KeyValuePair<string, object>> namedValuesSoFar = new List<KeyValuePair<string,object>>();
    ////        IList<KeyValuePair<string, object>> namedConstantsOrExpressions = Utilities.ToPairs<string, object>(namesAndConstantsOrExpressions);
    ////        foreach (KeyValuePair<string, object> namedConstantOrExpression in namedConstantsOrExpressions) {
    ////            Expression expression = namedConstantOrExpression.Value as Expression;
    ////            if (expression != null) {
    ////                namedExpressionsSoFar.Add(new KeyValuePair<string, Expression>(namedConstantOrExpression.Key, expression));
    ////            } else {
    ////                namedValuesSoFar.Add(namedConstantOrExpression);
    ////            }
    ////        }
    ////    }

    ////    public override Execution CreateExecution(Execution parent) {
    ////        Execution execution = new CallExecution(this, parent);
    ////        return execution;
    ////    }
    ////}
}
