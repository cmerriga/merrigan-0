using System;
using System.Collections;
using System.Collections.Generic;
using Merrigan0.ArraysInternal;
using Merrigan0.SetsInternal;

namespace Merrigan0 {
    //// Move ICollection/IList to a cast operation, backed by an Extender
    [Untested]
    [WhatItIs("An unordered group of distinct items. Distinctness is defined by the Set instance.")]
    public abstract class Set<T> : IEnumerable<T> {
        private const int ARRAY_EXTENSION = 0;
        private const int ILIST_EXTENSION = 1;

        private Extender extender;

        public Array<T> Array {
            get {
                Array<T> a = (Array<T>)extender[ARRAY_EXTENSION];
                if (a == null) {
                    a = new EnumerableWrapperArray<T>(this);
                    extender[ARRAY_EXTENSION] = a;
                }
                return a;
            }
        }

        public ICollection<T> ICollection {
            get {
                return (ICollection<T>)IList;
            }
        }

        public IList<T> IList {
            get {
                IList<T> list = (IList<T>)extender[ILIST_EXTENSION];
                if (list == null) {
                    list = new ArrayList<T>(Array);
                    extender[ILIST_EXTENSION] = list;
                }
                return list;
            }
        }

        [WhatItIs("The empty set. Interned.")]
        public static Set<T> Empty { get { return EmptySet<T>.Only; } }

        public static Set<T> From(IEnumerable<T> items) { return From(Array<T>.From(items)); }
        public static Set<T> From(params T[] block) { return From(Array<T>.From(block)); }
        public static Set<T> From(T item) { return new SingleElementSet<T>(item); }
        
        public static Set<T> From(Array<T> a) {
            //if (a.IsDistinct) {
            //    return new DistinctArraySet<T>(a);
            //}
            return new ArraySet<T>(a);
        }

        public static Set<T> FromDistinct(ICollection<T> collection) { return new ArraySet<T>(Array<T>.FromDistinct(collection)); }
        ////public static Set<T> FromDistinct(Array<T> array) { return new DistinctArraySet<T>(array); }
        //////public static Set<T> FromDistinct(Array<T> array, Func<T, T, int> compare) { return new SortedArraySet<T>(new SortingArray<T>(array, compare)); }

        [WhatItIs("Values that may be considered children of this set.")]
        public virtual Array<T> Children { get { return Array<T>.From(this); } }

        [WhatItIs("The set of all values not in this set.")]
        public virtual Set<T> Complement { get { return new ComplementSet<T>(this); } }

        ////// only for ICollection
        ///// <summary>
        ///// Gets the number of elements contained in the <see cref="ICollection{T}"/>.
        ///// </summary>
        ///// <value>The number of elements contained in the <see cref="ICollection{T}"/>.</value>
        //public int Count { get { return (int)Length; } }

        public virtual bool Inverse { get { return false; } }

        ///// <summary>
        ///// Gets a value indicating whether the ICollection<T> is read-only.
        ///// </summary>
        ///// <value><c>true</c> if the ICollection<T> is read-only; otherwise, <c>false</c>.</value>
        ///// <remarks>
        ///// A collection that is read-only does not allow the addition or removal of elements after the collection is created. 
        ///// Note that read-only in this context does not indicate whether individual elements of the collection can be modified, 
        ///// since the ICollection<T> interface only supports addition and removal operations. For example, the IsReadOnly 
        ///// property of an array that is cast or converted to an ICollection<T> object returns true, even though individual 
        ///// array elements can be modified.
        ///// </remarks>
        //public bool IsReadOnly { get { return true; } }

        public abstract long Length { get; }

        ////public Set(Func<T, T, int> compare) {
        ////    Compare = compare;
        ////}

        ///// <summary>
        ///// Adds an item to the 
        ///// </summary>
        ///// <param name="map">The object to add to the ICollection<T>.</param>
        ///// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        //public void Add(T item) { throw new NotSupportedException(); }

        [WhatItIs("Intersection. The items that are in both sets.")]
        public virtual Set<T> And(Set<T> s) { return new IntersectionSet<T>(this, s); }

        ///// <summary>
        ///// Removes all items from the ICollection<T>.
        ///// </summary>
        ///// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        //public void Clear() { throw new NotSupportedException(); }

        /// <summary>
        /// Determines whether the ICollection<T> contains a specific value.
        /// </summary>
        /// <param name="map">The object to locate in the ICollection<T>.</param>
        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
        /// implementation to use for comparing keys.
        /// </remarks>
        public abstract bool Contains(T item);

        public Set<TTo> Convert<TTo>() {
            return new ConvertSet<T, TTo>(this);
        }

        //public virtual object Convert(Type toType) {
        //    return new ConvertSet<
        //}

        ///// <summary>
        ///// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        ///// </summary>
        ///// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        ///// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ///// <exception cref="ArgumentNullException">array is null.</exception>
        ///// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        //public virtual void CopyTo(T[] array, int arrayIndex) {
        //    long i = 0;
        //    foreach (T item in this) {
        //        array[arrayIndex + i] = item;
        //        ++i;
        //    }
        //}

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public abstract IEnumerator<T> GetEnumerator();

        [WhatItIs("Minus. This set, without any of the elements of a second set.")]
        public virtual Set<T> Not(Set<T> s) { return new SubtractionSet<T>(this, s); }

        [WhatItIs("Union. This set, including any of the elements of a second set.")]
        public virtual Set<T> Or(Set<T> s) { return new UnionSet<T>(this, s); }

        ///// <summary>
        ///// Removes the first occurrence of a specific object from the ICollection<T>.
        ///// </summary>
        ///// <param name="map">The object to remove from the ICollection<T>.</param>
        ///// <returns>
        ///// true if item was successfully removed from the ICollection<T>; otherwise, false. 
        ///// This method also returns false if item is not found in the original ICollection<T>.
        ///// </returns>
        ///// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        ///// <remarks>
        ///// Implementations can vary in how they determine equality of objects; for example, List<T> uses Comparer<T>.Default, whereas, Dictionary<TKey,TValue>
        ///// allows the user to specify the IComparer<T> implementation to use for comparing keys.<para/>
        ///// In collections of contiguous elements, such as lists, the elements that follow the removed element move up to occupy the vacated spot. If the 
        ///// collection is indexed, the indexes of the elements that are moved are also updated. This behavior does not apply to 
        ///// collections where elements are conceptually grouped into buckets, such as a map table.
        ///// </remarks>
        //public bool Remove(T item) { throw new NotSupportedException(); }

        [WhatItIs("Whether a set is a subset of this one.")]
        public virtual bool Subset(Set<T> s) {
            if (s.Length > Length) {
                return false;
            }
            foreach (T item in s) {
                if (!Contains(item)) {
                    return false;
                }
            }
            return true;
        }

        [WhatItIs("The set as a POCO array.")]
        public virtual T[] ToBlock() {
            T[] block = new T[Length];
            long i = 0;
            foreach (T item in this) {
                block[i] = item;
                ++i;
            }
            return block;
        }

        public override string ToString() {
            return Array<T>.From(ToBlock()).ToString();
        }

        [WhatItIs("The union of this set and another item.")]
        public virtual Set<T> With(T item) {
            if (Contains(item)) {
                return this;
            }
            return new WithSet<T>(this, item);
        }

        [WhatItIs("This set, with any item removed.")]
        public virtual Set<T> Without(T item) {
            if (!Contains(item)) {
                return this;
            }
            return new WithoutSet<T>(this, item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
