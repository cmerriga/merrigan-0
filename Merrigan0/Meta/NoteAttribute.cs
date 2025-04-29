using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Untested]
    public class NoteAttribute : Attribute {
        private string note;

        public String Note { get { return note; } }

        public NoteAttribute(string note) {
            this.note = note;
        }
    }
}
