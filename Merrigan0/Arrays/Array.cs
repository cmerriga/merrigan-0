using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Merrigan0.ArraysInternal;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0 {
    [DebuggerDisplay("{DebuggerDisplay}")]
    public abstract class Array<T> : /* IList<T>, IReadOnlyList<T>,*/ IEnumerable<T>, IParent<T> {
        [ExtenderIndex]
        private const int ILIST_EXTENSION = 0;

        [ExtenderIndex]
        private const int BLOCK_EXTENSION = 1;

        private Extender extender;

        [return: Equals("Length", "a1.Length + a2.Length")]
        [Untested]
        public static Array<T> operator +(Array<T> a1, Array<T> a2) { return new ConcatenateArray<T>(a1, a2); }

        //[Example("(1, 2, 3)", 4, "(1, 2, 3, 4)")]
        [return: Equals("Length", "a.Length + 1")]
        [Untested]
        public static Array<T> operator +(Array<T> a, T item) { return new AppendArray<T>(a, item); }

        [Example(null, null)]
        [return: Equals("Length", "a.Length")]
        [Untested]
        public static implicit operator T[]([MayBeNull] Array<T> a) {
            if (a == null) {
                return null;
            }
            T[] block = (T[])a.extender[BLOCK_EXTENSION];
            if (block == null) {
                block = a.ToBlock();
                a.extender[BLOCK_EXTENSION] = block;
            }
            return block;
        }

        //// These are not allowed. Can't do conversions to or from interfaces
        //public static implicit operator ICollection<T>(Array<T> a) { }

        //public static implicit operator IList<T>(Array<T> a) { }

        //public static implicit operator Array<T>(IEnumerable enumerable) {
        //    return new EnumerableWrapperArray<object>(new TypedEnumerable(enumerable));
        //}

        //public static implicit operator Array<T>(IEnumerable<T> enumerable) {
        //    return new EnumerableWrapperArray<T>(enumerable);
        //}

        [Example(null, null)]
        [return: Equals("Length", "block.Length")]
        [Untested]
        public static implicit operator Array<T>([MayBeNull] T[] block) {
            if (block == null) {
                return null;
            }
            return new BlockWrapperArray<T>(block);
        }

        //[Example(null, null)]
        //[return: Equals("Length", "a.Length")]
        //[Untested]
        //public static implicit operator Node([MayBeNull] Array<T> a) {
        //    if (a == null) {
        //        return null;
        //    }
        //    return new ArrayNode<T>(a);
        //}

        //public static implicit operator Array<TTo>(Array<T> a) {
        //    if (a == null) { return null; }
        //    return new CastArray<T, TTo>(a);
        //}

        ////public static implicit operator Array<T>(ICollection<T> collection) {
        ////    if (collection == null) {
        ////        return null;
        ////    }
        ////    return new CollectionArray<T>(collection);
        ////}

        ////public static implicit operator Array<T>(IList<T> list) {
        ////    if (list == null) {
        ////        return null;
        ////    }
        ////    return new ListWrapperArray<T>(list);
        ////}

        [Untested]
        public static readonly Array<T> Empty = new EmptyArray<T>();

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <value>The element at the specified index.</value>
        /// <exception cref="ArgumentOutOfRangeException">index is not a valid index in the IList<T>.</exception>
        /// <remarks>
        /// This property provides the ability to access a specific element in the collection by using the 
        /// following syntax: myCollection[index].<para/>
        /// The C# language uses the this keyword to define the indexers instead of implementing the GetCharacter[] 
        /// property. Visual Basic implements GetCharacter[] as a default property, which provides the same indexing functionality.
        /// </remarks>
        [Untested]
        public T this[int index] {
            [Example("(1, 2, 3)", 1, 2)]
            get { return this[(long)index]; }

            [NotSupported]
            set { throw new NotSupportedException(); }
        }

        [Untested]
        public T this[long i] {
            get { return GetItem(i); }
        }

        [Equals("Length", "instance.Length")]
        [Untested]
        public virtual Array<T> Children { get { return this; } }

        [MayBeNull(When = "It is not sorted")]
        [Untested]
        public Func<T, T, int> CompareFunction { get; protected set; }

        [Equals("Length", "instance.Length")]
        [Untested]
        public T[] Block {
            get {
                T[] block = (T[])extender[BLOCK_EXTENSION];
                if (block == null) {
                    block = ToBlock();
                    extender[BLOCK_EXTENSION] = block;
                }
                return block;
            }
        }

        [Equals("instance")]
        public ICollection<T> ICollection {
            get {
                return IList;
            }
        }

        [Equals("Length", "instance.Length")]
        public IList<T> IList {
            get {
                IList<T> list = (IList<T>)extender[ILIST_EXTENSION];
                if (list == null) {
                    list = new ArrayList<T>(this);
                    extender[ILIST_EXTENSION] = list;
                }
                return list;
            }
        }

        ////// Need better name!
        [Untested]
        public bool IsDistinct { get; protected set; }

        [Untested]
        public abstract long Length { get; }

        [DiagnosticOnly]
        [Untested]
        protected String DebuggerDisplay {
            get {
                return ToString();
            }
        }

        [Untested]
        protected Array() { }

        [Untested]
        protected Array(Func<T, T, int> compare) {
            CompareFunction = compare;
        }

        [Untested]
        protected Array(bool distinct) {
            IsDistinct = distinct;
        }

        [Untested]
        protected Array(Func<T, T, int> compare, bool distinct) {
            CompareFunction = compare;
            IsDistinct = distinct;
        }

        [Untested]
        public static bool Equals(Array<T> a1, Array<T> a2) {
            if (a1.Length != a2.Length) {
                return false;
            }
            long a1Length = a1.Length;
            for (long i = 0; i < a1Length; ++i) {
                if (!object.Equals(a1[i], a2[i])) {
                    return false;
                }
            }
            return true;
        }

        [Untested]
        public static Array<T> From(T item) { return new SingleItemArray<T>(item); }
        [Untested]
        public static Array<T> From(T item, long repetitions) { return new FillArray<T>(item, repetitions); }

        [Untested]
        public static Array<object> From(IEnumerable enumerable) { return new EnumerableWrapperArray(enumerable); }

        [Untested]
        public static Array<T> From(IEnumerable<T> enumerable) { return new EnumerableWrapperArray<T>(enumerable); }

        //public static Array<T> From(ref IEnumerable<T> enumerable) { 
        //    Array<T> a = new EnumerableWrapperArray<T>(enumerable);
        //    enumerable = null;
        //    return a;
        //}

        [Untested]
        public static Array<T> From(IEnumerable<T> enumerable, long length) { return new EnumerableWrapperArray<T>(enumerable).Truncated(length); }
        [Untested]
        public static Array<T> From(IEnumerable<T> enumerable, long i, long length) { return new EnumerableWrapperArray<T>(enumerable).Subarray(i, length); }

        [Untested]
        public static Array<T> From(params T[] block) { return new BlockWrapperArray<T>(block); }
        ////public static Array<T> From(T[] items) { return new BlockWrapperArray<T>(items); }
        [Untested]
        public static Array<T> From(T[] items, long length) { return new BlockWrapperArray<T>(items).Truncated(length); }
        [Untested]
        public static Array<T> From(T[] items, long i, long length) { return new BlockWrapperArray<T>(items).Subarray(i, length); }

        [Untested]
        public static Array<T> From(ICollection<T> collection) { return new CollectionWrapperArray<T>(collection); }
        [Untested]
        public static Array<T> From(ICollection<T> collection, long length) { return new CollectionWrapperArray<T>(collection).Truncated(length); }
        [Untested]
        public static Array<T> From(ICollection<T> collection, long i, long length) { return new CollectionWrapperArray<T>(collection).Subarray(i, length); }
        [Untested]
        public static Array<T> From(IList<T> list) { return new ListWrapperArray<T>(list); }

        [Untested]
        public static Array<T> From(IList<T> list, long length) { return new ListWrapperArray<T>(list).Truncated(length); }
        [Untested]
        public static Array<T> From(IList<T> list, long i, long length) { return new ListWrapperArray<T>(list).Subarray(i, length); }

        [Untested]
        public static Array<T> FromDistinct(IEnumerable<T> enumerable) { return new EnumerableWrapperArray<T>(enumerable, null, true); }
        [Untested]
        public static Array<T> FromDistinct(params T[] block) { return new BlockWrapperArray<T>(block, null, true); }
        [Untested]
        public static Array<T> FromDistinct(ICollection<T> collection) { return new CollectionWrapperArray<T>(collection, null, true); }
        [Untested]
        public static Array<T> FromDistinct(IList<T> list) { return new ListWrapperArray<T>(list, null, true); }

        [Untested]
        public static Array<T> FromSorted(IEnumerable<T> enumerable) { return new EnumerableWrapperArray<T>(enumerable, Comparers.CompareFunction<T>(), false); }
        [Untested]
        public static Array<T> FromSorted(params T[] block) { return new BlockWrapperArray<T>(block, Comparers.CompareFunction<T>(), false); }
        [Untested]
        public static Array<T> FromSorted(ICollection<T> collection) { return new CollectionWrapperArray<T>(collection, Comparers.CompareFunction<T>(), false); }
        [Untested]
        public static Array<T> FromSorted(IList<T> list) { return new ListWrapperArray<T>(list, Comparers.CompareFunction<T>(), false); }

        [Untested]
        public static Array<T> FromSorted(IEnumerable<T> enumerable, Func<T, T, int> compare) { return new EnumerableWrapperArray<T>(enumerable, compare, false); }
        [Untested]
        public static Array<T> FromSorted(T[] block, Func<T, T, int> compare) { return new BlockWrapperArray<T>(block, compare, false); }
        [Untested]
        public static Array<T> FromSorted(ICollection<T> collection, Func<T, T, int> compare) { return new CollectionWrapperArray<T>(collection, compare, false); }
        [Untested]
        public static Array<T> FromSorted(IList<T> list, Func<T, T, int> compare) { return new ListWrapperArray<T>(list, compare, false); }

        [Untested]
        public static Array<T> FromSortedDistinct(IEnumerable<T> enumerable) { return new EnumerableWrapperArray<T>(enumerable, Comparers.CompareFunction<T>(), true); }
        [Untested]
        public static Array<T> FromSortedDistinct(params T[] block) { return new BlockWrapperArray<T>(block, Comparers.CompareFunction<T>(), true); }
        [Untested]
        public static Array<T> FromSortedDistinct(ICollection<T> collection) { return new CollectionWrapperArray<T>(collection, Comparers.CompareFunction<T>(), true); }
        [Untested]
        public static Array<T> FromSortedDistinct(IList<T> list) { return new ListWrapperArray<T>(list, Comparers.CompareFunction<T>(), true); }

        [Untested]
        public static Array<T> FromSortedDistinct(IEnumerable<T> enumerable, Func<T, T, int> compare) { return new EnumerableWrapperArray<T>(enumerable, compare, true); }
        [Untested]
        public static Array<T> FromSortedDistinct(T[] block, Func<T, T, int> compare) { return new BlockWrapperArray<T>(block, compare, true); }
        [Untested]
        public static Array<T> FromSortedDistinct(ICollection<T> collection, Func<T, T, int> compare) { return new CollectionWrapperArray<T>(collection, compare, true); }
        [Untested]
        public static Array<T> FromSortedDistinct(IList<T> list, Func<T, T, int> compare) { return new ListWrapperArray<T>(list, compare, true); }

        [Untested]
        public virtual bool All(bool condition) {
            foreach (T item in this) {
                if (!condition) {
                    return false;
                }
            }
            return true;
        }

        [Untested]
        public virtual bool All(Func<T, bool> condition) {
            foreach (T item in this) {
                if (!condition(item)) {
                    return false;
                }
            }
            return true;
        }

        [Untested]
        public virtual bool Any(bool condition) {
            foreach (T item in this) {
                if (condition) {
                    return true;
                }
            }
            return false;
        }

        [Untested]
        public virtual bool Any(Func<T, bool> condition) {
            foreach (T item in this) {
                if (condition(item)) {
                    return true;
                }
            }
            return false;
        }

        [Untested]
        public virtual Array<TTo> Cast<TTo>() { return new CastArray<T, TTo>(this); }

        ///// <summary>
        ///// Removes all items from the ICollection<T>.
        ///// </summary>
        ///// <exception cref="NotSupportedException">The ICollection<T> is read-only.</exception>
        //[NotSupported]
        //public void Clear() { throw new NotSupportedException(); }

        ////public delegate Array<TTo> ArrayAction
        [Untested]
        public virtual Array<TTo> Collect<TTo>(Func<T, Array<TTo>> find) {
            MutableArray<TTo> itemsSoFar = new MutableArray<TTo>();
            foreach (T item in this) {
                Array<TTo> itemsToCollect = find(item);
                itemsSoFar.Append(itemsToCollect);
            }
            return itemsSoFar.Current;
        }

        /// <summary>
        /// Determines whether the ICollection<T> contains a specific value.
        /// </summary>
        /// <param name="map">The object to locate in the ICollection<T>.</param>
        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
        /// implementation to use for comparing keys.
        /// </remarks>
        [Untested]
        public virtual bool Contains(T item) {
            long dummy;
            return TryGetIndex(item, 0, out dummy);
        }

        [Untested]
        public virtual bool Contains(Array<T> items) {
            long dummy;
            return TryGetIndex(items, 0, out dummy);
        }

        [Untested]
        public virtual Array<TTo> Convert<TTo>() { return new ConvertArray<T, TTo>(this); }

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        [Untested]
        public virtual void CopyTo(T[] array, int arrayIndex) {
            long length = Length;
            for (long i = 0; i < length; ++i) {
                array[arrayIndex + i] = GetItem(i);
            }
        }

        [Untested]
        [WhatItIs("All possible pairs of this and another array, in major-minor order.")]
        public virtual Array<Tuple<T, T2>> Cross<T2>(Array<T2> array) { return new CrossArray<T, T2>(this, array); }

        //// Should this be a Set? (Yes)
        //// Better name would allow IsDistinct to become Distinct. WithoutDuplicates?
        [Untested]
        public virtual Array<T> Distinct() {
            if (IsDistinct) {
                return this;
            }
            return new ArrayDistinctArray<T>(this);
        }

        [Untested]
        public virtual Array<T3> Dot<T2, T3>(Array<T2> array, Func<T, T2, T3> combine) { return new DotArray<T, T2, T3>(this, array, combine); }

        [Untested]
        public virtual void Each(Action<T> action) {
            foreach (T item in this) {
                action(item);
            }
        }

        [Untested]
        public virtual void Each<P1>(Action<T, P1> action, P1 argument1) {
            foreach (T item in this) {
                action(item, argument1);
            }
        }

        [Untested]
        public virtual void Each<P1, P2>(Action<T, P1, P2> action, P1 argument1, P2 argument2) {
            foreach (T item in this) {
                action(item, argument1, argument2);
            }
        }

        [Untested]
        public virtual void Each<P1, P2, P3>(Action<T, P1, P2, P3> action, P1 argument1, P2 argument2, P3 argument3) {
            foreach (T item in this) {
                action(item, argument1, argument2, argument3);
            }
        }

        [Untested]
        public virtual void Each(Action<long, T> action) {
            long length = Length;
            for (long i = 0; i < length; ++i) {
                action(i, GetItem(i));
            }
        }

        // Gets the indexes of all items fitting a condition
        [Untested]
        public Array<long> FindAll(Func<T, bool> condition) {
            //// could be lazy
            MutableArray<long> indexesSoFar = new MutableArray<long>();
            long length = Length;
            for (long i = 0; i < length; ++i) {
                if (condition(GetItem(i))) {
                    indexesSoFar.Append(i);
                }
            }
            return indexesSoFar.Current;
        }

        [Untested]
        public T First() {
            return GetItem(0);
        }

        [Untested]
        public T First(Func<T, bool> filter) {
            foreach (T item in this) {
                if (filter(item)) {
                    return item;
                }
            }
            throw new Exception();
        }

        [Untested]
        public Array<T> First(long n) {
            return new Subarray<T>(this, 0, n);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        [Untested]
        public virtual IEnumerator<T> GetEnumerator() { return new ArrayEnumerator<T>(this); }

        /// <summary>
        /// Determines the index of a specific map in the IList<T>.
        /// </summary>
        /// <param name="map">The object to locate in the IList<T>.</param>
        /// <returns>The index of map if found in the array; otherwise, -1.</returns>
        /// <remarks>
        /// If an object occurs multiple times in the array, the IndexOf method always returns the first instance found.
        /// </remarks>
        [Untested]
        public int IndexOf(T item) {
            long i;
            if (!TryGetIndex(item, 0, out i)) {
                return -1;
            }
            return (int)i;
        }

        [Untested]
        public virtual T Last() {
            return GetItem(Length - 1);
        }

        [Untested]
        public virtual Array<T> Last(long n) {
            return new Subarray<T>(this, Length - n, n);
        }

        [Untested]
        public virtual Array<T> Left([LessOrEqual("Length")] long length) { return Subarray(0L, length); }

        //public virtual Map<T, T2> Map<T2>(Func<T, T2> transform) { return new TransformMap<T, T2>(transform); }
        [Untested]
        public bool None(Func<T, bool> predicate) { return !Any(predicate); }

        [return: NotNegative]
        [Untested]
        public virtual long HowMany(Func<T, bool> predicate) {
            long howManySoFar = 0;
            foreach (T item in this) {
                if (predicate(item)) {
                    ++howManySoFar;
                }
            }
            return howManySoFar;
        }

        [Untested]
        public virtual Array<T> OfType(Type type) {
            return Where(t => Reflection.Implements(t, type));
        }

        [Untested]
        public virtual Array<TResult> OfType<TResult>() {
            return OfType(typeof(TResult)).Cast<TResult>();
        }

        [Untested]
        public virtual long Product(Func<T, long> map) {
            long productSoFar = 0L;
            foreach (T item in this) {
                productSoFar *= map(item);
            }
            return productSoFar;
        }

        [Untested]
        public virtual Array<T> Reverse() { return new ReverseArray<T>(this); }

        [Is("TypeArgument", typeof(IComparable))]
        [Untested]
        public virtual Array<T> Sorted() { return Sorted(Comparers.CompareFunction<T>()); }

        [Untested]
        public virtual Array<T> Sorted(Func<T, T, int> compare) { return new ArraySortedArray<T>(this, compare); }

        // Returns true if the comparison function is the same one this is compared by.
        [Untested]
        public bool SortedBy(Func<T, T, int> compare) { return Object.Equals(compare, CompareFunction); }

        [Untested]
        public virtual Array<T> Right([LessOrEqual("Length")] long length) { return Subarray(0L, length); }

        [Untested]
        public Array<T> Subarray(long i) { return Subarray(i, Length - i); }

        [Untested]
        public virtual Array<T> Subarray(long i, long length) { return new Subarray<T>(this, i, length); }

        [Untested]
        public virtual long Sum(Func<T, long> map) {
            long sumSoFar = 0L;
            foreach (T item in this) {
                sumSoFar += map(item);
            }
            return sumSoFar;
        }

        [Untested]
        public virtual Map<K, T> ToMap<K>(Func<T, K> getKey) {
            MutableMap<K, T> mapSoFar = new MutableMap<K, T>();
            foreach (T item in this) {
                mapSoFar.Add(getKey(item), item);
            }
            return mapSoFar.Current;
        }

        [Untested]
        public virtual Map<K, V> ToMap<K, V>(Func<T, K> getKeyFunction, Func<T, V> getValueFunction) {
            MutableMap<K, V> mapSoFar = new MutableMap<K, V>();
            foreach (T item in this) {
                mapSoFar.Add(getKeyFunction(item), getValueFunction(item));
            }
            return mapSoFar.Current;
        }

        [Untested]
        public override string ToString() {
            long maxLength = 40;
            MutableString stringSoFar = new MutableString();
            stringSoFar += Length;
            stringSoFar += " " + (Length == 1 ? "item" : "items") + (Length > 0 ? ": " : System.String.Empty);
            bool first = true;
            foreach (T item in this) {
                if (stringSoFar.Current.Length >= maxLength) { break; }
                if (first) {
                    first = false;
                } else {
                    stringSoFar += ", ";
                }
                stringSoFar += item;
            }
            return stringSoFar.Current.Truncated(maxLength);
        }

        [Untested]
        public virtual Array<T2> Transform<T2>(Func<T, T2> transform) { return new TransformArray<T, T2>(this, transform); }
        [Untested]
        public virtual Array<T2> Transform<T2>(Func<long, T, T2> transformWithIndex) { return new TransformWithIndexArray<T, T2>(this, transformWithIndex); }
        //public virtual Array<T2> TransformMemoized<T2>(Func<T, T2> transform) { return new MemoizedTransformArray<T, T2>(this, transform); }

        [WhatItDoes("Creates a fresh block with the same content.")]
        [return: ShallowCopy]
        [Untested]
        public virtual T[] ToBlock() {
            T[] block = new T[Length];
            CopyTo(block, 0);
            return block;
        }

        [Untested]
        public virtual Array<T> Truncated(long length) { return new TruncatedArray<T>(this, length); }

        [Untested]
        public virtual bool TryGetIndex(T item, long iBegin, out long i) {
            if (CompareFunction != null) {
                return TryBinarySearch(iBegin, Length, item, out i);
            } else {
                long length = Length;
                for (long iToTry = iBegin; iToTry < length; ++iToTry) {
                    if (object.Equals(GetItem(iToTry), item)) {
                        i = iToTry;
                        return true;
                    }
                }
                i = -1;
                return false;
            }
        }

        [Untested]
        public virtual bool TryGetIndex(Array<T> items, long iBegin, out long i) {
            long itemsLength = items.Length;
            if (itemsLength == 0) {
                i = iBegin;
                return true;
            }
            if (CompareFunction != null) {
                long iToTry;
                if (!TryBinarySearch(iBegin, Length, items[0], out iToTry)) {
                    i = -1;
                    return false;
                }
                bool found = true;
                for (int jToTry = 0; jToTry < itemsLength; ++jToTry) {
                    if (!object.Equals(GetItem(iToTry + jToTry), items[jToTry])) {
                        found = false;
                        break;
                    }
                }
                if (found) {
                    i = iToTry;
                    return true;
                }
                i = -1;
                return false;
            } else {
                //// Depending on size, use Boyer-Moore
                long length = Length;
                for (long iToTry = iBegin; iToTry < length; ++iToTry) {
                    if (iToTry + itemsLength > length) {
                        itemsLength = length - iToTry;
                    }
                    bool found = true;
                    for (int jToTry = 0; jToTry < itemsLength; ++jToTry) {
                        if (!object.Equals(GetItem(iToTry + jToTry), items[jToTry])) {
                            found = false;
                            break;
                        }
                    }
                    if (found) {
                        i = iToTry;
                        return true;
                    }
                }
                i = -1;
                return false;
            }
        }

        [Untested]
        public virtual bool TryGetIndex(Func<T, bool> condition, long iBegin, out long i) {
            long length = Length;
            for (long iToTry = iBegin; iToTry < length; ++iToTry) {
                if (condition(GetItem(iToTry))) {
                    i = iToTry;
                    return true;
                }
            }
            i = -1;
            return false;
        }

        //// the Try pattern should maybe be jettisoned in favor of run-time checking only when called for
        [Untested]
        public abstract bool TryGetItem(long i, out T item);

        [Untested]
        public virtual Array<T> Where(Func<T, bool> condition) { return new FilteredArray<T>(this, condition); }

        [Untested]
        public virtual Array<T> Without(T item) {
            return Where(i => !object.Equals(i, item));
        }

        [Untested]
        public virtual Array<T> WithSubsitution(Array<T> unwantedItems, Array<T> wantedItems) {
            long iLastInspected = 0;
            long iFoundItems;
            if (!TryGetIndex(unwantedItems, iLastInspected, out iFoundItems)) {
                return this;
            }
            MutableArray<T> withSubstitutionSoFar = new MutableArray<T>();
            do {
                withSubstitutionSoFar.Append(Subarray(iLastInspected, iFoundItems - iLastInspected));
                iLastInspected = iFoundItems + unwantedItems.Length;
            } while (TryGetIndex(unwantedItems, iLastInspected, out iFoundItems));
            return withSubstitutionSoFar.Current;
        }

        [Untested]
        public virtual Array<Tuple<T, T2>> Zip<T2>(Array<T2> array) { return new ZipArray<T, T2>(this, array); }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        [Untested]
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        //// the Try pattern should maybe be jettisoned in favor of run-time checking only when called for
        [Untested]
        protected T GetItem([Less("Length")][NotNegative] long i) {
            T item;
            if (!TryGetItem(i, out item)) {
                Utilities.ThrowIndexOutOfRangeException(i, Length);
            }
            return item;
        }

        [Untested]
        protected virtual IEnumerator<T> GetEnumeratorFrom(long i) { return new IndexedArrayEnumerator<T>(this, i); }

        [Exists("CompareFunction")]
        [Untested]
        protected bool TryBinarySearch(long begin, long end, T item, out long i) {
            int compareResult;
            Func<T, T, int> compare = CompareFunction;
            if (begin < end - 1) {
                long midway = (begin + end) / 2;
                compareResult = CompareFunction(GetItem(midway), item);
                if (CompareResult.LeftBigger(compareResult)) {
                    return TryBinarySearch(midway + 1, end, item, out i);
                }

                return TryBinarySearch(begin, midway, item, out i);
            }

            // End is just after begin, or the same
            compareResult = compare(GetItem(begin), item);
            if (CompareResult.Equal(compareResult)) {
                i = begin;
                return true;
            }
            if (CompareResult.RightBigger(compareResult) || end == begin) {
                goto notfound;
            }
            compareResult = compare(GetItem(end), item);
            if (CompareResult.Equal(compareResult)) {
                goto notfound;
            }

        notfound:
            i = 0;
            return false;
        }
    }
}
