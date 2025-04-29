using System;
using System.Collections.Generic;
using System.IO;

namespace Merrigan0 {
    // Turns a String into a .NET TextReader. A reader is a forward-traversable list of character.
    [Untested]
    public class StringTextReader : TextReader {
        private String s;
        private long i;
        private bool closed;

        public StringTextReader(String text) {
            this.s = text;
        }

        protected override void Dispose(bool disposing) {
            closed = true;
        }

        public override int Peek() {
            if (closed) {
                throw new ObjectDisposedException("StringReader");
            }
            if (i >= s.Length) {
                return -1;
            }
            return s[i];
        }

        public override int Read() {
            if (closed) {
                throw new ObjectDisposedException("StringReader");
            }
            ++i;
            if (i >= s.Length) {
                return -1;
            }
            return s[i];
        }

        public override int Read(char[] buffer, int index, int count) {
            int charsRemaining = (int)(s.Length - i);
            if (count > charsRemaining) {
                count = charsRemaining;
            }
            for (int iCopied = 0; iCopied < count; ++iCopied) {
                buffer[index + iCopied] = s[i];
                ++i;
            }
            return count;
        }

        // Reads to just past the next newline character combination (CR, LF, or CR/LF). Returns the 
        // characters preceding that. If the reader is at the end of the s, returns null.
        // If characters are found at the end of the s without a newline,
        // those characters are returned.
        public override string ReadLine() {
            // If the line had no characters *and* we were out of room in the string, return nothing
            if (i >= s.Length) {
                return null;
            }

            // Read all characters until encountering a CR, LF, or CR/LF pair
            long iStart = i;
            long iToTry = i;
            while (true) {
                // Stop if found LF (Unix)
                char ch = s[i];
                if (ch == '\n') {
                    ++i;
                    break;
                }

                // Stop if found CR (Mac) or CR/LF (TTY/Windows)
                if (ch == '\r') {
                    ++i;

                    // Skip any \n found
                    if (i < s.Length && s[i] == '\n') {
                        ++i;
                    }

                    break;
                } else {
                    ++i;
                    if (i >= s.Length) {
                        break;
                    }
                }
            }
            long length = i - iStart;
            char[] lineCharacters = new char[length];
            s.CopyTo(lineCharacters, 0);
            return new string(lineCharacters);
        }
    }
}
