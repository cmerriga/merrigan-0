using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ExecutionsInternal {
    internal class FinishExecution : ChangeExecution {
        private ExecutorTime? finishTime;
        private Glom product;

        public override ExecutorTime? FinishTime { get { return finishTime; } }
        public override Glom Product { get { return baseExecution.Product; } }

        public FinishExecution(Execution baseExecution, Glom product, ExecutorTime? finishTime) :
            base(baseExecution) 
        {
            this.finishTime = finishTime;
            this.product = product;
        }
    }
}
