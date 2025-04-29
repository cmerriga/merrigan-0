//using System;

//namespace Merrigan0 {
//    public abstract class Adapter {
//        public Type FromType { get; private set; }
//        public Type ToType { get; private set;  }

//        public Adapter(Type fromType, Type toType) {
//            FromType = fromType;
//            ToType = toType;
//        }

//        public abstract object To(object value);
//    }

//    public abstract class Adapter<TFrom, TTo> : Adapter {
//        public Adapter() :
//            base(typeof(TFrom), typeof(TTo)) {
//        }

//        public abstract TTo To(TFrom value);

//        public override object To(object value) {
//            return (object)To((TFrom)value);
//        }
//    }
//}
