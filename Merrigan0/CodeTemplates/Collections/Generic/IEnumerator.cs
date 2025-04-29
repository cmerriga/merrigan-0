using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0.Internal.CodeTemplates.Collections.Generic {
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=netframework-2.0
    // Updated 8/26/24.
    /// <summary>
    /// Supports a simple iteration over a generic collection.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    internal interface IEnumerator<T> : Merrigan0.Internal.CodeTemplates.Collections.IEnumerator, IDisposable {
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        /// <remarks>
        /// <see cref="Current"/> is undefined under any of the following conditions:
        /// <para>
        /// The enumerator is positioned before the first element in the collection, immediately after the enumerator is created. <see cref="MoveNext"/> must be called to advance the enumerator to the first element of the collection before reading the value of Current.
        /// </para>
        /// <para>
        /// The last call to <see cref="MoveNext"/> returned <c>false</c>, which indicates the end of the collection.
        /// </para>
        /// <para>
        /// The enumerator is invalidated due to changes made in the collection, such as adding, modifying, or deleting elements.
        /// </para>
        /// <para>
        /// Current returns the same object until <see cref="MoveNext"/> is called. <see cref="MoveNext"/> sets <see cref="Current"/> to the next element.
        /// </para>
        /// </remarks>
        new T Current { get; }
    }

    internal class ExampleEnumerator<T> : System.Collections.Generic.IEnumerator<T> {
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        /// <remarks>
        /// <see cref="Current"/> is undefined under any of the following conditions:
        /// <para>
        /// The enumerator is positioned before the first element in the collection, immediately after the enumerator is created. <see cref="MoveNext"/> must be called to advance the enumerator to the first element of the collection before reading the value of Current.
        /// </para>
        /// <para>
        /// The last call to <see cref="MoveNext"/> returned <c>false</c>, which indicates the end of the collection.
        /// </para>
        /// <para>
        /// The enumerator is invalidated due to changes made in the collection, such as adding, modifying, or deleting elements.
        /// </para>
        /// <para>
        /// Current returns the same object until <see cref="MoveNext"/> is called. <see cref="MoveNext"/> sets <see cref="Current"/> to the next element.
        /// </para>
        /// </remarks>
        public T Current {
            get {
                return default(T); ////
            }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        object System.Collections.IEnumerator.Current {
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
