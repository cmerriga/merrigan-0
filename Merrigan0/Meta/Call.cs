using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0 {
    //// Could use a rename! These calls will be made everywhere and should be as easy as possible.
    ////[Untested]
    ////public class Calls {
    ////    public static void Call(Action action) {
    ////        action();
    ////    }

    ////    public static void Call<T1>(Action<T1> action, T1 parameter1) {
    ////        action(parameter1);
    ////    }

    ////    public static void Call<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2) {
    ////        action(parameter1, parameter2);
    ////    }

    ////    public static void Call<T1, T2, T3>(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3) {
    ////        action(parameter1, parameter2, parameter3);
    ////    }

    ////    public static void Call<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
    ////        action(parameter1, parameter2, parameter3, parameter4);
    ////    }

    ////    public static void Call<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
    ////        action(parameter1, parameter2, parameter3, parameter4, parameter5);
    ////    }

    ////    public static void Call<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
    ////        action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
    ////    }

    ////    public static void Call<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
    ////        action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
    ////    }

    ////    public static void Call<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
    ////        action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
    ////    }

    ////    public static TResult Call<TResult>(Func<TResult> function) {
    ////        return function();
    ////    }

    ////    public static TResult Call<T1, TResult>(Func<T1, TResult> function, T1 parameter1) {
    ////        return function(parameter1);
    ////    }

    ////    public static TResult Call<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 parameter1, T2 parameter2) {
    ////        return function(parameter1, parameter2);
    ////    }

    ////    public static TResult Call<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3) {
    ////        return function(parameter1, parameter2, parameter3);
    ////    }

    ////    public static TResult Call<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
    ////        return function(parameter1, parameter2, parameter3, parameter4);
    ////    }

    ////    public static TResult Call<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
    ////        return function(parameter1, parameter2, parameter3, parameter4, parameter5);
    ////    }

    ////    public static TResult Call<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
    ////        return function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
    ////    }

    ////    public static TResult Call<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
    ////        return function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
    ////    }

    ////    public static TResult Call<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
    ////        return function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
    ////    }
    ////}
}
