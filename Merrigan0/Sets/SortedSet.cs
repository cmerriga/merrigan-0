using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////[Untested]
    ////public abstract class SortedSet<T> : Set<T> {
    ////    public virtual Set<T> And(Set<T> set2) { return new IntersectionSet<T>(this, set2); }
    ////    public override Array<T> Distinct() { return this; }

    ////    ////public override Set<T> Distinct(Func<T, T, int> compare) {
    ////    ////    if (object.ReferenceEquals(compare, Compare)) {
    ////    ////        return this;
    ////    ////    }
    ////    ////    return this.Sorted(compare).Distinct(compare);
    ////    ////}

    ////    public virtual bool In(T value) { return Contains(value); }
    ////    public virtual Set<T> Not(Set<T> set2) { return new SubtractionSet<T>(this, set2); }
    ////    public virtual Set<T> Or(Set<T> set2) { return new UnionSet<T>(this, set2); }

    ////    public virtual bool Subset(Set<T> values) {
    ////        foreach (T value in values) {
    ////            if (!In(value)) {
    ////                return false;
    ////            }
    ////        }
    ////        return true;
    ////    }

    ////    public virtual Set<T> With(T value) { return new EnsureInSet<T>(this, value); }
    ////    public virtual Set<T> Without(T value) { return new EnsureNotInSet<T>(this, value); }
    ////}
}
