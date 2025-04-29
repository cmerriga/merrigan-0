using System;
using System.Collections;
using System.Text;

namespace Merrigan0.Internal.CodeTemplates.Collections {
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator.current?view=netframework-2.0
    // Updated 8/26/24.
    /// <summary>
    /// Supports a simple iteration over a non-generic collection.
    /// </summary>
    interface IEnumerator {
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        object Current { get; }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element;
        /// <c>false</c> if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        bool MoveNext();

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        /// <exception cref="NotSupportedException">The enumerator does not support being reset.</exception>
        void Reset();
    }

    internal class ExampleEnumerator : IEnumerator {
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        public object Current {
            get {
                return null; ////
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            ////
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element;
        /// <c>false</c> if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        public bool MoveNext() {
            return false; ////
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        /// <exception cref="NotSupportedException">The enumerator does not support being reset.</exception>
        public void Reset() {
            ////
        }
    }
}
