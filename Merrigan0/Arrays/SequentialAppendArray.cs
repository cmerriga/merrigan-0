using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
//    // When you are accumulating an array from beginning to end
    //[Untested]
//    internal class SequentialAppendArray<T> : Array<T> {
//        private const long BlockSize = 1024;
//        private const int rightShift = 10;
//        private const int indexMask = 0x000003F;
//        protected T[][] blocks;
//        protected long length;

//        public override long Length { get { return length; } }

//        public SequentialAppendArray(Array<T> baseItems) { Current = baseItems; }
//        public SequentialAppendArray() : this(Array<T>.Empty) { }

//        public void Append(T map) {
//            if (length == 0) {
//                blocks = new T[1][];
//                blocks[0][0] = map;
//            } else {
//            }
//            ++length;
//        }

//        public void Append(Array<T> s) {
//            foreach (T map in s) {
//                Append(map);
//            }
//        }

//        public override Array<T> AsImmutable() { return new ConcatenateBlocksArray<T>(blocks); }

//        protected override T GetCharacter(long i) {

//        }
//    }
}
