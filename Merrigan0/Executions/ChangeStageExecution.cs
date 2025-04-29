using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ExecutionsInternal {
    internal class ChangeStageExecution : ChangeExecution {
        private ExecutionStage stage;

        public override ExecutionStage Stage { get { return stage; } }

        public ChangeStageExecution(Execution baseExecution, ExecutionStage stage) :
            base(baseExecution) 
        {
            this.stage = stage;
        }
    }
}
