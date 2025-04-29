using Executions.StructuredProgramming.Expressions;
using ExecutionsTest = Executions.Test;

namespace Executions.StructuredProgramming {
    public partial class Block : Statement {
        public static void Test() {
            TestConstructor();
        }

        public static void TestConstructor() {
            // Test that the constructor sets all values as expected
            Statement[] statements = new Statement[0];
            Block block = new Block(statements);
            ExecutionsTest.TestMemberEqual(block.Statements, statements, "Statements");
        }
    }
}
