using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.TestingInternal;

namespace Merrigan0 {
    // All the aspects of a test that are common to all executions of it.
    // Links to
    //      result history
    [Untested]
    public abstract class Test /*: Task*/ {
        public String Description { get; private set; }

        public Test(String description) {
            Description = description;
        }

        // Use to begin a using bracket:
        //      using (TestExecution test = TestExecution.Begin("Try this")) {
        public static TestExecution Begin(String description) {
            ImplicitTest test = new ImplicitTest(description);
            TestExecution testExecution = new TestExecution(test);
            testExecution.ReportStarted();
            return testExecution;
        }

        // Could be called explicitly from inline code, or by a compiled test
        public static void ReportFailed() {
            TestExecution.Ambient.ReportFailed();
        }

        // Could be called explicitly from inline code, or by a compiled test
        public static void ReportFailed(Exception exception) {
            TestExecution.Ambient.ReportFailed(exception);
        }

        // Could be called explicitly from inline code, or by a compiled test
        public static void ReportFailed(String failureMessage) {
            TestExecution.Ambient.ReportFailed(failureMessage);
        }

        public static void ReportFinished() {
            TestExecution.Ambient.ReportFinished();
        }

        ////public static void ReportResult(bool succeeded) {
        ////}


        ////public static void ReportResult(bool succeeded, String message) {
        ////}

        ////public static void ReportResult(bool succeeded, Exception exception) {
        ////    if (
        ////}

        public static void ReportSkipped(String skipReason = null) {
            TestExecution.Ambient.ReportSkipped(skipReason);
        }

        // Could be called explicitly from inline code, or by a compiled test
        public static void ReportStarted() {
            TestExecution.Ambient.ReportStarted();
        }

        // Could be called explicitly from inline code, or by a compiled test
        public static void ReportSucceeded() {
            TestExecution.Ambient.ReportSucceeded();
        }

        public static void RequestStop() {
            TestExecution.Ambient.RequestStop();
        }

        public static void Write(IndentedConsole console) {
            TestExecution.Ambient.Write(console);
        }
    }
}

//    //// Should really be the test itself, not executions of it
//    //// Test executions could take the place of TestResults
//    public abstract class Test {
//        private MutableArray<TestResult> childResultsSoFar = new MutableArray<TestResult>();
//        protected Test parent;
//        protected bool failed;
//        protected String failureMessage;
//        protected TestResult result;
//        protected bool skipped;
//        protected bool succeeded;

//        public static Test Current { get; protected set; }

//        public String Description { get; private set; }

//        [MayBeNull]
//        public TestResult Result {
//            get {
//                if (!Stopped) {
//                    return null;
//                }

//                // Create the result if it hasn't been done yet
//                if (result == null) {
//                    Array<TestResult> childResults = childResultsSoFar.Current;
//                    if (succeeded) {
//                        result = new SingleTestResult(this, true);
//                    } else if (failed) {
//                        result = new SingleTestResult(this, false);
//                    } else {
//                        result = null;
//                    }
//                }

//                return result;
//            }
//        }

//        public bool Stopped { get { return failed || succeeded || skipped; } }

//        public Test(String description) {
//            Description = description;
//            parent = Current;
//            if (parent != null && parent.Stopped) {
//                skipped = true;
//            }
//            Current = this;
//        }

//        // Returns all the executions in the given context
//        //public Array<TestExecution> Executions(Glom context);

//        public void ReportChildResult(TestResult result) {
//            childResultsSoFar.Append(result);
//        }

//        public void ReportFailed() {
//            ReportFailed(null);
//        }

//        public void ReportFailed(String failureMessage) {
//            failed = true;
//            this.failureMessage = failureMessage;
//            if (parent != null) {
//                parent.ReportChildResult(Result);
//            }
//        }

//        public void ReportResult(bool succeeded, String message) {
//            if (succeeded) {
//                ReportSucceeded();
//            } else {
//                ReportFailed(message);
//            }
//        }

//        public void ReportSucceeded() {
//            succeeded = true;
//            if (parent != null) {
//                parent.ReportChildResult(Result);
//            }
//        }

//        public override string ToString() {
//            return Description;
//        }

//        protected virtual String GetFailureMessage() {
//            return failureMessage;
//        }
//    }

//    // A test that is executed as inline code, i.e., not with a predefined Run() function
//    public class ImplicitTest : Test, IDisposable {
//        public ImplicitTest(String description) : base(description) { }

//        public virtual void Dispose() {
//            // Report the result
//            if (!skipped) {

//                //TestResult result;
//                //if (succeeded) {
//                //    result = new SingleTestResult(this, true);
//                //} else if (failed) {
//                //    result = new SingleTestResult(this, false);
//                //} else {
//                //    result = null;
//                //}

//                if (result != null) {
//                    parent.ReportChildResult(Result);
//                }
//            }

//            Current = parent;
//        }
//    }

//    // A test that is pre-coded, not wrapped around inline code
//    public abstract class CompiledTest : Test {
//        public CompiledTest(String description) : base(description) { }

//        public void Run(params object[] arguments) {
//            if (skipped) {
//                return;
//            }
//            ReallyRun(arguments);
//            if (parent != null) {
//                parent.ReportChildResult(Result);
//            }
//        }

//        protected abstract void ReallyRun(object[] arguments);
//    }
//}
