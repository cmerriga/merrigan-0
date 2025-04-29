using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Merrigan0.Internal.DotNet;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.Internal.DotNet.Polyfills.System; // Lazy
using Merrigan0.Internal.DotNet.Polyfills.System.Collections.Generic; // HashSet

namespace Merrigan0 {
    using System.Runtime.InteropServices;
    using Merrigan0.GlomsInternal;

    [Untested]
    public static class Reflection {
        private static BindingFlags anyMemberFlags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static string compilerGeneratedSubstring = "<";

        private static Func<object, object> identityFunction = (o => o);
        private static Dictionary<Type, Array<Type>> implementedTypesByImplementorType;
        private static Dictionary<Type, MutableSet<Type>> implementingTypesByImplementedType;

        private static DoubleDictionary<Type, Type, Func<object, object>> initializeFunctionsByFromTypeByType = new DoubleDictionary<Type, Type, Func<object, object>>();
        private static object initializeFunctionsByFromTypeByToTypeLockObject = new object();

        private static DoubleDictionary<Type, Type, Func<object, object>> implicitCastsByToTypeByFromType = new DoubleDictionary<Type, Type, Func<object, object>>();
        private static object implicitCastsByToTypeByFromTypeLockObject = new object();

        // From type, to type, function
        //private static DoubleDictionary<Type, Type, Func<object, object>> castFunctionsByToTypeByFromType =
        private static Dictionary<Type, Dictionary<Type, Func<object, object>>> castFunctionsByToTypeByFromType = new Dictionary<Type, Dictionary<Type, Func<object, object>>>() {
            //{
            //    typeof(object),
            //    new Dictionary<Type, Func<object, object>>() {
            //        {
            //            typeof(sbyte),
            //            o => (sbyte)o
            //        },
            //        {
            //            typeof(byte),
            //            o => (byte)o
            //        },
            //        {
            //            typeof(short),
            //            o => (short)o
            //        },
            //        {
            //            typeof(ushort),
            //            o => (ushort)o
            //        },
            //        {
            //            typeof(char),
            //            o => (char)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)o
            //        },
            //        {
            //            typeof(uint),
            //            o => (uint)o
            //        },
            //        {
            //            typeof(long),
            //            o => (long)o
            //        },
            //        {
            //            typeof(ulong),
            //            o => (ulong)o
            //        },
            //        {
            //            typeof(float),
            //            o => (float)o
            //        },
            //        {
            //            typeof(double),
            //            o => (double)o
            //        },
            //        {
            //            typeof(decimal),
            //            o => (decimal)o
            //        },
            //        {
            //            typeof(string),
            //            o => (sbyte)o
            //        },
            //        {
            //            typeof(sbyte),
            //            o => (int)o
            //        },
            //        {
            //            typeof(sbyte),
            //            o => (int)o
            //        },
            //        {
            //            typeof(byte),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(int),
            //            o => (int)(sbyte)o
            //        },
            //        {
            //            typeof(long),
            //            o => (long)(sbyte)o
            //        },
            //        {
            //            typeof(float),
            //            o => (float)(sbyte)o
            //        },
            //        {
            //            typeof(double),
            //            o => (double)(sbyte)o
            //        },
            //        {
            //            typeof(decimal),
            //            o => (decimal)(sbyte)o
            //        }
            //    }
            //},
            {
                typeof(sbyte),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(sbyte)o
                    },
                    {
                        typeof(long),
                        o => (long)(sbyte)o
                    },
                    {
                        typeof(float),
                        o => (float)(sbyte)o
                    },
                    {
                        typeof(double),
                        o => (double)(sbyte)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(sbyte)o
                    }
                }
            },
            {
                typeof(byte),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(byte)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(byte)o
                    },
                    {
                        typeof(long),
                        o => (long)(byte)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(byte)o
                    },
                    {
                        typeof(float),
                        o => (float)(byte)o
                    },
                    {
                        typeof(double),
                        o => (double)(byte)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(byte)o
                    }
                }
            },
            {
                typeof(short),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(short)o
                    },
                    {
                        typeof(long),
                        o => (long)(short)o
                    },
                    {
                        typeof(float),
                        o => (float)(short)o
                    },
                    {
                        typeof(double),
                        o => (double)(short)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(short)o
                    }
                }
            },
            {
                typeof(char),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(char)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(char)o
                    },
                    {
                        typeof(long),
                        o => (long)(char)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(char)o
                    },
                    {
                        typeof(float),
                        o => (float)(char)o
                    },
                    {
                        typeof(double),
                        o => (double)(char)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(char)o
                    }
                }
            },
            {
                typeof(ushort),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(ushort)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(ushort)o
                    },
                    {
                        typeof(long),
                        o => (long)(ushort)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(ushort)o
                    },
                    {
                        typeof(float),
                        o => (float)(ushort)o
                    },
                    {
                        typeof(double),
                        o => (double)(ushort)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(ushort)o
                    }
                }
            },
            {
                typeof(int),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(long),
                        o => (long)(int)o
                    },
                    {
                        typeof(float),
                        o => (float)(int)o
                    },
                    {
                        typeof(double),
                        o => (double)(int)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(int)o
                    }
                }
            },
            {
                typeof(uint),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(long),
                        o => (long)(uint)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(uint)o
                    },
                    {
                        typeof(float),
                        o => (float)(uint)o
                    },
                    {
                        typeof(double),
                        o => (double)(uint)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(uint)o
                    }
                }
            },
            {
                typeof(long),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(float),
                        o => (float)(long)o
                    },
                    {
                        typeof(double),
                        o => (double)(long)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(long)o
                    }
                }
            },
            {
                typeof(ulong),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(ulong)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(ulong)o
                    },
                    {
                        typeof(long),
                        o => (long)(ulong)o
                    },
                    {
                        typeof(float),
                        o => (float)(ulong)o
                    },
                    {
                        typeof(double),
                        o => (double)(ulong)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(ulong)o
                    }
                }
            },
            {
                typeof(float),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(float)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(float)o
                    },
                    {
                        typeof(long),
                        o => (long)(float)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(float)o
                    },
                    {
                        typeof(double),
                        o => (double)(float)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(float)o
                    }
                }
            },
            {
                typeof(double),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(double)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(double)o
                    },
                    {
                        typeof(long),
                        o => (long)(double)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(double)o
                    },
                    {
                        typeof(float),
                        o => (float)(double)o
                    },
                    {
                        typeof(decimal),
                        o => (decimal)(double)o
                    }
                }
            },
            {
                typeof(decimal),
                new Dictionary<Type, Func<object, object>>() {
                    {
                        typeof(int),
                        o => (int)(decimal)o
                    },
                    {
                        typeof(uint),
                        o => (uint)(decimal)o
                    },
                    {
                        typeof(long),
                        o => (long)(decimal)o
                    },
                    {
                        typeof(ulong),
                        o => (ulong)(decimal)o
                    },
                    {
                        typeof(float),
                        o => (float)(decimal)o
                    },
                    {
                        typeof(double),
                        o => (double)(decimal)o
                    }
                }
            }
        };
        private static object castFunctionsByToTypeByFromTypeLockObject = new object();

        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/types
        private static Set<Type> integerTypes = Set<Type>.From(
            typeof(sbyte), typeof(byte),
            typeof(short), typeof(char), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong));

        private static Lazy<Array<Assembly>> lazyAssemblies = new Lazy<Array<Assembly>>(() => Array<Assembly>.From(AppDomain.CurrentDomain.GetAssemblies()));

        private static Lazy<Array<String>> lazyNamespaceNames = new Lazy<Array<String>>(() => {
            Array<String> typeNames = Assemblies.Collect(a => Types(a)).Transform(t => (String)t.FullName);
            Array<String> namespaceNames = typeNames.Transform(n => n.BeforeLast('.')).Distinct();
            return namespaceNames;
        });

        //private static Set<Type> integerTypes = Set<Type>.From(
        //    typeof(sbyte), typeof(byte),
        //    typeof(short), typeof(char), typeof(ushort),
        //    typeof(int), typeof(uint),
        //    typeof(long), typeof(ulong));

        ////private static Lazy<Map<Assembly, Array<String>>> lazyNamespaceNamesByAssembly = new Lazy<Map<Assembly, Array<String>>>(() =>
        ////    Assemblies().Group(a => a.Name, Namespaces).
        ////    Array<String> typeNames = Assemblies().Collect(a => Types(a)).Transform(t => t.Namespace).Cast<String>();
        ////    return typeNames.Transform(n => n.BeforeLast('.')).Distinct();
        ////});

        private static Set<Type> realTypes = Set<Type>.From(typeof(double), typeof(float), typeof(decimal));

        private static Dictionary<Type, int> typesToSizes = new Dictionary<Type, int>() {
            { typeof(sbyte), sizeof(sbyte) },
            { typeof(byte), sizeof(byte) },
            { typeof(short), sizeof(short) },
            { typeof(ushort), sizeof(ushort) },
            { typeof(int), sizeof(int) },
            { typeof(uint), sizeof(uint) },
            { typeof(long), sizeof(long) },
            { typeof(ulong), sizeof(ulong) },
            { typeof(char), sizeof(char) },
            { typeof(float), sizeof(float) },
            { typeof(double), sizeof(double) },
            { typeof(decimal), sizeof(decimal) },
            { typeof(bool), sizeof(bool) }
        };

        private static Dictionary<Type, Dictionary<Type, Type>> upcastTypesByType2ByType1 = new Dictionary<Type, Dictionary<Type, Type>>() {
            {
                typeof(sbyte), 
                new Dictionary<Type, Type> {
                    { typeof(sbyte), typeof(int) },
                    { typeof(byte), typeof(int) },
                    { typeof(short), typeof(int) },
                    { typeof(char), typeof(int) },
                    { typeof(ushort), typeof(int) },
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(long) },
                    { typeof(long), typeof(long) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(byte), 
                new Dictionary<Type, Type> {
                    { typeof(byte), typeof(int) },
                    { typeof(short), typeof(int) },
                    { typeof(char), typeof(int) },
                    { typeof(ushort), typeof(int) },
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(uint) },
                    { typeof(long), typeof(long) },
                    { typeof(ulong), typeof(ulong) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(short), 
                new Dictionary<Type, Type> {
                    { typeof(short), typeof(int) },
                    { typeof(char), typeof(int) },
                    { typeof(ushort), typeof(int) },
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(long) },
                    { typeof(long), typeof(long) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(char), 
                new Dictionary<Type, Type> {
                    { typeof(char), typeof(int) },
                    { typeof(ushort), typeof(int) },
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(long) },
                    { typeof(long), typeof(long) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(ushort), 
                new Dictionary<Type, Type> {
                    { typeof(ushort), typeof(int) },
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(uint) },
                    { typeof(long), typeof(long) },
                    { typeof(ulong), typeof(ulong) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(int), 
                new Dictionary<Type, Type> {
                    { typeof(int), typeof(int) },
                    { typeof(uint), typeof(long) },
                    { typeof(long), typeof(long) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(uint), 
                new Dictionary<Type, Type> {
                    { typeof(uint), typeof(uint) },
                    { typeof(long), typeof(long) },
                    { typeof(ulong), typeof(ulong) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(long), 
                new Dictionary<Type, Type> {
                    { typeof(long), typeof(long) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(ulong), 
                new Dictionary<Type, Type> {
                    { typeof(ulong), typeof(ulong) },
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) },
                    { typeof(decimal), typeof(decimal) }
                }
            },
            {
                typeof(float), 
                new Dictionary<Type, Type> {
                    { typeof(float), typeof(float) },
                    { typeof(double), typeof(double) }
                }
            },
            {
                typeof(double), 
                new Dictionary<Type, Type> {
                    { typeof(double), typeof(double) }
                }
            },
            {
                typeof(decimal), 
                new Dictionary<Type, Type> {
                    { typeof(decimal), typeof(decimal) }
                }
            }
        };

        [Positive("Length")]
        [Dependent("Array<Assembly>.From")]
        public static Array<Assembly> Assemblies { get { return lazyAssemblies.Value; } }

        [Example(typeof(Merrigan0.String), true, Description = "regular abstract class")]
        [Example(typeof(Reflection), false, Description = "static class")]
        [Example(typeof(System.String), false, Description = "regular non-abstract class")]
        public static bool AbstractClass(Type type) {
            // Only using IsAbstract will return static classes as well. All static classes are
            // IsSealed, so just don't return any of those
            // https://stackoverflow.com/questions/1175888/determine-if-a-type-is-static
            return type.IsAbstract && !type.IsSealed;
        }

        [Dependent("Array<Assembly>.Assemblies")]
        [Untested]
        public static Assembly Assembly([Example("System")] String name) {
            String prefixToFind = name + ",";
            return Assemblies.First(a => ((String)a.FullName).StartsWith(prefixToFind));
        }

        [Dependent("Array<object>.From", "Array<object>.Cast<T>")]
        public static Array<T> Attributes<T>(MemberInfo member) {
            return Array<object>.From(member.GetCustomAttributes(typeof(T), true)).Cast<T>();
        }

        public static Array<Attribute> Attributes(MemberInfo member) {
            return member.GetCustomAttributes(true).ToArray().Cast<Attribute>();
        }

        [Dependent("Array<object>.From", "Array<object>.Cast<T>")]
        public static Array<T> Attributes<T>(ParameterInfo parameter) {
            return Array<object>.From(parameter.GetCustomAttributes(typeof(T), true)).Cast<T>();
        }

        public static Array<Attribute> Attributes(ParameterInfo parameter) {
            return parameter.GetCustomAttributes(true).ToArray().Cast<Attribute>();
        }

        [WhatItDoes("Tries to create an Invoke()-ready array of ordered values")]
        public static bool TryGetInvokeArguments(
            MethodBase method,
            Map<String, object> valuesByName,
            out object[] invokeArguments) 
        {
            ParameterInfo[] parameters = method.GetParameters();
            invokeArguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i) {
                ParameterInfo parameter = parameters[i];
                String name = parameter.Name;
                object value;
                if (!valuesByName.TryGetValue(name, out value)) {
                    if (!parameter.IsOptional) {
                        invokeArguments = null;
                        return false;
                    }
                    invokeArguments[i] = parameter.DefaultValue;
                } else {
                    invokeArguments[i] = value;
                }
            }
            return true;
        }

        public static bool CanCast(Type fromType, Type toType) {
            Func<object, object> dummyFunction;
            return TryGetCastFunction(fromType, toType, out dummyFunction);
        }

        [Example(typeof(int), 4, 4L)]
        [Example(typeof(object), null, null, Description = "null casts to null")]
        [Dependent("Cast(object, Type)")]
        public static T Cast<T>(object o) {
            return (T)Cast(o, typeof(T));
        }

        [Example(4, typeof(int), 4L)]
        [Example(null, typeof(object), null, Description = "null casts to null")]
        [Throws(typeof(InvalidCastException), null, typeof(int), Description = "null to value type")]
        [Dependent("Cast(object, Type, Type)")]
        public static object Cast(object o, Type toType) {
            if (o == null) {
                if (toType.IsValueType) {
                    throw new InvalidCastException();
                }
                return null;
            }

            return Cast(o, o.GetType(), toType);
        }

        // Casts everything to ints and up, but only to types that can hold any of the possible values
        // of the first.
        [Example(4, typeof(int), typeof(long), 4L)]
        [Example(null, typeof(object), typeof(String), null, Description = "null casts to null")]
        [Dependent("TryCast(object, Type, Type, out object)")]
        public static object Cast(object o, Type fromType, Type toType) {
            object cast;
            if (!TryCast(o, fromType, toType, out cast)) {
                throw new InvalidCastException();
            }
            return cast;
        }

        // Use if you are going to be casting from these types frequently.
        [Example(typeof(int), typeof(long), Description = "cast exists")]
        [Throws(typeof(NotSupportedException), typeof(int), typeof(string), Description = "no cast exists")]
        [Dependent("TryGetCastFunction(Type, Type, out object)")]
        public static Func<object, object> CastFunction(Type fromType, Type toType) {
            Func<object, object> castFunction;
            if (!TryGetCastFunction(fromType, toType, out castFunction)) {
                throw new NotSupportedException();
            }
            return castFunction;
        }

        [return: Positive("Length")]
        //[return: All("IsClass")]
        public static Array<Type> Classes(Assembly assembly) {
            return Types(assembly).Where(t => t.IsClass);
        }

        [Untested]
        public static Array<FieldInfo> ClassFields(Type type, bool includeNonpublic) {
            BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public;
            if (includeNonpublic) {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetFields(bindingFlags);
        }

        [Untested]
        public static Array<FieldInfo> ClassFields(Type type) {
            return ClassFields(type, false);
        }

        [Untested]
        public static FieldInfo ClassField(Type type, String name) {
            return type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        [Untested]
        public static object ClassFieldValue(Type type, String name) {
            return ClassField(type, name).GetValue(null);
        }

        [Untested]
        public static object ClassFieldValue(FieldInfo field) {
            return field.GetValue(null);
        }

        [Untested]
        public static Array<MethodInfo> ClassMethods(Type type) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static).ToArray<MethodInfo>();
        }

        [Untested]
        public static Array<PropertyInfo> ClassProperties(Type type, bool includeNonpublic) {
            BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public;
            if (includeNonpublic) {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetProperties(bindingFlags);
        }

        [Untested]
        public static Array<PropertyInfo> ClassProperties(Type type) {
            return ClassProperties(type, false);
        }

        [Untested]
        public static PropertyInfo ClassProperty(Type type, String name) {
            return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        [Untested]
        public static object ClassPropertyValue(Type type, String name) {
            return ClassProperty(type, name).GetValue(null, null);
        }

        [Untested]
        public static object ClassPropertyValue(PropertyInfo property) {
            return property.GetValue(null, null);
        }

        public static bool CompilerGenerated(MemberInfo member) {
            return member.Name.Contains(compilerGeneratedSubstring);
        }

        public static Type Components(
            /*[True("!IsGeneric || IsGenericTypeDefinition")] */Type type, 
            [MayBeNull(When = "!IsGeneric")] out Array<Type> typeArguments) {
            if (!SpecificGeneric(type)) { /////
                typeArguments = null;
                return type;
            }

            Type genericType = type.GetGenericTypeDefinition();
            typeArguments = type.GetGenericArguments();
            return genericType;
        }

        public static Array<Expression> Constraints(MemberInfo member) {
            Array<ConstraintAttribute> attributes = Reflection.Attributes<ConstraintAttribute>(member);
            return attributes.Transform(a => a.Constraint);
        }

        //doubleish
        //    float
        //    double
        //also
        //    sbyte
        //    byte
        //    short
        //    ushort
        //    int
        //    uint
        [WhatItIs("Whether the type is always convertible to double without loss of information.")]
        [Example(typeof(float), true)]
        [Example(typeof(ulong), true)]
        [Example(typeof(decimal), false)]
        public static bool ConvertibleToDouble(Type type) {
            return (
                type == typeof(double) ||
                type == typeof(float) ||
                type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(short) ||
                type == typeof(byte) ||
                type == typeof(ushort) ||
                type == typeof(sbyte));
        }

        //decimalish
        //    decimal
        //also
        //    sbyte
        //    byte
        //    short
        //    ushort
        //    int
        //    uint
        //    long
        //    ulong
        [WhatItIs("Whether the type is always convertible to decimal without loss of information.")]
        [Example(typeof(sbyte), true)]
        [Example(typeof(double), false)]
        [Example(typeof(decimal), true)]
        public static bool ConvertibleToDecimal(Type type) {
            return (
                type == typeof(decimal) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(uint) ||
                type == typeof(ulong) ||
                type == typeof(short) ||
                type == typeof(byte) ||
                type == typeof(ushort) ||
                type == typeof(sbyte));
        }

        //longish (convertible to long)
        //    sbyte
        //    byte
        //    short
        //    ushort
        //    int
        //    uint
        //    long
        [WhatItIs("Whether the type is always convertible to long without loss of information.")]
        [Example(typeof(sbyte), true)]
        [Example(typeof(ulong), true)]
        public static bool ConvertibleToLong(Type type) {
            return (
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(uint) ||
                type == typeof(short) ||
                type == typeof(byte) ||
                type == typeof(ushort) ||
                type == typeof(sbyte));
        }

        public static object CreateInstance<T>(Type type, IDictionary<String, T> valuesByName) {
            return Map<String, object>.From(valuesByName);
        }

        public static object CreateInstance(Type type, Map<String, object> valuesByName) {
            //ConstructorInfo constructor;
            //Array<String> namesNotFound;
            //Set<String> names = valuesByName.Domain;
            //ConstructorInfo[] constructors = type.GetConstructors();
            //if (!TryGetConstructorMatchingValues(constructors, names)) {
            //    constructor = GetConstructorMatchingMostValues(type, names, out namesNotFound);
            //}


            //// Find constructor that takes these names as parameters
            //ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //MutableArray<object> convertedArgumentsSoFar = new MutableArray<object>();
            //foreach (ConstructorInfo constructorToTry in typeToConstruct.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
            //    ParameterInfo[] parameters = constructorToTry.GetParameters();
            //    if (parameters.Length != arguments.Length) {
            //        continue;
            //    }
            //    bool failed = false;
            //    for (long i = 0; i < arguments.Length; ++i) {
            //        object convertedArgument;
            //        if (!Conversion.TryConvert(arguments[i], parameters[i].ParameterType, out convertedArgument)) {
            //            failed = true;
            //            break;
            //        }
            //        convertedArgumentsSoFar.Append(convertedArgument);
            //    }
            //    if (!failed) {
            //        constructor = constructorToTry;
            //        convertedArguments = convertedArgumentsSoFar.Current;
            //        return true;
            //    }
            //}
            //constructor = null;
            //convertedArguments = null;
            //return false;

            // Find From function of type and of its base classes that takes these names as parameters

            // For each incomplete constructor, find settable public properties that match all the names not in the constructor


            throw new Exception("No constructor or public properties matched all value names");
        }

        [WhatItDoes("Creates a new instance of the given class, using any constructor that matches the types of the provided initialization values.")]
        public static object CreateInstance(Type type, params object[] values) {
            //// Find constructor that takes these types as parameters
            //ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);


            //MutableArray<object> convertedArgumentsSoFar = new MutableArray<object>();
            //foreach (ConstructorInfo constructorToTry in typeToConstruct.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
            //    ParameterInfo[] parameters = constructorToTry.GetParameters();
            //    if (parameters.Length != arguments.Length) {
            //        continue;
            //    }
            //    bool failed = false;
            //    for (long i = 0; i < arguments.Length; ++i) {
            //        object convertedArgument;
            //        if (!Conversion.TryConvert(arguments[i], parameters[i].ParameterType, out convertedArgument)) {
            //            failed = true;
            //            break;
            //        }
            //        convertedArgumentsSoFar.Append(convertedArgument);
            //    }
            //    if (!failed) {
            //        constructor = constructorToTry;
            //        convertedArguments = convertedArgumentsSoFar.Current;
            //        return true;
            //    }
            //}
            //constructor = null;
            //convertedArguments = null;
            return null; ////
        }

        public static T CreateInstance<T>(Map<String, object> valuesByName) {
            return (T)CreateInstance(typeof(T), valuesByName);
        }

        public static T CreateInstance<T>(params object[] values) {
            // Create via constructor only
            return (T)CreateInstance(typeof(T), values);
        }

        [Example(typeof(Action), true)]
        [Example(typeof(System.String), false)]
        public static bool Delegate(Type type) {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        [Untested]
        public static String Description(MethodBase method) {
            MutableString sSoFar = new MutableString();
            sSoFar += method.Name;
            sSoFar += '(';
            Array<ParameterInfo> parameters = method.GetParameters();
            sSoFar += String.Join(parameters.Transform(p => p.Name), ", ").Truncated(50 - sSoFar.Current.Length - 1);
            sSoFar += ')';
            return sSoFar.Current;
        }

        public static bool Enumerable(Type type) {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        ////[Untested]
        ////public static Array<FieldInfo> Fields(Type type) {
        ////    return Fields(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        ////}

        ////[Untested]
        ////public static Array<FieldInfo> Fields(Type type, BindingFlags bindingFlags) {
        ////    return type.GetFields(bindingFlags).ToArray<FieldInfo>();
        ////}

        ////[Untested]
        ////public static FieldInfo Field(Type type, String name) {
        ////    return type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
        ////}

        [WhatItDoes("Gets the value of the member field or property, or a callable Delegate if a function.")]
        [Untested]
        public static object Get(object o, String name) {
            object value;
            if (!TryGet(o, name, out value)) {
                throw new Exception();
            }
            return value;
        }

        [Untested]
        public static T Get<T>(object o, String name) { return (T)Get(o, name); }

        //[Untested]
        //public static object GetMember(/*[NotNull] */this object o, /*[Identifier] */String s) {
        //    // Try to get this property
        //    Type type = o.GetType();
        //    PropertyInfo property = type.GetProperty(s, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (property != null) {
        //        return property.GetValue(o, null);
        //    }

        //    // Try to get this field
        //    FieldInfo field = type.GetField(s, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (field != null) {
        //        return field.GetValue(o);
        //    }

        //    // Try to get this method as a delegate
        //    MethodInfo method = type.GetMethod(s, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (method != null) {
        //        return System.Delegate.CreateDelegate(type, method);
        //    }

        //    throw new Exception();
        //}

        //[Untested]
        //public static T GetMember<T>(this object o, String s) {
        //    return (T)GetMember(o, s);
        //}

        //// change these so the specific method for a generic is returned IF generic arguments are supplied
        // Gets the method.
        [Untested]
        public static MethodInfo GetMethod(Type type, BindingFlags bindingFlags, String name) {
            return GetMethod(type, bindingFlags, name, Array<Type>.Empty);
        }

        // Gets the method and makes it non-generic using the first needed types from parameterTypes.
        [Untested]
        public static MethodInfo GetMethod(Type type, BindingFlags bindingFlags, String name, params Type[] parameterTypes) {
            return GetMethod(type, bindingFlags, name, Array<Type>.Empty, parameterTypes);
        }

        // Gets the method and makes it non-generic using the first needed types from parameterTypes.
        [Untested]
        public static MethodInfo GetMethod(Type type, BindingFlags bindingFlags, String name, Type[] genericArguments, params Type[] parameterTypes) {
            MethodInfo method;
            if (parameterTypes.Length == 0) {
                method = type.GetMethod(name, bindingFlags);
            } else {
                method = type.GetMethod(name, bindingFlags, null, parameterTypes, null);
            }

            // If the method takes generic arguments, use the ones provided to provide a specific method
            if (method.ContainsGenericParameters && (genericArguments.Length > 0)) {
                method = method.MakeGenericMethod(genericArguments);
            }

            return method;
        }

        [Untested]
        public static bool HandlingException() {
            // https://ayende.com/blog/2577/did-you-know-find-out-if-an-exception-was-thrown-from-a-finally-block
            // Obsolete and may be removed from future .NET frameworks.
            return Marshal.GetExceptionCode() == 0;
        }

        [Untested]
        public static bool HasAttribute(MemberInfo member, Type attributeType) {
            return member.GetCustomAttributes(attributeType, true).Length > 0;
        }

        [Untested]
        public static bool HasAttribute(ParameterInfo parameter, Type attributeType) {
            return parameter.GetCustomAttributes(attributeType, true).Length > 0;
            //// Was like this. Why?
            //foreach (Attribute attribute in parameter.GetCustomAttributes(attributeType, true)) {
            //    if (attribute.GetType() == attributeType) {
            //        return true;
            //    }
            //}
            //return false;
        }

        [Untested]
        public static bool HasAttribute(Type type, Type attributeType) {
            return type.GetCustomAttributes(attributeType, true).Length > 0;
        }

        public static bool HasInterface<T>(Type type) {
            return type.GetInterface(typeof(T).Name) != null;
        }

        public static bool HasInterface(Type type, Type @interface) {
            return type.GetInterface(@interface.Name) != null;
        }

        [WhatItIs("all parent classes (not interfaces), including the class itself, ordered from the class to its most remote base")]
        //[return: Contains("@class")]
        //[Example(typeof(CustomString), "[typeof(CustomString), typeof(String)]")]
        public static Array<Type> ImplementedClasses(Type @class) {
            MutableArray<Type> classesSoFar = new MutableArray<Type>();
            Type currentType = @class;
            while (true) {
                classesSoFar.Append(currentType);
                currentType = currentType.BaseType;
                if (currentType == null) {
                    break;
                }
            }
            return classesSoFar.Current;
        }

        // Ordered by specifity. Given class, specific interfaces for that class, base classes, specific interfaces
        // for that class, etc.
        [WhatItIs("all parent classes and interfaces, including the class or interface itself, " +
            "ordered so all types come before any of their base types")]
        [return: Note("Will not include any unspecified generic types like IEnumerable<>.")]
        [Untested]
        public static Array<Type> ImplementedTypes(Type type) {
            // If we're lucky we've done this before and saved the result
            Array<Type> implementedTypes;
            if (implementedTypesByImplementorType != null && implementedTypesByImplementorType.TryGetValue(type, out implementedTypes)) {
                return implementedTypes;
            }

            // Go up through class chain and add any interfaces not added yet
            Array<Type> implementedClasses = ImplementedClasses(type);
            HashSet<Type> interfacesFoundSoFar = new HashSet<Type>();
            MutableArray<Type> implementedTypesSoFar = new MutableArray<Type>();
            foreach (Type implementedClass in implementedClasses.Reverse()) {
                foreach (Type @interface in Reflection.Interfaces(implementedClass).Reverse()) {
                    if (!interfacesFoundSoFar.Contains(@interface)) {
                        implementedTypesSoFar.Prepend(@interface);
                        interfacesFoundSoFar.Add(@interface);
                    }
                }
                implementedTypesSoFar.Prepend(implementedClass);
            }

            // Collect the types in the proper more-derived to less-derived types, and remember them for the future
            implementedTypes = implementedTypesSoFar.Current;
            //IndentedConsole.Ambient.AppendLine(implementedTypes);
            if (implementedTypesByImplementorType == null) {
                implementedTypesByImplementorType = new Dictionary<Type, Array<Type>>();
            }
            implementedTypesByImplementorType[type] = implementedTypes;
            return implementedTypes;
        }

        [Untested]
        public static Set<Type> ImplementingClasses(Type type) {
            // Populate the whole global map if it doesn't exist yet
            MutableSet<Type> implementingClasses;
            if (implementingTypesByImplementedType == null) {
                Dictionary<Type, MutableSet<Type>> implementingClassesByImplementedTypeSoFar = new Dictionary<Type, MutableSet<Type>>();
                foreach (Assembly assembly in Array<Assembly>.From(Assembly("System"), Assembly("Merrigan0"))) {
                    foreach (Type implementingType in Reflection.Types(assembly)) {
                        if (!implementingType.IsClass) {
                            continue;
                        }
                        Array<Type> implementedTypes = Reflection.ImplementedTypes(implementingType);
                        foreach (Type implementedType in implementedTypes) {
                            if (!implementingClassesByImplementedTypeSoFar.TryGetValue(implementedType, out implementingClasses)) {
                                implementingClasses = new MutableSet<Type>();
                                implementingClassesByImplementedTypeSoFar.Add(implementedType, implementingClasses);
                            }
                            implementingClasses.Add(implementingType);
                        }
                    }
                }
                implementingTypesByImplementedType = implementingClassesByImplementedTypeSoFar;
            }


            if (implementingTypesByImplementedType.TryGetValue(type, out implementingClasses)) {
                return implementingClasses.Current;
            }

            ///// add a single-item set for all types like this to the dictionary when this is found
            return Set<Type>.From(type);
        }

        [Untested]
        public static bool Implements(object o, Type type) {
            return Implements(o.GetType(), type);
        }

        [Untested]
        public static bool Implements(Type implementorType, Type type) {
            return type.IsAssignableFrom(implementorType);
            //if (type.IsInterface) {
            //    foreach (Type implementedInterface in Interfaces(implementorType)) {
            //        if (implementedInterface == type) {
            //            return true;
            //        }
            //    }
            //    return false;
            //}

            //// Walk up the inheritance tree to see if the type is implemented as a parent
            //Type typeToTry = implementorType;
            //while (true) {
            //    if (typeToTry == type) {
            //        return true;
            //    }
            //    Type previousTypeToTry = typeToTry;
            //    typeToTry = typeToTry.BaseType;
                
            //    // Stop if we've reached the top of the inheritance tree. Type Object seems
            //    // to loop back to itself as the BaseType
            //    if (typeToTry == previousTypeToTry || typeToTry == null) {
            //        break;
            //    }
            //}
            //return false;
        }

        /*
         *  (int nIn, out int nOut, ref int nRef, int nOptional = -1)
         *  
            IsIn    IsOut    ParameterType.IsByRef  IsOptional
            False      False        False           False
            False      True        True           False
            False      False        True           False
            False      False        False           True
         */
        [Untested]
        public static bool In(ParameterInfo parameter) {
            return !parameter.ParameterType.IsByRef;
        }

        [Untested]
        public static Array<FieldInfo> InstanceFields(Type type, bool includeNonpublic) {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            if (includeNonpublic) {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetFields(bindingFlags);
        }

        [Untested]
        public static Array<FieldInfo> InstanceFields(Type type) {
            return InstanceFields(type, false);
        }

        [Untested]
        public static FieldInfo InstanceField(Type type, String name) {
            return type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        [Untested]
        public static object InstanceFieldValue(object o, String name) {
            return InstanceField(o.GetType(), name).GetValue(o);
        }

        [Untested]
        public static Array<MethodInfo> InstanceMethods(Type type) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToArray<MethodInfo>();
        }

        [Untested]
        public static Array<PropertyInfo> InstanceProperties(Type type, bool includeNonpublic) {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            if (includeNonpublic) {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetProperties(bindingFlags);
        }

        [Untested]
        public static Array<PropertyInfo> InstanceProperties(Type type) {
            return InstanceProperties(type, false);
        }

        [Untested]
        public static PropertyInfo InstanceProperty(Type type, String name) {
            return type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        [Untested]
        public static object InstancePropertyValue(object o, String name) {
            return InstanceProperty(o.GetType(), name).GetValue(o, null);
        }

        [Untested]
        public static object InstanceValue(object o, FieldInfo field) {
            return field.GetValue(o);
        }

        [Untested]
        public static object InstanceValue(object o, PropertyInfo property) {
            return property.GetValue(o, null);
        }

        [Concept("numeric", "a type whose instances can be mapped to distinct real numbers")]
        [Example(typeof(int), true)]
        [Example(typeof(bool), false)]
        [Example(typeof(string), false)]
        [Example(typeof(Nullable<long>), true)]
        public static bool Integer(Type type) {
            if (type.IsValueType) {
                if (type.IsEnum) {
                    return false;
                }
                if (Object.ReferenceEquals(type, typeof(int)) ||
                    Object.ReferenceEquals(type, typeof(long)) ||
                    Object.ReferenceEquals(type, typeof(uint)) ||
                    Object.ReferenceEquals(type, typeof(ulong)) ||
                    Object.ReferenceEquals(type, typeof(byte)) ||
                    Object.ReferenceEquals(type, typeof(sbyte)) ||
                    Object.ReferenceEquals(type, typeof(short)) ||
                    Object.ReferenceEquals(type, typeof(ushort))) {
                    return true;
                }
                return false;
            }

            if (Nullable(type)) {
                return Integer(System.Nullable.GetUnderlyingType(type));
            }

            return false;
        }

        [WhatItIs("all interfaces implemented by a type, in decreasing order of specificity including the type itself if an interface")]
        [Untested]
        public static Array<Type> Interfaces(Type type) {
            Array<Type> unorderedInterfaces = type.GetInterfaces();
            if (type.IsInterface) {
                unorderedInterfaces = unorderedInterfaces + type;
            }

            return Utilities.SortedTransitive(unorderedInterfaces, (t1, t2) => HasInterface(t1, t2));

            //return Set<Type>.FromDistinct(type.GetInterfaces()).With(type);
        }

        public static object Invoke(MethodBase method, [MayBeNull] object instance, Map<String, object> argumentsByName) {
            object[] invokeArguments;
            if (!TryGetInvokeArguments(method, argumentsByName, out invokeArguments)) {
                throw new Exception("Mismatched arguments");
            }
            return method.Invoke(instance, invokeArguments);
        }

        public static Map<String, object> MapFromObject(object o, Type typeHint = null) {
            Type type = typeHint == null ? null : typeHint;
            MutableMap<String, object> mapSoFar = new MutableMap<String, object>();
            foreach (FieldInfo field in InstanceFields(type)) {
                mapSoFar.Add(field.Name, field.GetValue(o));
            }
            foreach (PropertyInfo property in InstanceProperties(type)) {
                if (property.CanRead) {
                    mapSoFar.Add(property.Name, property.GetValue(o, null));
                }
            }
            return mapSoFar.Current;
        }

        [Untested]
        public static Array<MethodInfo> Methods(Type type) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ToArray<MethodInfo>();
        }

        [Untested]
        public static Array<MethodInfo> Methods(Type type, String name) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ToArray<MethodInfo>().Where(m => m.Name == name);
        }

        // Returns true iff o1 and o2, if cast to a common type, are the same.
        [Example(null, null, true)]
        [Example(null, "dummy", false)]
        [Example("dummy", null, false)]
        [Example(4, 4L, true)]
        public static bool MultitypeEquals(object o1, object o2) {
            if (Object.ReferenceEquals(o1, o2)) { return true; }
            if (o1 == null || o2 == null) { return false; }
            Type upcastType = Reflection.UpcastType(o1.GetType(), o2.GetType());
            bool equals;
            if (upcastType == typeof(double) || upcastType == typeof(float)) {
                equals = Math.Within(Reflection.Cast<double>(o1), Reflection.Cast<double>(o2), 0.0000000001);
            } else {
                equals = object.Equals(Reflection.Cast(o1, upcastType), Reflection.Cast(o2, upcastType));
            }
            return equals;
        }

        [WhatItIs("the names of all namespaces of all loaded assemblies")]
        [Untested]
        public static Array<String> NamespaceNames() { return lazyNamespaceNames.Value; }

        public static String Name(Type type) {
            if (type.IsGenericType) {
                String rootName = ((String)type.Name).Until("`");
                if (type.IsGenericTypeDefinition) {
                    return rootName + "<>";
                }
                return rootName + "<" + String.Join(((Array<Type>)type.GetGenericArguments()).Transform(t => Reflection.Name(t)), ", ") + ">";
            }
            return type.Name;
        }

        [Untested]
        public static object New(Type type, params object[] constructorArguments) {
            object o = Activator.CreateInstance(type, constructorArguments);
            return o;
        }

        [Untested]
        public static T New<T>(params object[] constructorArguments) {
            object o = Activator.CreateInstance(typeof(T), constructorArguments);
            return (T)o;
        }

        [Untested]
        public static object New(Type genericType, Type typeParameter, params object[] constructorArguments) {
            Type specificType = genericType.MakeGenericType(typeParameter);
            object o = Activator.CreateInstance(specificType, constructorArguments);
            return o;
        }

        [Untested]
        public static bool NotSupported(MethodBase method) {
            return Reflection.Attributes<NotSupportedAttribute>(method).Length > 0;
        }

        [Example(typeof(string), false)]
        [Example(typeof(Nullable<double>), true)]
        public static bool Nullable(Type type) {
            Type dummy;
            return Nullable(type, out dummy);
        }

        [Example(typeof(string), null, false)]
        [Example(typeof(Nullable<double>), typeof(double), true)]
        public static bool Nullable(Type type, out Type underlyingType) {
            underlyingType = System.Nullable.GetUnderlyingType(type);
            return (underlyingType != null);
        }

        [Concept("numeric", "a type whose instances can be mapped to distinct real numbers")]
        [Example(typeof(int), true)]
        [Example(typeof(bool), false)]
        [Example(typeof(string), false)]
        [Example(typeof(Nullable<double>), true)]
        public static bool Numeric(Type type) {
            if (type.IsValueType) {
                if (type.IsEnum) {
                    return false;
                }
                if (Object.ReferenceEquals(type, typeof(int)) ||
                    Object.ReferenceEquals(type, typeof(long)) ||
                    Object.ReferenceEquals(type, typeof(double)) ||
                    Object.ReferenceEquals(type, typeof(float)) ||
                    Object.ReferenceEquals(type, typeof(uint)) ||
                    Object.ReferenceEquals(type, typeof(ulong)) ||
                    Object.ReferenceEquals(type, typeof(byte)) ||
                    Object.ReferenceEquals(type, typeof(decimal)) ||
                    Object.ReferenceEquals(type, typeof(sbyte)) ||
                    Object.ReferenceEquals(type, typeof(short)) ||
                    Object.ReferenceEquals(type, typeof(ushort))) {
                    return true;
                }
                return false;
            }

            if (Nullable(type)) {
                return Numeric(System.Nullable.GetUnderlyingType(type));
            }

            return false;
        }

        [Untested]
        public static bool Out(ParameterInfo parameter) {
            return parameter.IsOut;
        }

        public static Array<Type> ProperImplementedTypes(Type type) {
            return ImplementedTypes(type).Without(type);
        }

        ////[Untested]
        ////public static Array<PropertyInfo> Properties(Type type) {
        ////    return Properties(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        ////}

        ////[Untested]
        ////public static Array<PropertyInfo> Properties(Type type, BindingFlags bindingFlags) {
        ////    return type.GetProperties(bindingFlags).ToArray<PropertyInfo>();
        ////}

        ////[Untested]
        ////public static PropertyInfo Property(Type type, String name) {
        ////    return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        ////}

        ////[Untested]
        ////public static object PropertyValue(object o, String name) {
        ////    return Property(o.GetType(), name).GetValue(o, null);
        ////}

        ////[Untested]
        ////public static Array<FieldInfo> PublicFields(Type type) {
        ////    FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        ////    return Array<FieldInfo>.From(fields);
        ////}

        //// get rid
        [Untested]
        public static Array<MethodInfo> PublicMemberMethods(Type type) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToArray<MethodInfo>();
        }

        //// get rid
        [Untested]
        public static Array<MethodInfo> PublicMemberMethods(Type type, String name) {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToArray<MethodInfo>().Where(m => m.Name == name);
        }

        //// get rid
        [Untested]
        public static Array<PropertyInfo> PublicProperties(Type type) {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return Array<PropertyInfo>.From(properties);
        }

        //// get rid
        [Untested]
        public static Array<PropertyInfo> PublicSettableProperties(Type type) {
            MutableArray<PropertyInfo> propertiesSoFar = new MutableArray<PropertyInfo>();
            foreach (PropertyInfo propertyToTry in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                if (propertyToTry.GetSetMethod() != null) {
                    propertiesSoFar.Append(propertyToTry);
                }
            }
            return propertiesSoFar.Current;
        }

        [Untested]
        public static Array<FieldInfo> PublicSettableFields(Type type) {
            MutableArray<FieldInfo> fieldsSoFar = new MutableArray<FieldInfo>();
            foreach (FieldInfo fieldToTry in type.GetFields(BindingFlags.Instance | BindingFlags.Public)) {
                if (!fieldToTry.IsInitOnly) {
                    fieldsSoFar.Append(fieldToTry);
                }
            }
            return fieldsSoFar.Current;
        }

        [Concept("real", "a type which can represent up to Double precision/range of real numbers")]
        [Example(typeof(int), false)]
        [Example(typeof(float), true)]
        [Example(typeof(double), true)]
        [Example(typeof(Nullable<double>), true)]
        public static bool Real(Type type) {
            if (Object.ReferenceEquals(type, typeof(float)) || Object.ReferenceEquals(type, typeof(double))) {
                return true;
            }

            if (Nullable(type)) {
                return Real(System.Nullable.GetUnderlyingType(type));
            }

            return false;
        }

        [Untested]
        public static bool Ref(ParameterInfo parameter) {
            return parameter.ParameterType.IsByRef && !parameter.IsOut;
        }

        [Untested]
        private static void RegisterCastFunction(Type fromType, Type toType, Func<object, object> castFunction) {
            lock (castFunctionsByToTypeByFromTypeLockObject) {
                Dictionary<Type, Func<object, object>> castFunctionsByFromType;
                if (!castFunctionsByToTypeByFromType.TryGetValue(fromType, out castFunctionsByFromType)) {
                    castFunctionsByFromType = new Dictionary<Type, Func<object, object>>();
                    castFunctionsByToTypeByFromType.Add(fromType, castFunctionsByFromType);
                }
                castFunctionsByFromType[toType] = castFunction;
            }
        }

        [Untested]
        private static void RegisterImplicitCastFunction(Type fromType, Type toType, Func<object, object> implicitCastFunction) {
            lock (implicitCastsByToTypeByFromTypeLockObject) {
                implicitCastsByToTypeByFromType[fromType, toType] = implicitCastFunction;
            }
        }

        [Untested]
        private static void RegisterInitializeFunction(Type toType, Type fromType, Func<object, object> initializeFunction) {
            lock (initializeFunctionsByFromTypeByToTypeLockObject) {
                initializeFunctionsByFromTypeByType[toType, fromType] = initializeFunction;
            }
        }

        [Untested]
        public static bool ReturnsVoid(MethodInfo method) {
            return method.ReturnType == typeof(void); //method.ReturnType != null && 
        }

        [Untested]
        public static bool SignedIntegerType(Type type) {
            return type == typeof(int) || type == typeof(long) || type == typeof(char) || type == typeof(sbyte) || type == typeof(short);
        }

        [Untested]
        public static int Sizeof(Type type) {
            return typesToSizes[type];
        }

        //// Doesn't work too good
        public static bool SpecificGeneric(Type type) {
            return type.IsGenericType;
        }

        public static MethodInfo SpecificMethod(/*[True("IsGenericMethodDefinition")] */MethodInfo genericMethodDefinition, params Type[] typeArguments) {
            return genericMethodDefinition.MakeGenericMethod(typeArguments);
        }

        public static Type SpecificType(/*[True("IsGenericTypeDefintion")] */Type genericType, params Type[] typeArguments) {
            Type specificType = genericType.MakeGenericType(typeArguments); //// test performance
            return specificType;
        }

        [WhatItIs("The analagous method from a specific type.")]
        [return: True("IsGenericMethod")]
        public static FieldInfo SpecificTypeField([True("DeclaringType.IsGenericTypeDefinition")] FieldInfo genericTypeField, Type specificType) {
            string name = genericTypeField.Name;
            FieldInfo specificTypeField = specificType.GetField(genericTypeField.Name, anyMemberFlags);
            if (specificTypeField == null) {
                throw new Exception();
            }
            return specificTypeField;
        }

        [WhatItIs("The analagous method from a specific type.")]
        [return: True("IsGenericMethod")]
        public static MethodInfo SpecificTypeMethod([True("DeclaringType.IsGenericTypeDefinition")] MethodInfo genericTypeMethod, Type specificType) {
            string name = genericTypeMethod.Name;
            Array<MethodInfo> specificTypeMethodsWithName = specificType.GetMethods(anyMemberFlags).ToArray().Where(m => m.Name == name);
            if (specificTypeMethodsWithName.Length == 1) {
                return specificTypeMethodsWithName[0];
            }

            ParameterInfo[] parameters;
            parameters = genericTypeMethod.GetParameters();
            MethodInfo specificTypeMethod = null;
            foreach (MethodInfo specificTypeMethodToTry in specificTypeMethodsWithName) {
                ParameterInfo[] specificTypeMethodParameters = specificTypeMethod.GetParameters();
                if (specificTypeMethodParameters.Length != parameters.Length) {
                    continue;
                }

                //// Doesn't distinguish yet between parameter lists with same length
                //if (Array<ParameterInfo>.Equals(specificTypeMethodParameters, parameters)) {
                if (specificTypeMethod != null) {
                    throw new Exception("Duplicate method with same parameter list length");
                }
                specificTypeMethod = specificTypeMethodToTry;
                //}
            }
            return specificTypeMethod;
        }

        [WhatItIs("The analagous property from a specific type.")]
        public static PropertyInfo SpecificTypeProperty([True("DeclaringType.IsGenericTypeDefinition")] PropertyInfo genericTypeProperty, Type specificType) {
            PropertyInfo property = specificType.GetProperty(genericTypeProperty.Name, anyMemberFlags);
            if (property == null) {
                throw new Exception();
            }
            return property;
        }

        ////public static bool Static(FieldInfo field) {
        ////    return field.IsStatic;
        ////}

        public static bool Static(PropertyInfo propertyInfo) {
            MethodInfo method = propertyInfo.GetGetMethod();
            if (method == null) {
                method = propertyInfo.GetSetMethod();
            }
            return method.IsStatic;
        }

        [Untested]
        public static bool TryCast<T>(object o, out T cast) {
            return TryCast(o, null, out cast);
        }

        [Untested]
        public static bool TryCast<T>(object o, [MayBeNull] Type fromTypeHint, out T cast) {
            object castObject;
            if (!TryCast(o, fromTypeHint, out castObject)) {
                cast = default(T);
                return false;
            }
            cast = (T)castObject;
            return true;
        }

        // Casts everything to ints and up, but only to types that can hold any of the possible values
        // of the first.
        [Untested]
        public static bool TryCast(object o, [MayBeNull] Type fromTypeHint, Type toType, out object cast) {
            // null becomes null, for reference types only
            if (o == null) {
                if (toType.IsValueType) {
                    throw new InvalidCastException();
                }
                cast = null;
                return true;
            }

            if (fromTypeHint == null) {
                fromTypeHint = o.GetType();
            }

            // Maybe it's just assignable
            if (toType.IsAssignableFrom(fromTypeHint)) {
                cast = o;
                return true;
            }

            Func<object, object> castFunction;
            if (!TryGetCastFunction(fromTypeHint, toType, out castFunction)) {
                cast = null;
                return false;
            }
            cast = castFunction(o);
            return true;
        }

        public static bool TryFindImplicitCast(Type fromType, Type toType, out Func<object, object> implicitCastFunction) {
            lock (implicitCastsByToTypeByFromTypeLockObject) {
                if (implicitCastsByToTypeByFromType.TryGetValue(fromType, toType, out implicitCastFunction)) {
                    return implicitCastFunction != null;
                }
            }

            // Do implicit operators too if IsAssignableFrom doesn't
            ParameterInfo[] parameters;
            foreach (MethodInfo method in toType.GetMethods(BindingFlags.Static | BindingFlags.Public)) {
                if (method.ReturnType == toType && method.Name == "op_Implicit") {
                    parameters = method.GetParameters();
                    if (parameters.Length == 1 && parameters[0].ParameterType == fromType) {
                        implicitCastFunction = o => method.Invoke(null, new object[] { o });
                        RegisterCastFunction(fromType, toType, implicitCastFunction);
                        return true;
                    }
                }
            }

            // Might be on the other type
            foreach (MethodInfo method in fromType.GetMethods(BindingFlags.Static | BindingFlags.Public)) {
                if (method.ReturnType == toType && method.Name == "op_Implicit") {
                    parameters = method.GetParameters();
                    if (parameters.Length == 1 && parameters[0].ParameterType == fromType) {
                        implicitCastFunction = o => method.Invoke(null, new object[] { o });
                        RegisterCastFunction(fromType, toType, implicitCastFunction);
                        return true;
                    }
                }
            }

            // If none was found, remember that
            RegisterImplicitCastFunction(fromType, toType, implicitCastFunction);
            return false;
        }

        [WhatItDoes("Finds a constructor or static From method on fromType dedicated to initializing from toType.")]
        public static bool TryFindInitializer(Type type, Type fromType, out Func<object, object> initializeFunction) {
            lock (initializeFunctionsByFromTypeByToTypeLockObject) {
                if (initializeFunctionsByFromTypeByType.TryGetValue(type, fromType, out initializeFunction)) {
                    return initializeFunction != null;
                }
            }

            // First find any constructors taking exactly one parameter that is exactly the from type
            foreach (ConstructorInfo constructor in type.GetConstructors()) {
                ParameterInfo[] parameters = constructor.GetParameters();

                // If there's a single parameter constructor that uses something assignable from fromType, use that
                if ((parameters.Length == 1) && (parameters[0].ParameterType == fromType)) {
                    initializeFunction = o => constructor.Invoke(new object[] { o });
                    RegisterInitializeFunction(type, fromType, initializeFunction);
                    return true;
                }
            }

            // Next find any static From methods
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)) {
                if (method.Name != "From") {
                    continue;
                }
                //// Array<object> found here instead of Array<int>
                ParameterInfo[] parameters = method.GetParameters();
                if ((parameters.Length == 1) && (parameters[0].ParameterType == fromType) && (method.ReturnType.IsAssignableFrom(type))) {
                    initializeFunction = o => method.Invoke(null, new object[] { o });
                    RegisterInitializeFunction(type, fromType, initializeFunction);
                    return true;
                }
            }

            // If none was found, remember that
            initializeFunction = null;
            RegisterInitializeFunction(type, fromType, initializeFunction);
            return false;
        }

        [WhatItDoes("Tries to get the value of the member field or property, or a callable Delegate if a function.")]
        [Untested]
        public static bool TryGet(object o, String name, out object value) {
            //// Wish this were not necessary, but see ObjectGlom with a null value; it fails when calling this
            if (o == null) {
                value = null;
                return false;
            }

            // Try to get this property
            Type type = o.GetType();
            PropertyInfo property = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null) {
                value = property.GetValue(o, null);
                return true;
            }

            // Try to get this field
            FieldInfo field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null) {
                value = field.GetValue(o);
                return true;
            }

            // Try to get this method as a delegate
            MethodInfo method = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            if (method != null) {
                if (method.IsStatic) {
                    value = method;
                    return true;
                }
                value = System.Delegate.CreateDelegate(type, o, method);
                return true;
            }

            value = null;
            return false;
        }

        public static bool TryGetConstructor(
            Array<object> arguments, 
            Type typeToConstruct, 
            out ConstructorInfo constructor, 
            out Array<object> convertedArguments) 
        {
            MutableArray<object> convertedArgumentsSoFar = new MutableArray<object>();
            foreach (ConstructorInfo constructorToTry in typeToConstruct.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
                ParameterInfo[] parameters = constructorToTry.GetParameters();
                if (parameters.Length != arguments.Length) {
                    continue;
                }
                bool failed = false;
                for (long i = 0; i < arguments.Length; ++i) {
                    object convertedArgument;
                    if (!Conversion.TryConvert(arguments[i], parameters[i].ParameterType, out convertedArgument)) {
                        failed = true;
                        break;
                    }
                    convertedArgumentsSoFar.Append(convertedArgument);
                }
                if (!failed) {
                    constructor = constructorToTry;
                    convertedArguments = convertedArgumentsSoFar.Current;
                    return true;
                }
            }
            constructor = null;
            convertedArguments = null;
            return false;
        }

        [Untested]
        public static bool TryGet<T>(object o, String name, out T value) {
            object valueObject;
            if (!TryGet(o, name, out valueObject)) {
                value = default(T);
                return false;
            }
            value = (T)valueObject;
            return true;
        }

        // Use if you are going to be casting from these types frequently.
        [Note("Includes same type, native cast, downcast, and implicit cast operators.")]
        [Untested]
        public static bool TryGetCastFunction(Type fromType, Type toType, out Func<object, object> castFunction) {
            // Trivial case: same types, just return original value
            if (Object.ReferenceEquals(fromType, toType) || toType == typeof(object)) { //// fromType?
                castFunction = o => o;
                return true;
            }

            // If it is an object type, let the conversion function be chosen as appr
            // Get our hands on all the functions that cast from the first type
            Dictionary<Type, Func<object, object>> castFunctionsByFromType;
            lock (castFunctionsByToTypeByFromTypeLockObject) {
                if (castFunctionsByToTypeByFromType.TryGetValue(fromType, out castFunctionsByFromType)) {
                    // Get our hands on the function that cast from the first to the second type
                    if (castFunctionsByFromType.TryGetValue(toType, out castFunction)) {
                        // If there was a cast function registered, but it was null, that means we looked before and couldn't 
                        // find one. So we save ourselves the trouble of looking again
                        return castFunction != null;
                    }
                }
            }

            // Downcast
            if (toType.IsAssignableFrom(fromType)) {
                castFunction = identityFunction;
                RegisterCastFunction(fromType, toType, castFunction);
                return true;
            }

            // Implicit cast operator
            if (TryFindImplicitCast(fromType, toType, out castFunction)) {
                RegisterCastFunction(fromType, toType, castFunction);
                return true;
            }

            // If we failed to find any cast, remember that there was none
            castFunction = null;
            RegisterCastFunction(fromType, toType, castFunction);
            return false;
        }

        public static bool TryGetClassPropertyOrFieldValue(Type type, String name, out object value) {
            PropertyInfo property = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null) {
                value = property.GetValue(null, null);
                return true;
            }
            FieldInfo field = type.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null) {
                value = field.GetValue(null);
                return true;
            }
            value = null;
            return false;
        }

        public static bool TryGetPropertyOrFieldValue(object o, String name, out object value) {
            Type type = o.GetType();
            PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null) {
                value = property.GetValue(o, null);
                return true;
            }
            FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null) {
                value = field.GetValue(o);
                return true;
            }
            value = null;
            return false;
        }

        [WhatItDoes("Finds the lowest compatible type.")]
        [Note("Use when you are going to be upcasting from these same types frequently.")]
        [Untested]
        public static bool TryGetUpcastFunctions(
            Type type1, 
            Type type2,
            out Type upcastType,
            out Func<object, object> upcastFunction1, 
            out Func<object, object> upcastFunction2) 
        {
            if (!TryUpcastType(type1, type2, out upcastType)) {
                goto fail;
            }

            if (!TryGetCastFunction(type1, upcastType, out upcastFunction1)) {
                goto fail;
            }

            if (!TryGetCastFunction(type2, upcastType, out upcastFunction2)) {
                goto fail;
            }

            return true;

        fail:
            upcastFunction1 = null;
            upcastFunction2 = null;
            return false;
        }

        [WhatItDoes("Finds the lowest compatible type.")]
        [Untested]
        public static bool TryUpcastType(Type type1, Type type2, out Type upcastType) {
            // If they're the same, use one of them
            if (Object.ReferenceEquals(type1, type2)) {
                upcastType = type1;
                return true;
            }

            // Value types require casts that are supported by .NET implicit conversions
            if (type1.IsValueType) {
                if (!type2.IsValueType) {
                    upcastType = null;
                    return false;
                }
                
                // See if the types can be converted, and if so, to what
                // First look up by type 1, type 2
                Dictionary<Type, Type> upcastTypeByType;
                if (upcastTypesByType2ByType1.TryGetValue(type1, out upcastTypeByType)) {
                    if (upcastTypeByType.TryGetValue(type2, out upcastType)) {
                        return true;
                    }
                }

                // Next try type 2, type 1
                if (upcastTypesByType2ByType1.TryGetValue(type2, out upcastTypeByType)) {
                    if (upcastTypeByType.TryGetValue(type1, out upcastType)) {
                        return true;
                    }
                }

                // No upcast was found
                upcastType = null;
                return false;
            }

            // Reference types are castable if assignable to something else that is common between them
            Array<Type> implementedType2Types = ImplementedTypes(type2);
            foreach (Type type1ToTry in ImplementedTypes(type1)) {
                foreach (Type type2ToTry in implementedType2Types) {
                    if (type1ToTry.IsAssignableFrom(type2ToTry)) {
                        upcastType = type1ToTry;
                        return true;
                    }
                    if (type2ToTry.IsAssignableFrom(type1ToTry)) {
                        upcastType = type2ToTry;
                        return true;
                    }
                }
            }
            
            // Couldn't find any common types
            //// Maybe not possible
            upcastType = null;
            return false;
        }

        [WhatItIs("Whether the type is an IEnumerable<T>.")]
        [Untested]
        public static bool ImplementsTypedEnumerable(Type type, [WhatItIs("The type T.")] out Type typeArgument) {
            foreach (Type @interface in Interfaces(type)) {
                // The interface we want is definitely generic, so skip if not
                if (!@interface.IsGenericType) {
                    continue;
                }

                Array<Type> typeArguments;
                Type genericInterface = Components(@interface, out typeArguments);
                if (genericInterface == typeof(IEnumerable<>)) {
                    if (TypedEnumerable(@interface, out typeArgument)) {
                        return true;
                    }
                }
            }

            typeArgument = null;
            return false;
        }

        [WhatItIs("Whether the type is exactly IEnumerable<T>.")]
        [Untested]
        public static bool TypedEnumerable(Type type, [WhatItIs("The type T.")] out Type typeArgument) {
            Array<Type> typeArguments;
            Type genericInterface = Components(type, out typeArguments);
            if (genericInterface == typeof(IEnumerable<>)) {
                typeArgument = typeArguments[0];
                return true;
            }

            typeArgument = null;
            return false;
        }

        [Untested]
        public static Type TypeParameter(Type type) {
            if (!SpecificGeneric(type)) {
                throw new ArgumentException();
            }
            Type[] typeParameters = type.GetGenericArguments();
            if (typeParameters.Length != 1) {
                throw new ArgumentException();
            }
            return typeParameters[0];
        }

        [Untested]
        public static Array<Type> TypeParameters(Type type) {
            if (!SpecificGeneric(type)) {
                throw new ArgumentException();
            }
            return Array<Type>.From(type.GetGenericArguments());
        }

        [Untested]
        public static Array<Type> Types() {
            return Assemblies.Collect(a => Types(a));
        }

        [Untested]
        public static Array<Type> Types(Assembly assembly) {
            return assembly.GetTypes();
        }

        [Example(typeof(short), typeof(int), typeof(int))]
        public static Type UpcastType(Type type1, Type type2) {
            Type upcastType;
            if (!TryUpcastType(type1, type2, out upcastType)) {
                throw new Exception();
            }
            return upcastType;
        }

        [Untested]
        public static object Value(/*[NotNull] */this Type type, /*[Identifier] */String name) {
            PropertyInfo property = type.GetProperty(name, BindingFlags.Static);
            if (property != null) {
                return property.GetValue(null, null);
            }
            FieldInfo field = type.GetField(name, BindingFlags.Static);
            if (field != null) {
                return field.GetValue(null);
            }
            throw new Exception("Property or field not found.");
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            // Attributes(MethodInfo)
            MethodInfo method = typeof(TestClass).GetMethod("Hold");
            Array<ExampleAttribute> exampleAttributes;
            exampleAttributes = Reflection.Attributes<ExampleAttribute>(typeof(TestClass).GetMethod("Hold"));
            Testing.TestEquals(exampleAttributes.Length, 1);

            Array<MayBeNullAttribute> nullAttributes;
            nullAttributes = Reflection.Attributes<MayBeNullAttribute>(typeof(TestClass).GetMethod("Hold"));
            Testing.TestEquals(nullAttributes.Length, 0);

            // Attributes(ParameterInfo)
            Array<ParameterInfo> parameters = method.GetParameters();
            ParameterInfo parameter = parameters.First(p => p.Name == "milliseconds");
            Array<NotNegativeAttribute> notNegativeAttributes;
            notNegativeAttributes = Reflection.Attributes<NotNegativeAttribute>(parameter);
            Testing.TestEquals(notNegativeAttributes.Length, 1);

            exampleAttributes = Reflection.Attributes<ExampleAttribute>(parameter);
            Testing.TestEquals(exampleAttributes.Length, 0);
        }

        [Untested]
        private static Array<SpecificParametersConstructorInfo> SpecificParametersConstructors(IEnumerable<ConstructorInfo> constructors) {
            MutableArray<SpecificParametersConstructorInfo> specificParametersConstructorsSoFar = new MutableArray<SpecificParametersConstructorInfo>();
            foreach (ConstructorInfo constructor in constructors) {
                ParameterInfo[] parameters = constructor.GetParameters();
                int nOptionalParametersSoFar = 0;
                foreach (ParameterInfo parameter in parameters) {
                    if (parameter.IsOptional) {
                        ++nOptionalParametersSoFar;
                    }
                }
                for (int n = 0; n <= nOptionalParametersSoFar; ++n) {
                    SpecificParametersConstructorInfo specificParametersConstructor = new SpecificParametersConstructorInfo(
                        constructor,
                        parameters.Length - nOptionalParametersSoFar + n);
                    specificParametersConstructorsSoFar.Append(specificParametersConstructor);
                }
            }
            return specificParametersConstructorsSoFar.Current;
        }

        [Untested]
        private static Array<SpecificParametersMethodInfo> SpecificParametersMethods(MethodInfo method) {
            MutableArray<SpecificParametersMethodInfo> specificParametersMethodsSoFar = new MutableArray<SpecificParametersMethodInfo>();
            ParameterInfo[] parameters = method.GetParameters();
            int nOptionalParametersSoFar = 0;
            foreach (ParameterInfo parameter in parameters) {
                if (parameter.IsOptional) {
                    ++nOptionalParametersSoFar;
                }
            }
            for (int n = 0; n <= nOptionalParametersSoFar; ++n) {
                SpecificParametersMethodInfo specificParametersMethod = new SpecificParametersMethodInfo(
                    method,
                    parameters.Length - nOptionalParametersSoFar + n);
                specificParametersMethodsSoFar.Append(specificParametersMethod);
            }
            return specificParametersMethodsSoFar.Current;
        }

        [Untested]
        private static bool TryGetConstructorMatchingValues(
            IEnumerable<ConstructorInfo> constructors,
            IEnumerable<String> names,
            out ConstructorInfo constructor) {
            // Find one that contains all the names
            foreach (ConstructorInfo constructorToTry in constructors) {
                ////
                //// return new SpecificParametersConstructorInfo
            }
            constructor = null;
            return false;
        }

        [DiagnosticOnly]
        private class TestClass {
            [DiagnosticOnly]
            [MayBeNull]
            public string StringProperty { get; set; }

            public TestClass(string stringProperty) {
                StringProperty = stringProperty;
            }

            [Example(4, 4)]
            public int Hold([NotNegative] int milliseconds) {
                return milliseconds;
            }
        }

        private class SpecificParametersConstructorInfo {
            public ConstructorInfo Constructor { get; private set; }
            public int NumberOfParameters { get; private set; }

            public SpecificParametersConstructorInfo(ConstructorInfo constructor, int nParameters) {
                Constructor = constructor;
                NumberOfParameters = nParameters;
            }
        }

        private class SpecificParametersMethodInfo {
            public MethodInfo Method { get; private set; }
            public int NumberOfParameters { get; private set; }

            public SpecificParametersMethodInfo(MethodInfo method, int nParameters) {
                Method = method;
                NumberOfParameters = nParameters;
            }
        }
    }
}

////if (toType == typeof(int)) {
////    if (fromType == typeof(sbyte)) {
////        cast = (int)(sbyte)o;
////    } else if (fromType == typeof(byte)) {
////        cast = (int)(byte)o;
////    } else if (fromType == typeof(short)) {
////        cast = (int)(short)o;
////    } else if (fromType == typeof(char)) {
////        cast = (int)(char)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (int)(ushort)o;
////    } else if (fromType == typeof(int)) {
////        cast = (int)(int)o;
////    }
////} else if (toType == typeof(uint)) {
////    if (fromType == typeof(byte)) {
////        cast = (uint)(byte)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (uint)(ushort)o;
////    } else if (fromType == typeof(char)) {
////        cast = (uint)(char)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (uint)(uint)o;
////    }
////} else if (toType == typeof(long)) {
////    if (fromType == typeof(sbyte)) {
////        cast = (long)(sbyte)o;
////    } else if (fromType == typeof(byte)) {
////        cast = (long)(byte)o;
////    } else if (fromType == typeof(short)) {
////        cast = (long)(short)o;
////    } else if (fromType == typeof(char)) {
////        cast = (long)(char)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (long)(ushort)o;
////    } else if (fromType == typeof(int)) {
////        cast = (long)(int)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (long)(uint)o;
////    }
////} else if (toType == typeof(ulong)) {
////    if (fromType == typeof(byte)) {
////        cast = (ulong)(byte)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (ulong)(ushort)o;
////    } else if (fromType == typeof(char)) {
////        cast = (ulong)(char)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (ulong)(uint)o;
////    }
////} else if (toType == typeof(float)) {
////    if (fromType == typeof(sbyte)) {
////        cast = (float)(sbyte)o;
////    } else if (fromType == typeof(byte)) {
////        cast = (float)(byte)o;
////    } else if (fromType == typeof(short)) {
////        cast = (float)(short)o;
////    } else if (fromType == typeof(char)) {
////        cast = (float)(char)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (float)(ushort)o;
////    } else if (fromType == typeof(int)) {
////        cast = (float)(int)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (float)(uint)o;
////    }
////} else if (toType == typeof(double)) {
////    if (fromType == typeof(sbyte)) {
////        cast = (double)(sbyte)o;
////    } else if (fromType == typeof(byte)) {
////        cast = (double)(byte)o;
////    } else if (fromType == typeof(short)) {
////        cast = (double)(short)o;
////    } else if (fromType == typeof(char)) {
////        cast = (double)(char)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (double)(ushort)o;
////    } else if (fromType == typeof(int)) {
////        cast = (double)(int)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (double)(uint)o;
////    } else if (fromType == typeof(float)) {
////        cast = (double)(float)o;
////    }
////} else if (toType == typeof(decimal)) {
////    if (fromType == typeof(sbyte)) {
////        cast = (decimal)(sbyte)o;
////    } else if (fromType == typeof(byte)) {
////        cast = (decimal)(byte)o;
////    } else if (fromType == typeof(short)) {
////        cast = (decimal)(short)o;
////    } else if (fromType == typeof(char)) {
////        cast = (decimal)(char)o;
////    } else if (fromType == typeof(ushort)) {
////        cast = (decimal)(ushort)o;
////    } else if (fromType == typeof(int)) {
////        cast = (decimal)(int)o;
////    } else if (fromType == typeof(uint)) {
////        cast = (decimal)(uint)o;
////    } else if (fromType == typeof(float)) {
////        cast = (decimal)(float)o;
////    } else if (fromType == typeof(double)) {
////        cast = (decimal)(double)o;
////    }
////}
////if (cast != null) {
////    return true;
////}

////return false;
