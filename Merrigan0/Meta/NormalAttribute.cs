using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class NormalAttribute : RangeAttribute {
        public NormalAttribute() : base(0.0, true, 1.0, true) { }

        //public override object RandomValue(Type type) {
        //    if (Object.ReferenceEquals(type, typeof(double))) {
        //        return (object)Random.Double(0.0, 1.0);
        //    } else if (Object.ReferenceEquals(type, typeof(float))) {
        //        return (object)(float)Random.Double(0.0, 1.0);
        //    }
        //    throw new Exception();
        //}

        //public override Constraint GetConstraint(Type type) {
        //    return (Constraint)Reflection.New(typeof(NormalConstraint));
        //}

        ////public override bool Acceptable(Scope context, object value) {
        ////    double doubleValue = (double)value;
        ////    return doubleValue >= 0.0 && doubleValue <= 1.0;

        ////    ////if (value == null) {
        ////    ////    return false;
        ////    ////}

        ////    ////IComparable comparableValue = value as IComparable;
        ////    ////if (comparableValue == null) {
        ////    ////    return false;
        ////    ////}

        ////    ////return (comparableValue.CompareTo(0.0) >= 0) && (comparableValue.CompareTo(1.0) <= 0);
        ////}
    }
}
