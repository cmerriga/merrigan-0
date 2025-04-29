using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.JsonInternal {
//    // A string format that allows the complete specification of C# objects. 
//    // JSON alone supports the creation of Nodes only.
    //[Untested]
//    public static class TypedJson {
//        ////public static object Parse(Type type, String typedJsonString) {

//        ////}

//        ////public static T Parse<T>(String typedJsonString) {
//        ////    return (T)Parse(typeof(T), typedJsonString);
//        ////}

//        // Takes apart the string and remembers where it found what
//        public static object Parse(String typedJsonString) { ////, out Array<Json> children) {
//            long i = 0;
//            object value = ReadValue(typedJsonString, i, out i);
//            i = Json.SkipWhitespace(typedJsonString, i);
//            if (i != typedJsonString.Length) {
//                throw new Exception("Invalid JSON.");
//            }
//            return value;
//        }

//        // Reads a value of any kind: string, number, object, array, Boolean, null
//        public static object ReadValue(String typedJsonString, long i, out long iNew) {
//            i = Json.SkipWhitespace(typedJsonString, i);
//            object value;
//            String s;
//            double r;
//            bool b;
//            Map<String, object> node;
//            Array<object> array;
//            if (TryReadType(typedJsonString, i, out s, out long iNew)) {
//                return ReadValue(Type.GetType(s), typedJsonString, i, out iNew);
//            }
//            if (Json.TryReadString(typedJsonString, i, out s, out iNew)) {
//                value = s;
//            } else if (Json.TryReadNumber(typedJsonString, i, out r, out iNew)) {
//                value = r;
//            } else if (Json.TryReadBoolean(typedJsonString, i, out b, out iNew)) {
//                value = b;
//            } else if (Json.TryReadNull(typedJsonString, i, out iNew)) {
//                value = null;
//            } else if (TryReadObject(typedJsonString, i, out node, out iNew)) {
//                value = node;
//            } else if (Json.TryReadArray(typedJsonString, i, out array, out iNew)) {
//                value = array;
//            } else {
//                throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
//            }
//            return value;
//        }

//        public static bool TryReadArray(String typedJsonString, long i, out Array<object> array, out long iNew) {
//            // End of overall string
//            long length = typedJsonString.Length;
//            if (i >= length) {
//                goto notfound;
//            }

//            // Doesn't start with left bracket
//            if (typedJsonString[i] != '[') {
//                goto notfound;
//            }

//            MutableArray<object> arraySoFar = new MutableArray<object>();
//            while (true) {
//                i = Json.SkipWhitespace(typedJsonString, i);
//                if (i >= length) {
//                    throw new Exception("No value found.");
//                }
//                object value = ReadValue(typedJsonString, i, out i);
//                arraySoFar.Append(value);
//                i = Json.SkipWhitespace(typedJsonString, i);
//                if (i >= length) {
//                    throw new Exception("No right bracket found.");
//                }

//                // Check for end of the array
//                char ch = typedJsonString[i];
//                if (ch == ']') {
//                    ++i;
//                    break;
//                }
//                if (ch != ',') {
//                    throw new Exception("No ',' found.");
//                }
//            }
//            array = arraySoFar.Current;
//            iNew = i;
//            return true;

//        notfound:
//            array = null;
//            iNew = 0L;
//            return false;
//        }
//    }
}
