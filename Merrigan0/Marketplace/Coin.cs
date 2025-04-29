using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("An atom of cash")]
    [Untested]
    public class Coin {
        public decimal Value { get; private set; }
        public Party Owner { get; private set; }
    }
}
