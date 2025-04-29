namespace Merrigan0.Internal.CodeTemplates {
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1?view=netframework-2.0
    // Last updated 8/26/24.
    //// This file updated only. Others must comply
    interface IList<T> : System.Collections.Generic.ICollection<T> {
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
        T this[int index] { get; set; }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IList{T}"/>.</param>
        /// <returns>The index of <c>item</c> if found in the list; otherwise, -1.</returns>
        /// <remarks>
        /// If an object occurs multiple times in the list, the <see cref="IndexOf"/> method always returns the first instance found.
        /// </remarks>
        int IndexOf(T item);

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="IList{T}"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is not a valid index in the <see cref="IList{T}"/>.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IList{T}"/> is read-only.</exception>
        /// <remarks>
        /// If <c>index</c> equals the number of items in the <see cref="IList{T}"/>, then <c>item</c> is appended to the list.<para/>
        /// In collections of contiguous elements, such as lists, the elements that follow the insertion point 
        /// move down to accommodate the new element. If the collection is indexed, the indexes of the elements 
        /// that are moved are also updated. This behavior does not apply to collections where elements are
        /// conceptually grouped into buckets, such as a hash table.
        /// </remarks>
        void Insert(int index, T item);

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
        void RemoveAt(int index);
    }

    internal class ExampleList<T> : IList<T> {
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
            get { return default(T); } ////
            set { } ////
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <value>The number of elements contained in the <see cref="ICollection{T}"/>.</value>
        public int Count { get { return 0; } } ////

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
        public bool IsReadOnly { get { return false; } } ////

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public void Add(T item) {
            ////
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        /// <remarks>
        /// <see cref="Count"/> must be set to 0, and references to other objects from elements of the collection must be released.
        /// </remarks>
        public void Clear() {
            ////
        }

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
        public bool Contains(T item) {
            return false; ////
        }

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
        public void CopyTo(T[] array, int arrayIndex) {
            ////
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator() {
            return null; ////
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IList{T}"/>.</param>
        /// <returns>The index of <c>item</c> if found in the list; otherwise, -1.</returns>
        /// <remarks>
        /// If an object occurs multiple times in the list, the <see cref="IndexOf"/> method always returns the first instance found.
        /// </remarks>
        public int IndexOf(T item) {
            return -1; ////
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="IList{T}"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is not a valid index in the <see cref="IList{T}"/>.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IList{T}"/> is read-only.</exception>
        /// <remarks>
        /// If <c>index</c> equals the number of items in the <see cref="IList{T}"/>, then <c>item</c> is appended to the list.<para/>
        /// In collections of contiguous elements, such as lists, the elements that follow the insertion point 
        /// move down to accommodate the new element. If the collection is indexed, the indexes of the elements 
        /// that are moved are also updated. This behavior does not apply to collections where elements are
        /// conceptually grouped into buckets, such as a hash table.
        /// </remarks>
        public void Insert(int index, T item) {
            ////
        }

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
        public bool Remove(T item) {
            return true; ////
        }

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
        public void RemoveAt(int index) {
            ////
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
