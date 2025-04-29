using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0.GlomsInternal {
    // An amorphous who-knows-what that could house a regular .NET object, a list of other gloms, or a mish-mash
    // of stuff. Short for "agglomeration". All you know are three things: 
    //      (1) you can evaluate an expression using it as a context, including navigating to another glom via an identifier
    //      (2) you can find out if something is true in its context
    //      (3) you can list some related truths that are known as a result of it, including truths about the glom itself
    //          like whether it's a number or a string or a list. Not all truths, but those might lead to others.
    [Untested]
    public class MutableGlom {
        public Glom Current { get; protected set; }

        public MutableGlom() : this(Glom.Global) { }

        public MutableGlom(Glom current) { Current = current; }

        public void Set(String name, object value) {
            Current = new SetGlom(Current, name, value);
        }
    }
}
