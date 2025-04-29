using System;

namespace Merrigan0.Internal.CodeTemplates.Collections.Generic {
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=netframework-2.0
    // Updated 8/26/24
    internal interface IEnumerable<T> : Merrigan0.Internal.CodeTemplates.Collections.IEnumerable {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        new System.Collections.Generic.IEnumerator<T> GetEnumerator();
    }

    internal class ExampleEnumerable<T> : System.Collections.Generic.IEnumerable<T> {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator() {
            return null; ////
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
