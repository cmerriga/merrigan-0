using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // An attribute that marks a method as not supposed to be called, for instance an interface method that is
    // not intended to be honored.
    [Note("Methods marked with this attribute must throw a NotSupportedException if called.")]
    [AttributeUsage(AttributeTargets.Method)] /// | AttributeTargets.Property)]
    public class NotSupportedAttribute : Attribute {
    }
}
