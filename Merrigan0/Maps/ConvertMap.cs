using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.SetsInternal;

namespace Merrigan0.ArraysInternal {
    // Use when you have a map from one type to another, but want a map with one or both types as
    // something else
    [Untested]
    internal class ConvertMap<TBaseFrom, TFrom, TBaseTo, TTo> : Map<TFrom, TTo> {
        private Map<TBaseFrom, TBaseTo> baseMap;
        private Func<object, object> convertAbscissaToBaseFunction;
        private Func<object, object> convertOrdinateFromBaseFunction;

        public override long Length { get { return baseMap.Length; } }

        public ConvertMap(Map<TBaseFrom, TBaseTo> baseMap) { 
            this.baseMap = baseMap;
            convertAbscissaToBaseFunction = Conversion.ConvertFunction(typeof(TFrom), typeof(TBaseFrom));
            convertOrdinateFromBaseFunction = Conversion.ConvertFunction(typeof(TBaseTo), typeof(TTo));
        }

        public override bool TryGetValue(TFrom from, out TTo to) {
            TBaseFrom baseFrom = (TBaseFrom)convertAbscissaToBaseFunction(from);
            TBaseTo baseTo;
            if (!baseMap.TryGetValue(baseFrom, out baseTo)) {
                to = default(TTo);
                return false;
            }
            to = (TTo)convertOrdinateFromBaseFunction(baseTo);
            return true;
        }

        protected override Set<TFrom> GetDomain() { return new ConvertSet<TBaseFrom, TFrom>(baseMap.Domain); } //// add Convert() to Set
    }
}
