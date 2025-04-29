#if !NET45

namespace System.Collections.Generic {
    /// <summary>
    /// Represents a read-only collection of elements that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the read-only list.</typeparam>
    public interface IReadOnlyList<T> : IEnumerable<T>, IReadOnlyCollection<T> {
        /// <summary>
        /// Gets the element at the specified index in the read-only list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <value>The element at the specified index in the read-only list.</value>
        T this[int index] { get; }
    }
}

#endif
