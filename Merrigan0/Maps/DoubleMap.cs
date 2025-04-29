using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Merrigan0.MapsInternal;

//namespace Merrigan0 {
//    //// Make implement IDictionary
//    [Untested]
//    public abstract class DoubleMap<TFrom1, TFrom2, TTo> : Array<Tuple<TFrom1, TFrom2, TTo>> {
//        ////private Set<Tuple<TFrom1, TFrom2>> domain;
//        ////private SetArray<Tuple<TFrom1, TFrom2>> domainArray;

//        public static new DoubleMap<TFrom1, TFrom2, TTo> Empty { get { return EmptyDoubleMap<TFrom1, TFrom2, TTo>.Only; } }

//        public TTo this[TFrom1 from1, TFrom2 from2] { get { return GetValue(from1, from2); } }

//        ////public virtual Set<Tuple<TFrom1, TFrom2>> Domain {
//        ////    get {
//        ////        if (domain == null) {
//        ////            domain = GetDomain();
//        ////        }
//        ////        return domain;
//        ////    }
//        ////}

//        public virtual Map<TTo, Tuple<TFrom1, TFrom2>> Inverse {
//            get {
//                MutableDoubleMap<TTo, TFrom2, TFrom1> inverseSoFar = new MutableDoubleMap<TTo, TFrom2, TFrom1>();
//                foreach (KeyValuePair<TFrom, TTo> pair in this) {
//                    inverseSoFar.Add(pair.Value, pair.Key);
//                }
//                return inverseSoFar.Current;
//            }
//        }

//        public override long Length { get { return Domain.Length; } }

//        protected Map(params object[] values) {
//            Array<Tuple<TFrom, TTo>> pairs = Utilities.ToPairs<TFrom, TTo>(values);

//        }

//        public static Map<TFrom, TTo> From(Array<TTo> values, Func<TTo, TFrom> keyFunction) {
//            return values.ToMap(keyFunction);
//        }

//        public static Map<TFrom, TTo> From(params object[] inputsAndOutputs) {
//            Dictionary<TFrom, TTo> dictionarySoFar = new Dictionary<TFrom, TTo>();
//            foreach (Tuple<TFrom, TTo> keyAndValue in Utilities.ToPairs<TFrom, TTo>(inputsAndOutputs)) {
//                dictionarySoFar.Add(keyAndValue.Item1, keyAndValue.Item2);
//            }
//            return new DictionaryMap<TFrom, TTo>(dictionarySoFar);
//        }

//        public virtual bool DomainContains(TFrom from) {
//            TTo dummy;
//            return TryGetValue(from, out dummy);
//        }

//        public override string ToString() {
//            return Domain.ToString();
//        }

//        public override bool TryGetItem(long i, out KeyValuePair<TFrom, TTo> item) {
//            if (domainArray == null) {
//                domainArray = new SetArray<TFrom>(Domain);
//            }
//            TFrom from;
//            if (!domainArray.TryGetItem(i, out from)) {
//                item = default(KeyValuePair<TFrom, TTo>);
//                return false;
//            }
//            item = new KeyValuePair<TFrom, TTo>(from, GetValue(from));
//            return true;
//        }

//        public abstract bool TryGetValue(TFrom from, out TTo to);

//        public virtual IDictionary<TFrom, TTo> ToDictionary() {
//            Dictionary<TFrom, TTo> valuesByKeySoFar = new Dictionary<TFrom, TTo>();
//            foreach (TFrom domainElement in Domain) {
//                valuesByKeySoFar.Add(domainElement, this[domainElement]);
//            }
//            return valuesByKeySoFar;
//        }

//        protected virtual TTo GetValue(TFrom from) {
//            TTo to;
//            if (!TryGetValue(from, out to)) {
//                throw new KeyNotFoundException();
//            }
//            return to;
//        }

//        protected abstract Set<TFrom> GetDomain();

//        //protected override KeyValuePair<TFrom, TTo> GetItem(long i) {
//        //    if (domainArray == null) {
//        //        domainArray = new SetArray<TFrom>(Domain);
//        //    }
//        //    TFrom from = domainArray[i];
//        //    return new KeyValuePair<TFrom, TTo>(from, GetValue(from));
//        //}
//    }

//    //[Untested]
//    //public class ConcreteMap<TFrom, TTo> : Map<TFrom, TTo> {
//    //    public override bool TryGetValue(TFrom from, out TTo to) {
//    //        to = default(TTo);
//    //        return false;
//    //    }

//    //    protected override Set<TFrom> GetDomain() {
//    //        return Set<TFrom>.Empty;
//    //    }
//    //}
//}
