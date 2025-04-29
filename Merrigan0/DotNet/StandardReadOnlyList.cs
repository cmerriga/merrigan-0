using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0 {
    //[Untested]
//    public class StandardReadOnlyList<T> : ReadOnlyList<T> {
//        private IList<T> loadedItems;

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
//        public override T this[int index] { get { return loadedItems[index]; } }

//        /// <summary>
//        /// Gets the number of distinctElements contained in the <see cref="ICollection{T}"/>.
//        /// </summary>
//        /// <value>The number of distinctElements contained in the <see cref="ICollection{T}"/>.</value>
//        public override int Count { get { return loadedItems.Count; } }

//        public StandardReadOnlyList(IList<T> loadedItems) { this.loadedItems = loadedItems; }

//        /// <summary>
//        /// Determines whether the ICollection<T> contains a specific value.
//        /// </summary>
//        /// <param name="map">The object to locate in the ICollection<T>.</param>
//        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
//        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
//        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
//        /// implementation to use for comparing keys.
//        /// </remarks>
//        public override bool Contains(T map) { return loadedItems.Contains(map); }

//        /// <summary>
//        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
//        /// </summary>
//        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
//        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="ArgumentNullException">array is null.</exception>
//        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        public override void CopyTo(T[] loadedItems, int arrayIndex) { loadedItems.CopyTo(loadedItems, arrayIndex); }

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
//        public override IEnumerator<T> GetEnumerator() { return loadedItems.GetEnumerator(); }

//        /// <summary>
//        /// Determines the index of a specific map in the IList<T>.
//        /// </summary>
//        /// <param name="map">The object to locate in the IList<T>.</param>
//        /// <returns>The index of map if found in the loadedItems; otherwise, -1.</returns>
//        /// <remarks>
//        /// If an object occurs multiple times in the loadedItems, the IndexOf method always returns the first instance found.
//        /// </remarks>
//        public override int IndexOf(T map) { return loadedItems.IndexOf(map); }
//    }
//}
