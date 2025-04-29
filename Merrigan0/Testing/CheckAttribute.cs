using System;

namespace Merrigan0 {
    [WhatItIs("An indication that a method is a check method for an instance of the class.")]
    [Concept("check", "a test that is recommended for run-time verification of the internal state of an instance")]
    [AttributeUsage(AttributeTargets.Method)]
    [Untested]
    public class CheckAttribute : Attribute {
    }
}
