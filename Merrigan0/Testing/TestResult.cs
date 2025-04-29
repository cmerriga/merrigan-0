//using System;

//namespace Merrigan0 {
//    [Untested]
//    public abstract class TestResult {
//        private WriteOptions defaultOptions = new WriteOptions(5, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Gray, false);

//        public virtual Array<TestResult> Children { get { return null; } }
//        public String Message { get; protected set; }
//        public abstract bool Passed { get; }
//        public Test Test { get; private set; }

//        ////public static TestResult From(Test test, bool passed) {
//        ////    return new SingleTestResult(test, passed);
//        ////}

//        ////public static TestResult From(Test test, Array<TestResult> children) {
//        ////    return new MultipleTestResult(children);
//        ////}

//        public TestResult(Test test, String message = null) {
//            Message = message;
//            Test = test;
//        }

//        public override string ToString() {
//            return Passed ? "passed" : "failed";
//        }

//        public void Write(WriteOptions options = null) {
//            if (options == null) {
//                options = defaultOptions;
//            }
//            Write(0, options);
//        }

//        protected virtual void Write(int depth, WriteOptions options) {
//            // Indent
//            int nIndentsToWrite = depth;
//            while (nIndentsToWrite > 0) {
//                Console.Write("    ");
//                --nIndentsToWrite;
//            }

//            // Description
//            Console.Write("{0}: ", Test == null ? (String)"[unknown]" : Test.Description);
            
//            // Passing indication
//            ConsoleColor previousColor = Console.ForegroundColor;
//            if (Passed) {
//                Console.ForegroundColor = options.SuccessColor;
//                Console.Write("passed");
//            } else {
//                Console.ForegroundColor = options.FailureColor;
//                Console.Write("failed");
//            }
//            Console.ForegroundColor = previousColor;
//            if (Message != null) {
//                Console.Write(" ");
//                Console.Write(Message.ToString());
//            }
//            Console.WriteLine();

//            // Children if any
//            if (Children != null) {
//                int childDepth = depth + 1;
//                if (childDepth <= options.MaxDepth) {
//                    foreach (TestResult child in Children) {
//                        child.Write(childDepth, options);
//                    }
//                }
//            }
//        }

//        public class WriteOptions {
//            public ConsoleColor FailureColor { get; private set; }
//            public int MaxDepth { get; private set; }
//            public ConsoleColor NeutralColor { get; private set; }
//            public bool OnlyFailures { get; private set; }
//            public ConsoleColor SuccessColor { get; private set; }
//            public ConsoleColor WarningColor { get; private set; }

//            public WriteOptions(
//                int maxDepth,
//                ConsoleColor successColor,
//                ConsoleColor failureColor,
//                ConsoleColor warningColor,
//                ConsoleColor neutralColor,
//                bool onlyFailures) {
//                FailureColor = failureColor;
//                MaxDepth = maxDepth;
//                NeutralColor = neutralColor;
//                OnlyFailures = onlyFailures;
//                SuccessColor = successColor;
//                WarningColor = warningColor;
//            }
//        }
//    }
//}
