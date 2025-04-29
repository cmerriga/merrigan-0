using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.ExecutionsInternal;
using Merrigan0.MetaInternal;

namespace Merrigan0 {
    [Untested]
    public enum ExecutionStage {
        NotStarted = 0,
        Started,
        Finished
    }

    public enum ExecutionRequest {
        None = 0,
        Run,
        Pause,
        Resume,
        Cancel
    }

    //public enum ExecutionStatus {
    //    NotStarted,
    //    RunRequested,
    //    Running,
    //    CancelRequested,
    //    Canceled,
    //    PauseRequested,
    //    Paused,
    //    ResumeRequested,
    //    Finished
    //}

    // Equivalent to a function call with arguments. Has inputs/context defined at creation time, as a single glom. Intended for an Executor to
    // call DoChunk() on it until done. Produces outputs as a single glom, with possibly overlapping names as inputs.
    //// the output glom should be itself? something to make only the minimal new() operation per execution
    //// right now the glom is not used - or represents all input properties?
    //// also for a loop, maybe could support "virtual"/on-demand children
    //// strategy: things needing be fast are C# compiled fields. Other things are gloms accessed by names
    [Concept("finished", "done executing, not necessarily successfully")]
    [Concept("tracking", "activity is recorded as a tree of Executions")]
    [Concept("checking", "input and output parameters are checked according to their restrictions")]
    [Mutable]
    public class Execution : IParent<Execution> /*: Glom */{
        public static readonly String ReturnVariableName = "returnValue";

        protected Exception exceptionEncountered;
        private MutableArray<Execution> childrenSoFar = new MutableArray<Execution>();

        public virtual Array<Execution> Children { get { return Array<Execution>.Empty; } } //// { return childrenSoFar.Current; } }

        [WhatItIs("The parent execution, plus some input parameters overlaid.")]
        public virtual CallGlom Context { get; private set; }

        public virtual Executor Executor { get { return Context.Executor; } }

        [Normal]
        public virtual double EstimatedProportionDone { get { return 0.0; } }

        //[OnlyIf("Finished")]
        public virtual bool Failed { get { return false; } }

        [MayBeNull("!Finished")]
        public virtual ExecutorTime? FinishTime { get { return null; } }

        [WhatItIs("Whether it has finished running. May have failed, succeeded, or been canceled")]
        public virtual bool Finished { get { return Stage == ExecutionStage.Finished; } }

        public virtual Execution Parent { get; private set; }
        //public bool Paused { get; protected set; }

        [WhatItIs("The fruits of it. Commonly overlaid over the running context of the parent " +
            "execution after finishing")]
        [MayBeNull("!Finished || Failed")]
        public virtual Glom Product { get { return null; } }

        [WhatItIs("The time it is requested to start")]
        public virtual DateTimeOffset? ScheduledTime { get; set; }

        public virtual ExecutionStage Stage { get { return ExecutionStage.NotStarted; } }

        //public ExecutionStatus Status { get { return ExecutionStatus.NotStarted; } }

        public virtual bool Started { get { return Stage != ExecutionStage.NotStarted; } }

        // Common to Execution
        [MayBeNull("!Started")]
        public virtual ExecutorTime? StartTime { get { return null; } }

        public virtual bool Succeeded { get { return false; } }

        protected Execution() : this(null) { }

        [Concept("context", "everything that is needed to execute, in one glom")]
        protected Execution(CallGlom context/*, params object[] arguments*//*, Action action*/) {
            //this.action = action;
        //    Context = parent == null ? new SimpleContext() : parent.Context;
            //this.arguments = arguments;
            Context = (context == null) ? (CallGlom)Glom.GlobalCall : context;
        }

        // These must be named differently than the Call ones because real functions will be cast to actions.
        [Untested]
        public static void Do(Action classAction) {
            Executor.Ambient.Do(classAction);
        }

        [Untested]
        public static void Do<T1>(Action<T1> classAction, T1 parameter1) {
            Executor.Ambient.Do(classAction, parameter1);
        }

        [Untested]
        public static void Do<T1, T2>(Action<T1, T2> classAction, T1 parameter1, T2 parameter2) {
            Executor.Ambient.Do(classAction, parameter1, parameter2);
        }

        [Untested]
        public static void Do<T1, T2, T3>(Action<T1, T2, T3> classAction, T1 parameter1, T2 parameter2, T3 parameter3) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3);
        }

        [Untested]
        public static void Do<T1, T2, T3, T4>(Action<T1, T2, T3, T4> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3, parameter4);
        }

        [Untested]
        public static void Do<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        [Untested]
        public static void Do<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        [Untested]
        public static void Do<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        [Untested]
        public static void Do<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            Executor.Ambient.Do(classAction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        // These must be named differently than the Do ones because real functions will be cast to actions.
        [Untested]
        public static TResult Call<TResult>(Func<TResult> memberFunction) {
            return Executor.Ambient.Call(memberFunction);
        }

        [Untested]
        public static TResult Call<T1, TResult>(Func<T1, TResult> memberFunction, T1 parameter1) {
            return Executor.Ambient.Call(memberFunction, parameter1);
        }

        [Untested]
        public static TResult Call<T1, T2, TResult>(Func<T1, T2, TResult> memberFunction, T1 parameter1, T2 parameter2) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3, parameter4);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        [Untested]
        public static TResult Call<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            return Executor.Ambient.Call(memberFunction, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        [DiagnosticOnly]
        [Test]
        public static void DoTest() {
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction0); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction1, 1); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction2, 1, "2"); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction3, 1, "2", 3); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction4, 1, "2", 3, "4"); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction5, 1, "2", 3, "4", 5); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction6, 1, "2", 3, "4", 5, "6"); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction7, 1, "2", 3, "4", 5, "6", 7); });
            Testing.TestRunsSuccessfully(() => { Do(TestClass.StaticAction8, 1, "2", 3, "4", 5, "6", 7, "8"); });
            TestClass instance = new TestClass();
            Testing.TestRunsSuccessfully(() => { Do(instance.Action0); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action1, 1); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action2, 1, "2"); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action3, 1, "2", 3); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action4, 1, "2", 3, "4"); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action5, 1, "2", 3, "4", 5); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action6, 1, "2", 3, "4", 5, "6"); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action7, 1, "2", 3, "4", 5, "6", 7); });
            Testing.TestRunsSuccessfully(() => { Do(instance.Action8, 1, "2", 3, "4", 5, "6", 7, "8"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc0); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc1, 1); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc2, 1, "2"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc3, 1, "2", 3); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc4, 1, "2", 3, "4"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc5, 1, "2", 3, "4", 5); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc6, 1, "2", 3, "4", 5, "6"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc7, 1, "2", 3, "4", 5, "6", 7); });
            Testing.TestRunsSuccessfully(() => { int n = Call(TestClass.StaticFunc8, 1, "2", 3, "4", 5, "6", 7, "8"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func0); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func1, 1); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func2, 1, "2"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func3, 1, "2", 3); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func4, 1, "2", 3, "4"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func5, 1, "2", 3, "4", 5); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func6, 1, "2", 3, "4", 5, "6"); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func7, 1, "2", 3, "4", 5, "6", 7); });
            Testing.TestRunsSuccessfully(() => { int n = Call(instance.Func8, 1, "2", 3, "4", 5, "6", 7, "8"); });
        }

        [DiagnosticOnly]
        private class TestClass {
            public static void StaticAction0() { }
            public static void StaticAction1(int n1) { }
            public static void StaticAction2(int n1, string s2) { }
            public static void StaticAction3(int n1, string s2, int n3) { }
            public static void StaticAction4(int n1, string s2, int n3, string s4) { }
            public static void StaticAction5(int n1, string s2, int n3, string s4, int n5) { }
            public static void StaticAction6(int n1, string s2, int n3, string s4, int n5, string s6) { }
            public static void StaticAction7(int n1, string s2, int n3, string s4, int n5, string s6, int n7) { }
            public static void StaticAction8(int n1, string s2, int n3, string s4, int n5, string s6, int n7, string s8) { }
            public void Action0() { }
            public void Action1(int n1) { }
            public void Action2(int n1, string s2) { }
            public void Action3(int n1, string s2, int n3) { }
            public void Action4(int n1, string s2, int n3, string s4) { }
            public void Action5(int n1, string s2, int n3, string s4, int n5) { }
            public void Action6(int n1, string s2, int n3, string s4, int n5, string s6) { }
            public void Action7(int n1, string s2, int n3, string s4, int n5, string s6, int n7) { }
            public void Action8(int n1, string s2, int n3, string s4, int n5, string s6, int n7, string s8) { }
            public static int StaticFunc0() { return 0; }
            public static int StaticFunc1(int n1) { return 1; }
            public static int StaticFunc2(int n1, string s2) { return 2; }
            public static int StaticFunc3(int n1, string s2, int n3) { return 3; }
            public static int StaticFunc4(int n1, string s2, int n3, string s4) { return 4; }
            public static int StaticFunc5(int n1, string s2, int n3, string s4, int n5) { return 5; }
            public static int StaticFunc6(int n1, string s2, int n3, string s4, int n5, string s6) { return 6; }
            public static int StaticFunc7(int n1, string s2, int n3, string s4, int n5, string s6, int n7) { return 7; }
            public static int StaticFunc8(int n1, string s2, int n3, string s4, int n5, string s6, int n7, string s8) { return 8; }
            public int Func0() { return 0; }
            public int Func1(int n1) { return 1; }
            public int Func2(int n1, string s2) { return 2; }
            public int Func3(int n1, string s2, int n3) { return 3; }
            public int Func4(int n1, string s2, int n3, string s4) { return 4; }
            public int Func5(int n1, string s2, int n3, string s4, int n5) { return 5; }
            public int Func6(int n1, string s2, int n3, string s4, int n5, string s6) { return 6; }
            public int Func7(int n1, string s2, int n3, string s4, int n5, string s6, int n7) { return 7; }
            public int Func8(int n1, string s2, int n3, string s4, int n5, string s6, int n7, string s8) { return 8; }
        }
    }
}

//public static object Call(object o, string methodName, params object[] parameters) {
//    return o.GetType().GetMethod(methodName).Invoke(o, parameters);
//}

//public static T Call<T>(object o, string methodName, params object[] parameters) {
//    return (T)o.Call(methodName, parameters);
//}

//////// First have to resolve parameters given and assign to names in the context - well, no
//////// Then have to 
////[return: WhatItIs("whether the execution is finished")]
////public virtual bool DoChunk() {
////    if (!StartTime.HasValue) {
////        StartTime = Executor.Time;
////    }
////    bool done = ReallyDoChunk();
////    if (done) {
////        MarkDone();
////        return true;
////    }
////    return false;
////}

////[WhenItsCalled("by an Executor")]
////public void Finish() {
////    while (!DoChunk()) { }
////}

////public virtual void ReportChildDone(Execution child) { }

////[WhatItIs("Waits for the execution to finish.")]
////[WhenItsCalled("Often from another thread.")]
////public void WaitUntilFinished() { WaitUntilFinished(TimeSpan.FromMilliseconds(100)); }

////[WhatItIs("Waits for the execution to finish.")]
////[WhenItsCalled("Often from another thread.")]
////[Concept("delay", "lag time between true execution finish, and when this function returns")]
////public void WaitUntilFinished(TimeSpan maxDelay) {
////    // Start with a short wait and double until equal to max delay
////    TimeSpan nextDelay = TimeSpan.FromMilliseconds(1);
////    while (!Finished) {
////        Thread.Sleep(nextDelay);
////        if (nextDelay < maxDelay) {
////            nextDelay = nextDelay + nextDelay;
////            if (nextDelay > maxDelay) {
////                nextDelay = maxDelay;
////            }
////        }
////    }
////}

////[WhatItIs("The meat of the execution.")]
////[WhenItsCalled("Repeatedly until the execution is finished.")]
////[Note("May be called multiple times after execution is finished.")]
////[return: WhatItIs("Whether the execution is finished.")]
////protected abstract bool ReallyDoChunk();

////protected virtual void MarkDone() {
////    FinishTime = Executor.Time;
////    Parent.ReportChildDone(this);
////}
