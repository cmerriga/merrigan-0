using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Executions.Actions;
using ExecutionsTest = Executions.Test;

namespace Executions {
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class Action {
        public static void Test() {
            TestDebuggerDisplay();
        }

        public static void TestDebuggerDisplay() {
            Action action;
            string result;
            action = new SequentialAction();
            result = action.DebuggerDisplay;
            ExecutionsTest.TestEqual(result, "Sequential", String.Format("Action.Name failed for class SequentialAction. Result: \"{0}\"", result));
            action = new SetConstantAction<long>("dummy", 0L);
            result = action.DebuggerDisplay;
            ExecutionsTest.TestEqual(result, "SetConstant", String.Format("Action.Name failed for class SetConstant. Result: \"{0}\"", result));
        }

        protected virtual string DebuggerDisplay {
            get {
                string typeName = this.GetType().Name;
                typeName = Text.RemoveNonalphanumeric(typeName);
                IList<string> parts = Text.GetLowerCasePartsFromMixedCase(typeName);
                List<string> partsWithoutAction = new List<string>(parts);
                for (int i = 0; i < partsWithoutAction.Count; ++i) {
                    if (partsWithoutAction[i] == "action") {
                        partsWithoutAction.RemoveRange(i, partsWithoutAction.Count - i);
                        break;
                    }
                }
                string name = Text.CombineAsPascalCase(partsWithoutAction);
                return name;
            }
        }
    }
}
