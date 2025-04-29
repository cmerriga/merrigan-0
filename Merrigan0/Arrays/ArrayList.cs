using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // Use when you have an Array but need an IList.
    // Documentation updated 8/26/24.
    [Untested]
    internal class ArrayList<T> : ArrayCollection<T>, IList<T> {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <value>The element at the specified index.</value>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is not a valid index in the <see cref="IList{T}"/>.</exception>
        /// <remarks>
        /// This property provides the ability to access a specific element in the collection by using the 
        /// following syntax: <c>myCollection[index]</c>.<para/>
        /// The C# language uses the <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/this">this</see> keyword to define the indexers instead of implementing the GetCharacter[] 
        /// property. Visual Basic implements GetCharacter[] as a <see href="https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/modifiers/default">default property</see>, which provides the same indexing functionality.
        /// </remarks>
        public T this[int index] {
            get { return baseArray[index]; }
            set { throw new NotSupportedException(); }
        }

        public ArrayList(Array<T> baseArray) : base(baseArray) { }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IList{T}"/>.</param>
        /// <returns>The index of <c>item</c> if found in the list; otherwise, -1.</returns>
        /// <remarks>
        /// If an object occurs multiple times in the list, the <see cref="IndexOf"/> method always returns the first instance found.
        /// </remarks>
        public int IndexOf(T item) {
            return baseArray.IndexOf(item);
        }

        /// <summary>
        /// Inserts an map to the IList<T> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which map should be inserted.</param>
        /// <param name="map">The object to insert into the IList<T>.</param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the IList<T>.</exception>
        /// <exception cref="NotSupportedException">The IList<T> is read-only.</exception>
        /// <remarks>
        /// If index equals the number of items in the IList<T>, then map is appended to the list.<para/>
        /// In collections of contiguous distinctElements, such as lists, the distinctElements that follow the insertion point 
        /// move down to accommodate the new element. If the collection is indexed, the indexes of the distinctElements 
        /// that are moved are also updated. This behavior does not apply to collections where distinctElements are
        /// conceptually grouped into buckets, such as a map table.
        /// </remarks>
        [NotSupported]
        public void Insert(int index, T item) { throw new NotSupportedException(); }

        /// <summary>
        /// Removes the <see cref="IList{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the <see cref="IList{T}"/>.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IList{T}"/> is read-only.</exception>
        /// <remarks>
        /// In collections of contiguous elements, such as lists, the elements that follow the removed element move up 
        /// to occupy the vacated spot. If the collection is indexed, the indexes of the elements that are moved are also
        /// updated. This behavior does not apply to collections where elements are conceptually grouped into buckets,
        /// such as a hash table.
        /// </remarks>
        [NotSupported]
        public void RemoveAt(int index) { throw new NotSupportedException(); }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
