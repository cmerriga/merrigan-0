using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.ExecutionsInternal;

namespace Merrigan0.ScrapInternal {
    ///// <summary>
    ///// Equivalent to a JavaScript object. Could be an object, a loadedItems, or an loadedItems.
    ///// </summary>
    [Untested]
    public abstract class Scope {
        private Scope parent;

        // Returns null if not found. null means no such named item exists.
        public Scope this[String name] {
            get {
                Scope scope;
                if (TryGetAtThisLevel(name, out scope)) {
                    return scope;
                }
                if (parent == null) {
                    return null;
                }
                return parent[name];
            }
        }

        public virtual bool Number { get { return false; } }
        public virtual bool Ordered { get { return false; } }
        public virtual Type Type { get { return null; } }

        protected Scope(Scope parent) {
            this.parent = parent;
        }

        public virtual object As(Type type) {
            return null;
        }

        public T As<T>() {
            return (T)As(typeof(T));
        }

        public Scope Evaluate(String code) {
            String[] firstAndRest = String.DivideAtFirst(code, '.');
            Scope child = this[firstAndRest[0]];
            if (firstAndRest.Length == 1) {
                return child;
            }
            return child.Evaluate(firstAndRest[1]);
        }

        public T Evaluate<T>(String code) {
            Scope result = Evaluate(code);
            ObjectScope objectResult = result as ObjectScope;
            if (objectResult == null) {
                throw new Exception();
            }
            return objectResult.To<T>();
        }

        // Returns an amorphous value that can be used by other epxressions
        protected abstract bool TryGetAtThisLevel(String name, out Scope scope);
    }

    [Untested]
    public class NumberScope : Scope {
        private object o;

        public override bool Number { get { return true; } }
        public override bool Ordered { get { return true; } }

        public NumberScope(object o) : base(ClassScope.From(o.GetType())) { this.o = o; }

        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            scope = new ObjectScope(Reflection.Get(o, name));
            return true;
        }

        public T To<T>() {
            return (T)o;
        }
    }

    [Untested]
    public class GlobalScope : Scope {
        public static readonly GlobalScope Only = new GlobalScope();

        protected GlobalScope() : base(null) { }

        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            if (Reflection.NamespaceNames().Contains(name)) {
                scope = new NamespaceScope(name);
                return true;
            }
            scope = null;
            return false;
        }
    }

    [Untested]
    public class NamespaceScope : Scope {
        private String namespaceName;

        public NamespaceScope(String namespaceName) : base(GetParentScope(namespaceName)) {
            this.namespaceName = namespaceName;
        }

        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            scope = new NamespaceScope(namespaceName + "." + name);
            return true;
        }


        private static Scope GetParentScope(String namespaceName) {
            String parentScopeNamespaceName = namespaceName.BeforeLast('.');
            if (parentScopeNamespaceName.Length == 0) {
                return GlobalScope.Only;
            }
            return new NamespaceScope(parentScopeNamespaceName);
        }
    }

    [Untested]
    public class ObjectScope : Scope {
        private object o;

        public ObjectScope(object o) : base(ClassScope.From(o.GetType())) { this.o = o; }

        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            scope = new ObjectScope(Reflection.Get(o, name));
            return true;
        }

        public T To<T>() {
            return (T)o;
        }
    }

    [Untested]
    public class MethodCallScope : Scope {
        private Exception exception;
        private object o;
        private MethodInfo method;
        private Map<String, object> parameterValuesByName;
        private object returnValue;
        ////private Type type;

        public MethodCallScope(
            [MayBeNull] object o, 
            MethodInfo method, 
            /*[Equal("Length", "method.GetParameters().Length")] */Array<object> parameterValues, 
            object returnValue,
            Exception exception)
            : base(o == null ? (Scope)ClassScope.From(method.ReflectedType) : (Scope)new ObjectScope(o)) {
            this.exception = exception;
            this.method = method;
            this.o = o;
            this.returnValue = returnValue;
            MutableMap<String, object> parameterValuesByNameSoFar = new MutableMap<String, object>();
            long iParameter = 0;
            foreach (ParameterInfo parameter in method.GetParameters()) {
                parameterValuesByNameSoFar.Add(parameter.Name, parameterValues[iParameter]);
                ++iParameter;
            }
            this.parameterValuesByName = parameterValuesByNameSoFar.Current;
        }

        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            // Look in parameters for this name
            object value;
            if (parameterValuesByName.TryGetValue(name, out value)) {
                scope = new ObjectScope(value);
                return true;
            }
            Array<ParameterInfo> parameters = method.GetParameters();
            Array<long> indexes = parameters.FindAll(p => p.Name == name);
            if (indexes.Length > 0) {
                scope = new ObjectScope(parameterValuesByName[indexes[0]]);
                return true;
            }

            // Is the name "returnValue"?
            if (name == "exception") {
                scope = new ObjectScope(exception);
                return true;
            }

            // Is the name "returnValue"?
            if (name == "returnValue") {
                scope = new ObjectScope(returnValue);
                return true;
            }

            scope = null;
            return false;
        }
    }

    [Untested]
    public class ClassScope : Scope {
        private static MutableMap<Type, ClassScope> classScopesByTypeSoFar = new MutableMap<Type, ClassScope>();

        private Type type;

        // Returns the interned scope for a type.
        public static ClassScope From(Type type) {
            ClassScope scope;
            if (!classScopesByTypeSoFar.Current.TryGetValue(type, out scope)) {
                scope = new ClassScope(type);
                classScopesByTypeSoFar.Add(type, scope);
            }
            return scope;
        }

        protected ClassScope(Type type) : base(new NamespaceScope(type.Namespace)) {
            this.type = type;
        }

        // Gets the value from the class/static level
        protected override bool TryGetAtThisLevel(String name, out Scope scope) {
            scope = new ObjectScope(type.Value(name));
            return true;
        }
    }
}
