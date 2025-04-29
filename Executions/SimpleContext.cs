using System.Collections.Generic;
using System.Text;

namespace Executions {
    public partial class SimpleContext : Context {
        private Dictionary<string, object> namesToValues;

        public Context Parent { get; private set; }

        public SimpleContext() : this(null) { }

        public SimpleContext(Context parent) {
            Parent = parent;
        }

        public SimpleContext(Context parent, params object[] namesAndValues) {
            Parent = parent;
            foreach (KeyValuePair<string, object> pair in Utilities.ToPairs<string, object>(namesAndValues)) {
                Set(pair.Key, pair.Value);
            }
        }

        public override void Delete(string name) {
            if (namesToValues != null) {
                namesToValues.Remove(name);
                if (namesToValues.Count == 0) {
                    namesToValues = null;
                }
            }
        }

        public override object Get(string name) {
            object value;
            if (namesToValues == null || !namesToValues.TryGetValue(name, out value)) {
                if (Parent != null) {
                    return Parent.Get(name);
                } else {
                    throw new KeyNotFoundException();
                }
            }
            return namesToValues[name];
        }

        public override void Set(string name, object value) {
            if (namesToValues == null) {
                namesToValues = new Dictionary<string, object>();
            }
            namesToValues[name] = value;
        }

        //public bool TryGet(string name, out object value) {

        //}
    }
}
