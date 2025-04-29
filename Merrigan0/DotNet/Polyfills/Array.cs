using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.Internal.DotNet.Polyfills.System {
    [Untested]
    public static class Array {
        public static T[] Empty<T>() {
            return EmptyArray<T>.Value;
        }
    
        private static class EmptyArray<T> {
            public readonly static T[] Value;

            static EmptyArray() {
                EmptyArray<T>.Value = new T[0];
            }
        }
    }
}
