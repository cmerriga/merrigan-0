using System;
using System.Collections.Generic;
using Merrigan0.ArraysInternal; // for ConvertEnumerator

namespace Merrigan0.SetsInternal {
    [Untested]
    internal class ConvertSet<TBase, T> : CoalescingSet<T> {
        private Set<TBase> baseSet;
        private IEnumerator<TBase> baseEnumerator;
        private Func<object, object> convertFunction;
        private Func<object, object> unconvertFunction;

        public override long Length { get { return baseSet.Length; } }

        public ConvertSet(Set<TBase> baseSet, Func<object, object> convertFunction = null, Func<object, object> unconvertFunction = null) {
            if (convertFunction == null) {
                if (!Conversion.TryGetConvertFunction(typeof(TBase), typeof(T), out convertFunction)) {
                    throw new Exception();
                }
            }
            if (unconvertFunction == null) {
                if (!Conversion.TryGetConvertFunction(typeof(T), typeof(TBase), out unconvertFunction)) {
                    throw new Exception();
                }
            }

            this.baseSet = baseSet;
            baseEnumerator = baseSet.GetEnumerator();
            this.convertFunction = convertFunction; //// Could get this later
            this.unconvertFunction = unconvertFunction;  //// Could get this later
        }

        public override bool Contains(T item) { return baseSet.Contains((TBase)unconvertFunction(item)); }

        public override IEnumerator<T> GetEnumerator() { return new ConvertEnumerator<TBase, T>(baseSet.GetEnumerator(), convertFunction); }

        protected override bool TryGetNextUncoalescedItem(out T item) {
            if (baseEnumerator.MoveNext()) {
                item = (T)convertFunction(baseEnumerator.Current);
                return true;
            }
            item = default(T);
            return false;
        }
    }
}
