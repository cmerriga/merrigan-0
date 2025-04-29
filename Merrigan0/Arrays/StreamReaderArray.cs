using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Merrigan0.ArraysInternal {
    // When you've got a .NET StreamReader but need an array of bytes.
    [Untested]
    internal class FileReaderArray : Array<byte> {
        private const int blockSize = 2000;

        // Initialized at creation. Becomes null once the bytes are entirely buffered in.
        private FileStream reader;
        private MutableArray<byte> bufferedSoFar = new MutableArray<byte>();

        public override long Length {
            get {
                // If the 
                if (reader == null) {
                    return bufferedSoFar.Current.Length;
                }

                // Read all until end
                while (true) {
                    byte[] bytes = new byte[blockSize];
                    long bytesRead = reader.Read(bytes, 0, blockSize);
                    if (bytesRead == 0) {
                        break;
                    }
                    if (bytesRead < blockSize) {
                        bufferedSoFar.Append(Array<byte>.From(bytes, bytesRead));
                        break;
                    }
                    bufferedSoFar.Append(Array<byte>.From(bytes));
                }

                reader.Close();
                reader = null;
                return bufferedSoFar.Current.Length;
            }
        }

        public FileReaderArray(FileStream reader) {
            this.reader = reader;
        }

        public override bool TryGetItem(long i, out byte item) {
            //// sketchy
            if (reader != null) {
                // Read all until i or end reached
                while (i >= bufferedSoFar.Current.Length) {
                    byte[] bytes = new byte[blockSize];
                    long bytesRead = reader.Read(bytes, 0, blockSize);
                    if (bytesRead == 0) {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                        break;
                    }
                    if (bytesRead < blockSize) {
                        bufferedSoFar.Append(Array<byte>.From(bytes, bytesRead));
                        break;
                    }
                    bufferedSoFar.Append(Array<byte>.From(bytes));
                }
            }
            return bufferedSoFar.Current.TryGetItem(i, out item);
        }
    }
}
