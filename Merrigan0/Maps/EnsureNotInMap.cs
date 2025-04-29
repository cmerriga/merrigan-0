using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    [Untested]
    public class EnsureNotInMap<TFrom, TTo> : Map<TFrom, TTo> {
        private Map<TFrom, TTo> baseMap;
        private TFrom from;

        public EnsureNotInMap(Map<TFrom, TTo> baseMap, TFrom from) {
            this.baseMap = baseMap;
            this.from = from;
        }

        public override bool TryGetValue(TFrom from, out TTo to) {
            if (!object.Equals(from, this.from) && baseMap.TryGetValue(from, out to)) {
                return true;
            }
            to = default(TTo);
            return false;
        }

        protected override Set<TFrom> GetDomain() { return baseMap.Domain.Without(from); }
    }
}
