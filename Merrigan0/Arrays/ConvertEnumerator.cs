using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Enumerator that supplies values of one type based on an enumerator that supplies values of another type
    [Untested]
    internal class ConvertEnumerator<TFrom, TTo> : IEnumerator<TTo> {
        private IEnumerator<TFrom> baseEnumerator;
        private Func<object, object> convertFunction;

        public TTo Current { get { return (TTo)convertFunction(baseEnumerator.Current); } }

        Object IEnumerator.Current { get { return Current; } }

        public ConvertEnumerator(IEnumerator<TFrom> baseEnumerator, Func<object, object> convertFunction = null) {
            if (convertFunction == null) {
                if (!Conversion.TryGetConvertFunction(typeof(TFrom), typeof(TTo), out convertFunction)) {
                    throw new Exception();
                }
            }

            this.baseEnumerator = baseEnumerator;
            this.convertFunction = convertFunction;
        }

        public void Dispose() { baseEnumerator.Dispose(); }

        public Boolean MoveNext() { return baseEnumerator.MoveNext(); }

        public void Reset() { baseEnumerator.Reset(); }
    }
}
