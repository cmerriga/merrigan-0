using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Merrigan0.ArraysInternal;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.ExecutionsInternal;
using Merrigan0.GlomsInternal;
using Merrigan0.NmlInternal;
using Merrigan0.RecognizersInternal;
using Random = Merrigan0.Random;

namespace Test {
    // Here to clear up compilation ambiguity with String
    using Merrigan0;

    public class Blah : IParent<Blah> {
        public Array<Blah> Children { get { return Array<Blah>.From(new Blah(), new Blah()); } }

        public override string ToString() {
            return "Blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah";
        }

        public int Double(int n) {
            return n * 2;
        }
    }

    class Test {
        static void Main(string[] args) {
            ////Glom g0 = new Glom("a", 0);
            ////Glom g1 = new Glom("a", 1);
            ////Glom g2 = new Glom("a", 2);
            ////Glom g3 = new Glom("a", 3);
            ////Glom g4 = new Glom("a", 4);
            ////Glom g5 = new Glom("a", 5);
            ////Glom g6 = new Glom("a", 6);
            ////Context c02 = new Context(g0, g2);
            ////Context c03 = new Context(g0, g3);
            ////Context c035 = new Context(g0, g3, g5);
            ////Context c036 = new Context(g0, g3, g6);
            ////Context c1 = new Context(g1);
            ////Context c14 = new Context(g1, g4);
            ////return;

            //Array<Type> types = Reflection.ImplementedTypes(typeof(ArrayGlom));
            //IndentedConsole.Ambient.AppendLine(types);

            //Array<Type> interfaces = Reflection.Interfaces(typeof(int[]));
            //IndentedConsole.Ambient.AppendLine(interfaces);

            TestExecution execution;
            execution = Testing.Test(() => {
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray");
                Testing.Test("Merrigan0.Array");

                //Testing.Test("Merrigan0.Hexadecimal");
//                Testing.Test("Merrigan0.Json2.Test");
               // Testing.Test("Merrigan0.Json2.EscapedUnicode");
                // Testing.Test("Merrigan0.Json2.StringifyArray");
                //Testing.Test("Merrigan0.ExampleAttribute.TestClass");

                //Testing.Test("Merrigan0.NmlInternal.Nml.Test");

                // Recognizers
                //Testing.Test("Merrigan0.RecognizersInternal.AndRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.AnyRepetitionsRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.BeginRecognizer.Test");
                ////Testing.Test("Merrigan0.RecognizersInternal.CharacterClassGroupRecognizer.Test"); // not testable yet
                ////Testing.Test("Merrigan0.RecognizersInternal.CharacterClassRecognizer.Test"); // not testable yet
                //Testing.Test("Merrigan0.RecognizersInternal.CharacterRangeRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.CharacterSetRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.CustomCharacterClassGroupRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.CustomCharacterClassRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.CustomRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.CustomRepetitionsRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.DigitRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.DoubleRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.EndRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.LineEndRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.NamedRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.NonzeroDigitRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.OneOrMoreRepetitionsRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.OptionalRecognizer.Test");
                //// Testing.Test("Merrigan0.RecognizersInternal.OrRecognizer.Test"); // not testable yet
                //Testing.Test("Merrigan0.RecognizersInternal.RegexRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.RepetitionsRangeRecognizer.Test");
                ////Testing.Test("Merrigan0.RecognizersInternal.RepetitionsRecognizer.Test"); // not testable yet
                //Testing.Test("Merrigan0.RecognizersInternal.SeparatedRepetitionsRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.SequenceRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.SpecificCharacterRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.SpecificStringRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.TransformingRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.TrueRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.UntilRecognizer.Test");

                // JSON
                //Testing.Test("Merrigan0.JsonInternal.JsonStringRecognizer.Test");
                //Testing.Test("Merrigan0.RecognizersInternal.MinimalWholeNumberRecognizer.Test");

                //    //    Testing.Test(merrigan0);
            //    //Testing.Test("Merrigan0.Math.StretchPositiveToReal", new UntilFailTestStrategy(15));
            //    //Array<int> a = Array<int>.From(1, 2, 3, 4, 5, 6);
            //    //AppendArray<int> appended = new AppendArray<int>(a, 7);
            //    //Testing.Check(appended);
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray.TryGetItem");
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray.Length");
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray.length");
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray.Sorted");
                //Testing.Test("Merrigan0.ArraysInternal.AppendArray");
                //Testing.Test("Merrigan0.X11Colors.Common");
                //Testing.Test("Merrigan0.String.CompareTo");
                ////Expression expression = Nml.Expression("((1, 2, 3), 4)");
                    ////IndentedConsole.Ambient.AppendLine(expression);
                    ////Glom glom = Nml.Glom(expression);
                    ////IndentedConsole.Ambient.AppendLine(glom);
            //    //Testing.Test(typeof(Math));
                //Heap<int>.Test();
                //Conversion.Test();
                //AndRecognizer.Test();
                //AnyRepetitionsRecognizer.Test();
                //BeginRecognizer.Test();
                //CustomCharacterClassGroupRecognizer.Test();
                //CustomCharacterClassRecognizer.Test();
                //CustomRecognizer.Test();
                //CustomRepetitionsRecognizer.Test();
                //CharacterRangeRecognizer.Test();
                //DigitRecognizer.Test();
                //DoubleRecognizer.Test();
                //EndRecognizer.Test();
                //LineEndRecognizer.Test();
                //NamedRecognizer.Test();
                //OneOrMoreRepetitionsRecognizer.Test();
                //OptionalRecognizer.Test();
                //OrRecognizer.Test();
                //RegexRecognizer.Test();
                //RepetitionsRangeRecognizer.Test();
                //SeparatedRepetitionsRecognizer.Test();
                //SequenceRecognizer.Test();
                //StringRecognizer.Test();
                //TransformingRecognizer.Test();
                //TrueRecognizer.Test();
                //UntilRecognizer.Test();
                //NmlRecognizer.Test();
            });
            execution.Write(IndentedConsole.Ambient);



            //double[] numbers = Conversion.Convert<double[]>(glom);
            //IndentedConsole.Ambient.AppendLine((numbers[0] + numbers[1] + numbers[2]) / 3);

            //Array<double> a;
            //Nml.Object("(1.1, 2.2, 3.3)", typeof(Array<double>));
            
            //Array<Type> implementedTypes = Reflection.ImplementedTypes(typeof(ReverseArray<int>));
            //IndentedConsole.Ambient.AppendLine(implementedTypes.Transform(t => t.Name));

            //Func<object, object> convertFunction;
            //Conversion.TryGetConvertFunction(typeof(String), typeof(string), out convertFunction);
            //return;

            //// Utilities.SortWithKey
            //Array<string> unsortedStrings = Array<string>.From(
            //    "four", "nine", "five", "four", "one", "six", "one");
            //Array<int> unsortedValues = Array<int>.From(4, 9, 5, 4, 1, 6, 1);
            //Array<string> sortedStrings;
            //Array<int> sortedValues;
            //Utilities.SortWithKey(unsortedStrings, unsortedValues, out sortedStrings, out sortedValues);
            //IndentedConsole.Ambient.AppendLine(sortedStrings);
            //IndentedConsole.Ambient.AppendLine(sortedValues);
            //return;

            // Conversion.FlatlyOrderedPairs
            //Array<int> values = Array<int>.From(4, 3, 2, 1);
            //Array<string> strings = Array<string>.From(
            //    "five", "four", "three", "two", "one");
            //Array<Tuple<string, int>> flatlyOrderedPairs = Conversion.FlatlyOrderedPairs(strings, values);
            //IndentedConsole.Ambient.AppendLine(flatlyOrderedPairs);

            //Glom context = new Glom(
            //    "a", 1,
            //    "b", 2,
            //    "c", 3,
            //    "D", new Glom("a", 10),
            //    "a$", "A",
            //    "b$", "BB",
            //    "c$", "CCC");
            //CommandConsole console = new CustomCommandConsole(
            //    Array<Command>.From(),
            //    //new Command("nml", RunNml, null, TrueRecognizer.Only),
            //    //new Command("quit", Quit, null)),
            //    tryProcessCommand: s => {
            //        try {
            //            Expression expression = Nml.Expression(s);
            //            object result = context.Evaluate(expression);
            //            Glom newContext = result as Glom;
            //            if (newContext != null) {
            //                context = newContext;
            //                IndentedConsole.Ambient.AppendLine(context);
            //            } else {
            //                IndentedConsole.Ambient.AppendLine(result);
            //            }
            //        } catch (Exception ex) {
            //            IndentedConsole.Ambient.AppendLine(ex.StackTrace);
            //        }
            //        return true;
            //    });
            //console.Run(null);
            //return;

            // TestConsole();

            //TestNml();

            //int result = Execution.Func(Plus, 3, 4);
            //System.Console.WriteLine(result);

            //Assembly merrigan0 = Reflection.Assembly("Merrigan0");
            //foreach (Assembly assembly in Reflection.Assemblies) {
            //    //if (Reflection.HasAttribute(assembly, 
            //    if (!((String)assembly.FullName).StartsWith("Merrigan0")) { continue; }
            //    //System.Console.WriteLine(assembly.FullName);
            //    merrigan0 = assembly;
            //}


            //string string1 = "large";
            //String string2 = String.From("turd");
            //String string3 = string1 + " " + string2;
            //System.Console.WriteLine(string3);

            //String string1 = "large";
            //String string2 = String.From("turd");
            //String string3 = String.Concatenate(string1, " ", string2);
            //System.Console.WriteLine(string3);

            //TestExecution execution;
            //execution = Testing.Test(() => {
            //    Testing.TestClass(typeof(Reflection));

            //    //Testing.TestClass(typeof(CustomCharacterClassGroupRecognizer));
            //    //Testing.TestClassMethod(typeof(Reflection).GetMethod("Assemblies", BindingFlags.Static | BindingFlags.Public));
            //    //Testing.TestClassMethod(Reflection.GetMethod(
            //    //    typeof(Reflection),
            //    //    BindingFlags.Static | BindingFlags.Public,
            //    //    "MultitypeEquals"));
            //    //Testing.TestClassMethod(Reflection.GetMethod(
            //    //    typeof(Reflection),
            //    //    BindingFlags.Static | BindingFlags.Public,
            //    //    "Cast",
            //    //    typeof(object)));
            //    //Reflection.Test();
            //});
            //execution.Write();

            //TestRecognizers();

            //// Test reading a numeric glom from a string
            //Glom glom = Nml.Expression("13").EvaluateGlom(null);

            //for (int i = 0; i < 500; ++i) {
            //    Type answer = Random.Ambient.Type();
            //    System.Console.WriteLine(answer.Name);
            //}

            //    //Testing.TestClassMethod(typeof(Reflection).GetMethod("Assemblies", BindingFlags.Static | BindingFlags.Public));
            //    //Testing.TestClassMethod(
            //    //    Reflection.GetMethod(
            //    //        typeof(Reflection),
            //    //        BindingFlags.Static | BindingFlags.Public,
            //    //        "Attributes",
            //    //        new Type[] { typeof(CommitmentAttribute) },
            //    //        typeof(MethodInfo)),
            //    //    typeof(TestClass).GetMethod("Method1"));
            //    Testing.TestClassMethod(Reflection.GetMethod(
            //        typeof(Math),
            //        BindingFlags.Static | BindingFlags.Public,
            //        "Average"));
            //    Testing.TestClassMethod(Reflection.GetMethod(
            //        typeof(Math),
            //        BindingFlags.Static | BindingFlags.Public,
            //        "Factorial"));
            //    //Testing.TestClassMethod(Reflection.GetMethod(
            //    //    typeof(Math),
            //    //    BindingFlags.Static | BindingFlags.Public,
            //    //    "Factorial"));

            ////TestReflectionCast();

            ////int c = Utilities.Convert<int>('c');
            ////System.Console.WriteLine(c);

            //TestResult result = Testing.Test(typeof(RandomGenerator));
            //result.Write();

            //TestRandomGenerator();
            //System.Console.WriteLine();

            ////TestResult result;
            ////result = Testing.TestClass(typeof(Hexadecimal));
            ////result.Write();

            //// Test random from probability distribution
            //double expected = 0;
            //double expectedSquared = 0;
            //int trials = 10000;
            //for (int i = 0; i < trials; ++i) {
            //    double poisson = Random.Exponential(200);
            //    expected += poisson;
            //    expectedSquared += poisson * poisson;
            //    //System.Console.WriteLine(poisson);
            //}
            //System.Console.WriteLine("E(X): {0}", expected / trials);
            //System.Console.WriteLine("E(X^2): {0}", expectedSquared / trials);
            //System.Console.WriteLine("V(X): {0}", expectedSquared / trials - (expected / trials) * (expected / trials));

            ////System.Diagnostics.Debugger.Launch();
            System.Console.ReadLine();
        }

        public static void MainStaticMethod1() { }

        public static void TestConsole() {
            IConsoleStream console = new SystemConsole();
            console.Append("hello");
            console.EndLine();
            console.AppendLine((String)"hello");
            Blah blah = new Blah();
            console.AppendLine(blah);
            console.EndLine();

            console = new IndentedConsole();
            console.AppendLine(blah);
        }

        public static void TestNml() {
            IndentedConsole console = new IndentedConsole(null, 3);
            Expression expression;

            // MinusExpression
            expression = Nml.Expression("3 - 14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // NegateExpression
            expression = Nml.Expression("-14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // NotEqualsExpression
            expression = Nml.Expression("3 != 14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // NotExpression
            expression = Nml.Expression("!false");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // RightShiftExpression
            expression = Nml.Expression("14 >> 2"); // should be 3
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // BitwiseAndExpression
            expression = Nml.Expression("3 & 14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // BitwiseNotExpression
            expression = Nml.Expression("~14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // BitwiseOrExpression
            expression = Nml.Expression("14 | 3"); // should be 15
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // BitwiseXorExpression
            expression = Nml.Expression("14 ^ 3"); // should be 15
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // ConstantExpression
            expression = Nml.Expression("15");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // ConstantExpression
            expression = Nml.Expression("true");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // ConstantExpression
            expression = Nml.Expression("false");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // ConstantExpression
            expression = Nml.Expression("null");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // EqualsExpression
            expression = Nml.Expression("13 == 13");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // GreaterExpression
            expression = Nml.Expression("14 > 13");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // GreaterOrEqualsExpression
            expression = Nml.Expression("14 >= 13");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // IdentifierExpression
            expression = Nml.Expression("id1");
            console.AppendLine(expression);
            Glom glom = new Glom("id1", 19);
            console.AppendLine(glom.Evaluate(expression));

            // LeftShiftExpression
            expression = Nml.Expression("14 << 2"); // should be 56
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // LessExpression
            expression = Nml.Expression("14 < 13");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // LessOrEqualsExpression
            expression = Nml.Expression("14 <= 13");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // LogicalAndExpression
            expression = Nml.Expression("true && false"); // expect false
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // LogicalOrExpression
            expression = Nml.Expression("true || false"); // expect true
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            // PlusExpression
            expression = Nml.Expression("3 + 14");
            console.AppendLine(expression);
            console.AppendLine(expression.Evaluate());

            expression = Nml.Expression("a, b, c");
            IndentedConsole.Ambient.AppendLine(expression);
            Glom context = new Glom(
                "a", 1,
                "b", 2,
                "c", 3);
            IndentedConsole.Ambient.AppendLine(context);
            object evaluation = context.Evaluate(expression);
            IndentedConsole.Ambient.AppendLine(evaluation);
            object sum = context.Evaluate(Nml.Expression("a + b * c"));
            IndentedConsole.Ambient.AppendLine(sum);
        }

        ////public static void TestRecognizers() {
        ////    TestExecution execution;
        ////    using (execution = Testing.Test.Begin("SeparatedRepetitionsRecognizer")) {
        ////        Recognizer recognizer = new SeparatedRepetitionsRecognizer(
        ////            new NamedRecognizer("hex-digit"),
        ////            new SequenceRecognizer(
        ////                new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
        ////                new CharacterRecognizer(','),
        ////                new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))));
        ////        TestTryRecognize(recognizer, "", true, true);
        ////        TestTryRecognize(recognizer, "a", true, true);
        ////        TestTryRecognize(recognizer, "as", false, true);
        ////        TestTryRecognize(recognizer, "as", true, false);
        ////        TestTryRecognize(recognizer, "a,1", true, true);
        ////        TestTryRecognize(recognizer, "a,1s", false, true);
        ////        TestTryRecognize(recognizer, "a,1s", true, false);
        ////        TestTryRecognize(recognizer, "a, 1 , B,0", true, true);
        ////    }
        ////    execution.Write();
        ////    System.Console.WriteLine();

        ////    //TestOrRecognizer();

        ////    //TestExecution execution;
        ////    //using (execution = Testing.Test.Begin("JsonNumberRecognizer")) {
        ////    //    recognizer = Json.JsonValueRecognizer;
        ////    //    recognizer = recognizer.NamedRecognizer("number");
        ////    //    //System.Console.WriteLine(Json.JsonNumberRecognizer.ToRegex());
        ////    //    TestTryRecognize(recognizer, "dirt", true, false);
        ////    //    TestTryRecognize(recognizer, "123", false, true);
        ////    //    TestTryRecognize(recognizer, "123", true, true);
        ////    //    TestTryRecognize(recognizer, "123a", false, true);
        ////    //    TestTryRecognize(recognizer, "123E+01", true, true);
        ////    //    TestTryRecognize(recognizer, "", true, false);
        ////    //}
        ////    //execution.Write();
        ////    //System.Console.WriteLine();
        ////}

        ////private static void TestTryRecognize(Recognizer recognizer, String s, bool wholeString, bool expected) {
        ////    using (Testing.Test.Begin(recognizer.GetType().Name + " " + s)) {
        ////        long i = 0;
        ////        bool recognized = wholeString ?
        ////            recognizer.TryRecognize(s) :
        ////            recognizer.TryRead(s, ref i);
        ////        if (recognized == expected) {
        ////            Testing.Test.ReportSucceeded();
        ////        } else {
        ////            Testing.Test.ReportFailed();
        ////        }
        ////    }
        ////}

        public static void TestRandomGenerator() {
            RandomGenerator randomGenerator = new RandomGenerator();
            bool b = randomGenerator.Bool();
            System.Console.WriteLine(b);
            byte @byte = randomGenerator.Byte();
            System.Console.WriteLine(@byte);
            char ch = randomGenerator.Char();
            System.Console.WriteLine(ch);
            decimal dec = randomGenerator.Decimal();
            System.Console.WriteLine(dec);
            System.Console.WriteLine();
            double d = randomGenerator.Double();
            System.Console.WriteLine(d);
            d = randomGenerator.Double(5.0);
            System.Console.WriteLine(d);
            d = randomGenerator.Double(20.0, 30.0);
            System.Console.WriteLine(d);
            float f = randomGenerator.Float();
            System.Console.WriteLine(f);
            f = randomGenerator.Float(5.0f);
            System.Console.WriteLine(f);
            f = randomGenerator.Float(20.0f, 30.0f);
            System.Console.WriteLine(f);
            DateTime time = randomGenerator.FutureTime();
            System.Console.WriteLine(time);
            time = randomGenerator.FutureTime(TimeSpan.FromMinutes(1));
            System.Console.WriteLine(time);
            Guid guid = randomGenerator.Guid();
            System.Console.WriteLine(guid);
            System.Console.WriteLine();
            int n = randomGenerator.Int();
            System.Console.WriteLine(n);
            n = randomGenerator.Int(100);
            System.Console.WriteLine(n);
            n = randomGenerator.Int(-100);
            System.Console.WriteLine(n);
            n = randomGenerator.Int(20, 30);
            System.Console.WriteLine(n);
            long l = randomGenerator.Long();
            System.Console.WriteLine(l);
            l = randomGenerator.Long(100 * 2000000000L);
            System.Console.WriteLine(l);
            l = randomGenerator.Long(-100 * 2000000000L);
            System.Console.WriteLine(l);
            l = randomGenerator.Long(20 * 2000000000L, 30 * 2000000000L);
            System.Console.WriteLine(l);
            double normal = randomGenerator.Normal(50.0, 10.0);
            System.Console.WriteLine(normal);
            System.Console.WriteLine();
            Nullable<DateTime> nullableTime = randomGenerator.Nullable<DateTime>();
            System.Console.WriteLine(nullableTime);
            nullableTime = (Nullable<DateTime>)randomGenerator.Nullable(typeof(DateTime));
            System.Console.WriteLine(nullableTime);
            time = randomGenerator.PastTime();
            System.Console.WriteLine(time);
            time = randomGenerator.PastTime(TimeSpan.FromMinutes(1));
            System.Console.WriteLine(time);
            sbyte @sbyte = randomGenerator.Sbyte();
            System.Console.WriteLine(@sbyte);
            short @short = randomGenerator.Short();
            System.Console.WriteLine(@short);
            String s = randomGenerator.String();
            System.Console.WriteLine(s);
            b = randomGenerator.Test(.2);
            System.Console.WriteLine(b);
            System.Console.WriteLine();
            uint u = randomGenerator.Uint();
            System.Console.WriteLine(u);
            ////u = randomGenerator.Uint(100U);
            ////System.Console.WriteLine(u);
            ////u = randomGenerator.Uint(-100U);
            ////System.Console.WriteLine(u);
            ////u = randomGenerator.Uint(20U, 30U);
            ////System.Console.WriteLine(u);
            ulong ul = randomGenerator.Ulong();
            System.Console.WriteLine(ul);
            ////ul = randomGenerator.Ulong(100 * 2000000000L);
            ////System.Console.WriteLine(ul);
            ////ul = randomGenerator.Ulong(-100 * 2000000000L);
            ////System.Console.WriteLine(ul);
            ////ul = randomGenerator.Ulong(20 * 2000000000L, 30 * 2000000000L);
            ////System.Console.WriteLine(ul);
            ushort us = randomGenerator.Ushort();
            System.Console.WriteLine(us);
            System.Console.WriteLine();
            TestClass instance = randomGenerator.Value<TestClass>();
            System.Console.WriteLine("instance");
            System.Console.WriteLine("    X: {0}", instance.X);
            System.Console.WriteLine("    Y: {0}", instance.Y);
            System.Console.WriteLine("    Z: {0}", instance.Z);
        }

        public static void TestReflectionCast() {
            sbyte sb;
            byte by;
            short sh;
            ushort ush;
            char ch;
            int n;
            uint u;
            long l;
            ulong ul;
            float f;
            double r;
            decimal d;

            // Cast from sbyte
            sbyte sbOriginal = (sbyte)127;
            sb = Reflection.Cast<sbyte>(sbOriginal);
            System.Console.WriteLine(sb);
            //sh = Reflection.Cast<short>(sbOriginal);
            //System.Console.WriteLine(sh);
            n = Reflection.Cast<int>(sbOriginal);
            System.Console.WriteLine(n);
            l = Reflection.Cast<long>(sbOriginal);
            System.Console.WriteLine(l);
            f = Reflection.Cast<float>(sbOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(sbOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(sbOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from byte
            byte byOriginal = (byte)255;
            by = Reflection.Cast<byte>(byOriginal);
            System.Console.WriteLine(by);
            //by = Reflection.Cast<byte>(byOriginal);
            //System.Console.WriteLine(by);
            //sh = Reflection.Cast<short>(byOriginal);
            //System.Console.WriteLine(sh);
            //ush = Reflection.Cast<ushort>(byOriginal);
            //System.Console.WriteLine(ush);
            //ch = Reflection.Cast<char>(byOriginal);
            //System.Console.WriteLine(ch);
            n = Reflection.Cast<int>(byOriginal);
            System.Console.WriteLine(n);
            u = Reflection.Cast<uint>(byOriginal);
            System.Console.WriteLine(u);
            l = Reflection.Cast<long>(byOriginal);
            System.Console.WriteLine(l);
            ul = Reflection.Cast<ulong>(byOriginal);
            System.Console.WriteLine(ul);
            f = Reflection.Cast<float>(byOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(byOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(byOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from short
            short shOriginal = (short)32767;
            sh = Reflection.Cast<short>(shOriginal);
            System.Console.WriteLine(sh);
            n = Reflection.Cast<int>(shOriginal);
            System.Console.WriteLine(n);
            l = Reflection.Cast<long>(shOriginal);
            System.Console.WriteLine(l);
            f = Reflection.Cast<float>(shOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(shOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(shOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from ushort
            ushort ushOriginal = (ushort)65535;
            ush = Reflection.Cast<ushort>(ushOriginal);
            System.Console.WriteLine(ush);
            char chOriginal = (char)65535;
            ch = Reflection.Cast<char>(chOriginal);
            System.Console.WriteLine(ch);
            n = Reflection.Cast<int>(ushOriginal);
            System.Console.WriteLine(n);
            u = Reflection.Cast<uint>(ushOriginal);
            System.Console.WriteLine(u);
            l = Reflection.Cast<long>(ushOriginal);
            System.Console.WriteLine(l);
            ul = Reflection.Cast<ulong>(ushOriginal);
            System.Console.WriteLine(ul);
            f = Reflection.Cast<float>(ushOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(ushOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(ushOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from char
            chOriginal = (char)65535;
            n = Reflection.Cast<int>(chOriginal);
            System.Console.WriteLine(n);
            u = Reflection.Cast<uint>(chOriginal);
            System.Console.WriteLine(u);
            l = Reflection.Cast<long>(chOriginal);
            System.Console.WriteLine(l);
            ul = Reflection.Cast<ulong>(chOriginal);
            System.Console.WriteLine(ul);
            f = Reflection.Cast<float>(chOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(chOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(chOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from int
            int nOriginal = (int)0x7FFFFFFF;
            n = Reflection.Cast<int>(nOriginal);
            System.Console.WriteLine(n);
            l = Reflection.Cast<long>(nOriginal);
            System.Console.WriteLine(l);
            f = Reflection.Cast<float>(nOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(nOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(nOriginal);
            System.Console.WriteLine(d);

            // Cast from uint
            uint uOriginal = (uint)0xFFFFFFFF;
            u = Reflection.Cast<uint>(uOriginal);
            System.Console.WriteLine(u);
            l = Reflection.Cast<long>(uOriginal);
            System.Console.WriteLine(l);
            ul = Reflection.Cast<ulong>(uOriginal);
            System.Console.WriteLine(ul);
            f = Reflection.Cast<float>(uOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(uOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(uOriginal);
            System.Console.WriteLine(d);

            // Cast from float
            float fOriginal = 4321.1234f;
            f = Reflection.Cast<float>(fOriginal);
            System.Console.WriteLine(f);
            r = Reflection.Cast<double>(fOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(fOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from double
            double rOriginal = 54321.12345;
            r = Reflection.Cast<double>(rOriginal);
            System.Console.WriteLine(r);
            d = Reflection.Cast<decimal>(rOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();

            // Cast from decimal
            decimal dOriginal = 987654321.123456789m;
            d = Reflection.Cast<decimal>(dOriginal);
            System.Console.WriteLine(d);

            System.Console.Write("Looks good? ");
            System.Console.ReadLine();
        }

        public static int Plus(int n1, int n2) {
            return n1 + n2;
        }

        public abstract class AbstractTestClass {
        }

        public class TestClass : AbstractTestClass {
            public int X;
            public int Y { get; set; }
            public int Z { get; private set; }

            public TestClass() {
            }

            public TestClass(int z) {
                Z = z;
            }

            public TestClass(int x, int y, int z) {
                X = x;
                Y = y;
                Z = z;
            }

            public static void StaticMethod1() { }

            [ExampleAttribute]
            [Throws]
            public void Method1() { }
        }

////        public static R Call<P1, P2, R>(Func<P1, P2, R> function, P1 parameter1, P2 parameter2) {
////#if DEBUG
////            System.Console.WriteLine("{0} {1}", parameter1, parameter2);
////            return function(parameter1, parameter2);
////#else
////            return function(parameter1, parameter2);
////#endif
////        }

////        public static int Funcie(int p1, int p2) {
////            return p1 + p2;
////        }

////        public static void Try00(Action action) {
////            action();
////        }

////        public static void Try20(Action<int, int> action) {
////            action(3, 4);
////        }
    }
}

////foreach (MethodInfo method in typeof(String).GetMethods(BindingFlags.Static | BindingFlags.Public)) {
////    Console.WriteLine(Reflection.MethodToString(method));
////}
////return;

