using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // The value is subject to change, either inside a method call, or after an object is created
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    [Untested]
    public class MutableAttribute : Attribute {
    }
}
