// Enable if the target .NET framework version for this project is less than 3.5
using Merrigan0;

// This must use the namespace given below to work.
namespace System.Runtime.CompilerServices {
    // Provided so that extensions will be recognized on older versions of C#.
    [AttributeUsageAttribute(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    [Untested]
    public class ExtensionAttribute : Attribute {
    }
}
