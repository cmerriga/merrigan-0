using System;
using System.IO;

////namespace Merrigan0.Internal.Text {
////    // Keep here to isolate the class name "Text"
////    using Merrigan0;

////    // When you have a TextReader, but you want Text
    ////[Untested]
////    internal class TextReaderText : Text  {
////        protected TextReader reader;
////        protected MutableArray<String> linesSoFar = new MutableArray<String>();
////        protected bool finishedReading;

////        public override long Length {
////            get {
////                if (!finishedReading) {
////                    ReadToEnd();
////                }
////                return linesSoFar.Length;
////            }
////        }

////        public TextReaderText(TextReader reader) {
////            this.reader = reader;
////        }

////        protected override String GetCharacter(long i) {
////            if (linesSoFar.Length < i) {
////                ReadUntil(i);
////            }
////            return linesSoFar[i];
////        }

////        protected override void ReadToEnd() {
////            string line;
////            while (true) {
////                line = reader.ReadLine();
////                if (line == null) {
////                    break;
////                }
////                linesSoFar.Append((String)line);
////            }
////            finishedReading = true;
////        }

////        protected void ReadUntil(long i) {
////            long lengthSoFar = linesSoFar.Length;
////            while (i >= lengthSoFar) {
////                string line = reader.ReadLine();
////                if (line == null) {
////                    throw new Exception();
////                }
////                linesSoFar.Append((String)line);
////                ++lengthSoFar;
////            }
////        }
////    }
////}
