using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Actions;

namespace Executions {
    public class Prerequisite {
        public object Constant;
        public Action Expression;

        public Prerequisite(object constantOrExpression) {
            Action expression = constantOrExpression as Action;
            if (expression != null) {
                Expression = expression;
            } else {
                Constant = constantOrExpression;
            }
        }
    }

    ///// MOVE TO FILE
    public class NamedExpression {
        public Expression Expression { get; private set; }
        public string Name { get; private set; }

        public NamedExpression(string name, Expression expression) {
            Expression = expression;
            Name = name;
        }
    }

    ///// MOVE TO FILE
    public class Property {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public Property(string name, object value) {
            Name = name;
            Value = value;
        }
    }

    public abstract partial class Action {
        public static readonly Action Dummy = new DummyAction();

        public IList<Prerequisite> Prerequisites { get; protected set; }
        ////public IList<Property> AdditionalProperties;
        ////public IList<NamedExpression> PrerequisiteNamedExpressions;

        ////// (name, {constant|expression})*
        ////protected Action(params object[] namesAndPrerequisites) {
        ////    // Digest the parameter specifications into list of deferred values and constant values
        ////    IList<KeyValuePair<string, object>> properties = Utilities.ToPairs<string, object>(namesAndPrerequisites);
        ////    List<Property> additionalPropertiesSoFar = new List<Property>();
        ////    List<NamedExpression> prerequisiteExpressionsSoFar = new List<NamedExpression>();
        ////    foreach (KeyValuePair<string, object> property in properties) {
        ////        Expression expression = property.Value as Expression;
        ////        if (expression != null) {
        ////            prerequisiteExpressionsSoFar.Add(new NamedExpression(property.Key, expression));
        ////        } else {
        ////            additionalPropertiesSoFar.Add(new Property(property.Key, property.Value));
        ////        }
        ////    }
        ////    AdditionalProperties = additionalPropertiesSoFar;
        ////    PrerequisiteNamedExpressions = prerequisiteExpressionsSoFar;
        ////}

        // {constant|expression}*
        protected Action(params object[] prerequisites) {
            // Digest the parameter specifications into list of deferred values and constant values
            List<Prerequisite> prerequisitesSoFar = new List<Prerequisite>();
            foreach (object prerequisite in prerequisites) {
                prerequisitesSoFar.Add(new Prerequisite(prerequisite));
            }
            Prerequisites = prerequisitesSoFar;
        }

        public abstract Execution CreateExecution(Execution parent);

        ////public virtual Execution CreateExecution(Execution parent, params object[] namesAndValues) {
        ////    Execution execution = CreateExecution(parent);
        ////    IList<KeyValuePair<string, object>> properties = Utilities.ToPairs<string, object>(namesAndValues);
        ////    List<NamedConstantOrExpression> parametersSoFar = new List<NamedConstantOrExpression>();
        ////    foreach (KeyValuePair<string, object> property in properties) {
        ////        parametersSoFar.Add(new NamedConstantOrExpression(property.Value, property.Key));
        ////    }
        ////    return execution;
        ////}
    }
}
