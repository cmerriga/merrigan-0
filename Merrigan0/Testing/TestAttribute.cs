using System;
using System.Reflection;

namespace Merrigan0 {
    // An attribute that marks a method as one to be run by automatic testing
    [AttributeUsage(AttributeTargets.Method)]
    [Untested]
    public class TestAttribute : Attribute {
    }
}
