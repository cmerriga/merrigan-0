using System;
using System.Collections;
using System.Text;

namespace Merrigan0.Internal.CodeTemplates.Collections {
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-2.0
    // Updated 8/26/24
    interface IEnumerable {
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator GetEnumerator();
    }

    internal class ExampleEnumerable : IEnumerable {
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        public System.Collections.IEnumerator GetEnumerator() {
            return null; ////
        }
    }
}
