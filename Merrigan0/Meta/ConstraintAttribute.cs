using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("marks a value as needing to comply with a condition, or a method as requiring a condition to be valid")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public abstract class ConstraintAttribute : Attribute {
        private Expression constraint;

        // Could be current context expression, or another expression, or null if the expression must still be resolved
        // lazily via the value of nml
        private Expression subjectExpression;

        // The untranslated definition of an expression. Null after SubjectExpression is lazily evaluated
        private string nml;

        public Expression Constraint {
            get {
                if (constraint == null) {
                    constraint = CreateConstraint();
                }
                return constraint;
            }
        }

        protected Expression SubjectExpression {
            get {
                if (subjectExpression == null) {
                    subjectExpression = Nml.Expression(nml);
                }
                return subjectExpression;
            }
        }

        // No arguments will cause the constraint to be based on the current context
        protected ConstraintAttribute() : this(null) { }

        // subject may be a string or an Expression
        protected ConstraintAttribute([MayBeNull] object subjectExpressionOrNml) {
            if (subjectExpressionOrNml == null) {
                subjectExpression = Expression.CurrentContext;
                return;
            }
            nml = subjectExpressionOrNml as string;
            if (nml == null) {
                subjectExpression = (Expression)subjectExpressionOrNml;
            }
        }

        // Returns something like a comparison or something that may be implied by the current context, or not
        // Maybe an expression based on the current context implies a fact.
        // Maybe the current context by itself implies a fact.
        // Use fact like: glomContext.Implies(constraint.Fact). methodCallReturnValueGlom.Implies(constraint.Fact)
        //      could be PositiveFact
        //      could be ExpressionFact(GreaterExpression(Expression, 0)) where ExpressionFacts 
        ////public abstract Fact Fact { get; }

        protected virtual Expression CreateConstraint() {
            throw new NotImplementedException();
        }

        //protected Array<ConstraintAttribute> Prerequisites { get { return Array<ConstraintAttribute>.Empty; } }

        //public bool AppendEvaluations(object value, MutableSet<ConstraintEvaluation> violatedConstraintsSoFar) {
        //    //foreach (ConstraintAttribute constraint in Prerequisites) {
        //    //    if (violatedConstraintsSoFar.Current.Contains(constraint))
        //    //    if (!constraint.Acceptable(value)) {
        //    //        violatedConstraintsSoFar.Append(constraint);
        //    //    }
        //    //}
        //    bool acceptableAtThisScope = Acceptable(value);
        //    return acceptableAtThisScope; /////
        //}

        //public abstract bool Acceptable(Scope context, object value);

        //public bool Acceptable(object value) { return Acceptable(null, value); }

        //public void GetViolations(object value) {
        //    // A hash set that doesn't really guarantee fidelity (could be hash collisions)
        //    MutableSet<ConstraintEvaluation> evaluationsSoFar = new MutableSet<ConstraintEvaluation>();
        //    AppendEvaluations(value, evaluationsSoFar);
        //}

        //public virtual object RandomValue(Type type) {
        //    int nAttempts = 0;
        //    while (nAttempts < 1000) {
        //        object value = Random.Value(type);
        //        if (Acceptable(value)) {
        //            return value;
        //        }
        //    }
        //    throw new Exception();
        //}
    }
}
