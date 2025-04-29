using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MetaInternal {
    [Untested]
    public enum ExecutionState : uint {
        NotStarted = 0x1,
        StartRequested = NotStarted << 1,
        Running = StartRequested << 1,
        PauseRequested = Running << 1,
        Paused = PauseRequested << 1,
        ResumeRequested = Paused << 1,
        CancelRequested = ResumeRequested << 1,
        Completed = CancelRequested << 1,
        Canceled = Completed << 1,
        Failed = Canceled << 1,
        PossiblyActiveMask = Running | PauseRequested | ResumeRequested | CancelRequested,
        DoneMask = Completed | Canceled | Failed,
        PossiblyDoneMask = DoneMask | CancelRequested
    };
}
