using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.GlomsInternal {
    // Implies (if 8)
    //      < 100, <= 1000, > 0, >= -15, == 8 != 9
    //      fractional -> .0
    //      binary -> 1000b
    //      
    // Disimplies
    //      > 100 >= 1000, < 0, <= -15, != 8, == 9
    //      fractional -> .1111
    //      binary -> 1001b
    //
    [Untested]
    internal class IntegerGlom : NumberGlom {
        public IntegerGlom(object number) : base(number) { }
    }
}
