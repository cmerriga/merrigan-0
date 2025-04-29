using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.MapsInternal {
    // A map with no s.
    [Untested]
    internal class EmptyMap<TFrom, TTo> : Map<TFrom, TTo> {
        public override bool TryGetValue(TFrom from, out TTo to) {
            to = default(TTo);
            return false;
        }

        protected override Set<TFrom> GetDomain() { return Set<TFrom>.Empty; }
    }
}
