using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // Normally array values are expected not to contain nulls. This attribute declares them okay.
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class ItemsMayBeNullAttribute : Attribute {
    }
}
