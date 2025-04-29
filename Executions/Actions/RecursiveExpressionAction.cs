using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    //// MAYBE SHOULD BRING BACK CALLACTION
    public class RecursiveCallAction : Action {
        public int LevelsUp { get; private set; }

        public RecursiveCallAction(int levelsUp, params object[] namesAndPrerequisites) :
            base(namesAndPrerequisites) {
            LevelsUp = levelsUp;
        }

        public override Execution CreateExecution(Execution parent) {
            return new RecursiveCallExecution(parent, this);
        }
    }
}