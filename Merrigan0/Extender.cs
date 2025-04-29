using System;

namespace Merrigan0 {
    [Untested]
    public struct Extender {
        private Extension extension;

        public object this[int i] {
            get {
                if (extension == null) {
                    extension = new Extension();
                }
                return extension.Get(i);
            }
            set {
                if (extension == null) {
                    extension = new Extension();
                }
                extension.Set(i, value);
            }
        }
    }
}
