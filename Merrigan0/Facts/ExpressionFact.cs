using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FactsInternal {
    ///*
    //// * if (expressionFact.Evaluate(context)) {
    //// *     ... }
    //// */

    ////[Untested]
    ////public class ExpressionFact : Expression {
    ////    private Expression 
    ////    public Fact Condition { get; private set; }
    ////    //public Fact Fact { get; private set; }

    ////    // Could be a POCO, Fact, or Glom
    ////    public object Value { get; private set; }

    ////    // value: could be a POCO, Fact, or Glom
    ////    public ExpressionFact(Fact condition, /*Fact fact*/object value) {
    ////        Condition = condition;
    ////        Value = value;
    ////    }

    ////    public override bool Disimplies(Fact fact) {
    ////        if (!Condition.Implies(/* ? */fact)) {
    ////            return false;
    ////        }
    ////        //// base on the nature of Value
    ////        return base.Disimplies(fact);
    ////    }

    ////    public override bool Implies(Fact fact) {
    ////        if (!Condition.Implies(/* ? */fact)) {
    ////            return false;
    ////        }
    ////        //// base on the nature of Value
    ////        return base.Disimplies(fact);
    ////    }
    ////}
}
