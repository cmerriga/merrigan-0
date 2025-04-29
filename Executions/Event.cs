namespace Executions {
    public class Event {
        public Context Context { get; private set; }
        public Action Action { get; private set; }

        public Event(Context context, Action action) {
            Context = context;
            Action = action;
        }
    }
}
