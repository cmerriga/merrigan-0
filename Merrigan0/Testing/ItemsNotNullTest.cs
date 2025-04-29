using System;
using System.Collections;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ItemsNotNullTest : CompiledTest {
        public static readonly ItemsNotNullTest Only = new ItemsNotNullTest();

        public ItemsNotNullTest() : base("items not null") { }

        protected override bool ReallyRun(object[] arguments, out String errorMessage) {
            IEnumerable enumerable = arguments[0] as IEnumerable;
            foreach (object item in enumerable) {
                if (item == null) {
                    errorMessage = "at least one item was null";
                    return false;
                }
            }
            errorMessage = null;
            return true;
        }
    }
}
