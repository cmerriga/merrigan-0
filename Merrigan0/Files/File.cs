using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Merrigan0.ArraysInternal;

namespace Merrigan0.FilesInternal {
    [Untested]
    public class File {
        private FileReaderArray bytes;

        public Array<byte> Bytes {
            get {
                if (bytes == null) {
                    bytes = new FileReaderArray(new FileStream(Path, FileMode.Open, FileAccess.Read));
                }
                return bytes;
            }
        }

        public String Path { get; private set; }

        public File(String path) {
            Path = path;
        }
    }
}
