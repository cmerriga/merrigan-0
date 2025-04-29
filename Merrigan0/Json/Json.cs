using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.Internal.DotNet.Polyfills;

namespace Merrigan0 {
/*
 * Stringify
 *      at each level, 
 *          decide which JavaScript type each should be
 *          execute the conversion necessary
 *          stringify each of those
 *              StringifyArray
 *              StringifyObject
 *              StringifyNumber
 *              StringifyString
 *              StringifyBoolean
 *              StringifyNull
 *              
 * Parse, Parse<T>
 *      { means object, [ means array, true/false means bool, null means null, " means string, anything else is number
 *      if type known, use converter back to that type
 *      if type unknown, use converter to JavaScriptValue
 * 
 */
    [Untested]
    public static class Json {
        //private static Dictionary<JsonType, Type> canonicalTypesByJavaScriptType = new Dictionary<JsonType, Type>() {
        //    { JsonType.Object, typeof(Map<String, object>) },
        //    { JsonType.Array, typeof(Array<object>) },
        //    { JsonType.String, typeof(String) },
        //    { JsonType.Number, typeof(double) },
        //    { JsonType.Boolean, typeof(bool) } 
        //};

        //// use existing conversions
        ////public static void RegisterTypeToCanonicalTypeConversion(
        ////    Func<Type> recognizeTypeFunction,
        ////    JsonType jsonType,
        ////    Func<object, object> convertToCanonicalType) {
        ////}

        ////public static void RegisterCanonicalTypeToTypeConversion(
        ////    Func<Type> recognizeTypeFunction,
        ////    JsonType jsonType,
        ////    Func<object, object> convertToCanonicalType) {
        ////}

        private static Map<char, char> escapableCharactersToCodes;

        // The escape codes, to the real characters
        private static Map<char, char> escapeCodesToCharacters;

        static Json() {
            escapableCharactersToCodes = Map<char, char>.From(
                '"', '"',
                '\\', '\\',
                '/', '/',
                '\b', 'b',
                '\f', 'f',
                '\n', 'n',
                '\r', 'r',
                '\t', 't');
            escapeCodesToCharacters = escapableCharactersToCodes.Inverse;
        }

        public static String Stringify(object o) {
            if (o == null) {
                return StringifyNull();
            }

            return StringifyNotNull(o, o.GetType());
        }

        public static String Stringify<T>(T value) {
            //// or EqualityComparer<T>.Default.Equals
            if (!typeof(T).IsValueType && Object.ReferenceEquals(value, default(T))) {
                return StringifyNull();
            }

            return StringifyNotNull(value, typeof(T));
        }

        public static String Stringify(object o, Type type) {
            if (o == null) {
                return StringifyNull();
            }

            return StringifyNotNull(o, type);
        }

        public static T Parse<T>(String s) {
            return (T)Parse(s, typeof(T));
        }

        public static object Parse(String s, Type type) {
            long i = 0L;
            object o = ReadValue(s, ref i);
            SkipWhitespace(s, ref i);
            if (i != s.Length) {
                throw new Exception("Invalid JSON");
            }
            object converted = Conversion.Convert(o, type);
            return converted;
        }

        [DiagnosticOnly]
        [Test]
        internal static void Test() {
            int n = 15;
            Testing.Test("int", n.ToString() == Stringify(n));

            String s = "goodad";
            Testing.Test("String", "\"" + s + "\"" == Stringify(s));

            s = "\t~\u0032~ ~\"~\\~\u0081";
            String result = Stringify(s);
            String expected = "\"\\t~2~ ~\\\"~\\\\~\\u0081\"";
            Testing.TestEquals(Stringify(s), expected);

            string systemString = "goodad";
            Testing.Test("string", Stringify(systemString) == "\"" + systemString + "\"");

            double r = .000000000000000246813579;
            string expectedSystemString = r.ToString();
            Testing.Test("double", Stringify(r) == expectedSystemString);

            bool b = true;
            Testing.Test("bool", Stringify(b) == "true");

            Array<bool> ab = Array<bool>.From(true, false);
            Testing.Test("Array<bool>", Stringify(ab) == "[true,false]");

            MutableMap<String, object> map = new MutableMap<String, object>();
            map.Add("abc", 1);
            map.Add("def", Array<int>.From(2, 3, 4));
            map.Add("ghi", true);
            String stringifiedMap = Stringify(map.Current);
            Testing.Test("object", stringifiedMap == "{\"abc\":1,\"def\":[2,3,4],\"ghi\":true}");
        }

        [Note("https://datatracker.ietf.org/doc/html/rfc8259#section-7")]
        [Example('\u12ab', false, "\\u12ab")]
        [Example('\u12ab', true, "\\u12AB")]
        [Example('\0', true, "\\u0000")]
        private static String EscapedUnicode(char ch, bool capital = false) {
            MutableString ms = new MutableString();
            ms.Append("\\u");
            ms.Append(Hexadecimal.HexDigit((ch >> 12) & 0xF, capital));
            ms.Append(Hexadecimal.HexDigit((ch >> 8) & 0xF, capital));
            ms.Append(Hexadecimal.HexDigit((ch >> 4) & 0xF, capital));
            ms.Append(Hexadecimal.HexDigit(ch & 0xF, capital));
            return ms.Current;
        }

        private static JsonType JsonTypeConversion(Type type, out Func<object, object> convertFunction) {
            // Types to check for conversion to: 
            // Map<String, object>
            //    IDictionary<string/String, any type>
            //    Map<string/String, any type>
            // Array<object>
            //    IEnumerable
            //    IEnumerable<any type>
            // double (convert longs/ulongs underneath thresholds)
            // string/String, DateTime, Guid, etc.
            // bool
            // Map<String, object> from any other object

            // This order matters a lot. Changing it will make many things fail
            if (Reflection.Numeric(type)) {
                if (Conversion.TryGetConvertFunction(type, typeof(double), out convertFunction)) {
                    return JsonType.Number;
                }
            }
            if (type == typeof(bool)) {
                convertFunction = Conversion.Identity;
                return JsonType.Boolean;
            }
            if (type == typeof(string)) {
                convertFunction = (o => String.From((string)o));
                return JsonType.String;
            }
            if (typeof(String).IsAssignableFrom(type)) {
                convertFunction = Conversion.Identity;
                return JsonType.String;
            }

            // Must be before array so that it is not treated as an array of key/value pairs
            if (Conversion.TryGetConvertFunction(type, typeof(Map<String, object>), out convertFunction)) {
                return JsonType.Object;
            }
            if (Conversion.TryGetConvertFunction(type, typeof(Array<object>), out convertFunction)) {
                return JsonType.Array;
            }
            //if (Conversion.TryGetConvertFunction(type, typeof(String), out convertFunction)) {
            //    return JsonType.String;
            //}
            throw new Exception("Couldn't find conversion from type " + type.Name + " to any JsonType.");
        }

        private static char ReadUnicode(String s, ref long i) {
            if (i >= s.Length - 4) {
                throw new Exception("Unfinished Unicode value.");
            }
            long iOriginal = i;
            int unicode = Hexadecimal.Number(s[i]);
            ++i;
            unicode <<= 4;
            unicode |= Hexadecimal.Number(s[i]);
            ++i;
            unicode <<= 4;
            unicode |= Hexadecimal.Number(s[i]);
            ++i;
            unicode <<= 4;
            unicode |= Hexadecimal.Number(s[i]);
            ++i;
            return (char)unicode;
        }

        // Reads a value of any kind: string, number, object, array, Boolean, null
        private static object ReadValue(String s, ref long i) {
            JsonType dummy;
            return ReadValue(s, ref i, out dummy);
        }

        // Reads a value of any kind: string, number, object, array, Boolean, null
        private static object ReadValue(String s, ref long i, out JsonType jsonType) {
            SkipWhitespace(s, ref i);
            object value;
            String sLiteral;
            double r;
            bool b;
            Map<String, object> node;
            Array<object> array;
            if (TryReadString(s, ref i, out sLiteral)) {
                value = sLiteral;
                jsonType = JsonType.String;
            } else if (TryReadNumber(s, ref i, out r)) {
                value = r;
                jsonType = JsonType.Number;
            } else if (TryReadBoolean(s, ref i, out b)) {
                value = b;
                jsonType = JsonType.Boolean;
            } else if (TryReadNull(s, ref i)) {
                value = null;
                jsonType = JsonType.Null;
            } else if (TryReadObject(s, ref i, out node)) {
                value = node;
                jsonType = JsonType.Object;
            } else if (TryReadArray(s, ref i, out array)) {
                value = array;
                jsonType = JsonType.Array;
            } else {
                throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
            }
            return value;
        }

        [Example("(\"aaa\",\"bbb\",333)", "\"[\"aaa\",\"bbb\",333]\"")]
        private static String StringifyArray(Array<object> items) {
            MutableString ms = new MutableString();
            ms.Append('[');
            bool first = true;
            foreach (object item in items) {
                if (!first) {
                    ms.Append(",");
                } else {
                    first = false;
                }
                ms.Append(Stringify(item));
            }
            ms.Append(']');
            return ms.Current;
        }

        private static String StringifyBoolean(bool b) {
            return b ? "true" : "false";
        }

        private static String StringifyDecimal(decimal d) {
            return d.ToString();
        }

        private static String StringifyDouble(double r) {
            return r.ToString();
        }

        private static String StringifyLong(long n) {
            return n.ToString();
        }

        private static String StringifyNotNull(object o, Type type) {
            // The meat of everything
            Func<object, object> convertFunction;
            String s;
            JsonType jsonType = JsonTypeConversion(type, out convertFunction);
            if (jsonType == JsonType.Number) {
                s = StringifyDouble((double)(convertFunction(o)));
            } else if (jsonType == JsonType.String) {
                s = StringifyString((String)(convertFunction(o)));
            } else if (jsonType == JsonType.Object) {
                s = StringifyObject((Map<String, object>)(convertFunction(o)));
            } else if (jsonType == JsonType.Boolean) {
                s = StringifyBoolean((bool)(convertFunction(o)));
            } else if (jsonType == JsonType.Array) {
                s = StringifyArray((Array<object>)(convertFunction(o)));
            } else {
                throw new Exception("Unhandled JsonType " + jsonType);
            }
            return s;
        }

        private static String StringifyNull() {
            return "null";
        }

        private static String StringifyNumber(object number) {
            Type type = number.GetType();
            String s;
            if (type == typeof(int)) {
                s = StringifyLong((long)(int)number);
            } else if (type == typeof(long)) {
                s = StringifyLong((long)number);
            } else if (type == typeof(double)) {
                s = StringifyDouble((double)number);
            } else if (type == typeof(float)) {
                s = StringifyDouble((double)(float)number);
            } else if (type == typeof(uint)) {
                s = StringifyLong((long)(uint)number);
            } else if (type == typeof(ulong)) {
                s = StringifyUlong((ulong)number);
            } else if (type == typeof(short)) {
                s = StringifyLong((long)(short)number);
            } else if (type == typeof(byte)) {
                s = StringifyLong((long)(byte)number);
            } else if (type == typeof(ushort)) {
                s = StringifyLong((long)(ushort)number);
            } else if (type == typeof(decimal)) {
                s = StringifyDecimal((decimal)number);
            } else if (type == typeof(sbyte)) {
                s = StringifyLong((long)(sbyte)number);
            } else {
                throw new Exception("Non-numeric type " + type.Name);
            }
            return s;
        }

        //// Use an "ObjectMap" class to make a dictionary if needed
        private static String StringifyObject(Map<String, object> map) {
            MutableString ms = new MutableString();
            ms.Append("{");
            bool first = true;
            foreach (KeyValuePair<String, object> pair in map) {
                if (first) {
                    first = false;
                } else {
                    ms.Append(",");
                }
                ms.Append(StringifyString(pair.Key));
                ms.Append(":");
                ms.Append(Stringify(pair.Value));
            }
            ms.Append("}");
            return ms.Current;
        }

        private static String StringifyString(String s) {
            MutableString ms = new MutableString("\""); // null;
            long i = 0L;
            long length = s.Length;

            // We will try to return just string s if possible
            //bool inNonescapedRun = false;
            //long iFirstNotNeedingEscape = 0L;
            long nCharactersSinceEscaped = 0;
            while (i < length) {
                char ch = s[i];

                string escapeSequence = null;
                if (ch < ' ') {
                    if (ch == '\n') {
                        escapeSequence = "\\n";
                    } else if (ch == '\r') {
                        escapeSequence = "\\r";
                    } else if (ch == '\t') {
                        escapeSequence = "\\t";
                    } else if (ch == '\b') {
                        escapeSequence = "\\b";
                    } else if (ch == '\f') {
                        escapeSequence = "\\f";
                    }
                } else if (ch <= '~') {
                    if (ch == '"') {
                        escapeSequence = "\\\"";
                    } else if (ch == '/') {
                        escapeSequence = "\\/";
                    } else if (ch == '\\') {
                        escapeSequence = "\\\\";
                    }
                } else {
                    escapeSequence = EscapedUnicode(ch);
                }
                if (escapeSequence != null) {
                    //// Make sure we're collecting a piecewise string
                    //if (ms == null) {
                    //    ms = new MutableString();
                    //}

                    // Finish up any nonescaped run
                    if (nCharactersSinceEscaped > 0) {
                        ms.Append(s.Substring(i - nCharactersSinceEscaped, nCharactersSinceEscaped));
                    }

                    // Add the escape sequence
                    ms.Append(escapeSequence);
                    nCharactersSinceEscaped = 0;
                } else {
                    ++nCharactersSinceEscaped;
                }
                ++i;
            }

            // Finish up any nonescaped run. Might be all of it!
            if (nCharactersSinceEscaped > 0) {
                //if (ms == null) {
                //    return s;
                //}

                ms.Append(s.Substring(i - nCharactersSinceEscaped, nCharactersSinceEscaped));
            }
            ms.Append("\"");

            return ms.Current;
        }

        private static String StringifyUlong(ulong u) {
            return u.ToString();
        }

        // Different than String.SkipWhitespace because it uses only JSON-legal whitespace.
        [Example("a \t\n\rb", 1, 5)]
        private static void SkipWhitespace(String s, ref long i) {
            long length = s.Length;
            while (i < length) {
                if (WhiteSpace(s[i])) {
                    break;
                }
                ++i;
            }
        }

        [Note("Does not skip white space before reading.")]
        private static bool TryReadArray(String s, ref long i, out Array<object> array) {
            // End of overall string
            long iOriginal = i;
            long length = s.Length;
            if (i >= length) {
                goto notfound;
            }

            // Doesn't start with left bracket
            if (s[i] != '[') {
                goto notfound;
            }

            MutableArray<object> arraySoFar = new MutableArray<object>();
            while (true) {
                SkipWhitespace(s, ref i);
                if (i >= length) {
                    throw new Exception("No value found.");
                }
                object value = ReadValue(s, ref i);
                arraySoFar.Append(value);
                SkipWhitespace(s, ref i);
                if (i >= length) {
                    throw new Exception("No right bracket found.");
                }

                // Check for end of the array
                char ch = s[i];
                if (ch == ']') {
                    ++i;
                    break;
                }
                if (ch != ',') {
                    throw new Exception("No ',' found.");
                }
            }
            array = arraySoFar.Current;
            return true;

        notfound:
            array = null;
            i = iOriginal;
            return false;
        }

        private static bool TryReadBoolean(String s, ref long i, out bool b) {
            long iOriginal = i;
            if (String.TryRead(s, i, "false", false, out i)) {
                b = false;
                return true;
            }
            if (String.TryRead(s, i, "true", false, out i)) {
                b = true;
                return true;
            }
            b = false;
            i = iOriginal;
            return false;
        }

        [Note("Does not skip white space before reading.")]
        private static bool TryReadNull(String s, ref long i) {
            long iOriginal = i;
            if (String.TryRead(s, i, "null", false, out i)) {
                return true;
            }
            i = iOriginal; //// can remove once String.TryRead is fixed to take ref i
            return false;
        }

        [Note("Does not skip white space before reading.")]
        private static bool TryReadNumber(String s, ref long i, out double number) {
            ///// Eventually use own IEEE-754
            ////if (String.TryReadNumber(s, ref i, out number)) {
            ////    return true;
            ////}
            // For now, get next word and parse that
            long iOriginal = i;
            String word;
            if (TryReadWord(s, ref i, out word)) {
                if (double.TryParse(word, out number)) {
                    return true;
                }
            }
            number = 0.0;
            i = iOriginal;
            return false;
        }

        [Note("Does not skip white space before reading.")]
        private static bool TryReadObject(String s, ref long i, out Map<String, object> map) {
            // End of overall string
            long iOriginal = i;
            long length = s.Length;
            if (i >= length) {
                goto notfound;
            }

            // Doesn't start with left brace
            if (s[i] != '{') {
                goto notfound;
            }

            MutableMap<String, object> mapSoFar = new MutableMap<String, object>();
            while (true) {
                SkipWhitespace(s, ref i);
                String name;
                if (!TryReadString(s, ref i, out name)) {
                    throw new Exception("No name found.");
                }
                SkipWhitespace(s, ref i);
                if (i >= length) {
                    throw new Exception("No ':' found.");
                }
                i = String.Skip(s, i, ':');
                SkipWhitespace(s, ref i);
                if (i >= length) {
                    throw new Exception("No value found.");
                }
                object value = ReadValue(s, ref i);
                SkipWhitespace(s, ref i);
                mapSoFar.Add(name, value);

                // Check for end of the object
                char ch = s[i];
                if (ch == '}') {
                    ++i;
                    break;
                }
                if (ch != ',') {
                    throw new Exception("No ',' found.");
                }
            }
            map = mapSoFar.Current;
            return true;

        notfound:
            map = null;
            i = iOriginal;
            return false;
        }

        [Note("Does not skip white space before reading.")]
        private static bool TryReadString(String s, ref long i, out String sRead) {
            // End of overall string
            long iOriginal = i;
            long length = s.Length;
            if (i >= length) {
                goto notfound;
            }

            // Doesn't start with quote character
            if (s[i] != '\"') {
                goto notfound;
            }

            // Started with quote. Move on to the next
            ++i;
            MutableString readSoFar = new MutableString();
            while (true) {
                // End of overall string
                if (i >= length) {
                    throw new Exception("Unmatched quotation mark.");
                }

                // End of string value
                char ch = s[i];
                if (ch == '"') {
                    ++i;
                    break;
                }
                if (ch == '\\') {
                    ++i;
                    if (i >= length) {
                        throw new Exception("Unfinished escape sequence.");
                    }
                    ch = s[i];
                    char properCharacter;
                    if (escapeCodesToCharacters.TryGetValue(ch, out properCharacter)) {
                        readSoFar.Append(properCharacter);
                    } else if (ch == 'u') {
                        ++i;
                        char unicode = ReadUnicode(s, ref i);
                        readSoFar.Append(unicode);
                    } else {
                        throw new Exception("Illegal escape sequence.");
                    }
                } else {
                    readSoFar.Append(ch);
                    ++i;
                }
            }
            sRead = readSoFar.Current;
            return true;

        notfound:
            sRead = null;
            i = iOriginal;
            return false;
        }

        // Reads to just past the word that starts at the Current location. Sets word to the 
        // characters preceding that and returns true. If the reader is at the end of the s, returns false.
        // If characters are found at the end of s without a newline, those characters are returned.
        private static bool TryReadWord(String s, ref long i, out String word) {
            long length = s.Length;
            while (true) {
                if (i >= length || WhiteSpace(s[i])) {
                    break;
                }
                ++i;
            }
            if (i == 0) {
                word = null;
                return false;
            }
            word = s.Substring(i, i - i);
            return true;
        }

        [Note("Uses the JSON-specific set of white space characters.")]
        [Example('b', false)]
        [Example(' ', true)]
        private static bool WhiteSpace(char ch) {
            return ch < '!' && (ch == ' ' || ch == '\n' || ch == '\r' || ch == '\t');
        }

        private enum JsonType {
            Object,
            Array,
            String,
            Number,
            Boolean,
            Null
        };
    }
}
