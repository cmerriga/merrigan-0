using System.Collections.Generic;

namespace Merrigan0 {
    /*
     * Theoretically, the atomic basis for every change:
     *      Add item to object with name n
     *      Set item on object with name n
     *      Delete property with name n on object
     *      Add item to list after index i
     *      Delete item from list at index i
     */
    public abstract class Event {
        private static List<Event> emptyEventList = new List<Event>();

        private List<Event> children;

        public Context Context { get; private set; }

        public List<Event> Children {
            get {
                if (children == null) {
                    return emptyEventList;
                }
                return children;
            }
        }

        public void AddChild(Event child) {
            if (children == null) {
                children = new List<Event>();
            }
            children.Add(child);
        }

        protected virtual void Initialize() {
        }
    }
}
