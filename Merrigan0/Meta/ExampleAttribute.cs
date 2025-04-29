using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0 {
    //// Enhance to have constraints rather than exact values
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = true)]
    [Untested]
    public class ExampleAttribute : CommitmentAttribute {
        private Array<object> values;

        public string Description { get; set; }

        public ExampleAttribute(
            [WhatItIs("A list of (1) instance initialization parameters, if for an instance function, (2) any generic type " +
                "parameters, (3) all input arguments and expected outputs, in the order expected in the method's parameter " +
                "list. Input and expected outputs are laid out in order, with ref values specified as two sequential values. " +
                "Unspecified optional arguments should be omitted.")]
            /*[Pattern("<generic-type:Type>*<input:Object>*<return-value:Object>?")] */params object[] values) {
                this.values = values;
        }

        //// This could be in another ExampleTest class
        public override void Test(object o, MethodInfo method) {
            object oToTest = o;
            bool instanceSpecifiedInArgument = !method.IsStatic && (oToTest == null);

            // Gather the locations of values in the arguments array, devoted to each specification:
            //      instance, type arguments, method parameters, out parameter expected values or checks
            int nArgumentsSpecified = (int)values.Length;
            int iGenericTypeArguments = instanceSpecifiedInArgument ? 1 : 0;
            int nGenericTypeArguments = method.GetGenericArguments().Length;
            int iParameterValues = iGenericTypeArguments + nGenericTypeArguments;

            // Arrange input arguments
            ParameterInfo[] parameters = method.GetParameters();
            int nInputParameters = 0; // includes optional parameters and not ref parameters
            int nOptionalParameters = 0;
            int nOutParameters = 0;
            int nRefParameters = 0;
            int nParameters = parameters.Length;
            int nOptionalArgumentsNotSpecified = 0;

            // ParameterInfo
            //    IsIn - never set
            //    IsOut - set on out only (not ref)
            //    ParameterType.IsByRef - set on ref and out
            foreach (ParameterInfo parameter in parameters) {
                if (Reflection.In(parameter)) {
                    ++nInputParameters;
                    if (parameter.IsOptional) {
                        ++nOptionalParameters;
                    }
                } else if (Reflection.Out(parameter)) {
                    ++nOutParameters;
                } else if (Reflection.Ref(parameter)) {
                    ++nRefParameters;
                } else {
                    throw new Exception("Unrecognized parameter type");
                }
            }

            // Figure out if any optional arguments were left unspecified
            bool returnValueExpected = !Reflection.ReturnsVoid(method);
            int nExpectedValuesIncludingOptionals = nGenericTypeArguments + nInputParameters + nOutParameters + nRefParameters * 2;
            if (nArgumentsSpecified < nExpectedValuesIncludingOptionals) {
                nOptionalArgumentsNotSpecified = nExpectedValuesIncludingOptionals - nArgumentsSpecified;
                if (nOptionalArgumentsNotSpecified > nOptionalParameters) {
                    throw new Exception("Too few arguments");
                }
            }

            // All values in and out
            int nParameterValuesSupplied = nExpectedValuesIncludingOptionals - nOptionalArgumentsNotSpecified;
            
            // If it's an instance method, but no object was supplied, create one from the first attribute argument
            if (instanceSpecifiedInArgument) {
                // If the type being tested is a string or String, use that
                object firstArgument = values[0];
                string firstArgumentString = firstArgument as string;
                ////Type instanceType = arguments[0].GetType();
                ////if (firstArgumentString != null && (!Reflection.Implements(instanceType, typeof(String)))) {
                ////    oToTest = Nml.Nml.Object(firstArgumentString, instanceType);
                ////    continue;
                ////}
                if (!Conversion.TryConvert(firstArgument, method.ReflectedType, out oToTest)) {
                    // Otherwise use NML
                    string firstArgumentNml = firstArgument as string;
                    if (firstArgumentNml != null) {
                        oToTest = Nml.Object(firstArgumentNml, method.ReflectedType);
                    } else {
                        // Otherwise take the object as-is
                        oToTest = firstArgument;
                    }
                }
            }

            // If the method is generic, apply those parameters to get a specific method
            ICollection<Type> coll = new Type[4];
            if (method.ContainsGenericParameters) {
                method = method.MakeGenericMethod(values.Subarray(iGenericTypeArguments, nGenericTypeArguments).Cast<Type>().Block);
            }
            
            // Set all inputs and refs to the value specified in the arguments, cast to the right .NET type for that parameter
            Array<object> parameterArguments = values.Subarray(iParameterValues, nParameterValuesSupplied);

            // The array which will be sent to Invoke(). In and ref arguments will be set, and out arguments set to null
            object[] invokeArguments = new object[parameters.Length];

            // The array of expected values (not the return value) from out and ref parameters. Not
            // all values will be set, since not all parameters are out or ref
            object[] expectedOutAndRefValues = new object[parameters.Length];

            // The index of the next argument to be set. This will ascend higher than i if there are ref parameters
            long iNextValue = iParameterValues;
            for (long i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                Type parameterType = parameter.ParameterType;

                // Out and ref parameters need an expected value
                bool outOrRef = Reflection.Out(parameter) || Reflection.Ref(parameter);
                if (outOrRef) {
                    // Ref and out types are "Int32&" and the like, but let's double check
                    if (parameterType.IsByRef) {
                        parameterType = parameterType.GetElementType();
                    } else {
                        //// warn
                    }
                }

                if (Reflection.Out(parameter)) {
                    // Out parameters are set to null
                    invokeArguments[i] = null;
                } else if (iNextValue >= nParameterValuesSupplied) {
                    //// assert
                    if (!parameter.IsOptional) {
                        throw new Exception("Filling in value for non-optional parameter");
                    }

                    // Fill in optional value for any skipped optionals. Needs no casting
                    invokeArguments[i] = parameter.DefaultValue;
                } else {
                    object input = parameterArguments[iNextValue];

                    // Nulls have no type anyway so just copy them
                    if (input == null) {
                        invokeArguments[i] = null;
                    } else {
                        // If the types are compatible, copy verbatim
                        Type inputType = input.GetType();

                        if (parameterType.IsAssignableFrom(inputType)) {
                            invokeArguments[i] = input;
                        } else {
                            // If the object is a string but the value isn't expected to be String, translate it from NML
                            string inputString = input as string;
                            if (inputString != null && (!Reflection.Implements(parameterType, typeof(String)))) {
                                invokeArguments[i] = Nml.Object(inputString, parameterType);
                            } else {
                                // If the object can be converted, do that
                                if (!Conversion.TryConvert(input, inputType, parameterType, out invokeArguments[i])) {
                                    throw new Exception();
                                }
                            }
                        }
                    }

                    ++iNextValue;
                }

                // Out and ref parameters need an expected value
                if (outOrRef) {
                    expectedOutAndRefValues[i] = Conversion.Convert(values[iNextValue], parameterType);
                    ++iNextValue;
                } else {
                    expectedOutAndRefValues[i] = null;
                }
            }

            //// Cast any expected outputs to the types
            //object[] castExpectedOutputs = new object[parameters.Length];
            //long iNextOutputArgument = nInputParameters; //// might not be in a row!
            //MutableArray<long> outParameterIndexes = new MutableArray<long>();
            //for (long i = 0; i < parameters.Length; ++i) {
            //    ParameterInfo parameter = parameters[i];
            //    if (!parameter.IsOut) {
            //        if (iNextOutputArgument >= argumentsRemaining.Length) {
            //            throw new Exception("Example missing at least one expected output value.");
            //        }
            //        object expectedOutput = argumentsRemaining[iNextOutputArgument];
            //        castExpectedOutputs[i] = Conversion.Convert(expectedOutput, parameter.ParameterType.GetElementType());
            //        outParameterIndexes.Append(i);
            //        ++iNextOutputArgument;
            //    }
            //}

            // Prepare the expected return value
            object castExpectedReturnValue = null;
            if (returnValueExpected) {
                int iExpectedReturnValue = iParameterValues + nParameterValuesSupplied;
                if (iExpectedReturnValue >= values.Length) {
                    throw new Exception("Example missing at least one expected output value.");
                }
                castExpectedReturnValue = Conversion.Convert(values[iExpectedReturnValue], method.ReturnType);
            }

            // Perform the test
            String testName = "example ";
            if (Description != null) {
                testName += Description;
            } else {
                if (parameters.Length > 0) {
                    testName += '(';
                    testName += String.Join(values, ", ");
                    testName += ')';
                }
            }
            Testing.Test(testName, () => {
                TestExecution execution;
                object returnValue = null;
                ////MethodExecutionContext methodExecutionContext;
                execution = Testing.TestRunsSuccessfully(() => {
                    ///// check out/ref parameters too
                    ////methodExecutionContext = Testing.Call(oToTest, methodInfo, castArgumentsByName);
                    ////returnValue = methodExecutionContext.ReturnValue;
                    returnValue = method.Invoke(oToTest, invokeArguments);
                });
                if (execution.Succeeded) {
                    //foreach (long iOutput in outParameterIndexes.Current) {
                    //    Testing.TestEquals(parameters[iOutput].Name, invokeArguments[iOutput], castExpectedOutputs[iOutput], v => "was " + v);
                    //}
                    
                    for (int i = 0; i < parameters.Length; ++i) {
                        ParameterInfo parameter = parameters[i];
                        if (Reflection.Out(parameter) || Reflection.Ref(parameter)) {
                            Testing.TestEquals(parameters[i].Name, invokeArguments[i], expectedOutAndRefValues[i], v => "was " + v);
                        }
                    }

                    if (returnValueExpected) {
                        ////if (castExpectedReturnValue == null || castExpectedReturnValue is String || castExpectedReturnValue is string) {
                            // Test the expected-type expected object
                            Testing.TestEquals("return value", returnValue, castExpectedReturnValue, rv => "returned " + rv);
                        ////} else {
                        ////    // Test via an expression
                        ////    Expression rightExpression;
                        ////    if (rightNml != null) {
                        ////        rightExpression = Nml.Nml.Expression(rightNml);
                        ////    } else {
                        ////        rightExpression = new ConstantExpression(oRight);
                        ////    }
                        ////    Expression equalsExpression = new EqualsExpression(new ConstantExpression(castExpectedReturnValue));
                        ////    Testing.Test("return value", 
                    }
                }
            });
        }

        protected static object CreateInstance(string nml, Type type) {
            return Nml.Object(nml, type);
        }

        [DiagnosticOnly]
        [Test]
        private static void TestClass() {
            ExampleAttribute example = new ExampleAttribute(1, 2, 3, -5);
            MethodInfo method = typeof(ExampleAttribute).GetMethod("DoSomething", BindingFlags.NonPublic | BindingFlags.Static);
            example.Test(method);

            example = new ExampleAttribute(1, 2, 3, 20, 4);
            method = typeof(ExampleAttribute).GetMethod("DoSomething", BindingFlags.NonPublic | BindingFlags.Static);
            example.Test(method);
        }

        [DiagnosticOnly]
        private static void DoSomething(int nIn, out int nOut, ref int nRef, int nOptional = -1) {
            nOut = nIn + 1;
            nRef += 2;
            nRef *= nOptional;
        }
    }
}
