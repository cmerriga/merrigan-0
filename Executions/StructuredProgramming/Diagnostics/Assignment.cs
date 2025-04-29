using Executions.StructuredProgramming.Expressions;
using ExecutionsTest = Executions.Test;

namespace Executions.StructuredProgramming {
    public partial class Assignment<T> : Statement {
        public static void Test() {
            ExecutionsTest.TestConstructors<Assignment<T>>();
            TestConstructor();
        }

        public static void TestConstructor() {
            // Test that the constructor sets all values as expected
            string variableName = "variableName1";
            Expression<int> expression = new Literal<int>(1);
            Assignment<int> assignment = new Assignment<int>(variableName, expression);
            ExecutionsTest.TestMemberEqual(assignment.VariableName, variableName, "VariableName");
            ExecutionsTest.TestMemberEqual(assignment.Expression, expression, "Expression");
            ExecutionsTest.TestMemberEqual(assignment.Type, typeof(int), "Type");
        }
    }
}
