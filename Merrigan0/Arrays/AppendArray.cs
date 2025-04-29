using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ArraysInternal {
    // When you have an array plus an map to put on the end
    internal class AppendArray<T> : Array<T> {
        private Array<T> items;

        ////[Equals("items.Last()")]
        private T item;

        [Equals("items.Length + 1")]
        private long length;

        [Equals("items.Length + 1")]
        public override long Length { get { return length; } }

        public AppendArray(Array<T> items, T item, [MayBeNull] Func<T, T, int> compare, bool distinct) :
            base(compare, distinct) {
            this.items = items;
            this.item = item;
            length = items.Length + 1;
        }

        public AppendArray(Array<T> items, T item) : this(items, item, null, false) { }

        ////[WhatItDoes("Does custom checks for internal consistency of the object.")]
        ////[DiagnosticOnly]
        ////[Check]
        ////public void Check() {
        ////}

        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection<T>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        public override void CopyTo([True("Length >= arrayIndex + length")] T[] array, int arrayIndex) {
            items.CopyTo(array, arrayIndex);
            array[arrayIndex + Length - 1] = item;
        }

        //[Example("((1, 2, 3), 4)", 3, 4, true)]
        public override bool TryGetItem(long i, out T item) {
            long length = items.Length;
            if (i < length) {
                item = items[i];
                return true;
            }
            if (i > length) {
                item = default(T);
                return false;
            }
            item = this.item;
            return true;
        }

        [DiagnosticOnly]
        [Test]
        private static void Test() {
            Array<int> a = Array<int>.From(1, 2, 3);
            AppendArray<int> aa = new AppendArray<int>(a, 4);
            Testing.TestEquals("length", aa.Length, 4);
            Testing.TestEquals("a[0]", aa[0], 1);
            Testing.TestEquals("a[3]", aa[3], 4);

            Testing.Test("CopyTo", () => {

            });
        }
    }
}
