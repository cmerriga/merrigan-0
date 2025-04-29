using System;

namespace Merrigan0.Internal.CodeTemplates.Collections.Generic {
    internal interface IDictionary<TKey, TValue> :
        System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>,
        System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> {
        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <value>The element with the specified key.</value>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <c>key</c> is not found.</exception>
        /// <exception cref="NotSupportedException">The property is set and the <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        TValue this[TKey key] { get; set; }

        /// <summary>
        /// Gets an <see cref="ICollection{TKey}"/>containing the keys of the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>An <see cref="ICollection{TKey}"/> containing the keys of the object that implements <see cref="IDictionary{TKey, TValue}"/>.</value>
        System.Collections.Generic.ICollection<TKey> Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection{TValue}"/>containing the values of the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>An <see cref="ICollection{TValue}"/> containing the values of the object that implements <see cref="IDictionary{TKey, TValue}"/>.</value>
        System.Collections.Generic.ICollection<TValue> Values { get; }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <c>key</c> is not found.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Determines whether the <see cref="IDictionary{TKey, TValue}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IDictionary{TKey, TValue}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the
        /// original <see cref="IDictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        bool Remove(TKey key);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value 
        /// for the type of the <c>value</c> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the object that implements <see cref="IDictionary{TKey, TValue}"/> contains an element with the specified key; 
        /// otherwise, <c>false</c>.
        /// </returns>
        bool TryGetValue(TKey key, out TValue value);
    }

    internal class ExampleDictionary<TKey, TValue> : IDictionary<TKey, TValue> {
        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <value>The element with the specified key.</value>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <c>key</c> is not found.</exception>
        /// <exception cref="NotSupportedException">The property is set and the <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        public TValue this[TKey key] {
            get { return default(TValue); } ////
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
        /// Gets an <see cref="ICollection{TKey}"/>containing the keys of the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>An <see cref="ICollection{TKey}"/> containing the keys of the object that implements <see cref="IDictionary{TKey, TValue}"/>.</value>
        public System.Collections.Generic.ICollection<TKey> Keys { get { return null; } } ////

        /// <summary>
        /// Gets an <see cref="ICollection{TValue}"/>containing the values of the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>An <see cref="ICollection{TValue}"/> containing the values of the object that implements <see cref="IDictionary{TKey, TValue}"/>.</value>
        public System.Collections.Generic.ICollection<TValue> Values { get { return null; } } ////

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item) {
            ////
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <c>key</c> is not found.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        public void Add(TKey key, TValue value) {
            ////
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
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
        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item) {
            return false; ////
        }

        /// <summary>
        /// Determines whether the <see cref="IDictionary{TKey, TValue}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IDictionary{TKey, TValue}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        public bool ContainsKey(TKey key) {
            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><c>array</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>arrayIndex</c> is less than 0.</exception>
        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            ////
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() {
            return null; ////
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the
        /// original <see cref="IDictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentNullException"><c>key</c> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey, TValue}"/> is read-only.</exception>
        public bool Remove(TKey key) {
            return true; ////
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the <see cref="ICollection{T}"/>; otherwise, <c>false</c>. 
        /// This method also returns <c>false</c> if <c>item</c> is not found in the original <see cref="ICollection{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        /// <remarks>
        /// Implementations can vary in how they determine equality of objects; for example, <see cref="List{T}"/> uses <see cref="Comparer{T}.Default"/>, 
        /// whereas, <see cref="Dictionary{TKey,TValue}"/>
        /// allows the user to specify the <see cref="IComparer{T}"/> implementation to use for comparing keys.<para/>
        /// In collections of contiguous distinct elements, such as lists, the distinct elements that follow the removed element move up to occupy the vacated spot. If the 
        /// collection is indexed, the indexes of the distinct elements that are moved are also updated. This behavior does not apply to 
        /// collections where distinct elements are conceptually grouped into buckets, such as a hash table.
        /// </remarks>
        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item) {
            return true; ////
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value 
        /// for the type of the <c>value</c> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the object that implements <see cref="IDictionary{TKey, TValue}"/> contains an element with the specified key; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value) {
            ////
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
