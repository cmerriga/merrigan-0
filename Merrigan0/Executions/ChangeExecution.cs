using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.MetaInternal;

namespace Merrigan0.ExecutionsInternal {
    internal class ChangeExecution : Merrigan0.Execution {
        protected Merrigan0.Execution baseExecution;

        public override Array<Execution> Children { get { return baseExecution.Children; } }
        public override CallGlom Context { get { return baseExecution.Context; } }
        public override double EstimatedProportionDone { get { return baseExecution.EstimatedProportionDone; } }
        public override Executor Executor { get { return baseExecution.Executor; } }
        public override bool Failed { get { return baseExecution.Failed; } }
        public override ExecutorTime? FinishTime { get { return baseExecution.FinishTime; } }
        public override bool Finished { get { return baseExecution.Finished; } }
        public override Execution Parent { get { return baseExecution.Parent; } }
        public override Glom Product { get { return baseExecution.Product; } }
        public override DateTimeOffset? ScheduledTime { get { return baseExecution.ScheduledTime; } }
        public override bool Started { get { return baseExecution.Started; } }
        public override bool Succeeded { get { return baseExecution.Succeeded; } }
        public override ExecutorTime? StartTime { get { return baseExecution.StartTime; } }

        public ChangeExecution(Merrigan0.Execution baseExecution) {
            this.baseExecution = baseExecution;
        }
    }
}
