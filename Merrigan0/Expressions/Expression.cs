using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.GlomsInternal; // GlobalGlom

namespace Merrigan0 {
    // If you want an explicitly-typed expression, wrap with a TypedExpression.
    [Untested]
    public abstract class Expression : IParent<Expression> {
        public static readonly Expression ComparableContext = new ComparableContextExpression();
        public static readonly Expression CurrentContext = new CurrentContextExpression();
        public static readonly Expression False = new ConstantExpression<bool>(false);
        public static readonly Expression Null = new ConstantExpression<object>(null);
        public static readonly Expression True = new ConstantExpression<bool>(true);

        public static implicit operator Expression(bool b) {
            return new ConstantExpression<bool>(b);
        }

        public static implicit operator Expression(byte b) {
            return new ConstantExpression<byte>(b);
        }

        public static implicit operator Expression(sbyte sb) {
            return new ConstantExpression<sbyte>(sb);
        }

        public static implicit operator Expression(ushort ush) {
            return new ConstantExpression<ushort>(ush);
        }

        public static implicit operator Expression(short sh) {
            return new ConstantExpression<short>(sh);
        }

        public static implicit operator Expression(char ch) {
            return new ConstantExpression<char>(ch);
        }

        public static implicit operator Expression(uint u) {
            return new ConstantExpression<uint>(u);
        }

        public static implicit operator Expression(int n) {
            return new ConstantExpression<int>(n);
        }

        public static implicit operator Expression(ulong ul) {
            return new ConstantExpression<ulong>(ul);
        }

        public static implicit operator Expression(long n) {
            return new ConstantExpression<long>(n);
        }

        public static implicit operator Expression(float f) {
            return new ConstantExpression<float>(f);
        }

        public static implicit operator Expression(double r) {
            return new ConstantExpression<double>(r);
        }

        public static implicit operator Expression(String s) {
            return new ConstantExpression<string>(s);
        }

        public static implicit operator Expression(Guid id) {
            return new ConstantExpression<Guid>(id);
        }

        public static implicit operator Expression(DateTime time) {
            return new ConstantExpression<DateTime>(time);
        }

        public static implicit operator Expression(DateTimeOffset time) {
            return new ConstantExpression<DateTimeOffset>(time);
        }

        public virtual Array<Expression> Children { get { return null; } }

        [UsedFor("determining proper compiled functions using the result of the expression")]
        public Type Type { get; protected set; }

        protected Expression() { }

        protected Expression(Type type = null) {
            Type = type;
        }

        public Glom EvaluateGlom(Glom context) {
            return Glom.From(Evaluate(context));
        }

        public object Evaluate() { return Evaluate(Glom.Global); }

        // May be a glom already, or may be a POCO
        public object Evaluate(Glom context) {
            object value;
            if (!TryEvaluate(context, out value)) {
                throw new Exception();
            }
            return value;
        }

        public T Evaluate<T>(Glom context) {
            return EvaluateGlom(context).As<T>();
        }

        public override string ToString() {
            return GetType().Name;
        }

        //// Why wouldn't this evaluate successfully?
        public abstract bool TryEvaluate(Glom context, out object value);
    }
}

