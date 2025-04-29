using System;
using System.Collections;
using System.Reflection;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.MetaInternal;

namespace Merrigan0 {
    [Note("Anything named Test creates exactly one new test, possibly with children.")]
    [Untested]
    public static class Testing {
        public static RandomGenerator Random = null;

        private static Array<Type> genericTypeArgumentsToTest = Array<Type>.From(typeof(int), typeof(String));

        static Testing() {
            Array<MethodInfo> exampleMethods = Array<MethodInfo>.From(typeof(ExampleClass).GetMethods());
            Array<ParameterInfo> exampleParameters = Array<ParameterInfo>.From(typeof(ExampleClass).GetMethod("Complicated").GetParameters());
            Array<Assembly> exampleAssemblies = Reflection.Assemblies.Where(a => a.FullName.StartsWith("Merrigan"));
            Random = new RandomGenerator();
            Random.RegisterNewFunctions(
                typeof(MethodInfo), (Func<object>)(() => Random.Item(exampleMethods)),
                typeof(ParameterInfo), (Func<object>)(() => Random.Item(exampleParameters)),
                typeof(Assembly), (Func<object>)(() => Random.Item(exampleAssemblies)));
        }

        [WhatItIs("An ordered list of arguments for a list of parameters, using named arguments provided.")]
        public static object[] Arguments(ParameterInfo[] parameters, Map<String, object> argumentsByParameterName) {
            object[] arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i) {
                string parameterName = parameters[i].Name;
                object argument;
                if (argumentsByParameterName.TryGetValue(parameterName, out argument)) {
                    arguments[i] = argument;
                }
            }
            return arguments;
        }

        [WhatItIs("A map of parameter names to values, presuming the arguments given are in the order of the parameters.")]
        public static Map<String, object> ArgumentsByName(ParameterInfo[] parameters, Array<object> argumentsPrefix) {
            MutableMap<String, object> argumentsByNameSoFar = new MutableMap<String, object>();
            for (int i = 0; i < argumentsPrefix.Length; ++i) {
                argumentsByNameSoFar.Add(parameters[i].Name, argumentsPrefix[i]);
            }
            return argumentsByNameSoFar.Current;
        }

        // Runs the given method and remembers the results via a MethodCallGlom
        internal static MethodExecutionContext Call(object instance, MethodInfo method, Map<String, object> argumentsByName = null) {
            if (argumentsByName == null) {
                argumentsByName = Map<String, object>.Empty;
            }
            ParameterInfo[] parameters = method.GetParameters();
            object[] arguments = ArgumentsBlock(parameters, argumentsByName);
            Map<String, object> outputsByName = null;
            object returnValue = null;
            Exception exceptionEncountered = null;
            MethodExecutionContext context;
            try {
                returnValue = method.Invoke(instance, arguments);
                outputsByName = OutputsByName(parameters, arguments);
                context = new InstanceMethodExecutionContext(instance, method, argumentsByName, outputsByName, returnValue);
            } catch (Exception ex) {
                exceptionEncountered = ex.InnerException;
                context = new InstanceMethodExecutionContext(instance, method, argumentsByName, exceptionEncountered);
            }
            return context;
        }

        // Runs the given method and remembers the results via a MethodCallGlom
        internal static MethodExecutionContext Call(MethodInfo method, Map<String, object> argumentsByName = null) {
            if (argumentsByName == null) {
                argumentsByName = Map<String, object>.Empty;
            }
            ParameterInfo[] parameters = method.GetParameters();
            object[] arguments = ArgumentsBlock(parameters, argumentsByName);
            Map<String, object> outputsByName = null;
            object returnValue = null;
            Exception exceptionEncountered = null;
            MethodExecutionContext context;
            try {
                returnValue = method.Invoke(null, arguments);
                outputsByName = OutputsByName(parameters, arguments);
                context = new ClassMethodExecutionContext(method, argumentsByName, outputsByName, returnValue);
            } catch (Exception ex) {
                exceptionEncountered = ex;
                context = new ClassMethodExecutionContext(method, argumentsByName, exceptionEncountered);
            }
            return context;
        }

        [WhatItDoes("Performs instance-checking tests, inline rather than as a full test.")]
        public static void Check(object o, Type type = null) {
            if (type == null) {
                type = (o == null) ? typeof(object) : o.GetType();
            }
            Glom instanceGlom = Glom.From(o);
            Context instanceContext = new Context("class", ClassGlom.From(type), null, instanceGlom);

            // First check all the constraints on public and private properties and fields
            foreach (PropertyInfo property in Reflection.InstanceProperties(type, true).Where(p => !Untested(p))) {
                // Skip any indexer properties
                if (property.GetIndexParameters().Length > 0) {
                    continue;
                }

                object value = Reflection.InstanceValue(o, property);
                Context valueContext = new Context("instance", instanceContext, null, Glom.From(value));
                DoVariableTests(property, valueContext, type, value);
                //bool first = true;
                //object value = null;
                //Context valueGlom = null;
                //foreach (Expression constraint in Reflection.Constraints(property)) {
                //    if (first) {
                //        value = Reflection.InstancePropertyValue(o, property);
                //        if (value == null && !Reflection.HasAttribute(property, typeof(MayBeNullAttribute))) {
                //            Test("must not be null", false);
                //            break;
                //        }
                //        valueGlom = new Context(instanceGlom, Glom.From(value));
                //        first = false;
                //    }
                //    TestConstraint(valueGlom, constraint);
                //}
            }

            foreach (FieldInfo field in Reflection.InstanceFields(type, true).Where(p => !Untested(p))) {
                object value = Reflection.InstanceValue(o, field);
                Context valueContext = new Context(instanceContext, Glom.From(value));
                DoVariableTests(field, valueContext, type, value);
                //bool first = true;
                //object value = null;
                //Context valueGlom = null;
                //foreach (Expression constraint in Reflection.Constraints(field)) {
                //    if (first) {
                //        value = Reflection.InstanceFieldValue(o, field);
                //        if (value == null && !Reflection.HasAttribute(field, typeof(MayBeNullAttribute))) {
                //            Test("must not be null", false);
                //            break;
                //        }
                //        valueGlom = new Context(instanceGlom, Glom.From(value));
                //        first = false;
                //    }
                //    TestConstraint(valueGlom, constraint);
                //}
            }

            // Then look for any [Check]-attributed functions from this or the base classes and call them
            foreach (Type typeToTest in Reflection.ImplementedTypes(type)) {
                foreach (MethodInfo method in Reflection.Methods(type).Where(m => Reflection.HasAttribute(m, typeof(CheckAttribute))).Where(p => !Untested(p))) {
                    method.Invoke(o, null);
                }
            }

        }

        // Generates values legal for the method's input parameters, with dummy values where
        // output parameters are expected.
        public static Map<String, object> RandomArguments(Type type, object instance, ParameterInfo[] parameters, Map<String, object> fixedArgumentsByName = null) {
            ////object[] argumentsSoFar = new object[parameters.Length];
            ////bool argumentsSpecified = (specifiedArgumentsPrefix != null && specifiedArgumentsPrefix.Length > 0);
            MutableMap<String, object> argumentsByNameSoFar = new MutableMap<String, object>();
            int maxTries = 1000;
            int tries = 0;
            while (true) {
                if (tries > maxTries) {
                    throw new Exception("Couldn't create qualifying random arguments");
                }
                for (int i = 0; i < parameters.Length; ++i) {
                    // Skip any out-only parameter
                    ParameterInfo parameter = parameters[i];
                    if (parameter.IsOut) {
                        continue;
                    }

                    Type parameterType;
                    if (parameter.ParameterType.IsByRef) {
                        // Ref types are the desired type plus "&"
                        parameterType = parameter.ParameterType.GetElementType();
                    } else {
                        parameterType = parameter.ParameterType;
                    }
                    argumentsByNameSoFar.Add(parameter.Name, Random.Value(parameterType));
                }
                if (CheckArguments(type, instance, parameters, argumentsByNameSoFar.Current)) {
                    break;
                }
                argumentsByNameSoFar = new MutableMap<String, object>();
                ++tries;
            }
            if (fixedArgumentsByName != null) {
                argumentsByNameSoFar.Add(fixedArgumentsByName);
            }
            return argumentsByNameSoFar.Current;
        }

        public static void Test(Assembly assembly) {
            using (Merrigan0.Test.Begin(assembly.FullName)) {
                foreach (Type @class in Reflection.Classes(assembly)) {
                    if (Reflection.HasAttribute(@class, typeof(DiagnosticOnlyAttribute)) ||
                        Reflection.AbstractClass(@class) ||
                        Reflection.HasAttribute(@class, typeof(UntestedAttribute)) ||
                        @class.IsNested ||
                        @class.IsSpecialName ||
                        typeof(Delegate).IsAssignableFrom(@class)) {
                        continue;
                    }
                    TestClass(@class);
                }
            }
        }

        public static TestExecution Test(FieldInfo field) {
            Type type = field.ReflectedType;
            object instance = null;
            if (type.IsGenericTypeDefinition) {
                TestExecution execution;
                using (execution = Merrigan0.Test.Begin(field.Name + " (generic type)")) {
                    foreach (Type typeArgument in genericTypeArgumentsToTest) {
                        Type specifiedType = type.MakeGenericType(typeArgument);
                        FieldInfo specifiedTypeField = Reflection.SpecificTypeField(field, specifiedType);
                        if (!specifiedTypeField.IsStatic) {
                            instance = Random.Value(specifiedType);
                        }
                        TestSpecifiedTypeField(specifiedTypeField, instance);
                    }
                }
                return execution;
            } else {
                if (!field.IsStatic) {
                    instance = Random.Value(type);
                }
                return TestSpecifiedTypeField(field, instance);
            }
        }

        [WhatItDoes("Tests the given method.")]
        public static TestExecution Test(MethodInfo method) {
            Type type = method.ReflectedType;
            if (type.IsGenericTypeDefinition) {
                // Test a couple different values for type parameter
                foreach (Type typeArgument in genericTypeArgumentsToTest) {
                    Testing.Test(method.Name + " (" + typeArgument.Name + ")", () => {
                        Type specifiedType = type.MakeGenericType(typeArgument);
                        MethodInfo specifiedTypeMethod = Reflection.SpecificTypeMethod(method, specifiedType);
                        TestSpecifiedTypeMethod(specifiedTypeMethod);
                    });
                }
                return TestExecution.Ambient;
            } else {
                return TestSpecifiedTypeMethod(method);
            }
        }

        public static TestExecution Test(PropertyInfo property) {
            Type type = property.ReflectedType;
            object instance = null;
            if (type.IsGenericTypeDefinition) {
                TestExecution execution;
                using (execution = Merrigan0.Test.Begin(property.Name + " (generic type)")) {
                    foreach (Type typeArgument in genericTypeArgumentsToTest) {
                        Type specifiedType = type.MakeGenericType(typeArgument);
                        PropertyInfo specifiedTypeProperty = Reflection.SpecificTypeProperty(property, specifiedType);
                        if (!Reflection.Static(specifiedTypeProperty)) {
                            instance = Random.Value(specifiedType);
                        }
                        TestSpecifiedTypeProperty(specifiedTypeProperty, instance);
                    }
                }
                return execution;
            } else {
                if (!Reflection.Static(property)) {
                    instance = Random.Value(type);
                }
                return TestSpecifiedTypeProperty(property, instance);
            }
        }

        public static TestExecution Test(String name, Func<bool> conditionFunction) {
            bool result = false;
            try {
                result = conditionFunction();
            } catch (Exception) {
            }
            return Test(name, result);
        }

        public static TestExecution Test(String name, bool condition) {
            return Test(name, condition, (String)null);
        }

        public static TestExecution Test(String name, bool condition, String failureMessage) {
            return Test(name, condition, () => failureMessage);
        }

        public static TestExecution Test(String name, bool condition, Func<String> failureMessageFunction = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin(name)) {
                if (condition) {
                    execution.ReportSucceeded();
                } else {
                    if (failureMessageFunction == null) {
                        execution.ReportFailed();
                    } else {
                        execution.ReportFailed(failureMessageFunction());
                    }
                }
            }
            return execution;
        }

        public static void Test(String nml) {
            Test(nml, (TestStrategy)null);
        }

        public static void Test(String nml, TestStrategy strategy) {
            try {
                using (Merrigan0.Test.Begin(nml)) {
                    Expression expression = Nml.Expression(nml);
                    object result = expression.Evaluate();
                    Utilities.Each(result, item => {
                        // If the item is a type, test that
                        Type type = item as Type;
                        if (type != null) {
                            Test(type);
                            return;
                        }

                        // If the item is a method, test that
                        MethodInfo method = item as MethodInfo;
                        if (method != null) {
                            Test(method);
                            return;
                        }

                        // If the item is a property, test that
                        PropertyInfo property = item as PropertyInfo;
                        if (property != null) {
                            Test(property);
                            return;
                        }

                        // If the item is a field, test that
                        FieldInfo field = item as FieldInfo;
                        if (field != null) {
                            Test(field);
                            return;
                        }
                    });
                }
            } catch {
                // Do nothing
            }
        }

        public static void Test(Type type, [Is("type")] object instance) {
            if (type.IsClass) {
                TestClass(type); //// doesn't work yet
            } else if (type.IsEnum) {
                throw new NotImplementedException();
                //TestEnum(type);
            } else if (type.IsValueType) {
                throw new NotImplementedException();
                //TestValueType(type);
            } else {
                throw new NotImplementedException();
            }
        }

        //// Is this needed?
        public static void Test(Type type) {
            if (type.IsClass) {
                TestClass(type);
            } else if (type.IsEnum) {
                throw new NotImplementedException();
                //TestEnum(type);
            } else if (type.IsValueType) {
                throw new NotImplementedException();
                //TestValueType(type);
            } else {
                throw new NotImplementedException();
            }
        }

        //// Is this needed?
        public static void Test(Type type, TestStrategy strategy) {
            if (type.IsClass) {
                TestClass(type, strategy);
            } else if (type.IsEnum) {
                throw new NotImplementedException();
                //TestEnum(type);
            } else if (type.IsValueType) {
                throw new NotImplementedException();
                //TestValueType(type);
            } else {
                throw new NotImplementedException();
            }
        }

        [Wrapper("Test(String, Action)")]
        public static TestExecution Test(Action function) { return Test(DateTime.UtcNow.ToString("G"), function); }

        public static TestExecution Test(String name, Action function) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin(name)) {
                try {
                    function();
                    execution.ReportFinished();
                } catch (Exception e) {
                    execution.ReportFailed(e);
                }
            }
            return execution;
        }

        [WhatItDoes("Performs all instance checks and reports it as a single test execution.")]
        public static TestExecution TestCheck(object o) {
            return Test("check", () => { Check(o); });
        }

        /*
         *      
         * TestCall with instance & params (implicit by order or string map)
         *      random params for those not given
         */
        [WhatItDoes("Runs tests on a class or instance method, base on its attributes.")]
        [Note("If no instance is specified, a random one is generated.")]
        public static TestExecution TestRandomCall(
            MethodInfo method,
            [MayBeNull(When = "method.IsStatic")] object instance = null)
        {
            return Test("random call", () => {
                // Some methods should not be called
                Glom context = (instance == null) ?
                    ClassGlom.From(method.ReflectedType) :
                    (Glom)(new Context("class", ClassGlom.From(method.ReflectedType), null, Glom.From(instance)));
                try {
                    foreach (ConstraintAttribute constraintAttribute in Reflection.Attributes<ConstraintAttribute>(method)) {
                        if (!(bool)constraintAttribute.Constraint.Evaluate(context)) {
                            Merrigan0.Test.ReportSkipped("Skipped due to constraint: " + constraintAttribute.Constraint);
                            return;
                        }
                    }
                } catch (Exception ex) {
                    Merrigan0.Test.ReportSkipped("Couldn't check all constraints: " + ex.Message);
                    return;
                }

                // Create the arguments. Sometimes that isn't possible so skip the test in that case
                Map<String, object> argumentsByName = null;
                try {
                    argumentsByName = RandomArguments(method.ReflectedType, instance, method.GetParameters());
                } catch (Exception ex) {
                    Merrigan0.Test.ReportSkipped("Couldn't create random arguments: " + ex.Message);
                    return;
                }

                if (Reflection.HasAttribute(method, typeof(NotSupportedAttribute))) {
                    TestThrowsException(method, instance, argumentsByName, typeof(NotSupportedException));
                } else {
                    bool instanceMethod = !method.IsStatic;

                    MethodExecutionContext methodExecution;
                    if (instanceMethod) {
                        methodExecution = Call(instance, method, argumentsByName);
                    } else {
                        methodExecution = Call(method, argumentsByName);
                    }
                    bool ranWithoutException = (methodExecution.Exception == null);
                    object returnValue = null;
                    bool returnValueOkay = true;
                    Test("succeeded", ranWithoutException);
                    String argumentsString = "(" + String.Join(argumentsByName.Transform(kvp => kvp.Value), ", ").Truncated(50) + ")";
                    if (ranWithoutException) {
                        if (method.ReturnType != null) {
                            returnValue = methodExecution.ReturnValue;
                            TestExecution returnValueExecution = TestReturnParameterValue(method.ReturnParameter, methodExecution);
                            if (returnValueExecution != null && returnValueExecution.Failed) {
                                returnValueOkay = false;
                            }
                        }
                    } else {
                        Merrigan0.Test.ReportFailed(argumentsString + ": " + methodExecution.Exception.Message);
                    }
                    if (!returnValueOkay) {
                        //// already tested this right?
                        Merrigan0.Test.ReportFailed(argumentsString + ", returned " + returnValue);
                    } else if (instance != null) { 
                        // The check must apply, even if the call threw an exception
                        TestCheck(instance); 
                    }
                }
            });
        }

        public static void TestClass(
            [Not("IsInterface")]
            [Not("IsAbstract")] 
            Type type,
            TestStrategy strategy = null) {
            if (type.IsGenericTypeDefinition) {
                // For generic types, test with a few sample types as the type parameter
                //// Only does single type parameters for now
                foreach (Type typeArgument in genericTypeArgumentsToTest) {
                    Type specifiedType = type.MakeGenericType(typeArgument);
                    TestSpecifiedType(specifiedType);
                }
            } else {
                TestSpecifiedType(type);
            }
        }

        public static TestExecution TestConstraint(Glom glom, Expression constraint) {
            return Test(constraint.ToString(), (bool)glom.Evaluate(constraint));
        }

        public static TestExecution TestConstructor(ConstructorInfo constructor) {
            return null;
        }

        [Wrapper("TestEquals(String, object, object, Func<object, String>)")]
        public static TestExecution TestEquals(object o, object expected, Func<object, String> failureMessageFunction = null) {
            return TestEquals("Expecting " + expected, o, expected, failureMessageFunction);
        }

        public static TestExecution TestEquals(String description, object o, object expected, Func<object, String> failureMessageFunction = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin(description)) {
                Reflection.MultitypeEquals(o, expected);
                bool passed = Reflection.MultitypeEquals(o, expected);
                if (passed) {
                    Merrigan0.Test.ReportSucceeded();
                } else {
                    Merrigan0.Test.ReportFailed(
                        (failureMessageFunction == null) ? (String)"was " + o : failureMessageFunction(o));
                }
            }
            return execution;
        }

        public static TestExecution TestEquals(object o1, object o2) {
            return Test("equals", EqualsOperator.Only.Run(o1, o2), "left did not equal " + o2);
        }

        public static TestExecution TestEquals(object o, object leftOrNml, object rightOrNml) {
            object left = Utilities.Resolve(o, leftOrNml);
            object right = Utilities.Resolve(o, rightOrNml);
            return TestEquals(left, right);
        }

        public static TestExecution TestGreaterOrEquals(object o1, object o2) {
            return Test("greater or equals", GreaterOrEqualsOperator.Only.Run(o1, o2), "left was less than " + o2);
        }

        public static TestExecution TestGreaterOrEquals(object o, object leftOrNml, object rightOrNml) {
            object left = Utilities.Resolve(o, leftOrNml);
            object right = Utilities.Resolve(o, rightOrNml);
            return TestGreaterOrEquals(left, right);
        }

        public static TestExecution TestImplements(object o, Type type) {
            return Test("implements " + type.Name, () => Reflection.Implements(o, type));
        }

        [WhatItDoes("Tests the fields, properties, and methods of an instance of a type.")]
        public static TestExecution TestInstance(object instance) {
            return TestInstance(instance, instance.GetType());
        }

        [WhatItDoes("Tests the fields, properties, and methods of an instance of a type.")]
        public static TestExecution TestInstance(
            object instance,
            [Not("IsAbstract")] 
            [Not("IsInterface")]
            [Not("IsGenericTypeDefinition")]
            Type type) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("instance")) {
                Array<FieldInfo> instanceFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (FieldInfo field in instanceFields.Where(f =>
                    !f.IsSpecialName &&
                    !Reflection.HasAttribute(f, typeof(DiagnosticOnlyAttribute)) &&
                    !Reflection.HasAttribute(f, typeof(UntestedAttribute)) &&
                    !Reflection.CompilerGenerated(f))) {
                    TestSpecifiedTypeField(field, instance);
                }

                Array<PropertyInfo> instanceProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (PropertyInfo property in instanceProperties.Where(p =>
                    !p.IsSpecialName &&
                    !Reflection.HasAttribute(p, typeof(DiagnosticOnlyAttribute)) &&
                    !Reflection.HasAttribute(p, typeof(UntestedAttribute)) &&
                    !Reflection.CompilerGenerated(p))) {
                    TestSpecifiedTypeProperty(property, instance);
                }

                Array<MethodInfo> instanceMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (MethodInfo methodInfo in instanceMethods.Where(m =>
                    !m.IsSpecialName &&
                    !Reflection.HasAttribute(m, typeof(DiagnosticOnlyAttribute)) &&
                    !Reflection.HasAttribute(m, typeof(UntestedAttribute)) &&
                    !Reflection.CompilerGenerated(m) &&
                    !m.IsPrivate)) {
                    Array<ParameterInfo> parameters = methodInfo.GetParameters();
                    if (parameters.Any(p => p.ParameterType.BaseType == typeof(MulticastDelegate))) {
                        continue;
                    }
                    TestSpecifiedTypeMethod(methodInfo, instance);
                }
            }
            return execution;
        }

        public static TestExecution TestNotNull(object o) {
            return Test("not null", o != null, "value was null");
        }

        public static TestExecution TestNotNull(object o, String nml) {
            object value = Glom.From(o).Evaluate(nml);
            return TestNotNull(value);
        }

        public static TestExecution TestNull(object o) {
            return Test("null", o == null, "value was not null");
        }

        public static TestExecution TestNull(object o, String nml) {
            object value = Glom.From(o).Evaluate(nml);
            return TestNull(value);
        }

        public static TestExecution TestRunsSuccessfully(Action function) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("runs successfully")) {
                try {
                    function();
                    execution.ReportSucceeded();
                } catch (Exception e) {
                    execution.ReportFailed(e);
                }
            }
            return execution;
        }

        public static TestExecution TestThrowsException(
            MethodInfo method,
            object instance,
            Map<String, object> argumentsByName,
            [Implements(typeof(Exception))] Type expectedExceptionType) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("throws " + expectedExceptionType.Name)) {
                MethodExecutionContext methodExecution;
                if (!method.IsStatic) {
                    methodExecution = Call(instance, method, argumentsByName);
                } else {
                    methodExecution = Call(method, argumentsByName);
                }
                if (methodExecution.Exception == null) {
                    Merrigan0.Test.ReportFailed("Did not throw any exception");
                } else {
                    Type exceptionType = methodExecution.Exception.GetType();
                    if (!expectedExceptionType.IsAssignableFrom(exceptionType)) {
                        execution.ReportFailed("Threw " + exceptionType.Name);
                    } else {
                        execution.ReportSucceeded();
                    }
                }
            }
            return execution;
        }

        public static bool Untested(MemberInfo member, Array<Attribute> attributes = null) {
            return Reflection.HasAttribute(member, typeof(UntestedAttribute));
        }

        [WhatItIs(
            "a block where all possible parameters are defined, in the order needed",
            "Optional parameters that are not supplied are set to their default value defined by the parameter.")]
        [return: True("Length == parameters.Length")]
        private static object[] ArgumentsBlock(ParameterInfo[] parameters, Map<String, object> argumentsByName) {
            object[] argumentsSoFar = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                if (parameter.IsOut) {
                    continue;
                }

                object argument;
                if (argumentsByName.TryGetValue(parameter.Name, out argument)) {
                    argumentsSoFar[i] = argument;
                } else if (parameter.IsOptional) {
                    argumentsSoFar[i] = parameter.DefaultValue;
                }
            }
            return argumentsSoFar;
        }

        private static bool CheckArguments(Type type, object instance, ParameterInfo[] parameters, Map<String, object> argumentsByName) {
            Glom argumentsGlom = Glom.From(argumentsByName); ////((Array<ParameterInfo>)parameters).Transform(p => (String)p.Name), arguments);
            for (int i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                if (parameter.IsOut) {
                    continue;
                }
                object argument = argumentsByName[parameter.Name];
                Glom argumentGlom = Glom.From(argument);
                Glom context; /// = new Context(argumentsGlom, argumentGlom);///////
                if (instance == null) {
                    context = new Context("class", ClassGlom.From(type), "arguments", argumentsGlom, null, argumentGlom);
                } else {
                    context = new Context(
                        "class", ClassGlom.From(type),
                        "instance", Glom.From(instance),
                        "arguments", argumentsGlom,
                        null, argumentGlom);
                }
                foreach (ConstraintAttribute attribute in parameter.GetCustomAttributes(typeof(ConstraintAttribute), true)) {
                    Expression constraint = attribute.Constraint;
                    if (!(bool)constraint.Evaluate(context)) {
                        return false;
                    }
                }

                // Test not null on any object returned
                Type parameterType = parameter.ParameterType.IsByRef ? parameter.ParameterType.GetElementType() : parameter.ParameterType;
                if (!parameterType.IsValueType && !Reflection.HasAttribute(parameter, typeof(MayBeNullAttribute))) {
                    if (argument == null) {
                        return false;
                    }
                }

                // Test items not null on any enumerable returned
                IEnumerable enumerableArgument = argument as IEnumerable;
                if (enumerableArgument != null && !Reflection.HasAttribute(parameter, typeof(ItemsMayBeNullAttribute))) {
                    foreach (object item in enumerableArgument) {
                        if (item == null) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        ////private static void DoDeclaredInstanceTests(object instance, Type type) {

        ////}

        //// Intention: make this the "helper" method (without test titles etc.) for both TestMethod
        //// and TestGetMethod/TestSetMethod

        ////public static void DoInstanceMethodTests(object instance, MethodInfo method) {
        ////    // Generic must be done with specific types
        ////    if (method.IsGenericMethodDefinition) {
        ////        throw new NotImplementedException();
        ////    } else {
        ////        // Go through all examples and see if they work. Examples are declared on generic methods
        ////        // so the generic parameters will be part of their specification
        ////        foreach (ExampleAttribute example in Reflection.Attributes<ExampleAttribute>(method)) {
        ////            // Creates its own instance
        ////            example.Test(method);
        ////        }

        ////        // Go through all expected throws and see if they throw
        ////        foreach (ThrowsAttribute throwsAttribute in Reflection.Attributes<ThrowsAttribute>(method)) {
        ////            throwsAttribute.Test(method);
        ////        }

        ////            if (!method.IsStatic) {
        ////                instance = Random.Value(method.DeclaringType);
        ////            }

        ////            Map<String, object> randomArgumentsByName = null;
        ////            try {
        ////                randomArgumentsByName = RandomArguments(method.DeclaringType, instance, method.GetParameters());
        ////            } catch (Exception) {
        ////                Testing.Test.ReportSkipped();
        ////            }

        ////            TestCall(method, instance, randomArgumentsByName);
        ////        }
        ////}

        private static void DoSpecifiedMethodTests(MethodInfo method, object instance = null) {
            // If it is a [Test] method, just run it and be done
            if (Reflection.HasAttribute(method, typeof(TestAttribute))) {
                if (!method.IsStatic) {
                    throw new Exception("Explicit test method must be static");
                }
                method.Invoke(null, null);
                return;
            }

            bool notSupported = Reflection.NotSupported(method);

            bool exampleWasTried = false;
            if (!notSupported) {
                // Go through all examples and see if they work. Examples are declared on generic methods
                // so the generic parameters will be part of their specification
                foreach (ExampleAttribute example in Reflection.Attributes<ExampleAttribute>(method)) {
                    // Examples create their own instance if called for
                    example.Test(method);
                    exampleWasTried = true;
                }
            }

            // Go through all expected throws and see if they throw
            foreach (ThrowsAttribute throwsAttribute in Reflection.Attributes<ThrowsAttribute>(method)) {
                // Throws create their own instance if called for
                throwsAttribute.Test(method);
            }

            // Test a random call if no examples have been tried
            if (!notSupported && !exampleWasTried) {
                // If method isn't static, we need an instance
                if (instance == null && !method.IsStatic) {
                    instance = Random.Value(method.ReflectedType);
                } else if (method.IsStatic) {
                    instance = null; //// warn
                }
                TestRandomCall(method, instance);
            }
        }

        private static void DoVariableTests(MemberInfo fieldOrProperty, Glom context, Type type, object value) {
            Array<Attribute> attributes = Reflection.Attributes(fieldOrProperty);
            Testing.Test(fieldOrProperty.Name, () => {
                foreach (ConstraintAttribute constraintAttribute in attributes.OfType<ConstraintAttribute>()) {
                    Expression constraint = constraintAttribute.Constraint;
                    using (Merrigan0.Test.Begin(constraint.ToString())) {
                        bool constraintOkay = (bool)constraint.Evaluate(context);
                        if (constraintOkay) {
                            Merrigan0.Test.ReportSucceeded();
                        } else {
                            Merrigan0.Test.ReportFailed("Value was " + value);
                        }
                    }
                }

                // Test not null on any value, unless
                //      - the parameter isn't even a reference type
                //      - it is marked MayBeNull
                if (!type.IsValueType &&
                    !attributes.Any(a => a is MayBeNullAttribute)) {
                    NotNullTest.Only.Run(value);
                }

                // Test items not null on any enumerable returned
                if (value is IEnumerable && !attributes.Any(a => a is ItemsMayBeNullAttribute)) {
                    ItemsNotNullTest.Only.Run(value);
                }
            });
        }

        [WhatItIs("a map where all output and ref parameters are represented")]
        private static Map<String, object> OutputsByName(ParameterInfo[] parameters, object[] arguments) {
            MutableMap<String, object> outputsByNameSoFar = new MutableMap<String, object>();
            for (int i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                if (parameter.IsOut || parameter.ParameterType.IsByRef) {
                    outputsByNameSoFar.Add(parameter.Name, arguments[i]);
                }
            }
            return outputsByNameSoFar.Current;
        }

        private static TestExecution ReallyTestParameterValue(
            String testName,
            ParameterInfo parameterInfo,
            MethodExecutionContext methodCall,
            object value,
            Array<ConstraintAttribute> constraintAttributes = null) 
        {
            // Gather the context with the method call above the parameter value
            Glom valueGlom = Glom.From(value);
            Glom context = new Context(methodCall, valueGlom);

            if (constraintAttributes == null) {
                object[] constraintAttributeObjects = parameterInfo.GetCustomAttributes(typeof(ConstraintAttribute), true);
                if (constraintAttributeObjects.Length == 0) {
                    return null;
                }
                constraintAttributes = Array<object>.From(constraintAttributeObjects).Cast<ConstraintAttribute>();
            } else if (constraintAttributes.Length == 0) {
                return null;
            }
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin(testName)) {
                foreach (ConstraintAttribute constraintAttribute in constraintAttributes) {
                    Expression constraint = constraintAttribute.Constraint;
                    using (Merrigan0.Test.Begin(constraint.ToString())) { ////attribute.GetType().FullName)) {
                        bool constraintOkay = (bool)constraint.Evaluate(context);
                        if (constraintOkay) {
                            Merrigan0.Test.ReportSucceeded();
                        } else {
                            Merrigan0.Test.ReportFailed("Value was " + value);
                        }
                    }
                }

                // Test not null on any object returned, unless
                //      - the parameter isn't even a reference type
                //      - it is marked MayBeNull
                //      - it is an optional parameter whose default value is null
                if (!parameterInfo.ParameterType.IsValueType &&
                    !Reflection.HasAttribute(parameterInfo, typeof(MayBeNullAttribute)) &&
                    !(parameterInfo.IsOptional && parameterInfo.DefaultValue == null)) {
                    NotNullTest.Only.Run(value);
                }

                // Test that no items are null on any enumerable returned, unless the return value is marked
                // ItemsMayBeNull
                if (value is IEnumerable && !Reflection.HasAttribute(parameterInfo, typeof(ItemsMayBeNullAttribute))) {
                    ItemsNotNullTest.Only.Run(value);
                }
            }
            return execution;
        }

        private static void TestExtenderIndexes(Type type) {
            Array<PropertyInfo> properties = Reflection.ClassProperties(type, true).Where(p => Reflection.HasAttribute(p, typeof(ExtenderIndexAttribute)));
            Array<FieldInfo> fields = Reflection.ClassFields(type, true).Where(f => Reflection.HasAttribute(f, typeof(ExtenderIndexAttribute)));
            if (properties.Length == 0 && fields.Length == 0) {
                return;
            }

            using (Merrigan0.Test.Begin("extender indexes")) {
                MutableArray<long> indexesSoFar = new MutableArray<long>();
                foreach (PropertyInfo property in properties) {
                    Test(property.Name, () => {
                        Test("is integer", Reflection.ConvertibleToLong(property.PropertyType));
                        if (Reflection.ConvertibleToLong(property.PropertyType)) {
                            long n = Reflection.Cast<long>(Reflection.ClassPropertyValue(property));
                            indexesSoFar.Append(n);
                            Testing.Test("not negative", n >= 0);
                        }
                    });
                }
                foreach (FieldInfo field in fields) {
                    Test(field.Name, () => {
                        Test("is integer", Reflection.ConvertibleToLong(field.FieldType));
                        if (Reflection.ConvertibleToLong(field.FieldType)) {
                            long n = Reflection.Cast<long>(Reflection.ClassFieldValue(field));
                            indexesSoFar.Append(n);
                            Testing.Test("not negative", n >= 0);
                        }
                    });
                }
                Array<long> indexes = indexesSoFar.Current;
                Test("indexes are sequential", () => {
                    bool passed = true;
                    for (long i = 0; i < indexes.Length; ++i) {
                        if (!indexes.Contains(i)) {
                            passed = false;
                            break;
                        }
                    }
                    return passed;
                });
            }
        }

        //public static void TestWithStrategy(CompiledTest test, TestStrategy strategy) {
        //    using (TestExecution execution = Testing.Test.Begin(strategy.Description(test))) {
        //        while (strategy.MustMoveOn(execution)) {
        //            test.Run();
        //        }
        //    }
        //}

        [WhatItDoes("Tests any explicit claims of anything within this class, at the class or instance level.")]
        private static void TestSpecifiedType(
            [Not("IsInterface")]
            [Not("IsAbstract")]
            [Not("IsGenericTypeDefinition")]
            Type type,
            TestStrategy strategy = null) {
            CustomTest test = new CustomTest("class " + Reflection.Name(type), a => {
                using (Merrigan0.Test.Begin("class")) {
                    Array<FieldInfo> classFields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (FieldInfo field in classFields.Where(f =>
                        !f.IsSpecialName && // generated property backing field names and operator overloads
                        !Reflection.HasAttribute(f, typeof(DiagnosticOnlyAttribute)) &&
                        !Reflection.HasAttribute(f, typeof(UntestedAttribute)) &&
                        !Reflection.CompilerGenerated(f))) {
                        TestSpecifiedTypeField(field);
                    }

                    TestExtenderIndexes(type);

                    Array<PropertyInfo> classProperties = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (PropertyInfo property in classProperties.Where(p =>
                        //!p.IsSpecialName &&
                        !Reflection.HasAttribute(p, typeof(DiagnosticOnlyAttribute)) &&
                        !Reflection.HasAttribute(p, typeof(UntestedAttribute)) &&
                        !Reflection.CompilerGenerated(p))) {
                        TestSpecifiedTypeProperty(property);
                    }

                    // .NET considers constructors instance methods
                    Array<ConstructorInfo> constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (ConstructorInfo constructor in constructors.Where(c =>
                        !Reflection.HasAttribute(c, typeof(DiagnosticOnlyAttribute)) &&
                        !Reflection.HasAttribute(c, typeof(UntestedAttribute)))) {
                        // Skip anything with delegate types
                        Array<ParameterInfo> parameters = constructor.GetParameters();
                        if (parameters.Any(p => p.ParameterType.BaseType == typeof(MulticastDelegate))) {
                            continue;
                        }
                        TestSpecifiedTypeConstructor(constructor);
                    }

                    Array<MethodInfo> classMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (MethodInfo method in classMethods.Where(m =>
                        !Reflection.HasAttribute(m, typeof(DiagnosticOnlyAttribute)) &&
                        !Reflection.HasAttribute(m, typeof(UntestedAttribute)) &&
                        !Reflection.CompilerGenerated(m))) {
                        Array<ParameterInfo> parameters = method.GetParameters();
                        if (parameters.Any(p => p.ParameterType.BaseType == typeof(MulticastDelegate))) {
                            continue;
                        }
                        TestSpecifiedTypeMethod(method);
                    }

                    // Run any special class-level tests
                    if (!(type.IsEnum || type.IsValueType)) {
                        // Run any explicit test methods on this class and all of its parent classes
                        Array<MethodInfo> classToTestMethods = ((Array<MethodInfo>)type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)).
                            Where(m => Reflection.HasAttribute(m, typeof(TestAttribute)));
                        foreach (MethodInfo methodInfo in classToTestMethods) {
                            Testing.Test("custom tests", () => { methodInfo.Invoke(null, null); });
                        }
                    }
                }

                // Test a random instance of this type
                object instance = Random.Value(type);
                TestInstance(instance, type);

                return !TestExecution.Ambient.ChildTests.Any(c => c.Failed);
            });
            if (strategy == null) {
                test.Run();
            } else {
                new StrategyTest(test, strategy).Run();
            }
        }

        // Tests before calls
        private static void TestParameterValue(ParameterInfo parameterInfo, MethodExecutionContext methodCall) { //// change to MethodCallContext
            object value = methodCall[parameterInfo.Name];
            ReallyTestParameterValue(parameterInfo.Name, parameterInfo, methodCall, value);
        }

        // Tests after calls
        private static TestExecution TestReturnParameterValue(ParameterInfo parameterInfo, MethodExecutionContext methodCall, Array<ConstraintAttribute> constraintAttributes = null) {
            object value = methodCall.ReturnValue;
            return ReallyTestParameterValue("return value", parameterInfo, methodCall, value, constraintAttributes);
        }

        [WhatItDoes("Tests a constructor.")]
        private static TestExecution TestSpecifiedTypeConstructor([False("DeclaringType.IsGenericTypeDefinition")] ConstructorInfo constructor) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("constructor " + Reflection.Description(constructor))) {
                // Create the arguments. Sometimes that isn't possible so skip the test in that case
                Map<String, object> argumentsByName = null;
                try {
                    argumentsByName = RandomArguments(constructor.ReflectedType, null, constructor.GetParameters());
                } catch (Exception ex) {
                    Merrigan0.Test.ReportSkipped("Couldn't create random arguments: " + ex.Message);
                    return execution;
                }

                ParameterInfo[] parameters = constructor.GetParameters();
                object[] arguments = ArgumentsBlock(parameters, argumentsByName);
                object instance = constructor.Invoke(arguments);

                //bool ranWithoutException = (methodExecution.Exception == null);
                //////object returnValue = null;
                //////bool returnValueOkay = true;
                //Test("succeeded", ranWithoutException);
                //String argumentsString = "(" + String.Join(argumentsByName.Transform(kvp => kvp.Value), ", ").Truncated(50) + ")";
                //if (ranWithoutException) {
                //    ////if (method.ReturnType != null) {
                //    ////    returnValue = methodExecution.ReturnValue;
                //    ////    TestExecution returnValueExecution = TestReturnParameterValue(method.ReturnParameter, methodExecution);
                //    ////    if (returnValueExecution != null && returnValueExecution.Failed) {
                //    ////        returnValueOkay = false;
                //    ////    }
                //    ////}
                //} else {
                //    Testing.Test.ReportFailed(argumentsString + ": " + methodExecution.Exception.Message);
                //}
                ////if (!returnValueOkay) {
                ////    //// already tested this right?
                ////    Testing.Test.ReportFailed(argumentsString + ", returned " + returnValue);
                ////} else if (instance != null) {
                ////    // The check must apply, even if the call threw an exception
                ////    TestCheck(instance);
                ////    }
                ////}
                TestCheck(instance);
            }
            return execution;
        }

        [WhatItDoes("Tests a field on a given instance, or a class field.")]
        private static TestExecution TestSpecifiedTypeField([False("DeclaringType.IsGenericTypeDefinition")] FieldInfo field, object instance = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("field " + field.Name)) {
                object value;
                if (field.IsStatic) {
                    value = field.GetValue(null);
                } else {
                    value = field.GetValue(instance);
                }

                Glom valueGlom = Glom.From(value);
                Glom context;
                if (field.IsStatic) {
                    context = new Context("class", Glom.From(field.ReflectedType), null, valueGlom);
                } else {
                    context = new Context("class", Glom.From(field.ReflectedType), "instance", Glom.From(instance), null, valueGlom);
                }
                Array<ConstraintAttribute> constraintAttributes = Reflection.Attributes<ConstraintAttribute>(field);
                foreach (ConstraintAttribute constraintAttribute in constraintAttributes) {
                    Expression constraint = constraintAttribute.Constraint;
                    using (Merrigan0.Test.Begin(constraint.ToString())) { ////attribute.GetType().FullName)) {
                        bool constraintOkay = (bool)constraint.Evaluate(context);
                        if (constraintOkay) {
                            Merrigan0.Test.ReportSucceeded();
                        } else {
                            Merrigan0.Test.ReportFailed("Value was " + value);
                        }
                    }
                }

                // Test not null on any value, unless
                //      - the parameter isn't even a reference type
                //      - it is marked MayBeNull
                if (!field.FieldType.IsValueType &&
                    !Reflection.HasAttribute(field, typeof(MayBeNullAttribute))) {
                    NotNullTest.Only.Run(value);
                }

                // Test items not null on any enumerable returned
                if (value is IEnumerable && !Reflection.HasAttribute(field, typeof(ItemsMayBeNullAttribute))) {
                    ItemsNotNullTest.Only.Run(value);
                }
            }

            return execution;
        }

        private static TestExecution TestSpecifiedTypeGetProperty(PropertyInfo property, MethodInfo getMethod, object instance = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("get " + property.Name)) {
                if (Reflection.NotSupported(getMethod)) {
                    TestThrowsException(getMethod, instance, null, typeof(NotSupportedAttribute));
                } else {
                    MethodExecutionContext methodCallContext = Call(instance, getMethod);
                    bool ranWithoutException = methodCallContext.Exception == null;
                    object returnValue = null;
                    bool returnValueOkay = true;
                    Test("succeeded", ranWithoutException);
                    if (ranWithoutException) {
                        returnValue = methodCallContext.ReturnValue;
                        Array<ConstraintAttribute> constraintAttributes = Reflection.Attributes<ConstraintAttribute>(property);
                        TestExecution returnValueExecution = TestReturnParameterValue(getMethod.ReturnParameter, methodCallContext, constraintAttributes);
                        if (returnValueExecution != null && returnValueExecution.Failed) {
                            returnValueOkay = false;
                        }
                    }
                    if (!ranWithoutException || !returnValueOkay) {
                        Merrigan0.Test.ReportFailed("returned " + returnValue);
                    }
                }
            }
            return execution;
        }

        /*
         * TestMethod whole
         *      if generic, call TestGenericMethod
         *      TestMethod with random instance & random params
         *      examples
         *      throws
         */
        [WhatItIs(
            "Tests a method of a class that has any type parameters specified.",
            "If the method itself is generic, tests some representative specifications.")]
        private static TestExecution TestSpecifiedTypeMethod([Not("DeclaringType.IsGenericTypeDefinition")] MethodInfo method, object instance = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin(Reflection.Description(method))) {
                if (method.IsGenericMethodDefinition) {
                    // The method itself is generic
                    foreach (Type typeToTest in genericTypeArgumentsToTest) {
                        int nArguments = method.GetGenericArguments().Length;
                        Array<Type> typeArguments = Array<Type>.From(typeToTest, nArguments);
                        MethodInfo specifiedTypeMethod = method.MakeGenericMethod(typeArguments);
                        DoSpecifiedMethodTests(specifiedTypeMethod, instance);
                    }
                } else {
                    DoSpecifiedMethodTests(method, instance);
                }
            }
            return execution;
        }

        /*
         * Doesn't matter if the property's get and/or set accessors are public. It just tests the
         * whole property.
         * TestProperty whole
         *      if generic, call TestGenericProperty
         *      TestProperty with random instance & random params
         *      examples
         *      throws
         */
        private static TestExecution TestSpecifiedTypeProperty([False("DeclaringType.IsGenericTypeDefinition")] PropertyInfo property, object instance = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("property " + property.Name)) {
                // Test any get accessor
                MethodInfo getMethod = property.GetGetMethod();
                if (getMethod == null) {
                    getMethod = property.GetGetMethod(true);
                }
                if (getMethod != null) {
                    TestSpecifiedTypeGetProperty(property, getMethod, instance);
                }

                // Test any set accessor
                MethodInfo setMethod = property.GetSetMethod();
                if (setMethod == null) {
                    setMethod = property.GetSetMethod(true);
                }
                if (setMethod != null &&
                    !typeof(MulticastDelegate).IsAssignableFrom(property.PropertyType) &&
                    !setMethod.IsPrivate) {
                    TestSpecifiedTypeSetProperty(property.Name, setMethod, instance);
                }
            }

            return execution;
        }

        private static TestExecution TestSpecifiedTypeSetProperty(String propertyName, MethodInfo setMethod, object instance = null) {
            TestExecution execution;
            using (execution = Merrigan0.Test.Begin("set " + propertyName)) {
                DoSpecifiedMethodTests(setMethod, instance);
                if (instance != null) { TestCheck(instance); }
            }
            return execution;
        }

        private class BaseExampleClass {
        }

        private class ExampleClass : BaseExampleClass {
            [DiagnosticOnly]
            public void Simplest() { }

            public bool LessSimple(int n) { 
                return false;
            }

            public String Complicated(Array<int> numbers, bool flag, [MayBeNull] String description) {
                return null;
            }
        }
    }
}
