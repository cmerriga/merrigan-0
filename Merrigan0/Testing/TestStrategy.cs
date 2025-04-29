using System;
using System.Collections;
using System.Reflection;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.GlomsInternal;

namespace Merrigan0 {
    //// put these in own file
    public abstract class TestStrategy {
        ////public abstract bool MustMoveOn(TestExecution execution);
        public abstract String Description(Test test);
        
        public abstract bool Run(CompiledTest test, object[] arguments, out String errorMessage);
    }
}
