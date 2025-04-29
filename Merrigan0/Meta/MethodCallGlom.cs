using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Merrigan0.GlomsInternal; // for ObjectGlom //// maybe not needed

namespace Merrigan0.MetaInternal {
    /*
     * MethodContext
     *      parent InstanceContext OR ClassContext
     *      
     * MethodCallGlom - just the call part
     *      arguments ArrayGlom
     *      method ObjectGlom(MethodInfo)
     * MethodResultContext
     *      outputs glom
     *      exception POCO
     *      
     * Context methodExecution = new Context(
     *      new ClassGlom(class),
     *      new ObjectGlom(instance),
     *      new MethodCallGlom(MethodInfo method, params object[] inputNamesAndValues),
     *      new MethodExecutionGlom(params object[] outputNamesAndValues));
     *      
     * Context methodExecution = new Context(
     *      new ClassGlom(class),
     *      new MethodCallGlom(MethodInfo method, params object[] inputNamesAndValues),
     *      new MethodExecutionGlom(params object[] outputNamesAndValues));
     */

    internal class MethodExecutionContext : Context {
        private CompiledGlomProperty<ArrayGlom> argumentsProperty = new CompiledGlomProperty<ArrayGlom>("arguments");
        private CompiledGlomProperty<MethodCallGlom> callProperty = new CompiledGlomProperty<MethodCallGlom>("call");
        private CompiledGlomProperty<ClassGlom> classProperty = new CompiledGlomProperty<ClassGlom>("class");
        private CompiledGlomProperty<Exception> exceptionProperty = new CompiledGlomProperty<Exception>("exception");
        private CompiledGlomProperty<MethodExecutionGlom> executionProperty = new CompiledGlomProperty<MethodExecutionGlom>("execution");
        private CompiledGlomProperty<ObjectGlom> instanceProperty = new CompiledGlomProperty<ObjectGlom>("instance");
        private CompiledGlomProperty<MethodInfo> methodProperty = new CompiledGlomProperty<MethodInfo>("method");
        private CompiledGlomProperty<Glom> outputsProperty = new CompiledGlomProperty<Glom>("outputs");
        private CompiledGlomProperty<object> returnValueProperty = new CompiledGlomProperty<object>("returnValue");

        public ArrayGlom Arguments { get { return argumentsProperty.Value(this); } }
        public MethodCallGlom Call { get { return callProperty.Value(this); } }
        public ClassGlom Class { get { return classProperty.Value(this); } }
        public Exception Exception { get { return exceptionProperty.Value(this); } }
        public MethodExecutionGlom Execution { get { return executionProperty.Value(this); } }
        public ObjectGlom Instance { get { return instanceProperty.Value(this); } }
        public MethodInfo Method { get { return methodProperty.Value(this); } }
        public Glom Outputs { get { return outputsProperty.Value(this); } }
        public object ReturnValue { get { return returnValueProperty.Value(this); } }

        protected MethodExecutionContext(
            object instance,
            MethodInfo method,
            Map<String, object> argumentsByName,
            Map<String, object> outputsByName,
            object returnValue)
            : base(
                "class", ClassGlom.From(method.ReflectedType),
                "instance", new ObjectGlom(instance),
                "call", new MethodCallGlom(method, new Glom(argumentsByName)),
                "execution", new MethodExecutionGlom(new Glom(outputsByName), returnValue))
        {
        }

        protected MethodExecutionContext(
            object instance,
            MethodInfo method,
            Map<String, object> argumentsByName,
            Exception exception)
            : base(
                "class", ClassGlom.From(method.ReflectedType),
                "instance", new ObjectGlom(instance),
                "call", new MethodCallGlom(method, new Glom(argumentsByName)),
                "exception", new MethodExecutionGlom(exception)) 
        {
        }
    }

    internal class InstanceMethodExecutionContext : MethodExecutionContext {
        public InstanceMethodExecutionContext(
            object instance,
            MethodInfo method,
            Map<String, object> argumentsByName,
            Map<String, object> outputsByName,
            object returnValue)
            : base(
                instance,
                method,
                argumentsByName,
                outputsByName,
                returnValue)
        {
        }

        public InstanceMethodExecutionContext(
            object instance,
            MethodInfo method,
            Map<String, object> argumentsByName,
            Exception exception)
            : base(
                instance,
                method,
                argumentsByName,
                exception)
        {
        }
    }

    internal class ClassMethodExecutionContext : MethodExecutionContext {
        public ClassMethodExecutionContext(
            MethodInfo method,
            Map<String, object> argumentsByName,
            Map<String, object> outputsByName,
            object returnValue)
            : base(
                null,
                method,
                argumentsByName,
                outputsByName,
                returnValue)
        {
        }

        public ClassMethodExecutionContext(
            MethodInfo method,
            Map<String, object> argumentsByName,
            Exception exception)
            : base(
                null,
                method,
                argumentsByName,
                exception)
        {
        }
    }

    [WhatItIs("a completed method call")]
    [Property("outputs", typeof(Glom))]
    [Property("exception", typeof(Exception))]
    [Property("returnValue", typeof(object))]
    [Untested]
    internal class MethodExecutionGlom : Glom {
        public MethodExecutionGlom(
            Glom outputs,
            object returnValue)
            : base(new Glom(outputs, "returnValue", returnValue))
        {
        }

        public MethodExecutionGlom(Exception exception)
            : base(new Glom("exception", exception)) {
        }
    }

    [WhatItIs("a representation of a completed method call. Exposes local call parameters first (after running), object properties (if any) next, and " +
        "class properties next")]
    [Property("method", typeof(MethodInfo))]
    [Property("arguments", typeof(Glom))]
    [Untested]
    internal class MethodCallGlom : Glom {
        public MethodCallGlom(
            MethodInfo method,
            Glom arguments) :
            base(arguments, "method", method)
        {
        }

        //[Note("If object is specified, then the container is a representation of the object. Otherwise it is a representation "
        //    + "of the class.")]
        //public MethodCallGlom(
        //    object o,
        //    MethodInfo method,
        //    Array<object> parameterValues,
        //    object returnValue,
        //    Exception exception)
        //{
        //    this.exception = exception;
        //    this.method = method;
        //    this.o = o;
        //    this.returnValue = returnValue;
        //    MutableMap<String, object> parameterValuesByNameSoFar = new MutableMap<String, object>();
        //    long iParameter = 0;
        //    foreach (ParameterInfo parameter in method.GetParameters()) {
        //        parameterValuesByNameSoFar.Add(parameter.Name, parameterValues[iParameter]);
        //        ++iParameter;
        //    }
        //    this.inputValues = parameterValuesByNameSoFar.Current;
        //}

        ////protected override Glom GetContainer() {
        ////    return o == null ? (Glom)ClassGlom.From(method.DeclaringType) : (Glom)new ObjectGlom(o);
        ////}

        //protected override bool TryGetAtThisLevel(String name, out object o) {
        //    // Look in parameters for this name
        //    object value;
        //    if (inputValues.TryGetValue(name, out value)) {
        //        o = value;
        //        return true;
        //    }
        //    Array<ParameterInfo> parameters = method.GetParameters();
        //    Array<long> indexes = parameters.FindAll(p => p.Name == name);
        //    if (indexes.Length > 0) {
        //        o = inputValues[indexes[0]];
        //        return true;
        //    }

        //    if (name == "method") {
        //        o = method;
        //        return true;
        //    }

        //    if (name == "exception") {
        //        o = exception;
        //        return true;
        //    }

        //    if (name == "returnValue") {
        //        o = returnValue;
        //        return true;
        //    }

        //    o = null;
        //    return false;
        //}
    }
}
