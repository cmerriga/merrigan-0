using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Merrigan0.StringsInternal;

namespace Merrigan0.FilesInternal {
    [Untested]
    internal class FileString : TextReaderString {
        public FileString(String path) : base(new StreamReader(path)) { }
    }
}
