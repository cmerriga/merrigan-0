using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0 {
//    // An array where values are accessed via two integer indexes. Often called a ragged array.
//    [Untested]
//    public abstract class Array2D<T> : Array<Array<T>>, IParent<T> {
//        public static readonly Array2D<T> Empty = new EmptyArray2D<T>();
//        private const int iString = 0;
//        private const int iFlattened = 1;

//        private Extender lazyExtender;

//        protected Extender Extender {
//            get {
//                if (lazyExtender == null) {
//                    lazyExtender = new Extender();
//                    lazyExtender.RegisterCreateResultFunction(iString, () => this.AsString());
//                    lazyExtender.RegisterCreateResultFunction(iFlattened, () => this.Flattened);
//                }
//                return lazyExtender;
//            }
//        }

//        public static Array2D<T> Empty { get { return EmptyArray2D<T>.Only; } }

//        public virtual Array<T> this[long r] { get { return Row(r); } }

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
//        public T this[int r, int i] {
//            get { return this[(long)r, (long)i]; }

//            [NotSupported]
//            set { throw new NotSupportedException(); }
//        }

//        public T this[long r, long i] {
//            get { return Item(r, i); }
//        }

//        public override Array<T> Children { get { return Flattened.Children; } }

//        public virtual long Count { get { return Flattened.Length; } }

//        public Array<T> Flattened { get { return (Array<T>)Extender[iFlattened]; } }

//        public abstract long Height { get; }

//        static Array2D() {
//            ////RegisterConversion(typeof(Array<Array<T>>), AsArrayOfArrays);
//        }

//        public static bool Equals(Array2D<T> ra1, Array2D<T> ra2) {
//            if (ra1.Height != ra2.Height) {
//                return false;
//            }

//            long h = ra1.Height;
//            for (long r = 0; r < h; ++r) {
//                long w = ra1[r].Length;
//                if (w != ra2[r].Length) {
//                    return false;
//                }
//                for (long i = 0; i < w; ++i) {
//                    if (!object.Equals(ra1[r, i], ra2[r, i])) {
//                        return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public static Array2D<T> From(T item) { return new SingleItemArray2D<T>(item); }

//        public virtual bool All(bool condition) {
//            return Flattened.All(condition);
//        }

//        public virtual bool All(Func<T, bool> condition) {
//            return Flattened.All(condition);
//        }

//        public virtual bool Any(bool condition) {
//            return Flattened.Any(condition);
//        }

//        public virtual bool Any(Func<T, bool> condition) {
//            return Flattened.Any(condition);
//        }

//        ////public virtual Array<Array<T>> AsArrayOfArrays() {

//        ////}

//        public virtual Array2D<T> Bottom([LessOrEqual("Height")] long length) { return Subarray(Height - length, Height); }

//        public virtual Array2D<TTo> Cast<TTo>() { return new CastArray2D<T, TTo>(this); }

//        /// <summary>
//        /// Determines whether the ICollection<T> contains a specific value.
//        /// </summary>
//        /// <param name="map">The object to locate in the ICollection<T>.</param>
//        /// <returns>true if map is found in the ICollection<T>; otherwise, false.</returns>
//        /// <remarks>Implementations can vary in how they determine equality of objects; for example, List<T> uses 
//        /// Comparer<T>.Default, whereas Dictionary<TKey,TValue> allows the user to specify the IComparer<T> 
//        /// implementation to use for comparing keys.
//        /// </remarks>
//        public virtual bool Contains(T item) {
//            return Flattened.Contains(item);
//        }

//        public virtual bool Contains(Array<T> items) {
//            return Flattened.Contains(items);
//        }

//        public virtual Array2D<TTo> Convert<TTo>() { return new ConvertArray2D<T, TTo>(this); }

//        /// <summary>
//        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
//        /// </summary>
//        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
//        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="ArgumentNullException">array is null.</exception>
//        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        public virtual void CopyTo(T[] array, int arrayIndex) {
//            Flattened.CopyTo(array, arrayIndex);
//        }

//        public virtual void Each(Action<T> action) {
//            Flattened.Each(action);
//        }

//        public virtual void Each<P1>(Action<T, P1> action, P1 argument1) {
//            Flattened.Each(action, argument1);
//        }

//        public virtual void Each<P1, P2>(Action<T, P1, P2> action, P1 argument1, P2 argument2) {
//            Flattened.Each(action, argument1, argument2);
//        }

//        public virtual void Each<P1, P2, P3>(Action<T, P1, P2, P3> action, P1 argument1, P2 argument2, P3 argument3) {
//            Flattened.Each(action, argument1, argument2, argument3);
//        }

//        public virtual void Each(Action<long, T> action) {
//            Flattened.Each(action);
//        }

//        // Gets the indexes of all items fitting a condition
//        public Array<Tuple<long, long>> FindAll(Func<T, bool> condition) {
//            //// could be lazy
//            MutableArray<Tuple<long, long>> indexesSoFar = new MutableArray<Tuple<long, long>>();
//            for (long r = 0L; r < Height; ++r) {
//                Array<T> row = this[r];
//                long w = row.Length;
//                for (long i = 0L; i < w; ++i) {
//                    if (condition(row[i])) {
//                        indexesSoFar.Append(new Tuple<long, long>(r, i));
//                    }
//                }
//            }
//            return indexesSoFar.Current;
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
//        public virtual IEnumerator<T> GetEnumerator() {
//            for (long r = 0L; r < Height; ++r) {
//                long w = RowLength(r);
//                for (long i = 0L; i < w; ++i) {
//                    yield return Item(r, i);
//                }
//            }
//        }

//        [return: NotNegative]
//        public virtual long HowMany(Func<T, bool> predicate) {
//            return Flattened.HowMany(predicate);
//        }

//        public bool None(Func<T, bool> predicate) { return !Any(predicate); }

//        public abstract Array<T> Row(long r);

//        public virtual long RowLength(long r) {
//            return Row(r).Length;
//        }

//        public virtual Array2D<T> Subarray(long r, long h) { return new Subarray2D<T>(this, r, h); }

//        public virtual Array2D<T> Subarray(long r, long h) { return new Subarray2D<T>(this, r, h); }

//        public virtual Array2D<T> Top([LessOrEqual("Height")] long length) { return Subarray(0L, length); }

//        public override string ToString() {
//            return (string)Extender[iString];
//        }

//        public virtual Matrix<T2> Transform<T2>(Func<T, T2> transform) { return new TransformArray2D<T, T2>(this, transform); }
//        public virtual Matrix<T2> Transform<T2>(Func<long, long, T, T2> transformWithIndexes) { return new TransformWithIndexesTransformArray2D<T, T2>(this, transformWithIndexes); }
//        //public virtual Array<T2> TransformMemoized<T2>(Func<T, T2> transform) { return new MemoizedTransformArray<T, T2>(this, transform); }

//        public abstract bool TryGetItem(long r, long c, out T item);

//        //public virtual Array<T> WithSubsitution(Array<T> unwantedItems, Array<T> wantedItems) {
//        //    long iLastInspected = 0;
//        //    long iFoundItems;
//        //    if (!TryGetIndex(unwantedItems, iLastInspected, out iFoundItems)) {
//        //        return this;
//        //    }
//        //    MutableArray<T> withSubstitutionSoFar = new MutableArray<T>();
//        //    do {
//        //        withSubstitutionSoFar.Append(Subarray(iLastInspected, iFoundItems - iLastInspected));
//        //        iLastInspected = iFoundItems + unwantedItems.Length;
//        //    } while (TryGetIndex(unwantedItems, iLastInspected, out iFoundItems));
//        //    return withSubstitutionSoFar.Current;
//        //}

//        protected virtual Array<T> AsArray() {
//            return new FlattenedArray2D<T>(this);
//        }

//        protected virtual string AsString() {
//            long maxLength = 40;
//            MutableString stringSoFar = new MutableString();
//            stringSoFar += Height;
//            stringSoFar += " rows";
//            stringSoFar += " [";
//            for (long r = 0L; r < Height; ++r) {
//                if (stringSoFar.Current.Length >= maxLength) { break; }
//                if (r != 0L) {
//                    stringSoFar += ", ";
//                }
//                stringSoFar += '(';
//                Array<T> row = Row(r);
//                long w = row.Length;
//                for (long c = 0L; c < w; ++c) {
//                    if (stringSoFar.Current.Length >= maxLength) { break; }
//                    if (c != 0L) {
//                        stringSoFar += ", ";
//                    }
//                    stringSoFar += row[c];
//                }
//                stringSoFar += ')';
//            }
//            return stringSoFar.Current.Truncated(maxLength - 1) + ']';
//        }

//        // Must throw IndexOutOfRangeException if indexes are wrong
//        protected virtual T Item([Less("Height")][NotNegative] long r, /*[Less("Width")]*/[NotNegative] long i) {
//            return ItemUnchecked(r, i);
//        }

//        protected abstract T ItemUnchecked(long r, long i);
//    }
//}
