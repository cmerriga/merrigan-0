using System.IO;

namespace Merrigan0.StringsInternal {
    // When you've got a .NET TextReader but need a String.
    [Untested]
    internal class TextReaderString : String {
        private const int blockSize = 2000;

        // Initialized at creation. Becomes null once the string is entirely buffered in.
        private TextReader reader;
        private MutableString bufferedSoFar = new MutableString();

        public override long Length {
            get {
                // If the 
                if (reader == null) {
                    return bufferedSoFar.Current.Length;
                }

                // Read all until end
                while (true) {
                    char[] characters = new char[blockSize];
                    long charactersRead = reader.ReadBlock(characters, 0, blockSize);
                    if (charactersRead == 0) {
                        break;
                    }
                    if (charactersRead < blockSize) {
                        bufferedSoFar.Append(String.From(characters, charactersRead));
                        break;
                    }
                    bufferedSoFar.Append(String.From(characters));
                }

                reader.Close();
                reader = null;
                return bufferedSoFar.Current.Length;
            }
        }

        public TextReaderString(TextReader reader) {
            this.reader = reader;
        }

        public override bool TryGetCharacter(long i, out char ch) {
            //// sketchy
            if (reader != null) {
                // Read all until i or end reached
                while (i >= bufferedSoFar.Current.Length) {
                    char[] characters = new char[blockSize];
                    long charactersRead = reader.ReadBlock(characters, 0, blockSize);
                    if (charactersRead == 0) {
                        break;
                    }
                    if (charactersRead < blockSize) {
                        bufferedSoFar.Append(String.From(characters, charactersRead));
                        break;
                    }
                    bufferedSoFar.Append(String.From(characters));
                }
            }
            if (i >= bufferedSoFar.Current.Length) {
                ch = default(char);
                return false;
            }
            return bufferedSoFar.Current.TryGetCharacter(i, out ch);
        }

        //protected override char GetCharacter(long i) {
        //    //// sketchy
        //    if (reader != null) {
        //        // Read all until i or end reached
        //        while (i >= bufferedSoFar.Current.Length) {
        //            char[] characters = new char[blockSize];
        //            long charactersRead = reader.ReadBlock(characters, 0, blockSize);
        //            if (charactersRead == 0) {
        //                break;
        //            }
        //            if (charactersRead < blockSize) {
        //                bufferedSoFar.Append(String.From(characters, charactersRead));
        //                break;
        //            }
        //            bufferedSoFar.Append(String.From(characters));
        //        }
        //    }
        //    Utilities.ThrowIfIndexOutOfRange(i, bufferedSoFar.Current.Length);
        //    return bufferedSoFar.Current[i];
        //}
    }
}
