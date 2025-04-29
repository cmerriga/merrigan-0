using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
//    [Untested]
//    public class MinimumExpression : AggregateExpression {
//        static MinusExpression() {
//            RegisterTypedCalculationFunction<sbyte>(typeof(MinusExpression), (l, r) => (sbyte)l - (sbyte)r);
//            RegisterTypedCalculationFunction<byte>(typeof(MinusExpression), (l, r) => (byte)l - (byte)r);
//            RegisterTypedCalculationFunction<short>(typeof(MinusExpression), (l, r) => (short)l - (short)r);
//            RegisterTypedCalculationFunction<ushort>(typeof(MinusExpression), (l, r) => (ushort)l - (ushort)r);
//            RegisterTypedCalculationFunction<char>(typeof(MinusExpression), (l, r) => (char)l - (char)r);
//            RegisterTypedCalculationFunction<int>(typeof(MinusExpression), (l, r) => (int)l - (int)r);
//            RegisterTypedCalculationFunction<uint>(typeof(MinusExpression), (l, r) => (uint)l - (uint)r);
//            RegisterTypedCalculationFunction<long>(typeof(MinusExpression), (l, r) => (long)l - (long)r);
//            RegisterTypedCalculationFunction<ulong>(typeof(MinusExpression), (l, r) => (ulong)l - (ulong)r);
//            RegisterTypedCalculationFunction<float>(typeof(MinusExpression), (l, r) => (float)l - (float)r);
//            RegisterTypedCalculationFunction<double>(typeof(MinusExpression), (l, r) => (double)l - (double)r);
//            RegisterTypedCalculationFunction<decimal>(typeof(MinusExpression), (l, r) => (decimal)l - (decimal)r);
//        }

//        protected override string Separator { get { return "-"; } }

//        public MinusExpression(Expression left, Expression right) : this(left, right, null) { }

//        public MinusExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

//        ////protected override Func<object, object, object> CalculateFunction(Type type) {
//        ////    if (type == typeof(sbyte)) {
//        ////        return (l, r) => (sbyte)l - (sbyte)r;
//        ////    } else if (type == typeof(byte)) {
//        ////        return (l, r) => (byte)l - (byte)r;
//        ////    } else if (type == typeof(short)) {
//        ////        return (l, r) => (short)l - (short)r;
//        ////    } else if (type == typeof(ushort)) {
//        ////        return (l, r) => (ushort)l - (ushort)r;
//        ////    } else if (type == typeof(char)) {
//        ////        return (l, r) => (char)l - (char)r;
//        ////    } else if (type == typeof(int)) {
//        ////        return (l, r) => (int)l - (int)r;
//        ////    } else if (type == typeof(uint)) {
//        ////        return (l, r) => (uint)l - (uint)r;
//        ////    } else if (type == typeof(long)) {
//        ////        return (l, r) => (long)l - (long)r;
//        ////    } else if (type == typeof(ulong)) {
//        ////        return (l, r) => (ulong)l - (ulong)r;
//        ////    } else if (type == typeof(float)) {
//        ////        return (l, r) => (float)l - (float)r;
//        ////    } else if (type == typeof(double)) {
//        ////        return (l, r) => (double)l - (double)r;
//        ////    } else if (type == typeof(decimal)) {
//        ////        return (l, r) => (decimal)l - (decimal)r;
//        ////    }
//        ////    throw new Exception();
//        ////}

//        //protected override Func<object, object, object> ValueFunction(Type type) {
//        //    return ValueFunction(type);
//        //}

//        ////protected override object Value(object left, object right) {
//        ////    // See if it's String plus anything
//        ////    String leftString = left as String;
//        ////    String rightString = right as String;
//        ////    if (leftString != null) {
//        ////        if (rightString != null) {
//        ////            return leftString + rightString;
//        ////        }
//        ////        return leftString + right;
//        ////    }
//        ////    if (rightString != null) {
//        ////        return left.ToString() + rightString;
//        ////    }

//        ////    //// Maybe that type will be known beforehand sometimes
//        ////    Type leftType = left.GetType();
//        ////    Type rightType = right.GetType();
//        ////    Type upcastType = Reflection.UpcastType(leftType, rightType);
//        ////    object castLeft = Reflection.Cast(left, leftType, upcastType);
//        ////    object castRight = Reflection.Cast(right, rightType, upcastType);
//        ////    if (upcastType == typeof(int)) {
//        ////        return (int)castLeft + (int)castRight;
//        ////    } else if (upcastType == typeof(uint)) {
//        ////        return (uint)castLeft + (uint)castRight;
//        ////    } else if (upcastType == typeof(long)) {
//        ////        return (long)castLeft + (long)castRight;
//        ////    } else if (upcastType == typeof(ulong)) {
//        ////        return (ulong)castLeft + (ulong)castRight;
//        ////    } else if (upcastType == typeof(float)) {
//        ////        return (float)castLeft + (float)castRight;
//        ////    } else if (upcastType == typeof(double)) {
//        ////        return (double)castLeft + (double)castRight;
//        ////    } else if (upcastType == typeof(decimal)) {
//        ////        return (decimal)castLeft + (decimal)castRight;
//        ////    }
//        ////    throw new InvalidOperationException();
//        ////}
//    }
}
