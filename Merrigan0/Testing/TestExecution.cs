using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Merrigan0 {
    using Console = System.Console;

    [Untested]
    public enum TestStatus {
        Undetermined,
        Succeeding,
        Succeeded,
        Failing,
        Failed,
        Skipped,
        NothingToTest
    }

    [Untested]
    public class TestExecution : /*Merrigan0.ExecutionsInternal.Execution, */IDisposable {
        private static WriteOptions defaultOptions = new WriteOptions(
            7 /*//// 5*/, 
            ConsoleColor.Green, 
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Gray,
            false);

        private object[] arguments;
        private MutableArray<TestExecution> childTestsSoFar = new MutableArray<TestExecution>();
        private Exception exceptionEncountered;
        private TestStatus statusAtThisLevel;
        private String resultMessage;
        private bool testSkipped;

        [MayBeNull(When = "no test is running")]
        public static TestExecution Ambient { get; protected set; }

        [MayBeNull(When = "a test is running but there have been no exceptions")]
        //[MayBeNotValid(When = "no test is running")]
        public static Exception CurrentException { get; protected set; }

        public static RandomGenerator Random = new RandomGenerator();

        public Array<TestExecution> ChildTests { get { return childTestsSoFar.Current; } }

        // May be null, if not finished or there was none
        [MayBeNull(When = "not failed or no message was recorded")]
        public String FailureMessage {
            get {
                if (!Finished) {
                    return null;
                }
                if (resultMessage != null) {
                    return resultMessage;
                }
                if (childTestsSoFar.Current.Length > 0) {
                    long nFailed = ChildTests.HowMany(c => c.Failed);
                    resultMessage = nFailed + " out of " + ChildTests.Length + " failed";
                }
                return null;
            }
        }

        public bool Failed { get { return Status == TestStatus.Failed; } }

        public bool Finished {
            get {
                return Status == TestStatus.Skipped ||
                    Status == TestStatus.Failed ||
                    Status == TestStatus.Succeeded;
            }
        }

        public TestExecution ParentTestExecution { get; private set; }
        ////public bool TestStarted { get { return TestStatus != TestStatus.Undetermined } }

        public TestStatus Status {
            get {
                if ((statusAtThisLevel == TestStatus.Undetermined) && Multiple) {
                    bool anyFailed = ChildTests.Any(t => t.Failed);
                    bool anySucceeded = ChildTests.Any(t => t.Succeeded);
                    if (anyFailed) {
                        return TestStatus.Failing;
                    }
                    if (anySucceeded) {
                        return TestStatus.Succeeding;
                    }
                }
                return statusAtThisLevel;
            }
        }

        public bool StopRequested { get; protected set; }
        public bool Succeeded { get { return Status == TestStatus.Succeeded; } }
        public Test Test { get; private set; }

        protected bool Multiple { get { return ChildTests.Length > 0; } }

        public TestExecution(Test test, params object[] arguments) {
            this.arguments = arguments;
            Test = test;
            ParentTestExecution = Ambient;
            if (ParentTestExecution != null) {
                ParentTestExecution.AddChild(this);
            }
            Ambient = this;
        }

        // Common to Execution
        public void AddChild(TestExecution child) {
            childTestsSoFar.Append(child);
        }

        public virtual void Dispose() {
            if (!Finished) {
                OnFinished();
            }
        }

        ////public void ReportChildChanged(TestExecution child) {
        ////}

        // Could be called explicitly from inline code, or by a compiled test
        public void ReportFailed() {
            ReportFailed((String)null);
        }

        // Could be called explicitly from inline code, or by a compiled test
        public void ReportFailed(Exception exception) {
            statusAtThisLevel = TestStatus.Failed;
            this.exceptionEncountered = exception; ////
            OnFinished();
        }

        // Could be called explicitly from inline code, or by a compiled test
        public void ReportFailed(String failureMessage) {
            statusAtThisLevel = TestStatus.Failed;
            this.resultMessage = failureMessage;
            OnFinished();
        }

        public void ReportFinished() {
            OnFinished();
        }

        public void ReportSkipped(String skipReason = null) {
            testSkipped = true;
            resultMessage = skipReason;
            OnFinished();
        }

        public void ReportStarted() {
            statusAtThisLevel = TestStatus.Undetermined;
        }

        // Could be called explicitly from inline code, or by a compiled test
        public void ReportSucceeded() {
            statusAtThisLevel = TestStatus.Succeeded;
            OnFinished();
        }

        public void RequestStop() {
            StopRequested = true;
        }

        /*
         * Stringifies this as one line.
         */
        public override string ToString() {
            return Status.ToString().ToLowerInvariant();
        }

        public void Write(IndentedConsole console, WriteOptions options = null) {
            if (options == null) {
                options = defaultOptions;
            }
            Write(console, 0, options);
        }

        protected virtual void OnFinished() {
            if (testSkipped) {
                statusAtThisLevel = TestStatus.Skipped;
            }
            if (!Succeeded && !Failed) {
                bool anyFailed = ChildTests.Any(t => t.Failed);
                if (TestExecution.CurrentException != null) {
                    exceptionEncountered = TestExecution.CurrentException;
                    anyFailed = true;
                }
                bool anySucceeded = ChildTests.Any(t => t.Succeeded);
                if (anyFailed) {
                    statusAtThisLevel = TestStatus.Failed;
                } else if (anySucceeded) {
                    statusAtThisLevel = TestStatus.Succeeded;
                } else {
                    statusAtThisLevel = TestStatus.NothingToTest;
                }
            }
            Ambient = ParentTestExecution;
        }

        protected virtual void OnStarted() {
            Ambient = this;
        }

        protected virtual void Write(IndentedConsole console, int depth, WriteOptions options = null) {
            // Description
            console.Append((Test == null ? (String)"[unknown]" : Test.Description) + ": ");
            
            // Passing indication
            ConsoleColor previousColor = Console.ForegroundColor;
            if (Succeeded) {
                Console.ForegroundColor = options.SuccessColor;
                console.Append("passed");
            } else if (Failed) {
                Console.ForegroundColor = options.FailureColor;
                console.Append("failed");
            } else if (Status == TestStatus.NothingToTest) {
                console.Append("[nothing to test]");
            } else {
                Console.ForegroundColor = options.WarningColor;
                console.Append(Status.ToString().ToLowerInvariant());
            }
            if (!Succeeded && FailureMessage != null) {
                console.In();
                console.Append(FailureMessage);
                console.Out();
            } else {
                console.EndLine();
            }
            Console.ForegroundColor = previousColor;

            // Children if any
            if (ChildTests != null) {
                int childDepth = depth + 1;
                if (childDepth <= options.MaxDepth) {
                    console.In();
                    foreach (TestExecution child in ChildTests) {
                        child.Write(console, childDepth, options);
                    }
                    console.Out();
                }
            }
        }

        public class WriteOptions {
            public ConsoleColor FailureColor { get; private set; }
            public int MaxDepth { get; private set; }
            public ConsoleColor NeutralColor { get; private set; }
            public bool OnlyFailures { get; private set; }
            public ConsoleColor SuccessColor { get; private set; }
            public ConsoleColor WarningColor { get; private set; }

            public WriteOptions(
                int maxDepth,
                ConsoleColor successColor,
                ConsoleColor failureColor,
                ConsoleColor warningColor,
                ConsoleColor neutralColor,
                bool onlyFailures) {
                FailureColor = failureColor;
                MaxDepth = maxDepth;
                NeutralColor = neutralColor;
                OnlyFailures = onlyFailures;
                SuccessColor = successColor;
                WarningColor = warningColor;
            }
        }
    }
}

//// For now, until executions can be used
////// May be null, if not finished
////public TestResult Result {
////    get {
////        if (!Finished) {
////            return null;
////        }


////        if (Succeeded) {
////            return new SingleTestResult(
////    }
////}

////public void ReportResult(bool succeeded) {
////}


////public void ReportResult(bool succeeded, String message) {
////}

////public void ReportResult(bool succeeded, Exception exception) {
////    if (
////}
