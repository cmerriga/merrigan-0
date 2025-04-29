#if !NET40
using Merrigan0;

namespace System {
    // Tuple types that are the equivalent of .NET 4.5+ types, so that similar code can run against earlier .NET frameworks.
    [Untested]
    public class Tuple<T1> {
        public T1 Item1;
        public Tuple(T1 item1) { Item1 = item1; }

        public override string ToString() {
            return Item1.ToString();
        }
    }

    [Untested]
    public class Tuple<T1, T2> {
        public T1 Item1;
        public T2 Item2;
        public Tuple(T1 item1, T2 item2) {
            Item1 = item1;
            Item2 = item2;
        }

        public override string ToString() {
            return Item1.ToString() + ", " + Item2.ToString();
        }
    }

    [Untested]
    public class Tuple<T1, T2, T3> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public Tuple(T1 item1, T2 item2, T3 item3) {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public override string ToString() {
            return Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString();
        }
    }

    [Untested]
    public class Tuple<T1, T2, T3, T4> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public override string ToString() {
            return Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString();
        }
    }

    [Untested]
    public class Tuple<T1, T2, T3, T4, T5> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public override string ToString() {
            return Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString();
        }
    }

    [Untested]
    public class Tuple<T1, T2, T3, T4, T5, T6> {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public override string ToString() {
            return Item1.ToString() + ", " + Item2.ToString() + ", " + Item3.ToString() + ", " + Item4.ToString() + ", " + Item5.ToString() + ", " + Item6.ToString();
        }
    }
}

#endif
