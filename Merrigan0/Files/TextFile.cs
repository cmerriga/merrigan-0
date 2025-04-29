using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FilesInternal {
    [Untested]
    public class TextFile : File {
        private FileString contents;

        public String Contents {
            get {
                if (contents == null) {
                    contents = new FileString(Path);
                }
                return contents;
            }
        }

        public TextFile(String path) : base(path) { }
    }
}
