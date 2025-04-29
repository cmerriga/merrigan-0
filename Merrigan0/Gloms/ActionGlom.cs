using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.GlomsInternal {
////    // A glom that is ready to receive parameters if necessary, or provide a result if no parameters needed.
////    [Untested]
////    internal class ActionGlom : Glom, IEnumerable<object> {
////        private Array<object> items;

////        public object this[long i] { get { return items[i]; } }
////        public object this[int i] { get { return items[i]; } }
////        public long Length { get { return items.Length; } }

////        public ArrayGlom(params object[] items) :
////            this((IEnumerable)items)
////        {
////        }

////        public ArrayGlom(IEnumerable items) {
////            this.items = Array<object>.From(items);
////        }

////        public ArrayGlom(Glom prototype, IEnumerable items)
////            : base(prototype) 
////        {
////            this.items = Array<object>.From(items);
////        }

////        // Type must be IEnumerable something
////        public override object As(Type type) {
////            return base.As(type);
////        }

////        public virtual IEnumerator<object> GetEnumerator() {
////            return items.GetEnumerator();
////        }

////        // Returns an amorphous value that can be used by other epxressions
////        protected override bool TryGetAtThisLevel(String name, out object o) {
////            if (name == "Length") {
////                o = Length;
////                return true;
////            }
////            return base.TryGetAtThisLevel(name, out o);
////        }

////        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
////    }
}
