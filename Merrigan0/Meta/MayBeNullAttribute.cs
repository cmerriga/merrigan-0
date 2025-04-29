using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // Normally reference-type input parameters or return values are expected not to be null. This attribute declares them okay.
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class MayBeNullAttribute : Attribute {
        private string iffConditionNml;
        private Expression iffCondition;

        [MayBeNull]
        public Expression IffCondition {
            get {
                if (iffCondition != null) {
                    return iffCondition;
                }
                if (iffConditionNml == null) {
                    return null;
                }
                iffCondition = Nml.Expression(iffConditionNml);
                return iffCondition;
            }
        }
                
        // The cases when the value may be null
        public string When { get; set; }

        public MayBeNullAttribute() { }

        public MayBeNullAttribute(string iffConditionNml) {
            this.iffConditionNml = iffConditionNml;
        }
    }
}
