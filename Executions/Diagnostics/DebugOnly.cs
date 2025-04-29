using System;
using System.Collections.Generic;
using System.Reflection;

namespace Executions.Diagnostics {
    public static class DebugOnly {
        // Performs all known checks on the parameters of the current method
        public static void CheckParameters(params object[] values) {
#if DEBUG
            MethodBase methodBase = MethodBase.GetCurrentMethod();
            CheckParameters(methodBase.GetParameters(), values);
#endif
        }

        public static void CheckParameters(IEnumerable<ParameterInfo> parameterInfos, IList<object> values) {
            int iCurrentValue = 0;
            foreach (ParameterInfo parameterInfo in parameterInfos) {
                if (ShouldCheckParameter(parameterInfo)) {
                    CheckParameter(parameterInfo, values[iCurrentValue]);
                    ++iCurrentValue;
                }
            }
        }

        public static bool ShouldCheckParameter(ParameterInfo parameterInfo) {
            Type type = parameterInfo.ParameterType;
            if (parameterInfo.IsIn || type.IsByRef) {
                return true;
            }
            return false;
        }

        public static void CheckParameter(ParameterInfo parameterInfo, object value) {
            // If it's not optional and it's not a value type, it must not be null
            Type type = parameterInfo.ParameterType;
            if (!parameterInfo.IsOptional || !type.IsValueType) {
                if (value == null) {
                    HandleNullParameter(parameterInfo);
                }
            }

            // Go through each DiagnosticAttribute and check it
            foreach (ParameterDiagnosticAttribute parameterDiagnostic in parameterInfo.GetCustomAttributes<ParameterDiagnosticAttribute>(true)) {
                parameterDiagnostic.Check(parameterInfo, value);
            }
        }

        public static void HandleNullParameter(ParameterInfo parameterInfo) {
            Console.WriteLine("Parameter {0} must not be null.", parameterInfo.Name);
        }
    }
}
