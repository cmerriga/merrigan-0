using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    internal class TypedEnumerable : IEnumerable<object> {
        private IEnumerable baseEnumerable;

        public TypedEnumerable(IEnumerable baseEnumerable) {
            this.baseEnumerable = baseEnumerable;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object> GetEnumerator() {
            return new TypedEnumerator(baseEnumerable.GetEnumerator());
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
