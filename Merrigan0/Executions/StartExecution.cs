using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ExecutionsInternal {
    internal class StartExecution : ChangeExecution {
        private ExecutorTime? startTime;

        public override ExecutorTime? StartTime { get { return startTime; } }

        public StartExecution(Execution baseExecution, ExecutorTime? startTime) :
            base(baseExecution) 
        {
            this.startTime = startTime;
        }
    }
}
