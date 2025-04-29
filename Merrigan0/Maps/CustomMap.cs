using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Merrigan0 {
    [Untested]
    public class CustomMap<TFrom, TTo> : Map<TFrom, TTo> {
        private Set<TFrom> domain;

        // Will never be called for a from value that is not in the domain
        private Func<TFrom, TTo> getValueFunction;

        public CustomMap(Set<TFrom> domain, Func<TFrom, TTo> getValueFunction) {
            this.domain = domain;
            this.getValueFunction = getValueFunction;
        }

        public override bool TryGetValue(TFrom from, out TTo to) {
            if (!Domain.Contains(from)) {
                to = default(TTo);
                return false;
            }
            to = getValueFunction(from);
            return true;
        }

        protected override Set<TFrom> GetDomain() {
            return domain;
        }
    }
}
