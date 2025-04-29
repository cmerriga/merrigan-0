using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public abstract class BaseConsole : IConsoleStream {
        public abstract int NumberOfColumns { get; }

        public virtual void Append(char ch) {
            Append((object)ch);
        }

        public virtual void Append(object o) {
            if (o == null) {
                return;
            }
            Append(o.ToString());
        }

        public abstract void Append(string s);

        public virtual void Append(String s) {
            Append((object)s);
        }

        public virtual void Append<T>(IParent<T> parent) {
            Append(parent.ToString());
        }

        public virtual void AppendLine(string s) {
            Append(s);
            EndLine();
        }

        public virtual void AppendLine(String s) {
            Append(s);
            EndLine();
        }

        public virtual void AppendLine(object o) {
            Append(o);
            EndLine();
        }

        public virtual void AppendLine<T>(IParent<T> parent) {
            Append(parent);
            // The normal parent append ends any line already
        }

        public abstract void EndLine();
    }
}
