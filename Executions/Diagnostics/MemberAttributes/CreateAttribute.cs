using System;

namespace Executions.Diagnostics.MemberAttributes {
    public class CreateAttribute : MemberDiagnosticAttribute {
        [Create("name")]
        public string Name { get; private set; }

        public CreateAttribute(string name) {
            Name = name;
        }

        ////public override void Check(MemberInfo memberInfo, object value) ()
    }
}
