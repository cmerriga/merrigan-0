namespace Executions {
    public abstract class ExecutionMode {
        ////private static ExecutionMode current;

        public static ExecutionMode Current { get; private set; }

        static ExecutionMode() {
            
        }

        public abstract void HandleFatalError();
    }
}
