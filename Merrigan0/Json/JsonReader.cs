using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.JsonInternal {
    //[Untested]
//    public class JsonReader {
//        long i;
//        String s;

//        public JsonReader(String s) {
//            this.s = s;
//        }

//        public R Call<P1, P2, R>(Func<P1, P2, R> function, P1 parameter1, P2 parameter2) {
//#if DEBUG
//            return function(parameter1, parameter2);
//#else
//            return function(parameter1, parameter2);
//#endif
//        }

//        // Reads a value of any kind: string, number, object, array, Boolean, null
//        public virtual object Read() {
//            i = Call(Json.SkipWhitespace, this.s, i);
//            object value;
//            String s;
//            double r;
//            bool b;
//            Map<String, object> node;
//            Array<object> array;
//            if (TryReadString(json, i, out s, out iNew)) {
//                value = s;
//            } else if (TryReadNumber(json, i, out r, out iNew)) {
//                value = r;
//            } else if (TryReadBoolean(json, i, out b, out iNew)) {
//                value = b;
//            } else if (TryReadNull(json, i, out iNew)) {
//                value = null;
//            } else if (TryReadObject(json, i, out node, out iNew)) {
//                value = node;
//            } else if (TryReadArray(json, i, out array, out iNew)) {
//                value = array;
//            } else {
//                throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
//            }
//            return value;
//        }
//    }

//    public class TypedJsonReader : JsonReader {
//        public TypedJsonReader(String s) {
//            this.s = s;
//        }

//        public virtual object Read() {
//        }
//    }
}
