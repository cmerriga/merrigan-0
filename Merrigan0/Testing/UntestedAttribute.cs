using System;

namespace Merrigan0 {
    // An attribute that marks a method as not ready for testing, and therefore suspicious to use
    [AttributeUsage(
        AttributeTargets.Assembly | 
        AttributeTargets.Class | 
        AttributeTargets.Constructor |
        AttributeTargets.Enum | 
        AttributeTargets.Field | 
        AttributeTargets.Method | 
        AttributeTargets.Property | 
        AttributeTargets.Struct)]
    [Untested]
    public class UntestedAttribute : Attribute {
    }
}
