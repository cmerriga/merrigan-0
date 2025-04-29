using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an IEnumerable but need an Array.
    [Untested]
    internal class EnumerableWrapperArray : CoalescingArray<object> {
        private IEnumerator enumerator;

        public EnumerableWrapperArray(IEnumerable enumerable) : this(enumerable, null, false) { }

        public EnumerableWrapperArray(IEnumerable enumerable, Func<object, object, int> compare, bool distinct) :
            base(compare, distinct) {
            enumerator = enumerable.GetEnumerator();
        }

        protected override bool TryGetNextUncoalescedItem(out object item) {
            if (!enumerator.MoveNext()) {
                item = null;
                return false;
            }
            item = enumerator.Current;
            return true;
        }
    }

    // Use when you have an IEnumerable but need an Array.
    [Untested]
    internal class EnumerableWrapperArray<T> : CoalescingArray<T> {
        private IEnumerator<T> enumerator;

        public EnumerableWrapperArray(IEnumerable<T> enumerable) : this(enumerable, null, false) { }

        public EnumerableWrapperArray(IEnumerable<T> enumerable, Func<T, T, int> compare, bool distinct) :
            base(compare, distinct)
        {
            enumerator = enumerable.GetEnumerator();
        }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            if (!enumerator.MoveNext()) {
                item = default(T);
                return false;
            }
            item = enumerator.Current;
            return true;
        }
    }
}



////        private MutableArray<T> loadedItems;
////        private IEnumerator<T> enumerator;

////        public override long Length {
////            get {
////                if (enumerator != null) {
////                    LoadAll();
////                }
////                return loadedItems.Current.Length;
////            }
////        }

////        public EnumerableArray(IEnumerable<T> enumerable)
////        {
////            enumerator = enumerable.GetEnumerator();
////            loadedItems = new MutableArray<T>();
////        }

////        public override IEnumerator<T> GetEnumerator() {
////            LoadAll();
////            return loadedItems.Current.GetEnumerator();
////        }

////        public override T[] ToBlock() {
////            LoadAll();
////            return loadedItems.Current.ToBlock();
////        }

////        public override IList<T> ToIList() {
////            LoadAll();
////            return new ArrayList<T>(loadedItems.Current);
////        }

////        protected override T GetItem(long i) {
////            LoadUntil(i);
////            return loadedItems.Current[i];
////        }

////        protected void LoadAll() {
////            if (enumerator != null) {
////                while (true) {
////                    if (!LoadBlock())
////                        break;
////                }
////            }
////        }

////        protected void LoadUntil(long i) {
////            if (i < loadedItems.Current.Length) {
////                return;
////            }

////            if (enumerator == null) {
////                throw new IndexOutOfRangeException();
////            }

////            while (LoadBlock()) {
////                if (i < loadedItems.Current.Length) {
////                    return;
////                }
////            }

////            throw new IndexOutOfRangeException();
////        }

////        [return:WhatItIs("Whether there are more items remaining")]
////        protected bool LoadBlock() {
////            // Assume enumerator != null
////            lock (enumerator) {
////                int i = 0;
////                while (i < 256) {
////                    if (!enumerator.MoveNext()) {
////                        enumerator = null;
////                        return false;
////                    }
////                    loadedItems.Append(enumerator.Current);
////                }
////                return true;
////            }
////        }
////    }
////}
