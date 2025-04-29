using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.MapsInternal;

namespace Merrigan0 {
    [Untested]
    public class MutableMap<TFrom, TTo> /*: Map<TFrom, TTo> */ {
        public static MutableMap<TFrom, TTo> operator +(MutableMap<TFrom, TTo> map1, Map<TFrom, TTo> map2) {
            map1.Add(map2);
            return map1;
        }

        public TTo this[TFrom from] {
            set { SetValue(from, value); }
        }

        public Map<TFrom, TTo> Current { get; protected set; }

        public MutableMap() : this(Map<TFrom, TTo>.Empty) { }

        public MutableMap(Map<TFrom, TTo> baseMap) { Current = baseMap; }

        public MutableMap<TFrom, TTo> From(IDictionary<TFrom, TTo> dictionary) {
            return new MutableMap<TFrom,TTo>(new DictionaryMap<TFrom, TTo>(dictionary));
        }

        public virtual void Add(TFrom from, TTo to) { SetValue(from, to); }
        public virtual void Add(Map<TFrom, TTo> map) { Current = new AddMap<TFrom, TTo>(Current, map); }

        public virtual void Clear() { Current = Map<TFrom, TTo>.Empty; }

        public virtual bool Remove(TFrom from) {
            if (!Current.DomainContains(from)) {
                return false;
            }

            Current = new EnsureNotInMap<TFrom, TTo>(Current, from);
            return true;
        }

        public override string ToString() { return Current.ToString(); }

        protected virtual void SetValue(TFrom from, TTo to) { Current = new EnsureInMap<TFrom, TTo>(Current, from, to); }
    }
}
