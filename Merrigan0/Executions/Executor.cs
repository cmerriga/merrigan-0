using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.ExecutionsInternal;

namespace Merrigan0 {
    /*
     * For normal execution:
     *      1. Regular calls
     *          Action(...), Func(...)
     *          Execution gotten via LastExecution
     *      2. Async calls
     *          Execution Begin(...), Execution Begin(...), End(Execution[])
     *          Begins do nothing except put in current frame's list of stuff needing done
     *          
     * Data-programmed execution:
     *      1. Regular calls
     *          no calls - actions/chunks placed in queue
     *      2. Async calls
     *          no calls - actions/chunks placed in queue
     */
    // A time indicator that has a reasonable granularity for ClockTime (say 1 ms) and a number of
    // arbitrary steps after it
    [Untested]
    public struct ExecutorTime {
        public DateTimeOffset ClockTime;
        public long Ticks;
    }

    [WhatItIs("An object that controls the execution of arbitrary actions.")]
    [Untested]
    public abstract class Executor {
        public static Executor Ambient = new NormalExecutor();

        private static TimeSpan tenMilliseconds = TimeSpan.FromMilliseconds(10);
        
        private ExecutorTime time;

        ////private Action<object[]> onBeginExecution;
        ////private Action<object[]> onEndExecution;
        ////private Action<Executor> onBeginChunk;
        ////private Action<Executor> onEndChunk;

        [WhatItIs("A continually increasing number of chunks executed, starting at 0.")]
        public abstract long ChunksExecuted { get; }

        [WhatItIs("Whether there are any more executions running or scheduled.")]
        public abstract bool Empty { get; }

        // Will never be the same twice
        public ExecutorTime Time {
            get {
                ++time.Ticks;
                return time;
            }
            protected set {
                time = value;
            }
        }

        /*
         * The canonical way to call functions, such that they are executed in the mode of the current ambient
         * executor.
         */
        public abstract void Do(Action classAction);
        public abstract void Do<T1>(Action<T1> classAction, T1 parameter1);
        public abstract void Do<T1, T2>(Action<T1, T2> classAction, T1 parameter1, T2 parameter2);
        public abstract void Do<T1, T2, T3>(Action<T1, T2, T3> classAction, T1 parameter1, T2 parameter2, T3 parameter3);
        public abstract void Do<T1, T2, T3, T4>(Action<T1, T2, T3, T4> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
        public abstract void Do<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
        public abstract void Do<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6);
        public abstract void Do<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7);
        public abstract void Do<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8);

        public abstract MutableExecution BeginAction(Action classAction);
        public abstract MutableExecution BeginAction<T1>(Action<T1> classAction, T1 parameter1);
        public abstract MutableExecution BeginAction<T1, T2>(Action<T1, T2> classAction, T1 parameter1, T2 parameter2);
        public abstract MutableExecution BeginAction<T1, T2, T3>(Action<T1, T2, T3> classAction, T1 parameter1, T2 parameter2, T3 parameter3);
        public abstract MutableExecution BeginAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
        public abstract MutableExecution BeginAction<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
        public abstract MutableExecution BeginAction<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6);
        public abstract MutableExecution BeginAction<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7);
        public abstract MutableExecution BeginAction<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8);

        public abstract MutableExecution BeginFunc<TResult>(Func<TResult> memberFunction);
        public abstract MutableExecution BeginFunc<T1, TResult>(Func<T1, TResult> memberFunction, T1 parameter1);
        public abstract MutableExecution BeginFunc<T1, T2, TResult>(Func<T1, T2, TResult> memberFunction, T1 parameter1, T2 parameter2);
        public abstract MutableExecution BeginFunc<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3);
        public abstract MutableExecution BeginFunc<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
        public abstract MutableExecution BeginFunc<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
        public abstract MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6);
        public abstract MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7);
        public abstract MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8);

        public abstract void DoNextChunk();

        [WhatItDoes("Executes all known executions and those they spawn, for a maximum time.")]
        [return: WhatItIs("Whether all known executions have been done.")]
        public virtual bool Execute(TimeSpan maxTime) {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime + maxTime;
            int maxIterations = 100;
            while (!Empty && DateTime.UtcNow < endTime) {
                int nIterationsSoFar = 0;
                while (!Empty) {
                    DoNextChunk();
                    ++nIterationsSoFar;
                    if (nIterationsSoFar > maxIterations) {
                        return false;
                    }
                }
            }
            return true;
        }

        public abstract TResult Call<TResult>(Func<TResult> memberFunction);
        public abstract TResult Call<T1, TResult>(Func<T1, TResult> memberFunction, T1 parameter1);
        public abstract TResult Call<T1, T2, TResult>(Func<T1, T2, TResult> memberFunction, T1 parameter1, T2 parameter2);
        public abstract TResult Call<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3);
        public abstract TResult Call<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
        public abstract TResult Call<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
        public abstract TResult Call<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6);
        public abstract TResult Call<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7);
        public abstract TResult Call<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8);

        public abstract void RemoveCurrent();

        public void Schedule(MutableExecution execution) { Schedule(execution, DateTimeOffset.UtcNow); }

        public abstract void Schedule(MutableExecution execution, DateTimeOffset time);

        //public override void ScheduleBlocking(Execution execution) { ScheduleBlocking(execution, DateTimeOffset.UtcNow); }
        
        //public abstract void ScheduleBlocking(Execution execution, DateTimeOffset time);

        [DiagnosticOnly]
        [Test]
        public virtual void Test() {
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

        [WhatItDoes("Updates the ClockTime property to be based off the current wall clock.")]
        protected virtual void UpdateClockTime() {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            if (now != time.ClockTime) {
                time.ClockTime = now;
                time.Ticks = 0;
            }
        }

        public abstract void WaitUntilFinished(Array<MutableExecution> executions);

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
