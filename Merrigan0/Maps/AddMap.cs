using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    //// needs to enforce a no-duplicates policy. For now prefers the records in the additional map
    [Untested]
    internal class AddMap<TFrom, TTo> : Map<TFrom, TTo> {
        private Map<TFrom, TTo> baseMap;
        private Map<TFrom, TTo> additionalMap;

        public AddMap(Map<TFrom, TTo> baseMap, Map<TFrom, TTo> additionalMap) {
            this.baseMap = baseMap;
            this.additionalMap = additionalMap;
        }

        public override bool TryGetValue(TFrom from, out TTo to) {
            if (additionalMap.TryGetValue(from, out to)) {
                return true;
            }
            if (baseMap.TryGetValue(from, out to)) {
                return true;
            }
            return false;
        }

        protected override Set<TFrom> GetDomain() { return baseMap.Domain.Or(additionalMap.Domain); }
    }
}
