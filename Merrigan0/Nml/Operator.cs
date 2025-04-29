using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Merrigan0.ExecutionsInternal;

namespace Merrigan0.NmlInternal {
    //[Untested]
    //public enum OperatorChildrenStyle {
    //    One,
    //    Two,
    //    Unlimited
    //};

    //// A thing you can find in a basic language parsing, like +, -, *, %, etc. Generally combines two expressions
    //// but a unary operator may accept a null right expression.
    //[Untested]
    //public abstract class Operator {
    //    public abstract OperatorChildrenStyle ChildrenStyle { get; }
    //    public abstract String Text { get; }
    //    public abstract int Tightness { get; }
    //    //public abstract Type Type { get; }

    //    public abstract Expression CreateExpression(params Expression[] children);
    //}

    //////public abstract class Operator<T> : Operator {
    //////    //public override Type Type { get { return typeof(T); } }

    //////    public override Expression CreateExpression(params Expression[] children) {
    //////        return CreateTypedExpression(children);
    //////    }

    //////    public abstract Expression<T> CreateTypedExpression(params Expression[] children);
    //////}
    //////public abstract class UnaryOperator<T> : Operator<T> {
    //////    public override OperatorChildrenStyle ChildrenStyle { get { return OperatorChildrenStyle.One; } }

    //////    protected UnaryOperator(String text, int tightness) : base(text, tightness) { }
    //////}

    //////public class BoolNotOperator : UnaryOperator<bool> {
    //////    private static String text = "~";

    //////    public override String Text { get { return text; } }

    //////    public override Expression CreateExpression(params Expression[] children) {

    //////    }

    //////}

    //////public abstract class BinaryOperator : Operator {
    //////    public override OperatorChildrenStyle ChildrenStyle { get { return OperatorChildrenStyle.Two; } }

    //////    protected BinaryOperator(String text, int tightness) : base(text, tightness) { }
    //////}
}
