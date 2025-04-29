using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    [Untested]
    internal class EnsureInMap<TFrom, TTo> : Map<TFrom, TTo> {
        private Map<TFrom, TTo> baseMap;
        private TFrom from;
        private TTo to;

        public EnsureInMap(Map<TFrom, TTo> baseMap, TFrom from, TTo to) {
            this.baseMap = baseMap;
            this.from = from;
            this.to = to;
        }

        public override bool TryGetValue(TFrom from, out TTo to) {
            if (baseMap.TryGetValue(from, out to)) {
                return true;
            }
            if (object.Equals(from, this.from)) {
                to = this.to;
                return true;
            }
            to = default(TTo);
            return false;
        }

        protected override Set<TFrom> GetDomain() { return baseMap.Domain.With(from); }
    }
}
