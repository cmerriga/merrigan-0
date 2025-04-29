using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.CssInternal {
    internal class CssNamedColor {
        public String Name { get; private set; }
        public Color Color { get; private set; }

        public CssNamedColor(String name, String xrrggbb) {
            Name = name;
            Color = Merrigan0.Css.Rgb(xrrggbb);
        }
    }
}
