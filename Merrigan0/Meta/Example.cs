using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0.MetaInternal {
    [Untested]
    public class Example {
        public object[] Inputs { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
        public object[] Outputs { get; private set; }

        public Example(MethodInfo methodInfo, object[] values) {
            MethodInfo = methodInfo;
            ParameterInfo[] parameters = methodInfo.GetParameters();
            List<object> inputsSoFar = new List<object>();
            List<object> outputsSoFar = new List<object>();
            int iValue = 0;
            for (int i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                if (parameter.IsIn) {
                    inputsSoFar.Add(values[iValue]);
                    ++iValue;
                }
                if (parameter.IsOut) {
                    outputsSoFar.Add(values[iValue]);
                    ++iValue;
                }
            }

            Inputs = inputsSoFar.ToArray();
            Outputs = outputsSoFar.ToArray();
        }
    }
}
