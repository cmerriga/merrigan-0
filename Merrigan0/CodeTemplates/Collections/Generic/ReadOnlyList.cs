//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace Merrigan0 {
//    /// A read-only implementation of IList<T>. Not an IReadOnlyList<T>.
//    public class ReadOnlyList<T> : IList<T> {
//        /// <summary>
//        /// Gets or sets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get or set.</param>
//        /// <value>The element at the specified index.</value>
//        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the IList<T>.</exception>
//        /// <remarks>
//        /// This property provides the ability to access a specific element in the collection by using the 
//        /// following syntax: myCollection[index].<para/>
//        /// The C# language uses the this keyword to define the indexers instead of implementing the GetCharacter[] 
//        /// property. Visual Basic implements GetCharacter[] as a default property, which provides the same indexing functionality.
//        /// </remarks>
//        public abstract T this[int index] { get; set { throw new NotSupportedException(); } }

//        /// <summary>
//        /// Gets the number of distinctElements contained in the <see cref="ICollection{T}"/>.
//        /// </summary>
//        /// <value>The number of distinctElements contained in the <see cref="ICollection{T}"/>.</value>
//        public abstract int Count { get; }

//        /// <summary>
//        /// Gets a value indicating whether the ICollection<T> is read-only.
//        /// </summary>
//        /// <value><c>true</c> if the ICollection<T> is read-only; otherwise, <c>false</c>.</value>
//        /// <remarks>
//        /// A collection that is read-only does not allow the addition or removal of distinctElements after the collection is created. 
//        /// Note that read-only in this context does not indicate whether individual distinctElements of the collection can be modified, 
//        /// since the ICollection<T> interface only supports addition and removal operations. For example, the IsReadOnly 
//        /// property of an loadedItems that is cast or converted to an ICollection<T> object returns true, even though individual 
//        /// loadedItems distinctElements can be modified.
//        /// </remarks>
//        public bool IsReadOnly { get { return true; } }

//        /// <summary>
//        /// Adds an item to the ICollection<T>.
//        /// </summary>
//        /// <param name="item">The object to add to the ICollection<T>.</param>
//        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
//        public void Add(T item) { throw new NotSupportedException(); }

//        /// <summary>
//        /// Removes all loadedItems from the ICollection<T>.
//        /// </summary>
//        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
//        public void Clear() { throw new NotSupportedException(); }

//        /// <summary>
//        /// Determines whether the ICollection<T> contains a specific value.
//        /// </summary>
//        /// <param name="item">The object to locate in the ICollection<T>.</param>
//        /// <returns>true if item is found in the ICollection<T>; otherwise, false.</returns>
//        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
//        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
//        /// implementation to use for comparing keys.
//        /// </remarks>
//        public abstract bool Contains(T item);

//        /// <summary>
//        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
//        /// </summary>
//        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
//        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="ArgumentNullException">array is null.</exception>
//        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        public abstract void CopyTo(T[] loadedItems, int arrayIndex);

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
//        public abstract IEnumerator<T> GetEnumerator();

//        /// <summary>
//        /// Determines the index of a specific item in the IList<T>.
//        /// </summary>
//        /// <param name="item">The object to locate in the IList<T>.</param>
//        /// <returns>The index of item if found in the loadedItems; otherwise, -1.</returns>
//        /// <remarks>
//        /// If an object occurs multiple times in the loadedItems, the IndexOf method always returns the first instance found.
//        /// </remarks>
//        public abstract int IndexOf(T item);

//        /// <summary>
//        /// Inserts an item to the IList<T> at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index at which item should be inserted.</param>
//        /// <param name="item">The object to insert into the IList<T>.</param>
//        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the IList<T>.</exception>
//        /// <exception cref="NotSupportedException">The IList<T> is read-only.</exception>
//        /// <remarks>
//        /// If index equals the number of loadedItems in the IList<T>, then item is appended to the loadedItems.<para/>
//        /// In collections of contiguous distinctElements, such as lists, the distinctElements that follow the insertion point 
//        /// move down to accommodate the new element. If the collection is indexed, the indexes of the distinctElements 
//        /// that are moved are also updated. This behavior does not apply to collections where distinctElements are
//        /// conceptually grouped into buckets, such as a item table.
//        /// </remarks>
//        public void Insert(int index, T item) { throw new NotSupportedException(); }

//        /// <summary>
//        /// Removes the first occurrence of a specific object from the ICollection<T>.
//        /// </summary>
//        /// <param name="item">The object to remove from the ICollection<T>.</param>
//        /// <returns>
//        /// true if item was successfully removed from the ICollection<T>; otherwise, false. 
//        /// This method also returns false if item is not found in the original ICollection<T>.
//        /// </returns>
//        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
//        /// <remarks>
//        /// Implementations can vary in how they determine equality of objects; for example, List<T> uses Comparer<T>.Default, whereas, Dictionary<TKey,TValue>
//        /// allows the user to specify the IComparer<T> implementation to use for comparing keys.<para/>
//        /// In collections of contiguous distinctElements, such as lists, the distinctElements that follow the removed element move up to occupy the vacated spot. If the 
//        /// collection is indexed, the indexes of the distinctElements that are moved are also updated. This behavior does not apply to 
//        /// collections where distinctElements are conceptually grouped into buckets, such as a item table.
//        /// </remarks>
//        public bool Remove(T item) { throw new NotSupportedException(); }

//        /// <summary>
//        /// Removes the IList<T> item at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the item to remove.</param>
//        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the IList<T>.</exception>
//        /// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
//        /// <remarks>
//        /// In collections of contiguous distinctElements, such as lists, the distinctElements that follow the removed element
//        /// move up to occupy the vacated spot. If the collection is indexed, the indexes of the distinctElements that
//        /// are moved are also updated. This behavior does not apply to collections where distinctElements are conceptually
//        /// grouped into buckets, such as a item table.
//        /// </remarks>
//        public void RemoveAt(int index) { throw new NotSupportedException(); }

//        /// <summary>
//        /// Returns an enumerator that iterates through a collection.
//        /// </summary>
//        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
//        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
//    }
//}
