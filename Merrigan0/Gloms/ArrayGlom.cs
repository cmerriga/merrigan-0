using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0.GlomsInternal {
    // A glom that contains ordered children.
    [Untested]
    internal class ArrayGlom : ObjectGlom, IEnumerable<Glom> {
        public Glom this[int i] { get { return Items[i]; } }
        public Glom this[long i] { get { return Items[i]; } }
        public Array<Glom> Items { get; private set; }
        public long Length { get { return Items.Length; } }

        public ArrayGlom(params object[] items) :
            this((IEnumerable)items)
        {
        }

        public ArrayGlom(IEnumerable items) : base(items) {
            Items = Array<object>.From(items).Transform(o => Glom.From(o));
        }

        ////public ArrayGlom(Glom prototype, IEnumerable items)
        ////    : base(prototype) 
        ////{
        ////    this.items = Array<object>.From(items);
        ////}

        // Type must be IEnumerable something
        public override object As(Type type) {
            return base.As(type);
        }

        //// this belongs only in the Glom area
        [WhatItDoes("Creates a glom containing gloms with the given property set to the equivalent in this glom.")]
        public ArrayGlom Dice(String propertyName) {
            return Dice(Array<String>.From(propertyName));
        }

        public virtual ArrayGlom Dice(Array<String> propertyNames) {
            MutableArray<Glom> dicedItemsSoFar = new MutableArray<Glom>();
            foreach (object glom in this) {
                MutableMap<String, object> valuesByName = new MutableMap<String, object>();
                foreach (String name in propertyNames) {
                    valuesByName.Add(name, this[name]);
                }
                Glom dicedGlom = new Glom(valuesByName);
                dicedItemsSoFar.Append(dicedGlom);
            }
            return new ArrayGlom(dicedItemsSoFar.Current);
        }

        public virtual IEnumerator<Glom> GetEnumerator() {
            return Items.GetEnumerator();
        }

        // Returns an amorphous value that can be used by other epxressions
        protected override bool TryGetAtThisLevel(String name, out object o) {
            if (name == "Length") {
                o = Length;
                return true;
            }
            if (name == "Min") {
                o = Utilities.Min(this);
                return true;
            }
            if (name == "Max") {
                o = Utilities.Max(this);
                return true;
            }
            return base.TryGetAtThisLevel(name, out o);
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
