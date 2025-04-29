using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Untested]
    public class ThrowsAttribute : CommitmentAttribute {
        public string Description { get; set; }
        public object[] Inputs { get; private set; }
        public Type Type { get; private set; }
        ////public String TypeName { get; private set; }

        public ThrowsAttribute(params object[] inputs) {
            Inputs = inputs;
        }

        public ThrowsAttribute(Type type, params object[] inputs) {
            Inputs = inputs;
            Type = type;
        }

        //public ThrowsAttribute(String typeName, params object[] inputs) {
        //    Inputs = inputs;
        //    TypeName = typeName;
        //}

        public override void Test(object instance, MethodInfo method) {
            using (Merrigan0.Test.Begin("throws " + (Type == null ? typeof(Exception) : Type).Name + " when given " + String.Join((Array<object>)Inputs, " "))) {
                try {
                    method.Invoke(instance, Inputs);

                    // Uh-oh, we shouldn't have gotten here
                    Merrigan0.Test.ReportFailed("Did not throw any exception");
                } catch (Exception e) {
                    // If there was a specific type needed, return a failure if the exception isn't that type
                    if (Type != null && e.InnerException != null) {
                        Type innerExceptionType = e.InnerException.GetType();
                        if (!Type.IsAssignableFrom(innerExceptionType)) {
                            Merrigan0.Test.ReportFailed("Threw incorrect type " + innerExceptionType.FullName);
                        }
                    }
                    Merrigan0.Test.ReportSucceeded();
                }
            }
        }
    }
}
