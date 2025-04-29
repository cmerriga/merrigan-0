using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0.NmlInternal {
    // If you want an explicitly-typed expression, wrap with a TypedExpression.
    [Untested]
    public class NmlExpression : Expression {
        private String nml;
        private Lazy<Expression> lazyExpression;

        public override Array<Expression> Children { get { return Array<Expression>.From(lazyExpression.Value); } }

        public NmlExpression(String nml) : this(nml, null) { }

        public NmlExpression(String nml, Type type) : 
            base(null) 
        {
            lazyExpression = new Lazy<Expression>(() => Merrigan0.Nml.Expression(nml));
            this.nml = nml;
        }

        //// Why wouldn't this evaluate successfully?
        public override bool TryEvaluate(Glom context, out object value) {
            return lazyExpression.Value.TryEvaluate(context, out value);
        }
    }
}
