using System;

namespace Merrigan0 {
    [WhatItIs("An indication that a static field is an index into a local Extender.")]
    [UsedFor("Testing for uniqueness and sequential value.")]
    [AttributeUsage(AttributeTargets.Field)]
    [Untested]
    public class ExtenderIndexAttribute : Attribute {
    }
}
