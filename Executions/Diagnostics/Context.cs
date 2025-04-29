using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ExecutionsTest = Executions.Test;

namespace Executions {
    ////[DebuggerDisplay("{DebuggerDisplay}")]
    public partial class Context {
        ////public static void Test() {
        ////    TestIndexGet();
        ////    TestIndexSet();
        ////    TestInitializeParent();
        ////    TestDelete();
        ////    TestGet();
        ////    TestTypedGet();
        ////    TestSet();
        ////}

        ////protected static void TestIndexGet() {
        ////    // Simple context
        ////    Context simple = CreateSimpleContext();
        ////    TestIndexGet(simple, "abc", "abc");
        ////    TestIndexGet(simple, "A123", 123);
        ////    TestIndexGet(simple, "Abc", "Abc");

        ////    // Context with parent, local definition
        ////    Context withParent = CreateContextWithParent();
        ////    TestIndexGet(withParent, "child1", "child");

        ////    // Context with parent, parent definition
        ////    TestIndexGet(withParent, "parent1", 123);

        ////    // Context with parent, conflicting definitions
        ////    TestIndexGet(withParent, "conflicting1", "conflicting-child");
        ////}

        ////protected static void TestIndexGet(Context context, string name, object expectedResult) {
        ////    object result = context[name];
        ////    if (!result.Equals(expectedResult)) {
        ////        Console.WriteLine("IndexGet failed on \"{0}\". Result: \"{1}\"", name, result);
        ////    }
        ////}

        ////protected static void TestIndexSet() {
        ////    // Simple context
        ////    Context simple = new Context();
        ////    TestIndexSet(simple, "abc", "abc");
        ////    TestIndexSet(simple, "A123", 123);
        ////    TestIndexSet(simple, "Abc", "Abc");

        ////    // Context with parent, local definition
        ////    Context parent = new Context();
        ////    Context child = new Context(parent);
        ////    TestIndexSet(child, "child1", "child");

        ////    // Context with parent, parent definition
        ////    parent["parent1"] = 123;
        ////    TestIndexGet(child, "parent1", 123);

        ////    // Context with parent, conflicting definitions
        ////    parent["conflicting1"] = "conflicting-parent";
        ////    TestIndexSet(child, "conflicting1", "conflicting-child");
        ////}

        ////protected static void TestIndexSet(Context context, string name, object expectedResult) {
        ////    context[name] = expectedResult;
        ////    object result = context[name];
        ////    if (!result.Equals(expectedResult)) {
        ////        Console.WriteLine("IndexSet failed on \"{0}\". Result: \"{1}\"", name, result);
        ////    }
        ////}

        ////protected static void TestInitializeParent() {
        ////    Context noParent = new Context();
        ////    ExecutionsTest.TestEqual(noParent.Parent, null, "Parent should have been null.");
        ////    Context nullParent = new Context();
        ////    ExecutionsTest.TestEqual(nullParent.Parent, null, "Parent should have been null.");
        ////    Context parent = new Context();
        ////    Context withParent = new Context(parent);
        ////    ExecutionsTest.TestEqual(withParent.Parent, parent, "Parent was incorrect.");
        ////}

        ////protected static void TestDelete() {
        ////    Context simple = CreateSimpleContext();
        ////    simple.Delete("abc");
        ////    try {
        ////        simple.Get("abc");
        ////        Console.WriteLine("Property \"abc\" was not deleted.");
        ////    } catch {
        ////    }

        ////    Context withParent = CreateContextWithParent();
        ////    withParent.Delete("child1");
        ////    try {
        ////        withParent.Get("child1");
        ////        Console.WriteLine("Property \"child1\" was not deleted.");
        ////    } catch {
        ////    }
        ////    withParent.Delete("conflicting1");
        ////    try {
        ////        object conflicting1 = withParent.Get("conflicting1");
        ////        ExecutionsTest.TestEqual(conflicting1, "conflicting-parent", "Parent property \"conflicting1\" was \"{0}\" instead of \"{1}\"", conflicting1, "conflicting-parent");
        ////    } catch {
        ////        Console.WriteLine("Property \"conflicting1\" was not found in the parent.");
        ////    }
        ////}

        ////protected static void TestGet() {
        ////    // Simple context
        ////    Context simple = CreateSimpleContext();
        ////    TestGet(simple, "abc", "abc");
        ////    TestGet(simple, "A123", 123);
        ////    TestGet(simple, "Abc", "Abc");

        ////    // Context with parent, local definition
        ////    Context withParent = CreateContextWithParent();
        ////    TestGet(withParent, "child1", "child");

        ////    // Context with parent, parent definition
        ////    TestGet(withParent, "parent1", 123);

        ////    // Context with parent, conflicting definitions
        ////    TestGet(withParent, "conflicting1", "conflicting-child");
        ////}

        ////protected static void TestGet(Context context, string name, object expectedResult) {
        ////    object result = context.Get(name);
        ////    if (!result.Equals(expectedResult)) {
        ////        Console.WriteLine("Get failed on \"{0}\". Result: \"{1}\"", name, result);
        ////    }
        ////}

        ////protected static void TestTypedGet() {
        ////    // Simple context
        ////    Context simple = CreateSimpleContext();
        ////    TestTypedGet<string>(simple, "abc", "abc");
        ////    TestTypedGet<int>(simple, "A123", 123);
        ////    TestTypedGet<string>(simple, "Abc", "Abc");

        ////    // Context with parent, local definition
        ////    Context withParent = CreateContextWithParent();
        ////    TestTypedGet<string>(withParent, "child1", "child");

        ////    // Context with parent, parent definition
        ////    TestTypedGet<int>(withParent, "parent1", 123);

        ////    // Context with parent, conflicting definitions
        ////    TestTypedGet<string>(withParent, "conflicting1", "conflicting-child");
        ////}

        ////protected static void TestTypedGet<T>(Context context, string name, object expectedResult) {
        ////    T result = context.Get<T>(name);
        ////    if (!result.Equals(expectedResult)) {
        ////        Console.WriteLine("Typed get failed on \"{0}\". Result: \"{1}\"", name, result);
        ////    }
        ////}

        ////protected static void TestSet() {
        ////    // Simple context
        ////    Context simple = new Context();
        ////    TestSet(simple, "abc", "abc");
        ////    TestSet(simple, "A123", 123);
        ////    TestSet(simple, "Abc", "Abc");

        ////    // Context with parent, local definition
        ////    Context parent = new Context();
        ////    Context child = new Context(parent);
        ////    TestSet(child, "child1", "child");

        ////    // Context with parent, parent definition
        ////    parent.Set("parent1", 123);
        ////    TestGet(child, "parent1", 123);

        ////    // Context with parent, conflicting definitions
        ////    parent["conflicting1"] = "conflicting-parent";
        ////    TestSet(child, "conflicting1", "conflicting-child");
        ////}

        ////protected static void TestSet(Context context, string name, object expectedResult) {
        ////    context.Set(name, expectedResult);
        ////    object result = context[name];
        ////    if (!result.Equals(expectedResult)) {
        ////        Console.WriteLine("IndexSet failed on \"{0}\". Result: \"{1}\"", name, result);
        ////    }
        ////}

        ////protected static Context CreateSimpleContext() {
        ////    Context context = new Context();
        ////    context.Set("abc", "abc");
        ////    context.Set("A123", 123);
        ////    context.Set("Abc", "Abc");
        ////    return context;
        ////}

        ////protected static Context CreateContextWithParent() {
        ////    Context parent = new Context();
        ////    parent.Set("parent1", 123);
        ////    parent.Set("conflicting1", "conflicting-parent");

        ////    Context context = new Context(parent);
        ////    context.Set("child1", "child");
        ////    context.Set("conflicting1", "conflicting-child");
        ////    return context;
        ////}

        ////protected virtual string DebuggerDisplay {
        ////    get {
        ////        if (namesToValues == null) {
        ////            return "[none]";
        ////        }

        ////        StringBuilder builder = new StringBuilder();
        ////        int i = 0;
        ////        int count = namesToValues.Count;
        ////        string[] keys = namesToValues.Keys.ToArray();
        ////        bool first = true;
        ////        while (i < 2 && i < count) {
        ////            if (first) {
        ////                first = false;
        ////            } else {
        ////                builder.Append(", ");
        ////            }
        ////            string key = keys[i];
        ////            builder.AppendFormat("{0}: {1}", key, namesToValues[key]);
        ////            ++i;
        ////        }
        ////        if (i < count) {
        ////            builder.AppendFormat(", ... [+ {0} more]", count - i);
        ////        }
        ////        return builder.ToString();
        ////    }
        ////}
    }
}
