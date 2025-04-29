using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.FilesInternal {
    [Untested]
    public static class Files {
        public static String Contents(string path) {
            return new FileString(path);
        }
    }
}
