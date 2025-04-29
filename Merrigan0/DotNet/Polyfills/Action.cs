// This must use the namespace given below to work. Otherwise lambda functions may not find the type name they are looking for.
namespace System { //// Merrigan0.Internal.DotNet.Polyfills.System {
    // Action delegate types that are the equivalent of .NET 3.0+ types, so that similar code can run against earlier .NET frameworks.
    public delegate void Action();
    ////public delegate void Action<T1>(T1 parameter);
    public delegate void Action<T1, T2>(T1 parameter1, T2 parameter2);
    public delegate void Action<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3);
    public delegate void Action<T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
    public delegate void Action<T1, T2, T3, T4, T5>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6);
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7);
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7, T8>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8);
}
