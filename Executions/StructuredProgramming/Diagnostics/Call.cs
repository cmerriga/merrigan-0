using System;
using Executions.StructuredProgramming.Expressions;
using ExecutionsTest = Executions.Test;

namespace Executions.StructuredProgramming {
    public partial class Call<T> : Expression<T> {
        public static void Test() {
            TestConstructor();
        }

        public static void TestConstructor() {
            // Test that the constructor sets all values as expected
            Variable[] parameters = new Variable[] {
                new Variable<int>("int1", "int1"),
                new Variable<string>("string1", "string1")
            };
            Function<int> function = new Function<int>("dummy", parameters, "Whatevers", Array.Empty<Statement>());
            Expression[] expressions = new Expression[] {
                new Literal<int>(1),
                new Literal<string>("1")
            };
            Call<int> call = new Call<int>(function, expressions);
            ExecutionsTest.TestMemberEqual(call.Function, function, "Function");
            ExecutionsTest.TestMemberEqual(call.ParameterValues, expressions, "ParameterValues");
            ExecutionsTest.TestMemberEqual(call.Type, typeof(int), "Type");
        }
    }
}
