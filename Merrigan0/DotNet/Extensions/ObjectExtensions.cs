using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using Merrigan0.ExecutionsInternal; // EmptyEnumerator

namespace Merrigan0.Internal.DotNet.Extensions {
    using Merrigan0.Internal.DotNet.Polyfills.System; //// needed for Array.Empty

    [Untested]
    public static class ObjectExtensions {
        private static object[] noObjects = Array.Empty<object>();

        ////public static T As<T>(this object value) {
        ////    return Adapters.As<T>(value);
        ////}


        // Creates a new object is the same type and has the same values. Shallow copy
        public static T Clone<T>(this T value) {
            return To<T>(value);
        }

        // Creates a new object that 
        public static T Except<T>(this object o, params object[] namesAndValues) {
            return (T)Except(o, typeof(T), namesAndValues);
        }

        // Creates a new object with the given names and values
        public static object Except(this object source, Type destinationType, params object[] namesAndValues) {
            return null;
        //    if (source == null) {
        //        // Maybe this should throw when given a value destinationType
        //        return Activator.CreateInstance(destinationType);
        //    }

        //////    Type nullableDesiredType = Nullable.GetUnderlyingType(destinationType);
        //////    if (nullableDesiredType != null) {
        //////        return To(source, nullableDesiredType);
        //////    }

        //////    Type sourceType = source.GetType();
        //////    Type nullableOriginalType = Nullable.GetUnderlyingType(sourceType);
        //////    if (nullableOriginalType != null) {
        //////        return To(Utilities.GetNullableValue(source), destinationType);
        //////    }

        //////    Type itemType;
        //////    if (destinationType.IsArray) {
        //////        IEnumerable sourceEnumerable = source as IEnumerable;
        //////        itemType = destinationType.GetElementType();
        //////        IList list = source as IList;
        //////        long length = sourceEnumerable.GetLength();

        //////        IList array = Array.CreateInstance(itemType, length);

        //////        int i = 0;
        //////        foreach (object map in sourceEnumerable) {
        //////            array[i] = To(map, itemType);
        //////            ++i;
        //////        }
        //////        return array;
        //////    } else if (destinationType.HasInterface<IList>()) {
        //////        IEnumerable sourceEnumerable = source as IEnumerable;
        //////        itemType = destinationType.GetGenericArguments()[0];
        //////        IList sourceList = source as IList;
        //////        IList list = (sourceList == null) ?
        //////            (IList)Activator.CreateInstance(destinationType) :
        //////            (IList)Activator.CreateInstance(destinationType, sourceList.Count);

        //////        foreach (object map in sourceEnumerable) {
        //////            list.Add(To(map, itemType));
        //////        }
        //////        return list;
        //////    } else if (destinationType.IsValueType && destinationType.IsAssignableFrom(sourceType)) {
        //////        return source;
        //////    } else if (destinationType.IsEnum) {
        //////        return Utilities.ConvertEnum(source, destinationType);
        //////    } else if (sourceType.HasInterface<IConvertible>()) {
        //////        // Guid values need the GetConverter strategy; DateTimes need ChangeType
        //////        string stringifiedValue = source as string;
        //////        if (destinationType != typeof(DateTime) && destinationType != typeof(DateTimeOffset) && stringifiedValue != null) {
        //////            return TypeDescriptor.GetConverter(destinationType).ConvertFromInvariantString(stringifiedValue);
        //////        } else {
        //////            return Convert.ChangeType(source, destinationType);
        //////        }
        //////    } else {
        //////        // Create a basic object
        //////        object newValue = Activator.CreateInstance(destinationType);

        //////        Map<string, PropertyInfo> availablePublicProperties = sourceType.
        //////            GetProperties(BindingFlags.Public | BindingFlags.Instance).
        //////            ToArray().
        //////            Where(info => info.GetSetMethod() != null).
        //////            ToMap(info => info.Name);

        //////        ///////
        //////    }
        }

        public static object Property(this object o, string name) {
            PropertyInfo property = o.GetType().GetProperty(name);
            if (property != null) {
                return property.GetValue(o, noObjects);
            }
            FieldInfo field = o.GetType().GetField(name);
            if (field != null) {
                return field.GetValue(o);
            }
            throw new Exception("Property was not found.");
        }

        public static T Property<T>(this object o, string name) {
            return (T)o.Property(name);
        }

        //// Gets all settable properties (public, protected, private) or uses the Serializable attributes
        //public static Array<PropertyInfo> GetSerializableProperties(this object o) {
        //    ISerializable serializable = o as ISerializable;
        //    if (serializable != null) {
        //        return GetSerializableProperties(serializable);
        //    }
        //    foreach (
        //}

        //public static Array<PropertyInfo> GetSerializableProperties(this ISerializable serializable) {
        //}

        ////public static implicit operator String(object o) {
        ////    return (String)o.ToString();
        ////}

        public static T To<T>(this object o) {
            return Except<T>(o, noObjects);
        }

        public static object To(this object o, Type type) {
            return Except(o, type, noObjects);
        }

        [WhatItDoes("Creates an expression with this value.")]
        public static Expression ToExpression(this object o) {
            return new ConstantExpression((o == null) ? typeof(object) : o.GetType(), o);
        }
    }
}

//// this conditional is necessary if myType can be an interface,
//// because an interface doesn't implement itself: for example,
//// typeof (IList<int>).GetInterfaces () does not contain IList<int>!
//if (myType.IsInterface && myType.IsGenericType && 
//    myType.GetGenericTypeDefinition () == typeof (IList<>))
//    return myType.GetGenericArguments ()[0] ;

//foreach (var i in myType.GetInterfaces ())
//    if (i.IsGenericType && i.GetGenericTypeDefinition () == typeof (IList<>))
//        return i.GetGenericArguments ()[0] ;