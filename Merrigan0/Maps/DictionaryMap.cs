using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    // Use when you have an IDictionary but need a Map.
    [Untested]
    internal class DictionaryMap<TFrom, TTo> : Map<TFrom, TTo> {
        private IDictionary<TFrom, TTo> dictionary;
        public override long Length { get { return dictionary.Count; } }

        public override bool DomainContains(TFrom from) { return dictionary.ContainsKey(from); }
        public override IDictionary<TFrom, TTo> ToDictionary() { return dictionary; }
        public override bool TryGetValue(TFrom from, out TTo to) { return dictionary.TryGetValue(from, out to); }

        public DictionaryMap(IDictionary<TFrom, TTo> dictionary) { this.dictionary = dictionary; }

        protected override Set<TFrom> GetDomain() { return Set<TFrom>.FromDistinct(dictionary.Keys); }
    }
}
