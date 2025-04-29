using System;
using System.Collections.Generic;

namespace Merrigan0.SetsInternal {
    ////[Untested]
    ////internal class HashSet<T> : Set<T> {
    ////    private Dictionary<T, long> indexesByItem;
    ////    private Array<T> items;

    ////    public override long Length { get { return items.Length; } }

    ////    public HashSet(Array<T> items) {
    ////        MutableArray<T> distinctItemsSoFar = new MutableArray<T>();
    ////        indexesByItem = new Dictionary<T, long>();
    ////        long length = items.Length;
    ////        for (long i = 0; i < length; ++i) {
    ////            T item = items[i];
    ////            if (!indexesByItem.ContainsKey(item)) {
    ////                distinctItemsSoFar.Append(item);
    ////                indexesByItem.Add(item, distinctItemsSoFar.Current.Length - 1);
    ////            }
    ////        }
    ////        this.items = distinctItemsSoFar.Current;
    ////    }

    ////    public override bool In(T value) { return indexesByItem.ContainsKey(value); }

    ////    protected override T GetItem(long i) {
    ////        return items[i];
    ////    }
    ////}
}
