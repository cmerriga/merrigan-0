using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Merrigan0.FactsInternal;
using Merrigan0.GlomsInternal;
using Merrigan0.MetaInternal;

namespace Merrigan0 {
    public enum GlomType {
        Object,
        Array,
        Number,
        String,
        Boolean
    }

    // An amorphous who-knows-what that could house a regular .NET object, a list of other Gloms, or a mish-mash
    // of stuff. Short for "agglomeration". All you know are three things: 
    //      (1) you can evaluate an expression using a Glom as a context, including navigating to another Glom via an identifier
    //      (2) you can find out if some Fact is true in its context
    //      (3) you can find out if some Fact are false in its context
    //      (4) you can list some related Facts that are known as a result of it, including Facts about the Glom itself
    //          like whether it's a number or a string or a list. Not all Facts directly, but those might lead to others.
    [Untested]
    public class Glom : Fact {
        public static readonly Glom Global = new GlobalGlom();
        public static readonly Glom GlobalCall = new GlobalCallGlom();
        public static readonly Glom Types = new TypesGlom();
        public static readonly String ValueName = "value";

        [MayBeNull]
        protected Glom prototype;

        private Dictionary<ConditionalFact, bool?> conditionalFactsToTruth; //// make a Map
        private IDictionary<String, object> valuesByName; //// make a Map

        // 
        // Returns null if not found. null means no such named item exists.
        public object this[String name] {
            get {
                object o;
                if (TryGet(name, out o)) {
                    return o;
                }
                return null;
            }
        }

        public IDictionary<String, object> ValuesByName { get { return valuesByName; } }

        // A POCO type that this glom can be perfectly translated to, efficiently
        ////public Type Type { get; protected set; }

        [WhatItIs("Which basic type this is: Object, Number, etc.")]
        public virtual GlomType Type { get { return GlomType.Object; } }

        public Glom(params object[] namesAndValues) : this(null, namesAndValues) { }

        public Glom(Glom prototype, params object[] namesAndValues) : this(prototype, Utilities.ToPairs<string, object>(namesAndValues).Transform<Fact>(p => new NamedValueFact(p.Item1, p.Item2))) { }

        public Glom(Map<String, object> valuesByName) : this(null, valuesByName) { }

        public Glom(Glom prototype, Map<String, object> valuesByName) {
            this.prototype = prototype;
            this.valuesByName = valuesByName.ToDictionary();
        }

        public Glom(Array<Fact> directImplications) : this(null, directImplications) { }

        public Glom(Glom prototype, Array<Fact> directImplications) : base(directImplications) {
            this.prototype = prototype;
            Dictionary<ConditionalFact, bool?> conditionalFactsToTruthSoFar = new Dictionary<ConditionalFact,bool?>();
            Dictionary<String, object> valuesByNameSoFar = new Dictionary<String, object>();
            foreach (Fact directImplication in directImplications) {
                ConditionalFact conditionalFact = directImplication as ConditionalFact;
                if (conditionalFact != null) {
                    conditionalFactsToTruthSoFar.Add(conditionalFact, null);
                    continue;
                }
                NamedValueFact namedValue = directImplication as NamedValueFact;
                if (namedValue != null) {
                    valuesByNameSoFar.Add(namedValue.Name, namedValue.Value);
                    continue;
                }
            }
            conditionalFactsToTruth = conditionalFactsToTruthSoFar;
            valuesByName = valuesByNameSoFar;
        }

        // Return something we can resolve implies/disimplies queries from.
        // Object o could be a fact already, or just a POCO.
        // An integer type should be returned as an exclusive value that disimplies other integers.
        // A complex struct should be returned as a glom.
        public static Fact AsFact(object o) {
            Fact thisFact = o as Fact;
            if (thisFact != null) {
                return thisFact;
            }
            Type type = o.GetType();
            if (!type.IsValueType) {
                return new ObjectGlom(o);
            }
            if (type == typeof(DateTime)) {
                return new TimeGlom((DateTime)o);
            }
            if (type == typeof(DateTimeOffset)) {
                return new TimeGlom((DateTimeOffset)o);
            }
            return null; //// must return decent gloms for value types
        }

        public static Glom From(object o) {
            // o is not a Map<String, object> because that's handled in a separate call
            //// maybe, really?

            // null
            if (o == null) {
                return Glom.Global;
            }

            // Already a glom
            Glom g = o as Glom;
            if (g != null) {
                return g;
            }

            // Contains ordered items
            IEnumerable enumerable = o as IEnumerable;
            if (enumerable != null) {
                return Glom.From(enumerable);
            }

            // A String or string
            String s = o as String;
            if (s != null) {
                return Glom.From(s);
            }
            string systemString = o as string;
            if (systemString != null) {
                return Glom.From(systemString);
            }

            // Numeric
            Type type = o.GetType();
            if (Reflection.Numeric(type)) {
                return new NumberGlom(o);
            }

            // A type
            type = o as Type;
            if (type != null) {
                return TypeGlom.From(type);
            }
            return new ObjectGlom(o);
        }

        public static Glom From(Array<String> names, Array<object> values) {
            Map<String, object> valuesByName = names.Zip(values).ToMap(t => t.Item1, t => t.Item2);
            return Glom.From(valuesByName);
        }

        public static Glom From(Map<String, object> valuesByName) {
            return new Glom(valuesByName);
        }

        public static Glom From(IEnumerable o) {
            return new ArrayGlom(o);
        }

        public static Glom From(String s) {
            return new StringGlom(s);
        }

        public static Glom From(string s) {
            return new StringGlom(s);
        }

        [WhatItDoes("Returns a representation of this instance as the given type.")]
        [Note("Not all types will be supported.")]
        public virtual object As(Type type) {
            return null;
        }

        [WhatItDoes("Returns a representation of this instance as the given type.")]
        [Note("Not all types will be supported.")]
        [Wrapper("As(Type)")]
        public T As<T>() {
            return (T)As(typeof(T));
        }

        //public virtual object Child(String name) {
        //    return valuesByName[name];
        //}

        // Transforms the current situation into a new one.
        public object Evaluate(String nml) {
            ////
            return Evaluate(Nml.Expression(nml));
        }

        // Transforms the current situation into a new one.
        public object Evaluate(Expression expression) {
            return expression.Evaluate(this);
        }

        public override bool Disimplies(Fact fact) {
            // Check the things that definitely apply
            if (base.Disimplies(fact)) {
                return true;
            }
            
            // Check the things that might apply
            foreach (KeyValuePair<ConditionalFact, bool?> conditionalFactToTruth in conditionalFactsToTruth) {
                bool conditionTrue;
                if (!conditionalFactToTruth.Value.HasValue) {
                    conditionTrue = Implies(conditionalFactToTruth.Key);
                    conditionalFactsToTruth[conditionalFactToTruth.Key] = conditionTrue;
                } else {
                    conditionTrue = conditionalFactToTruth.Value.HasValue;
                }
                if (conditionTrue && Glom.AsFact(conditionalFactToTruth.Key.Value).Disimplies(fact)) {
                    return true;
                }
            }
            return false;
        }

        public override bool Implies(Fact fact) {
            // Check the things that definitely apply
            if (base.Disimplies(fact)) {
                return true;
            }

            // Check the things that might apply
            foreach (KeyValuePair<ConditionalFact, bool?> conditionalFactToTruth in conditionalFactsToTruth) {
                bool conditionTrue;
                if (!conditionalFactToTruth.Value.HasValue) {
                    conditionTrue = Implies(conditionalFactToTruth.Key);
                    conditionalFactsToTruth[conditionalFactToTruth.Key] = conditionTrue;
                } else {
                    conditionTrue = conditionalFactToTruth.Value.HasValue;
                }
                if (conditionTrue && Glom.AsFact(conditionalFactToTruth.Key.Value).Implies(fact)) {
                    return true;
                }
            }
            return false;
        }

        [WhatItDoes("Attempts to retrieve a value (POCO or Glom) matching the given name from anywhere that it can reasonably found.")]
        public virtual bool TryGet(String name, out object o) {
            if (TryGetAtThisLevel(name, out o)) {
                return true;
            }

            if (prototype != null && prototype.TryGet(name, out o)) {
                return true;
            }

            o = null;
            return false;
        }

        // Returns an amorphous value that can be used by other epxressions
        [WhatItDoes("Attempts to retrieve a value (POCO or Glom) matching the given name, from only this exact instance.")]
        protected virtual bool TryGetAtThisLevel(String name, out object o) {
            // Check the things that might apply
            //foreach (KeyValuePair<ConditionalFact, bool?> conditionalFactToTruth in conditionalFactsToTruth) {
            //    WordFact conditionWordFact = conditionalFactToTruth.Key.Condition as WordFact;
            //    if (conditionWordFact.Word == name) {
            //        o = conditionalFactToTruth.Key.Value;
            //        return true;
            //    }
            //}

            if (valuesByName.TryGetValue(name, out o)) {
                return true;
            }

            // If they requested a POCO value, but no one was able to provide one, the best we can do is this object
            if (name == ValueName) {
                o = this;
                return true;
            }

            o = null;
            return false;
        }

        [WhatItIs("An efficient accessor for Gloms with a compiled property backed by a named value.")]
        protected struct CompiledGlomProperty<T> {
            private String name;
            private T value;
            private bool valueSet;

            public CompiledGlomProperty(String name) {
                this.name = name;
                this.value = default(T);
                this.valueSet = false;
            }

            public T Value(Glom glom) {
                if (!valueSet) {
                    value = (T)glom[name];
                    valueSet = true;
                }
                return value;
            }
        }
    }
}
