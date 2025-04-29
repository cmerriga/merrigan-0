using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an Array but need an ICollection.
    // Documentation updated 8/26/24.
    [Untested]
    internal class ArrayCollection<T> : ICollection<T> {
        protected Array<T> baseArray;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <value>The number of elements contained in the <see cref="ICollection{T}"/>.</value>
        public int Count { get { return (int)baseArray.Length; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        /// <value><c>true</c> if the <see cref="ICollection{T}"/> is read-only; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// A collection that is read-only does not allow the addition or removal of distinct elements after the collection is created. 
        /// Note that read-only in this context does not indicate whether individual distinct elements of the collection can be modified, 
        /// since the <see cref="ICollection{T}"/> interface only supports addition and removal operations. For example, the <see cref="IsReadOnly"/>
        /// property of an array that is cast or converted to an <see cref="ICollection{T}"/> object returns true, even though individual 
        /// distinct elements can be modified.
        /// </remarks>
        public bool IsReadOnly { get { return true; } }

        public ArrayCollection(Array<T> baseArray) { this.baseArray = baseArray; }

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        [NotSupported]
        public void Add(T item) { throw new NotSupportedException(); }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        /// <remarks>
        /// <see cref="Count"/> must be set to 0, and references to other objects from elements of the collection must be released.
        /// </remarks>
        [NotSupported]
        public void Clear() { throw new NotSupportedException(); }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ICollection{T}"/>.</param>
        /// <returns><c>true</c> if item is found in the <see cref="ICollection{T}"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Implementations can vary in how they determine equality of objects; for example, <see cref="List{T}"/> uses 
        /// <see cref="Comparer{T}.Default"/>, whereas <see cref="Dictionary{TKey,TValue}"/> allows the user to specify the <see cref="IComparer{T}"/> 
        /// implementation to use for comparing keys.
        /// </remarks>
        public bool Contains(T item) { return baseArray.Contains(item); }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The Array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><c>array</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>arrayIndex</c> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        /// The number of elements in the source <see cref="ICollection{T}"/> is greater than the available space from
        /// <c>arrayIndex</c> to the end of the destination <c>array</c>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex) { baseArray.CopyTo(array, arrayIndex); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() { return baseArray.GetEnumerator(); }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// <c>true</c> if <c>item</c> was successfully removed from the <see cref="ICollection{T}"/>; otherwise, <c>false</c>. 
        /// This method also returns <c>false</c> if <c>item</c> is not found in the original <see cref="ICollection{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        /// <remarks>
        /// Implementations can vary in how they determine equality of objects; for example, <see cref="List{T}"/> uses <see cref="Comparer{T}.Default"/>, 
        /// whereas, <see cref="Dictionary{TKey,TValue}"/>
        /// allows the user to specify the <see cref="IComparer{T}"/> implementation to use for comparing keys.<para/>
        /// In collections of contiguous elements, such as lists, the elements that follow the removed element move up to occupy the vacated spot. If the 
        /// collection is indexed, the indexes of the elements that are moved are also updated. This behavior does not apply to 
        /// collections where elements are conceptually grouped into buckets, such as a hash table.
        /// </remarks>
        [NotSupported]
        public bool Remove(T item) { throw new NotSupportedException(); }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
