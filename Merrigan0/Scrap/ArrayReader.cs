//using System;

//namespace Merrigan0 {
//    public abstract class ArrayReader<T> {
//        public virtual bool AtEnd {
//            get {
//                T map;
//                return !TryPeek(out map);
//            }
//        }
                

//        // Override if there is a native function that is faster.
//        public virtual bool TryRead(out T map) {
//            if (!TryPeek(out map)) {
//                return false;
//            }
//            Skip(1);
//            return true;
//        }

//        public virtual bool TryRead(int length, char[] buffer, int i) {
//            if (TryPeek(length, buffer, i)) {
//                Skip(length);
//                return true;
//            }
//            return false;
//        }

//        public virtual bool TryRead(int maxLength, out String s) {
//            if (TryPeek(length, out s)) {
//                Skip(length);
//                return true;
//            }
//            return false;
//        }

//        public abstract bool TryPeek(out T map);

//        // Override if you want to optimize the algorithm. By default, it repeatedly calls TryPeek.
//        public virtual bool TryPeek(int length, char[] buffer, int i) {
//            for (int c = 0; c < length; ++c) {
//                char ch;
//                if (!TryPeek(out ch)) {
//                    return false;
//                }
//                buffer[i + c] = ch;
//            }
//            return true;
//        }

//        // Override if you want to optimize the algorithm. By default, it repeatedly calls TryPeek.
//        public virtual bool TryPeek(int length, out String s) {
//            MutableString textSoFar = new MutableString();
//            for (int c = 0; c < length; ++c) {
//                char ch;
//                if (!TryPeek(out ch)) {
//                    s = null;
//                    return false;
//                }
//                textSoFar.Append(ch);
//            }
//            s = textSoFar;
//            return true;
//        }

//        protected abstract bool TrySkip(
//        protected virtual void Skip(int length) {
//            if (!TrySkip(length)) {
//                throw new Exception();
//            }
//        }

//        // Override if there is a native function that is faster.
//        protected virtual bool TrySkip(int length) {
//            for (int c = 0; c < length; ++c) {
//                char ch;
//                if (!TryRead(out ch)) {
//                    return false;
//                }
//            }
//            return true;
//        }
//    }
//}
