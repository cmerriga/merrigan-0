using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("A fixed-width text grid that allows infinite appends of objects, using the product of their " +
        "ToString functions.")]
    public class SystemConsole : BaseConsole {
        public static SystemConsole Ambient = new SystemConsole();

        public override int NumberOfColumns {
            get { return System.Console.BufferWidth; }
        }

        public override void Append(string s) {
            System.Console.Write(s);
        }

        public override void EndLine() {
            System.Console.WriteLine();
        }
    }
}
