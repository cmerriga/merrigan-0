using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Merrigan0.ExecutionsInternal;

namespace Merrigan0.NmlInternal {
    //[Untested]
    //public class CustomOperator : Operator {
    //    private OperatorChildrenStyle childrenStyle;
    //    private Func<Expression, Expression> createOneChildExpression;
    //    private Func<Expression, Expression, Expression> createTwoChildExpression;
    //    private String text;
    //    private int tightness;

    //    public override OperatorChildrenStyle ChildrenStyle { get { return childrenStyle; } }
    //    public override String Text { get { return text; } }
    //    public override int Tightness { get { return tightness; } }

    //    public CustomOperator(String text, int tightness, Func<Expression, Expression> createOneChildExpression) :
    //        this(text, tightness) {
    //        this.childrenStyle = OperatorChildrenStyle.One;
    //        this.createOneChildExpression = createOneChildExpression;
    //    }

    //    public CustomOperator(String text, int tightness, Func<Expression, Expression, Expression> createTwoChildExpression) :
    //        this(text, tightness) {
    //        this.childrenStyle = OperatorChildrenStyle.Two;
    //        this.createTwoChildExpression = createTwoChildExpression;
    //    }

    //    protected CustomOperator(String text, int tightness) {
    //        this.text = text;
    //        this.tightness = tightness;
    //    }

    //    public override Expression CreateExpression(params Expression[] children) {
    //        if (ChildrenStyle == OperatorChildrenStyle.One) {
    //            // Check if types are compatible
    //            ////
    //            return createOneChildExpression(children[0]);
    //        } else if (ChildrenStyle == OperatorChildrenStyle.Two) {
    //            // Check if types are compatible
    //            ////
    //            return createTwoChildExpression(children[0], children[1]);
    //        } else {
    //            throw new NotImplementedException();
    //        }
    //    }
    //}
}
