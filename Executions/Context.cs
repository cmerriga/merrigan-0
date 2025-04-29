using System.Collections.Generic;

namespace Executions {
    public abstract partial class Context {
        public object this[string name] {
            get { return Get(name); }
            set { Set(name, value); }
        }

        public abstract void Delete(string name);

        public abstract object Get(string name);

        public T Get<T>(string name) { return (T)Get(name); }

        public abstract void Set(string name, object value);

        //public bool TryGet(string name, out object value) {

        //}
    }
}
