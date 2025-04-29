using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0.ExecutionsInternal {
    internal class NormalActionExecution : MutableExecution {
        private Action action;

        public NormalActionExecution(Action action) {
            this.action = action;
        }

        protected override bool ReallyDoChunk() {
            action();
            return true;
        }
    }

    internal class NormalFuncExecution<TResult> : MutableExecution {
        private Func<TResult> func;

        public NormalFuncExecution(Func<TResult> func) {
            this.func = func;
        }

        protected override bool ReallyDoChunk() {
            Glom product = Glom.From(func());
            Current = new FinishExecution(Current, product, Current.Executor.Time);
            return true;
        }
    }
}
