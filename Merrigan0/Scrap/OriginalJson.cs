//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using Merrigan0.Internal.DotNet.Extensions;
//using Merrigan0.Internal.DotNet.Polyfills;
//using Merrigan0.ExecutionsInternal;
//using Merrigan0.GlomsInternal;
//using Merrigan0.JsonInternal;
//using Merrigan0.MapsInternal;
//using Merrigan0.GlomsInternal;
//using Merrigan0.Internal.Text;
//using Merrigan0.RecognizersInternal;

//namespace Merrigan0 {
//    // An enhancement on top of a string or a glom, that provides
//    //      The string (String)
//    //      The top value represented by that string, as a Glom (Glom)
//    //      And each direct child, each providing these same things (Children)
//    // Json(glom): glom defined, children not, string not
//    // Json(s): glom not defined, children not, string defined
//    // A la carte parsing utilities (TryRead)
//    //      TryRead()
//    //      TryReadArray()
//    //      TryReadBoolean()
//    //      TryReadNumber()
//    //      TryReadObject()
//    //      TryReadString()
//    // A la carte serializing utilities (Append)
//    //      Append()
//    //      AppendArray()
//    //      AppendBoolean()
//    //      AppendNumber()
//    //      AppendObject()
//    //      AppendString()

//    /*
//     * JsonString
//     * JsonNamedValue
//     * JsonArray
//     * JsonObject
//     * JsonNumber
//     * JsonBoolean
//     */

//    // A representation of a full piece of JSON, representing one value, which lazily can provide
//    // that value or JSON objects for its children.
//    // RFC-8259 https://datatracker.ietf.org/doc/html/rfc8259
//    [Untested]
//    public class OriginalJson {
//        public static String FalseToken = "false";
//        public static String NullToken = "null";
//        public static String TrueToken = "true";
//        private static Map<char, char> escapableCharactersToCodes;

//        // The escape codes, to the real characters
//        private static Map<char, char> escapeCodesToCharacters;

//        //private static Set<char> whitespaceCharacters = Set<char>.From(' ', '\t', '\r', '\n');

//        //private Array<Json> children;
//        private String contents;
//        private Glom glom;
//        private Array<OriginalJson> children;

//        public Array<OriginalJson> Children {
//            get {
//                throw new NotImplementedException();
//                ////if (children == null) {
//                ////    object value = Json.Parse(contents, out children);
//                ////    if (this.value == null) {
//                ////        this.value = value;
//                ////    }
//                ////}
//                ////return children;
//            }
//        }

//        public Glom Glom {
//            get {
//                if (glom == null) {
//                    glom = ParseGlomAndCreateChildren(contents, out children);
//                }
//                return glom;
//            }
//        }

//        public String String {
//            get {
//                if (contents == null) {
//                    contents = StringifyAndCreateChildren(glom, out children);
//                }
//                return contents;
//            }
//        }

//        static OriginalJson() {
//            escapableCharactersToCodes = Map<char, char>.From(
//                '"', '"',
//                '\\', '\\',
//                '/', '/',
//                '\b', 'b',
//                '\f', 'f',
//                '\n', 'n',
//                '\r', 'r',
//                '\t', 't');
//            escapeCodesToCharacters = escapableCharactersToCodes.Inverse;
//        }

//        public OriginalJson(String s) /*, object value = null)*/ {
//            this.contents = s;
//        }

//        public OriginalJson(Glom glom) {
//            this.glom = glom;
//        }
        
//        #region Writing POCOs

//        // Simplest: turn the object into a string
//        public static void Append<T>(MutableString ms, T value) {
//            Append(ms, (object)value, typeof(T));
//        }

//        public static void Append(MutableString ms, bool b) {
//            ms.Append(b ? TrueToken : FalseToken);
//        }

//        public static void Append(MutableString ms, decimal d) {
//            ms.Append(d.ToString());
//        }

//        public static void Append(MutableString ms, sbyte sb) {
//            ms.Append(sb.ToString());
//        }

//        public static void Append(MutableString ms, byte b) {
//            ms.Append(b.ToString());
//        }

//        public static void Append(MutableString ms, short sh) {
//            ms.Append(sh.ToString());
//        }

//        public static void Append(MutableString ms, ushort ush) {
//            ms.Append(ush.ToString());
//        }

//        public static void Append(MutableString ms, int n) {
//            ms.Append(n.ToString());
//        }

//        public static void Append(MutableString ms, uint u) {
//            ms.Append(u.ToString());
//        }

//        public static void Append(MutableString ms, long l) {
//            ms.Append(l.ToString());
//        }

//        public static void Append(MutableString ms, ulong ul) {
//            ms.Append(ul.ToString());
//        }

//        public static void Append(MutableString ms, float f) {
//            ms.Append(f.ToString());
//        }

//        public static void Append(MutableString ms, double r) {
//            ms.Append(r.ToString());
//        }

//        public static void Append(MutableString ms, char ch) {
//            ms.Append(String.From(ch));
//        }

//        public static void Append(MutableString ms, string s) {
//            if (s == null) {
//                ms.Append(NullToken);
//            } else {
//                ms.Append('"');
//                AppendEscaped(ms, s);
//                ms.Append('"');
//            }
//        }

//        public static void Append(MutableString ms, String s) {
//            if (s == null) {
//                ms.Append(NullToken);
//            } else {
//                ms.Append('"');
//                AppendEscaped(ms, s);
//                ms.Append('"');
//            }
//        }

//        public static void Append(MutableString ms, Guid guid) {
//            ms.Append(guid.ToString());
//        }

//        public static void Append(MutableString ms, DateTime time) {
//            ms.Append(time.ToString("Z"));
//        }

//        public static void Append(MutableString ms, DateTimeOffset time) {
//            ms.Append(time.ToString("Z"));
//        }

//        public static void Append<T>(MutableString ms, Nullable<T> nullable) where T : struct {
//            if (nullable.HasValue) {
//                Append(ms, nullable.Value);
//            } else {
//                ms.Append(NullToken);
//            }
//        }

//        //// Override in writers
//        public static void Append(MutableString ms, object o) {
//            if (o == null) {
//                ms.Append(NullToken);
//            } else {
//                Append(ms, o, o.GetType());
//            }
//        }

//        // Accommodate
//        //      enums
//        //      Nullables
//        //      reference types
//        //// Override in writers
//        public static void Append(MutableString ms, object o, Type type) {
//            // Is it null?
//            if (o == null) {
//                ms.Append(NullToken);
//            }

//            // Is it a value type? (simple types, well-known structs)
//            if (type.IsValueType) {
//                // Most common first
//                //// replace with dictionary
//                if (type == typeof(int)) {
//                    Append(ms, (int)o);
//                } else if (type == typeof(bool)) {
//                    Append(ms, (bool)o);
//                } else if (type == typeof(long)) {
//                    Append(ms, (long)o);
//                } else if (type.IsEnum) {
//                    Append(ms, o, Enum.GetUnderlyingType(type));
//                } else if (type == typeof(decimal)) {
//                    Append(ms, (decimal)o);
//                } else if (type == typeof(sbyte)) {
//                    Append(ms, (sbyte)o);
//                } else if (type == typeof(byte)) {
//                    Append(ms, (byte)o);
//                } else if (type == typeof(short)) {
//                    Append(ms, (short)o);
//                } else if (type == typeof(ushort)) {
//                    Append(ms, (ushort)o);
//                } else if (type == typeof(uint)) {
//                    Append(ms, (uint)o);
//                } else if (type == typeof(ulong)) {
//                    Append(ms, (ulong)o);
//                } else if (type == typeof(float)) {
//                    Append(ms, (float)o);
//                } else if (type == typeof(ulong)) {
//                    Append(ms, (ulong)o);
//                } else if (type == typeof(char)) {
//                    Append(ms, (char)o);
//                } else if (type == typeof(DateTime)) {
//                    Append(ms, (DateTime)o);
//                } else if (type == typeof(DateTimeOffset)) {
//                    Append(ms, (DateTimeOffset)o);
//                } else if (type == typeof(Guid)) {
//                    Append(ms, (Guid)o);
//                } else {
//                    // If it's nullable, re-call this method with the basic type
//                    Type underlyingType;
//                    if (Utilities.IsNullable(type, out underlyingType)) {
//                        object underlyingValue = Utilities.GetNullableValue(o);
//                        if (underlyingValue == null) {
//                            ms.Append(NullToken);
//                        }
//                        Append(ms, underlyingValue, underlyingType);
//                    }

//                    // Otherwise it's a struct and should be done as an object
//                    AppendPocoObject(ms, o, type);
//                }
//            } else {
//                // It's a reference type: String, string, or non-simple class
//                String s = o as String;
//                if (s != null) {
//                    Append(ms, s);
//                } else {
//                    string systemString = o as string;
//                    if (systemString != null) {
//                        Append(ms, systemString);
//                    } else {
//                        IEnumerable items = o as IEnumerable;
//                        if (items != null) {
//                            AppendPocoArray(ms, items);
//                        } else {
//                            AppendPocoObject(ms, o, type);
//                        }
//                    }
//                }
//            }
//        }

//        public static void AppendPocoArray(MutableString ms, IEnumerable items) {
//            ms.Append('[');
//            bool first = true;
//            foreach (object item in items) {
//                if (!first) {
//                    ms.Append(",");
//                } else {
//                    first = false;
//                }
//                Append(ms, item);
//            }
//            ms.Append(']');
//        }

//        public static void AppendPocoObject(MutableString ms, object o, Type type) {
//            Map<String, object> map;
//            map = o as Map<String, object>;
//            if (map == null) {
//                //// Also see if it's any Map<{string|String}, [any]> or IDictionary<{string|String, [any]> or IReadOnlyDictionary<...>
//                map = Reflection.MapFromObject(o, type);
//            }

//            ms.Append("{");
//            bool first = true;
//            foreach (String key in map.Domain.Array.Sorted()) {
//                if (first) {
//                    first = false;
//                } else {
//                    ms.Append(",");
//                }
//                ms.Append("\"");
//                AppendEscaped(ms, key);
//                ms.Append("\":");
//                Append(ms, map[key]);
//            }
//            ms.Append("}");
//        }

//        [WhatItDoes("Adds the given string, in escaped format to avoid ambiguous quotation marks and reduce often-mistaken characters to ASCII sequences.")]
//        public static void AppendPocoString(MutableString ms, String s) {
//            ms.Append('"');
//            AppendToStringLiteral(ms, s);
//            ms.Append('"');
//        }

//        #endregion

//        #region Writing Gloms

//        [WhatItDoes("Appends the JSON representation of the glom, appropriate for the glom's type.")]
//        public static void Append(MutableString ms, Glom g) {
//            // Is it null?
//            if (g == null) {
//                ms.Append(NullToken);
//                return;
//            }

//            // Most common first
//            if (g.Type == GlomType.Number) {
//                AppendNumber(ms, (NumberGlom)g);
//            } else if (g.Type == GlomType.String) {
//                AppendString(ms, (StringGlom)g);
//            } else if (g.Type == GlomType.Boolean) {
//                AppendBoolean(ms, (BooleanGlom)g);
//            } else if (g.Type == GlomType.Object) {
//                AppendObject(ms, g);
//            } else if (g.Type == GlomType.Array) {
//                AppendArray(ms, (ArrayGlom)g);
//            }
//        }

//        //public static void AppendUlong(MutableString ms, ulong u) {
//        //    ms.Append(u.ToString());
//        //    //ulong uRemaining = u;
//        //    //if (uRemaining == 0) {
//        //    //    ms.Append("0");
//        //    //    return;
//        //    //}

//        //    //// Make the backwards version
//        //    //MutableString backwardsSoFar = new MutableString();
//        //    //while (true) {
//        //    //    int digit = '0' + (int)(uRemaining % 10L);
//        //    //    backwardsSoFar.Append((char)digit);
//        //    //    uRemaining /= 10L;
//        //    //    if (uRemaining == 0L) {
//        //    //        break;
//        //    //    }
//        //    //}
//        //    //ms.Append(backwardsSoFar.Reverse());
//        //}

//        #endregion

//        public override string ToString() {
//            return String.ToString();
//        }

//        public static String JsonString(Glom g) {
//            MutableString ms = new MutableString();
//            Append(ms, g);
//            return ms.Current;
//        }

//        public static Glom Read(String s) { ////, out Array<Json> children) {
//            //// 
//            long i = 0;
//            Glom g = ReadValue(s, ref i);
//            SkipWhitespace(s, ref i);
//            if (i != s.Length) {
//                throw new Exception("Invalid JSON.");
//            }
//            return g;
//        }

//        [Example("\"\"", 0, 2, "")]
//        [Example(" \"123\" ", 1, 6, "123")]
//        [Example(" \"1\"3\" ", 1, 6, "1\"3")]
//        public static String ReadString(
//            [Equals("First", '\"')][Equals("Last", '\"')] String s,
//            [Less("s.Length")] long i,
//            [True(". >= 0 && . <= s.Length")] out long iNew) {
//            String sRead;
//            long iWorking = i;
//            if (!TryReadPocoString(s, ref iWorking, out sRead)) {
//                throw new Exception("No string found.");
//            }
//            iNew = iWorking;
//            return sRead;
//        }

//        [DiagnosticOnly]
//        [Test]
//        public static void Test() {
//        }

//        #region Read into objects

//        // Reads a value of any kind: string, number, object, array, Boolean, null
//        public static object ReadPocoValue(String s, ref long i) {
//            SkipWhitespace(s, ref i);
//            object value;
//            String sLiteral;
//            double r;
//            bool b;
//            Map<String, object> node;
//            Array<object> array;
//            if (TryReadPocoString(s, ref i, out sLiteral)) {
//                value = sLiteral;
//            } else if (TryReadPocoNumber(s, ref i, out r)) {
//                value = r;
//            } else if (TryReadPocoBoolean(s, ref i, out b)) {
//                value = b;
//            } else if (TryReadPocoNull(s, ref i)) {
//                value = null;
//            } else if (TryReadPocoObject(s, ref i, out node)) {
//                value = node;
//            } else if (TryReadPocoArray(s, ref i, out array)) {
//                value = array;
//            } else {
//                throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
//            }
//            return value;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadPocoArray(String s, ref long i, out Array<object> array) {
//            // End of overall string
//            long iOriginal = i;
//            long length = s.Length;
//            if (i >= length) {
//                goto notfound;
//            }

//            // Doesn't start with left bracket
//            if (s[i] != '[') {
//                goto notfound;
//            }

//            MutableArray<object> arraySoFar = new MutableArray<object>();
//            while (true) {
//                SkipWhitespace(s, ref i);
//                if (i >= length) {
//                    throw new Exception("No value found.");
//                }
//                object value = ReadPocoValue(s, ref i);
//                arraySoFar.Append(value);
//                SkipWhitespace(s, ref i);
//                if (i >= length) {
//                    throw new Exception("No right bracket found.");
//                }

//                // Check for end of the array
//                char ch = s[i];
//                if (ch == ']') {
//                    ++i;
//                    break;
//                }
//                if (ch != ',') {
//                    throw new Exception("No ',' found.");
//                }
//            }
//            array = arraySoFar.Current;
//            return true;

//        notfound:
//            array = null;
//            i = iOriginal;
//            return false;
//        }

//        public static bool TryReadPocoBoolean(String s, ref long i, out bool b) {
//            long iOriginal = i;
//            if (String.TryRead(s, i, FalseToken, false, out i)) {
//                b = false;
//                return true;
//            }
//            if (String.TryRead(s, i, TrueToken, false, out i)) {
//                b = true;
//                return true;
//            }
//            b = false;
//            i = iOriginal;
//            return false;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadPocoNull(String s, ref long i) {
//            long iOriginal = i;
//            if (String.TryRead(s, i, NullToken, false, out i)) {
//                return true;
//            }
//            i = iOriginal; //// can remove once String.TryRead is fixed to take ref i
//            return false;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadPocoNumber(String s, ref long i, out double number) {
//            ///// Eventually use own IEEE-754
//            ////if (String.TryReadNumber(s, ref i, out number)) {
//            ////    return true;
//            ////}
//            // For now, get next word and parse that
//            long iOriginal = i;
//            String word;
//            if (TryReadWord(s, ref i, out word)) {
//                if (double.TryParse(word, out number)) {
//                    return true;
//                }
//            }
//            number = 0.0;
//            i = iOriginal;
//            return false;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadPocoObject(String s, ref long i, out Map<String, object> map) {
//            // End of overall string
//            long iOriginal = i;
//            long length = s.Length;
//            if (i >= length) {
//                goto notfound;
//            }

//            // Doesn't start with left brace
//            if (s[i] != '{') {
//                goto notfound;
//            }

//            MutableMap<String, object> mapSoFar = new MutableMap<String, object>();
//            while (true) {
//                SkipWhitespace(s, ref i);
//                String name;
//                if (!TryReadPocoString(s, ref i, out name)) {
//                    throw new Exception("No name found.");
//                }
//                SkipWhitespace(s, ref i);
//                if (i >= length) {
//                    throw new Exception("No ':' found.");
//                }
//                i = String.Skip(s, i, ':');
//                SkipWhitespace(s, ref i);
//                if (i >= length) {
//                    throw new Exception("No value found.");
//                }
//                object value = ReadPocoValue(s, ref i);
//                SkipWhitespace(s, ref i);
//                mapSoFar.Add(name, value);

//                // Check for end of the object
//                char ch = s[i];
//                if (ch == '}') {
//                    ++i;
//                    break;
//                }
//                if (ch != ',') {
//                    throw new Exception("No ',' found.");
//                }
//            }
//            map = mapSoFar.Current;
//            return true;

//        notfound:
//            map = null;
//            i = iOriginal;
//            return false;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadPocoString(String s, ref long i, out String sRead) {
//            // End of overall string
//            long iOriginal = i;
//            long length = s.Length;
//            if (i >= length) {
//                goto notfound;
//            }

//            // Doesn't start with quote character
//            if (s[i] != '\"') {
//                goto notfound;
//            }

//            // Started with quote. Move on to the next
//            ++i;
//            MutableString readSoFar = new MutableString();
//            while (true) {
//                // End of overall string
//                if (i >= length) {
//                    throw new Exception("Unmatched quotation mark.");
//                }

//                // End of string value
//                char ch = s[i];
//                if (ch == '"') {
//                    ++i;
//                    break;
//                }
//                if (ch == '\\') {
//                    ++i;
//                    if (i >= length) {
//                        throw new Exception("Unfinished escape sequence.");
//                    }
//                    ch = s[i];
//                    char properCharacter;
//                    if (escapeCodesToCharacters.TryGetValue(ch, out properCharacter)) {
//                        readSoFar.Append(properCharacter);
//                    } else if (ch == 'u') {
//                        ++i;
//                        char unicode = ReadUnicode(s, ref i);
//                        readSoFar.Append(unicode);
//                    } else {
//                        throw new Exception("Illegal escape sequence.");
//                    }
//                } else {
//                    readSoFar.Append(ch);
//                    ++i;
//                }
//            }
//            sRead = readSoFar.Current;
//            return true;

//        notfound:
//            sRead = null;
//            i = iOriginal;
//            return false;
//        }

//        #endregion

//        #region Read into Gloms

//        // Reads a value of any kind: string, number, object, array, Boolean, null
//        public static Glom ReadValue(String s, ref long i) {
//            SkipWhitespace(s, ref i);
//            Glom g;
//            String sLiteral;
//            double r;
//            bool b;
//            if (TryReadPocoString(s, ref i, out sLiteral)) {
//                g = Glom.From(sLiteral);
//            } else if (TryReadPocoNumber(s, ref i, out r)) {
//                g = Glom.From(r);
//            } else if (TryReadPocoBoolean(s, ref i, out b)) {
//                g = Glom.From(b);
//            } else if (TryReadPocoNull(s, ref i)) {
//                g = null;
//            } else if (TryReadObject(s, ref i, out g)) {
//                // Do nothing
//            } else if (TryReadArray(s, ref i, out g)) {
//                // Do nothing
//            } else {
//                throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
//            }
//            return g;
//        }

//        [Note("Does not skip white space before reading.")]
//        public static bool TryReadGlom(String s, ref long i, out Glom g) {
//            Map<String, object> map;
//            if (!TryReadPocoObject(s, ref i, out map)) {
//                g = null;
//                return false;
//            }
//            g = Glom.From(map);
//            return true;
//        }

//        public static bool TryReadArray(String s, ref long i, out Glom g) {
//            Array<object> a;
//            if (!TryReadPocoArray(s, ref i, out a)) {
//                g = null;
//                return false;
//            }
//            g = Glom.From(a); // a.Transform(o => Glom.From(o));
//            return true;
//        }

//        public static bool TryReadObject(String s, ref long i, out Glom g) {
//            Map<String, object> map;
//            if (!TryReadPocoObject(s, ref i, out map)) {
//                g = null;
//                return false;
//            }
//            g = Glom.From(map);
//            return true;
//        }

//        #endregion

//        [Note("Uses the JSON-specific set of white space characters.")]
//        [Example('b', false)]
//        [Example(' ', true)]
//        public static bool WhiteSpace(char ch) {
//            return ch <= '!' && (ch == ' ' || ch == '\n' || ch == '\r' || ch == '\t');
//        }

//        protected Glom ParseGlomAndCreateChildren(String s, out Array<OriginalJson> children) {
//            // Forget children for now
//            children = null; ////

//            return Read(s);
//        }

//        protected String StringifyAndCreateChildren(Glom g, out Array<OriginalJson> children) {
//            // Forget children for now
//            children = null; ////

//            MutableString ms = new MutableString();
//            Append(ms, g);
//            return ms.Current;
//        }

//        [Note("https://datatracker.ietf.org/doc/html/rfc8259#section-7")]
//        protected static void AppendEscapedUnicode(MutableString ms, char ch) {
//            ms.Append("\\u");
//            ms.Append((ch >> 24) & 0xFF);
//            ms.Append((ch >> 16) & 0xFF);
//            ms.Append((ch >> 8) & 0xFF);
//            ms.Append(ch & 0xFF);
//        }

//        //[WhatItDoes("Appends a representation of the number with minimal sections: -, integer part, decimal, decimal part, optional +En exponent.")]
//        //protected static void AppendDecimal(MutableString ms, decimal d) {
//        //    ms.Append(d.ToString());
//        //}

//        [WhatItDoes("Appends a string that is the original text, but with the quote backslash characters prefixed by a backslash")]
//        protected static void AppendEscaped(MutableString ms, String s) {
//            int i = 0;
//            while (true) {
//                if (i >= s.Length) {
//                    break;
//                }
//                if (s[i] == '\\' || s[i] == '"') {
//                    ms.Append('\\');
//                    ++i;
//                }
//                ms.Append(s[i]);
//                ++i;
//            }
//        }

//        //protected static void AppendLong(MutableString ms, long n) {
//        //    ms.Append(n.ToString());
//        //    //if (n < 0L) {
//        //    //    if (n == 0xFFFFFFFFL) {
//        //    //        ms.Append("-9223372036854775808");
//        //    //        return;
//        //    //    }
//        //    //    ms.Append('-');
//        //    //    n = -n;
//        //    //}
//        //    //long nRemaining = n;
//        //    //if (nRemaining == 0) {
//        //    //    ms.Append("0");
//        //    //    return;
//        //    //}

//        //    //// Make the backwards version
//        //    //MutableString backwardsSoFar = new MutableString();
//        //    //while (true) {
//        //    //    int digit = '0' + (int)(nRemaining % 10);
//        //    //    backwardsSoFar.Append((char)digit);
//        //    //    nRemaining /= 10;
//        //    //    if (nRemaining == 0) {
//        //    //        break;
//        //    //    }
//        //    //}
//        //    //ms.Append(backwardsSoFar.Current.Reverse());
//        //}

//        [WhatItDoes("Appends a representation of the array.")]
//        protected static void AppendArray(MutableString ms, ArrayGlom bg) {
//            ms.Append('[');
//            bool first = true;
//            foreach (Glom g in bg) {
//                if (!first) {
//                    ms.Append(",");
//                } else {
//                    first = false;
//                }
//                Append(ms, g);
//            }
//            ms.Append(']');
//        }

//        [WhatItDoes("Appends a representation of the Boolean.")]
//        protected static void AppendBoolean(MutableString ms, BooleanGlom bg) {
//            ms.Append(bg.Value ? TrueToken : FalseToken);
//        }

//        //[WhatItDoes("Appends a representation of the number with minimal sections: -, integer part, decimal, decimal part, optional +En exponent.")]
//        //protected static void AppendDouble(MutableString ms, double r) {
//        //    // If it can be stringified like a long, do that
//        //    if (System.Math.Truncate(r) == r) {
//        //        AppendLong(ms, (long)r);
//        //        return;
//        //    }

//        //    ms.Append(r.ToString());
//        //}

//        [WhatItDoes("Adds a number in JSON format to a string.")]
//        protected static void AppendNumber(MutableString ms, NumberGlom ng) {
//            object numericObject = ng.Number;
//            Type type = numericObject.GetType();
//            if (type == typeof(int)) {
//                Append(ms, (long)(int)numericObject);
//            } else if (type == typeof(long)) {
//                Append(ms, (long)numericObject);
//            } else if (type == typeof(double)) {
//                Append(ms, (double)numericObject);
//            } else if (type == typeof(float)) {
//                Append(ms, (double)(float)numericObject);
//            } else if (type == typeof(uint)) {
//                Append(ms, (long)(uint)numericObject);
//            } else if (type == typeof(ulong)) {
//                Append(ms, (ulong)numericObject);
//            } else if (type == typeof(short)) {
//                Append(ms, (long)(short)numericObject);
//            } else if (type == typeof(byte)) {
//                Append(ms, (long)(byte)numericObject);
//            } else if (type == typeof(ushort)) {
//                Append(ms, (long)(ushort)numericObject);
//            } else if (type == typeof(decimal)) {
//                Append(ms, (decimal)numericObject);
//            } else if (type == typeof(sbyte)) {
//                Append(ms, (long)(sbyte)numericObject);
//            } else {
//                throw new Exception("Non-numeric type " + type.Name);
//            }
//        }

//        // Appends a string surrounded with { }
//        //// Needs to be owned by some strategy object like a JsonWriter
//        protected static void AppendObject(MutableString ms, /*[Must(".Type == GlomType.Object")]*/ Glom g) {
//            ms.Append("{");
//            bool first = true;
//            foreach (KeyValuePair<String, object> pair in g.ValuesByName) {
//                if (first) {
//                    first = false;
//                } else {
//                    ms.Append(",");
//                }
//                ms.Append("\"");
//                AppendEscaped(ms, pair.Key);
//                ms.Append("\":");
//                Append(ms, pair.Value);
//            }
//            ms.Append("}");
//        }

//        [WhatItDoes("Adds a string in JSON format to a string.")]
//        protected static void AppendString(MutableString ms, StringGlom sg) {
//            AppendStringLiteral(ms, sg.Value);
//        }

//        protected static void AppendStringLiteral(MutableString ms, String s) {
//        }

//        [WhatItDoes("Adds the character to a string literal in progress. If it needs an escape or Unicode sequence, that is added.")]
//        [Note("https://datatracker.ietf.org/doc/html/rfc8259#section-7")]
//        protected static void AppendToStringLiteral(MutableString ms, char ch) {
//            // All ASCII special characters
//            if (ch < 32) {
//                if (ch == '\n') {
//                    ms.Append("\n");
//                } else if (ch == '\r') {
//                    ms.Append("\r");
//                } else if (ch == '\t') {
//                    ms.Append("\t");
//                } else if (ch == '\b') {
//                    ms.Append("\b");
//                } else if (ch == '\f') {
//                    ms.Append("\f");
//                } else {
//                    AppendEscapedUnicode(ms, ch);
//                }
//            } else if (ch > '~') {
//                // All higher-than-ASCII codes
//                AppendEscapedUnicode(ms, ch);
//            } else if (ch == '"') {
//                ms.Append("\"");
//            } else if (ch == '/') {
//                ms.Append("\\/");
//            } else if (ch == '\\') {
//                ms.Append("\\\\");
//            } else {
//                ms.Append(ch);
//            }
//        }

//        [WhatItDoes("Appends a string to a JSON string literal in progress. If it needs an escape or Unicode sequence, that is added.")]
//        protected static void AppendToStringLiteral(MutableString ms, String s) {
//            long i = 0L;
//            long length = s.Length;
//            bool inNonescapedRun = false;
//            long iFirstNotNeedingEscape = -1L;
//            while (i < length) {
//                char ch = s[i];
//                bool needsEscaping = (ch < ' ' || ch == '"' || ch == '\\' || ch == '/' || ch > '~');
//                if (needsEscaping) {
//                    if (inNonescapedRun) {
//                        ms.Append(s.Substring(iFirstNotNeedingEscape, i - iFirstNotNeedingEscape));
//                        inNonescapedRun = false;
//                    }
//                    AppendToStringLiteral(ms, ch);
//                } else if (!inNonescapedRun) {
//                    iFirstNotNeedingEscape = i;
//                    inNonescapedRun = true;
//                }
//                ++i;
//            }
//            if (inNonescapedRun) {
//                ms.Append(s.Substring(iFirstNotNeedingEscape, i - iFirstNotNeedingEscape));
//            }
//        }

//        //protected bool NeedsEscaping(char ch) {
//        //    return (ch < ' ' || ch == '"' || ch == '\\' || ch == '/' || ch > '~');
//        //}

//        protected static char ReadUnicode(String s, ref long i) {
//            if (i >= s.Length - 4) {
//                throw new Exception("Unfinished Unicode value.");
//            }
//            long iOriginal = i;
//            int unicode = Hexadecimal.Number(s[i]);
//            ++i;
//            unicode <<= 4;
//            unicode |= Hexadecimal.Number(s[i]);
//            ++i;
//            unicode <<= 4;
//            unicode |= Hexadecimal.Number(s[i]);
//            ++i;
//            unicode <<= 4;
//            unicode |= Hexadecimal.Number(s[i]);
//            ++i;
//            return (char)unicode;
//        }

//        // Different than String.SkipWhitespace because it uses only JSON-legal whitespace.
//        protected static void SkipWhitespace(String s, ref long i) {
//            long length = s.Length;
//            while (i < length) {
//                if (WhiteSpace(s[i])) {
//                    break;
//                }
//                ++i;
//            }
//        }

//        // Reads to just past the word that starts at the Current location. Sets word to the 
//        // characters preceding that and returns true. If the reader is at the end of the s, returns false.
//        // If characters are found at the end of s without a newline, those characters are returned.
//        protected static bool TryReadWord(String s, ref long i, out String word) {
//            long length = s.Length;
//            while (true) {
//                if (i >= length || WhiteSpace(s[i])) {
//                    break;
//                }
//                ++i;
//            }
//            if (i == 0) {
//                word = null;
//                return false;
//            }
//            word = s.Substring(i, i - i);
//            return true;
//        }
//    }
//}

//////// Reads the next value
//////public static Node Read(TextReader reader, out Json json) {
//////    Node nodeRead;

//////    // Skip whitespace
//////    SkipWhitespace(reader);

//////    // Read next: null, true, false, <number>, ", {, [
//////    int ch = reader.Peek();
//////    if (ch < 0) {
//////        throw new Exception("No value was found.");
//////    }
//////    if (ch == '"') {
//////        nodeRead = ReadString(reader).ToNode();
//////    } else if (ch == '{') {
//////        return ReadNode(reader);
//////    } else if (ch == '[') {
//////        return ReadArrayNode(reader);
//////    } else {
//////        // Get the next word
//////        List<char> charactersSoFar = new List<char>();
//////        while (true) {
//////            ch = reader.Peek();
//////            if (ch < 0 ||
//////                ch == ',' ||
//////                ch == '}' ||
//////                Char.IsWhiteSpace((char)ch)) {
//////                break;
//////            }
//////            ch = reader.Read();
//////            charactersSoFar.Add((char)ch);
//////        }
//////        string word = new string(charactersSoFar.ToArray());
//////        string wordToLowerTrimmed = word.ToLowerInvariant().Trim();
//////        if (wordToLowerTrimmed == "null") {
//////            return NullNode.Only;
//////        } else if (wordToLowerTrimmed == "true") {
//////            return ObjectNode.True;
//////        } else if (wordToLowerTrimmed == "false") {
//////            return ObjectNode.False;
//////        } else {
//////            return new ObjectNode(Double.Parse(word));
//////        }
//////    }

//////    children = childrenSoFar.Current;
//////}

////public static String ReadString(TextReader reader) {
////    int ch = reader.Read();
////    if (ch != '"') {
////        throw new Exception("Bad parameter.");
////    }
////    List<char> charactersSoFar = new List<char>();
////    while (true) {
////        ch = reader.Read();
////        if (ch < 0)
////            throw new Exception("Unmatched quotation mark.");
////        if (ch == '\\') {
////            ch = reader.Read();
////            if (ch < 0) {
////                throw new Exception("Unfinished escape sequence.");
////            }
////        } else if (ch == '"')
////            break;
////        charactersSoFar.Add((char)ch);
////    }
////    return String.From(charactersSoFar.ToArray());
////}

//////public static Node ReadNode(TextReader reader) {
//////    int ch = reader.Read();
//////    if (ch != '{') {
//////        throw new Exception("Bad parameter.");
//////    }
//////    MutableNode mapSoFar = new MutableNode();
//////    while (true) {
//////        SkipWhitespace(reader);
//////        string name = ReadString(reader);
//////        SkipWhitespace(reader);
//////        ch = reader.Peek();
//////        if (ch != ':') {
//////            throw new Exception("Expected a colon after the property name.");
//////        }
//////        ch = reader.Read();
//////        SkipWhitespace(reader);
//////        object value = Read(reader);
//////        mapSoFar.Set(name, value);
//////        SkipWhitespace(reader);

//////        // Check for end of the object
//////        ch = reader.Read();
//////        if (ch < 0) {
//////            throw new Exception("Unfinished hash.");
//////        }
//////        if (ch == '}') {
//////            break;
//////        }
//////        if (ch != ',') {
//////            throw new Exception("Expected a comma.");
//////        }
//////    }
//////    return mapSoFar.Current;
//////}

//////public static ArrayNode ReadArrayNode(TextReader reader) {
//////    int ch = reader.Read();
//////    if (ch != '[') {
//////        throw new Exception("Bad parameter.");
//////    }
//////    List<object> objectsSoFar = new List<object>();
//////    while (true) {
//////        SkipWhitespace(reader);

//////        // Check for end of array
//////        if (reader.Peek() == ']') {
//////            reader.Read();
//////            break;
//////        }

//////        // If it's not the first, skip the comma
//////        if (objectsSoFar.Count > 0) {
//////            ch = reader.Read();
//////            if (ch != ',') {
//////                throw new Exception("Expected a comma between array values.");
//////            }
//////        }

//////        object o = Read(reader);
//////        objectsSoFar.Add(o);
//////    }
//////    return objectsSoFar.ToNodeArray();
//////}

////        type == typeof(int) ||
////        type == typeof(long) ||
////        type == typeof(uint) ||
////        type == typeof(short) ||
////        type == typeof(byte) ||
////        type == typeof(ushort) ||
////        type == typeof(sbyte));
////    //longish (convertible to long)
////    //    sbyte
////    //    byte
////    //    short
////    //    ushort
////    //    int
////    //    uint
////    //    long
////    //ulongish
////    //    ulong
////    //doubleish
////    //    float
////    //    double
////    //decimalish
////    //    decimal
////    bool convertibleToLong = Reflection.ConvertibleToLong(type);
////    bool convertibleToDouble = Reflection.ConvertibleToDouble(type);
////    bool convertibleToDecimal = Reflection.ConvertibleToDecimal(type);
////    if (convertibleToLong) {
////        AppendLong(ms, 
////    bool integer = Reflection.Integer(type);
////    bool real = Reflection.Real(type);
////    if (integer) {
////        long n = (long)

////            new OptionalRecognizer(new SpecificCharacterRecognizer('-')),
////    //MinimalWholeNumberRecognizer.Only,
////            new OrRecognizer(
////                "int",
////                new SpecificCharacterRecognizer('0', rt => 0),
////                new SequenceRecognizer(
////                    new NonzeroDigitRecognizer(),
////                    new AnyRepetitionsRecognizer(new DigitRecognizer()))),
////            new OptionalRecognizer(new SequenceRecognizer(
////                "frac",
////                new SpecificCharacterRecognizer('.'),
////    //DecimalPartRecognizer.Only,
////            new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9')))),
////            new OptionalRecognizer(new SequenceRecognizer(
////                "exp",
////                new OrRecognizer(
////                    new SpecificCharacterRecognizer('e'),
////                    new SpecificCharacterRecognizer('E')),
////                new OrRecognizer(
////                    new SpecificCharacterRecognizer('+'),
////                    new SpecificCharacterRecognizer('-')),
////                new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9'))))
////}

//// Appends a string surrounded with { }
////// Needs to be owned by some strategy object like a JsonWriter
