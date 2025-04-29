using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0 {
//    // An array where values are accessed via two integer indexes, but every row index has the same
//    // number of column indexes.
//    [Untested]
//    public abstract class RectangularArray<T> : Array2D<T> {
//        public static implicit operator T[,](RectangularArray<T> a) {
//            if (a == null) {
//                return null;
//            }
//            return a.ToBlock();
//        }

//        public static implicit operator RectangularArray<T>(T[,] block) {
//            if (block == null) {
//                return null;
//            }
//            return new BlockRectangularArray<T>(block);
//        }

//        public static new RectangularArray<T> Empty { get { return EmptyRectangularArray<T>.Only; } }

//        public abstract long Width { get; }

//        protected RectangularArray() {
//            asArray = AsArray();
//        }

//        public static bool Equals(RectangularArray<T> ra1, RectangularArray<T> ra2) {
//            if (ra1.Height != ra2.Height || ra1.Width != ra2.Width) {
//                return false;
//            }
//            long h = ra1.Height;
//            long w = ra1.Width;
//            for (long r = 0; r < h; ++r) {
//                for (long c = 0; c < w; ++c) {
//                    if (!object.Equals(ra1[r, c], ra2[r, c])) {
//                        return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public static Array2D<T> From(T item) { return new SingleItemRectangularArray<T>(item); }
//        public static Matrix<T> From(T item, long rows, long columns) { return new FillMatrix<T>(item, rows, columns); }

//        //public static Array<object> From(IEnumerable enumerable) { return new UntypedEnumerableWrapperArray(enumerable); }
//        //public static Array<T> From(IEnumerable<T> enumerable) { return new EnumerableWrapperArray<T>(enumerable); }
//        //public static Array<T> From(IEnumerable<T> enumerable, long length) { return new EnumerableWrapperArray<T>(enumerable).Truncated(length); }
//        //public static Array<T> From(IEnumerable<T> enumerable, long i, long length) { return new EnumerableWrapperArray<T>(enumerable).Subarray(i, length); }

//        public static Matrix<T> From(T[,] items, long h, long w) { return From(items, 0L, 0L, h, w); }
//        public static Matrix<T> From(T[,] items, long r, long c, long h, long w) { return new BlockWrapperMatrix<T>(items).Submatrix(r, c, h, w); }

//        public virtual bool All(bool condition) {
//            return asArray.All(condition);
//        }

//        public virtual bool All(Func<T, bool> condition) {
//            return asArray.All(condition);
//        }

//        public virtual bool Any(bool condition) {
//            return asArray.Any(condition);
//        }

//        public virtual bool Any(Func<T, bool> condition) {
//            return asArray.Any(condition);
//        }

//        public virtual Matrix<T> Bottom([LessOrEqual("Height")] long length) { return Submatrix(Height - length, 0L, Height, Width); }

//        public virtual Matrix<TTo> Cast<TTo>() { return new CastMatrix<T, TTo>(this); }

//        public virtual Array<T> Column(long c) {
//            return new MatrixColumnArray<T>(this, c);
//        }

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
//            long dummy;
//            return asArray.TryGetIndex(item, 0, out dummy);
//        }

//        public virtual bool Contains(Array<T> items) {
//            long dummy;
//            return asArray.TryGetIndex(items, 0, out dummy);
//        }

//        public virtual Matrix<TTo> Convert<TTo>() { return new ConvertMatrix<T, TTo>(this); }

//        /// <summary>
//        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
//        /// </summary>
//        /// <param name="loadedItems">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
//        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="ArgumentNullException">array is null.</exception>
//        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        public virtual void CopyTo(T[] array, int arrayIndex) {
//            asArray.CopyTo(array, arrayIndex);
//        }

//        public virtual void Each(Action<T> action) {
//            asArray.Each(action);
//        }

//        public virtual void Each<P1>(Action<T, P1> action, P1 argument1) {
//            asArray.Each(action, argument1);
//        }

//        public virtual void Each<P1, P2>(Action<T, P1, P2> action, P1 argument1, P2 argument2) {
//            asArray.Each(action, argument1, argument2);
//        }

//        public virtual void Each<P1, P2, P3>(Action<T, P1, P2, P3> action, P1 argument1, P2 argument2, P3 argument3) {
//            asArray.Each(action, argument1, argument2, argument3);
//        }

//        public virtual void Each(Action<long, T> action) {
//            asArray.Each(action);
//        }

//        // Gets the indexes of all items fitting a condition
//        public Array<Tuple<long, long>> FindAll(Func<T, bool> condition) {
//            //// could be lazy
//            MutableArray<Tuple<long, long>> indexesSoFar = new MutableArray<Tuple<long, long>>();
//            for (long r = 0; r < Height; ++r) {
//                for (long c = 0; c < Width; ++c) {
//                    if (condition(GetItem(r, c))) {
//                        indexesSoFar.Append(new Tuple<long, long>(r, c));
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
//                for (long c = 0L; c < Width; ++c) {
//                    yield return this[r, c];
//                }
//            }
//        }

//        public virtual Matrix<T> Left([LessOrEqual("Width")] long length) { return Submatrix(0L, 0L, Height, length); }

//        [return: NotNegative]
//        public virtual long HowMany(Func<T, bool> predicate) {
//            return asArray.HowMany(predicate);
//        }

//        //public virtual Matrix<T> Inverse() { return new InverseMatrix<T>(this); }

//        public bool None(Func<T, bool> predicate) { return !Any(predicate); }

//        public virtual Matrix<T> Right([LessOrEqual("Width")] long length) { return Submatrix(0L, Width - length, Height, length); }

//        public virtual Array<T> Row(long r) {
//            MutableArray<T> rowSoFar = new MutableArray<T>();
//            for (long c = 0; c < Width; ++c) {
//                rowSoFar.Append(this[r, c]);
//            }
//            return rowSoFar.Current;
//        }

//        public virtual Matrix<T> Submatrix(long r, long c, long h, long w) { return new Submatrix<T>(this, r, c, h, w); }

//        //public virtual Matrix<T> Times([Equals("m2.Height", "Height")][Equals("m2.Width", "Width")] Matrix<T> m2) {
//        //    return new MultiplyMatrix<T>(this, m2);
//        //}

//        public virtual Matrix<T> Top([LessOrEqual("Top")] long length) { return Submatrix(0L, 0L, length, Width); }

//        public override string ToString() {
//            long maxLength = 40;
//            MutableString stringSoFar = new MutableString();
//            stringSoFar += Height;
//            stringSoFar += 'x';
//            stringSoFar += Width;
//            stringSoFar += " [";
//            for (long r = 0L; r < Height; ++r) {
//                if (stringSoFar.Current.Length >= maxLength) { break; }
//                if (r != 0L) {
//                    stringSoFar += ", ";
//                }
//                stringSoFar += '(';
//                for (long c = 0L; c < Width; ++c) {
//                    if (stringSoFar.Current.Length >= maxLength) { break; }
//                    if (c != 0L) {
//                        stringSoFar += ", ";
//                    }
//                    stringSoFar += this[r, c];
//                }
//                stringSoFar += ')';
//            }
//            return stringSoFar.Current.Truncated(maxLength - 1) + ']';
//        }

//        public virtual Matrix<T2> Transform<T2>(Func<T, T2> transform) { return new TransformMatrix<T, T2>(this, transform); }
//        public virtual Matrix<T2> Transform<T2>(Func<long, long, T, T2> transformWithIndexes) { return new TransformWithIndexesMatrix<T, T2>(this, transformWithIndexes); }
//        //public virtual Array<T2> TransformMemoized<T2>(Func<T, T2> transform) { return new MemoizedTransformArray<T, T2>(this, transform); }

//        [WhatItDoes("Creates a fresh block with the same content.")]
//        [return: ShallowCopy]
//        public virtual T[,] ToBlock() {
//            T[,] block = new T[Height, Width];
//            for (long r = 0; r < Height; ++r) {
//                for (long c = 0; c < Width; ++c) {
//                    block[r, c] = this[r, c];
//                }
//            }
//            return block;
//        }

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
//            return new MatrixArray<T>(this);
//        }

//        //// maybe go with unchecked pattern someday
//        protected T GetItem([Less("Height")][NotNegative] long r, [Less("Width")][NotNegative] long c) {
//            T item;
//            if (!TryGetItem(r, c, out item)) {
//                Utilities.ThrowIndexOutOfRangeException(r, Height); //// not right
//            }
//            return item;
//        }
//    }
//}
