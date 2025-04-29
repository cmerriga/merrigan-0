using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.Internal.DotNet.Polyfills;
using Merrigan0.RecognizersInternal;

namespace Merrigan0.JsonInternal {
    [Untested]
    internal class JsonStringRecognizer : Recognizer {
        public static JsonStringRecognizer Only = new JsonStringRecognizer();

        // The real character, to the escape codes. Example: character '\b' is represented by a 'b' after the escape
        private static Map<char, char> escapableCharactersToCodes;

        // The escape codes, to the real characters. Example: character 'b' after a '\' escape character means character '\b'
        private static Map<char, char> escapeCodesToCharacters;

        static JsonStringRecognizer() {
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

        public JsonStringRecognizer() : base("string") { }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            long i;
            RecognitionTree tree;
            bool result;
            using (Merrigan0.Test.Begin("empty string")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"\"", ref i, out tree);
                Testing.Test("returns true", result);
                Testing.TestEquals(i, 2);
                Testing.TestEquals(tree.Data, String.Empty);
            }

            using (Merrigan0.Test.Begin("invalid string")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"", ref i, out tree);
                Testing.Test("returns false", !result);
                Testing.TestEquals(i, 0);
                Testing.TestNull(tree);
            }

            using (Merrigan0.Test.Begin("no string")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("", ref i, out tree);
                Testing.Test("returns false", !result);
                Testing.TestEquals(i, 0);
                Testing.TestNull(tree);
            }

            using (Merrigan0.Test.Begin("no string")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead(" ", ref i, out tree);
                Testing.Test("returns false", !result);
                Testing.TestEquals(i, 0);
                Testing.TestNull(tree);
            }

            using (Merrigan0.Test.Begin("single char")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"a\"", ref i, out tree);
                Testing.Test("returns true", result);
                Testing.TestEquals(i, 3);
                Testing.TestEquals(tree.Data, (String)"a");
            }

            using (Merrigan0.Test.Begin("single escaped char")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"\\\"\"", ref i, out tree);
                Testing.Test("returns true", result);
                Testing.TestEquals(i, 4);
                Testing.TestEquals(tree.Data, (String)"\"");
            }

            using (Merrigan0.Test.Begin("single escaped Unicode")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"\\u1111\"", ref i, out tree);
                Testing.Test("returns true", result);
                Testing.TestEquals(i, 8);
                Testing.TestEquals(tree.Data, (String)"\u1111");
            }

            using (Merrigan0.Test.Begin("escaped char after multiple unescaped chars")) {
                i = 0;
                result = JsonStringRecognizer.Only.TryRead("\"abc\\\\def\"", ref i, out tree);
                Testing.Test("returns true", result);
                Testing.TestEquals(i, 10);
                Testing.TestEquals(tree.Data, (String)"abc\\def");
            }
        }

        // Default behavior is to use the sequential, step-by-step recognition
        //[Example(" \"ab\"c\\\\", 1, "..Data == \"ab\"c\\\"")]
        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            if (i >= s.Length) { goto fail; }
            char ch = s[i];
            if (ch != '"') { goto fail; }
            long iToTry = i;
            ++iToTry;

            // If no escape characters are found, we will never use this mutable string. Once
            // we find at least one, though, we move over to adding each new character to it
            MutableString sSoFar = null; 
            while (true) {
                if (iToTry >= s.Length) { goto fail; }
                ch = s[iToTry];
                if (ch == '"') {
                    ++iToTry;
                    break;
                }
                if (ch == '\\') {
                    // Create a mutable string if called for
                    if (sSoFar == null) {
                        sSoFar = new MutableString(s.Substring(i + 1, iToTry - (i + 1)));
                    }
                    
                    // Do whole escape sequence
                    ++iToTry;
                    if (iToTry >= s.Length) { goto fail; }
                    ch = s[iToTry];

                    // If it's a "u" thing, parse that whole code
                    if (ch == 'u') {
                        ++iToTry;
                        long iEndOfCode = iToTry + 4;
                        if (iEndOfCode >= s.Length) { goto fail; }
                        int chSoFar = 0;
                        while (iToTry < iEndOfCode) {
                            chSoFar = chSoFar * 16 + Hexadecimal.Number(s[iToTry]);
                            ++iToTry;
                        }
                        sSoFar.Append((char)chSoFar);
                    } else {
                        sSoFar.Append(escapeCodesToCharacters[ch]);
                        ++iToTry;
                    }
                } else {
                    if (sSoFar != null) {
                        sSoFar.Append(ch);
                    }
                    ++iToTry;
                }
            }
            String result;
            if (sSoFar == null) {
                // Everything between quotes
                result = s.Substring(i + 1, iToTry - (i + 1) - 1);
            } else {
                result = sSoFar.Current;
            }
            tree = new RecognitionTree(this, s, iToTry, iToTry - i, result);
            i = iToTry;
            return true;

        fail:
            tree = null;
            return false;
        }
    }

    [Untested]
    public class JsonNumberRecognizer : SequenceRecognizer {
        public static JsonNumberRecognizer Only = new JsonNumberRecognizer();


        protected JsonNumberRecognizer() : base(
            "number",
                    new OptionalRecognizer(new SpecificCharacterRecognizer('-')),
            //MinimalWholeNumberRecognizer.Only,
                    new OrRecognizer(
                        "int",
                        new SpecificCharacterRecognizer('0', rt => 0),
                        new SequenceRecognizer(
                            new NonzeroDigitRecognizer(),
                            new AnyRepetitionsRecognizer(new DigitRecognizer()))),
                    new OptionalRecognizer(new SequenceRecognizer(
                        "frac",
                        new SpecificCharacterRecognizer('.'),
            //DecimalPartRecognizer.Only,
                    new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9')))),
                    new OptionalRecognizer(new SequenceRecognizer(
                        "exp",
                        new OrRecognizer(
                            new SpecificCharacterRecognizer('e'),
                            new SpecificCharacterRecognizer('E')),
                        new OrRecognizer(
                            new SpecificCharacterRecognizer('+'),
                            new SpecificCharacterRecognizer('-')),
                        new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9'))))
) { }

        [DiagnosticOnly]
        [Test]
        private new static void Test() {

        }
    }

    //// A representation of a full piece of JSON, representing one value, which lazily can provide
    //// that value or JSON objects for its children.
    //// RFC-8259 https://datatracker.ietf.org/doc/html/rfc8259
    //[Untested]
    //public class JsonRecognizer : String {
    //    public static String Colon = ":";
    //    public static String Comma = ",";
    //    public static String False = "false";
    //    public static String Null = "null";
    //    public static JsonRecognizer Only = new JsonRecognizer();
    //    public static String True = "true";
    //    public static String Quote = "\"";

    //    // https://datatracker.ietf.org/doc/html/rfc8259#section-3
    //    public static OrRecognizer JsonValueRecognizer = new OrRecognizer(
    //        "value",
    //        Array<Recognizer>.From(
    //            new SpecificStringRecognizer("false", False, rt => false),
    //            new SpecificStringRecognizer("null", Null, rt => null),
    //            new SpecificStringRecognizer("true", True, rt => true),

    //            // object = begin-object [ member *( value-separator member ) ] end-object
    //            new SequenceRecognizer(
    //                "object",
    //                Array<Recognizer>.From(
    //                    new NamedRecognizer("begin-object"),
    //                    new OptionalRecognizer(new SequenceRecognizer(
    //                        new NamedRecognizer("member"),
    //                        new AnyRepetitionsRecognizer(new SequenceRecognizer(
    //                            new NamedRecognizer("value-separator"),
    //                            new NamedRecognizer("member"))))),
    //                    new NamedRecognizer("end-object")),
    //        // member = string name-separator value
    //                Array<Recognizer>.From(
    //                    new SequenceRecognizer(
    //                        "member",
    //                        new NamedRecognizer("string"),
    //                        new NamedRecognizer("name-separator"),
    //                        new NamedRecognizer("value"))),
    //                rt => {
    //                    MutableMap<String, object> membersSoFar = new MutableMap<String, object>();
    //                    RecognitionTree membersTree = rt.Children[1];
    //                    if (membersTree.Children.Length > 0) {
    //                        RecognitionTree firstMemberTree = membersTree.Children[0];
    //                        membersSoFar.Add((String)firstMemberTree.Children[0].Data, firstMemberTree.Children[2].Data);
    //                        RecognitionTree otherMembersTree = membersTree.Children[1];
    //                        foreach (RecognitionTree separatedMemberTree in otherMembersTree.Children) {
    //                            RecognitionTree memberTree = separatedMemberTree.Children[1];
    //                            membersSoFar.Add((String)memberTree.Children[0].Data, memberTree.Children[2].Data);
    //                        }
    //                    }
    //                    return Glom.From(membersSoFar.Current);
    //                }),

    //            // array = begin-array [ value *( value-separator value ) ] end-array
    //            new SequenceRecognizer(
    //                "array",
    //                Array<Recognizer>.From(
    //                    new NamedRecognizer("begin-array"),
    //                    new OptionalRecognizer(new SequenceRecognizer(
    //                        new NamedRecognizer("value"),
    //                        new AnyRepetitionsRecognizer(new SequenceRecognizer(
    //                            new NamedRecognizer("value-separator"),
    //                            new NamedRecognizer("value"))))),
    //                    new NamedRecognizer("end-array")),
    //                null,
    //                rt => {
    //                    MutableArray<object> valuesSoFar = new MutableArray<object>();
    //                    RecognitionTree valuesTree = rt.Children[1];
    //                    if (valuesTree.Children.Length > 0) {
    //                        RecognitionTree firstValueTree = valuesTree.Children[0];
    //                        valuesSoFar.Append(firstValueTree.Data);
    //                        RecognitionTree otherValuesTree = valuesTree.Children[1];
    //                        foreach (RecognitionTree separatedValuesTree in otherValuesTree.Children) {
    //                            valuesSoFar.Append(separatedValuesTree.Children[1]);
    //                        }
    //                    }
    //                    return Glom.From(valuesSoFar.Current);
    //                }),

    //            // number = [ minus ] int [ frac ] [ exp ]
    //            new SequenceRecognizer(
    //                "number",
    //                new OptionalRecognizer(new SpecificCharacterRecognizer('-')),
    //                new OrRecognizer(
    //                    "int",
    //                    new SpecificCharacterRecognizer('0', rt => 0),
    //                    new SequenceRecognizer(
    //                        new NonzeroDigitRecognizer(),
    //                        new AnyRepetitionsRecognizer(new DigitRecognizer()))),
    //                new OptionalRecognizer(new SequenceRecognizer(
    //                    "frac",
    //                    new SpecificCharacterRecognizer('.'),
    //                    new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9')))),
    //                new OptionalRecognizer(new SequenceRecognizer(
    //                    "exp",
    //                    new OrRecognizer(
    //                        new SpecificCharacterRecognizer('e'),
    //                        new SpecificCharacterRecognizer('E')),
    //                    new OrRecognizer(
    //                        new SpecificCharacterRecognizer('+'),
    //                        new SpecificCharacterRecognizer('-')),
    //                    new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9'))))),

    //            // quotation-mark *char quotation-mark
    //            new SequenceRecognizer(
    //                "string",
    //                new SpecificCharacterRecognizer('"'),
    //                new AnyRepetitionsRecognizer(new OrRecognizer(
    //                    "char",
    //                    new OrRecognizer(
    //                        "unescaped",
    //                        new CharacterRangeRecognizer('\u0020', '\u0021'), // ' ', '!' (skip '"')
    //                        new CharacterRangeRecognizer('\u0023', '\u005B'), // '#', '[' (skip '\')
    //                        new CharacterRangeRecognizer('\u005D', '\uFFFF')), // '~'... (every other character
    //                    new OrRecognizer(
    //                        "escaped",
    //                        new SpecificStringRecognizer("\\\"", rt => "\""),
    //                        new SpecificStringRecognizer("\\\\", rt => "\\"),
    //                        new SpecificStringRecognizer("\\/", rt => "\/"),
    //                        new SpecificStringRecognizer("\\b", rt => "\b"),
    //                        new SpecificStringRecognizer("\\f", rt => "\f"),
    //                        new SpecificStringRecognizer("\\n", rt => "\n"),
    //                        new SpecificStringRecognizer("\\r", rt => "\r"),
    //                        new SpecificStringRecognizer("\\t", rt => "\t"),
    //                        new SequenceRecognizer(
    //                            new SpecificStringRecognizer("\\u"),
    //                            new RepetitionsRangeRecognizer(new NamedRecognizer("hex-digit"), 4, 4)),
    //                            rt => {
    //                                char ch;
    //                                return ch;
    //                            }))),
    //                new SpecificCharacterRecognizer('"'))),
    //        Array<Recognizer>.From(
    //            new SequenceRecognizer(
    //                "begin-array",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer('['),
    //                new NamedRecognizer("ws")),
    //            new SequenceRecognizer(
    //                "begin-object",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer('{'),
    //                new NamedRecognizer("ws")),
    //            new SequenceRecognizer(
    //                "end-array",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer(']'),
    //                new NamedRecognizer("ws")),
    //            new SequenceRecognizer(
    //                "end-object",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer('}'),
    //                new NamedRecognizer("ws")),
    //            new SequenceRecognizer(
    //                "name-separator",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer(':'),
    //                new NamedRecognizer("ws")),
    //            new SequenceRecognizer(
    //                "value-separator",
    //                new NamedRecognizer("ws"),
    //                new SpecificCharacterRecognizer(','),
    //                new NamedRecognizer("ws")),
    //            new CharacterSetRecognizer("ws", ' ', '\t', '\n', '\r'))
    //        );

    //    //private static HierarchicalRecognizer jsonRecognizer;
        
    //    //static JsonRecognizer() {
    //    //   Only = new JsonRecognizer();
    //    //}

    //    private static Set<char> whitespaceCharacters = Set<char>.From(' ', '\t', '\r', '\n');

    //    //private Array<Json> children;
    //    private String contents;
    //    private object value;

    //    public Array<Json> Children {
    //        get {
    //            throw new NotImplementedException();
    //            ////if (children == null) {
    //            ////    object value = Json.Parse(contents, out children);
    //            ////    if (this.value == null) {
    //            ////        this.value = value;
    //            ////    }
    //            ////}
    //            ////return children;
    //        }
    //    }

    //    public override long Length { get { return contents.Length; } }

    //    public object Value {
    //        get {
    //            if (value == null) {
    //                value = Json.Parse(contents);
    //            }
    //            return value;
    //        }
    //    }

    //    static Json() {
    //    }

    //    public Json(String contents, object value = null) {
    //        this.contents = contents;
    //        this.value = value;
    //    }

    //    // Simplest: turn the object into a string
    //    public static void Append<T>(MutableString ms, T value) {
    //        Append(ms, (object)value, typeof(T));
    //    }

    //    public static void Append(MutableString ms, bool b) {
    //        ms.Append(b ? True : False);
    //    }

    //    public static void Append(MutableString ms, decimal d) {
    //        ms.Append(d.ToString());
    //    }

    //    public static void Append(MutableString ms, sbyte sb) {
    //        ms.Append(sb.ToString());
    //    }

    //    public static void Append(MutableString ms, byte b) {
    //        ms.Append(b.ToString());
    //    }

    //    public static void Append(MutableString ms, short sh) {
    //        ms.Append(sh.ToString());
    //    }

    //    public static void Append(MutableString ms, ushort ush) {
    //        ms.Append(ush.ToString());
    //    }

    //    public static void Append(MutableString ms, int n) {
    //        ms.Append(n.ToString());
    //    }

    //    public static void Append(MutableString ms, uint u) {
    //        ms.Append(u.ToString());
    //    }

    //    public static void Append(MutableString ms, long l) {
    //        ms.Append(l.ToString());
    //    }

    //    public static void Append(MutableString ms, ulong ul) {
    //        ms.Append(ul.ToString());
    //    }

    //    public static void Append(MutableString ms, float f) {
    //        ms.Append(f.ToString());
    //    }

    //    public static void Append(MutableString ms, double r) {
    //        ms.Append(r.ToString());
    //    }

    //    public static void Append(MutableString ms, char ch) {
    //        ms.Append(Quote);
    //        AppendEscaped(ms, ch);
    //        ms.Append(Quote);
    //    }

    //    public static void Append(MutableString ms, string s) {
    //        if (s == null) {
    //            ms.Append(Null);
    //        } else {
    //            ms.Append(Quote);
    //            AppendEscaped(ms, s);
    //            ms.Append(Quote);
    //        }
    //    }

    //    public static void Append(MutableString ms, String s) {
    //        if (s == null) {
    //            ms.Append(Null);
    //        } else {
    //            ms.Append(Quote);
    //            AppendEscaped(ms, s);
    //            ms.Append(Quote);
    //        }
    //    }

    //    public static void Append(MutableString ms, Guid guid) {
    //        ms.Append(guid.ToString());
    //    }

    //    public static void Append(MutableString ms, DateTime time) {
    //        ms.Append(time.ToString("Z"));
    //    }

    //    public static void Append(MutableString ms, DateTimeOffset time) {
    //        ms.Append(time.ToString("Z"));
    //    }

    //    public static void Append<T>(MutableString ms, Nullable<T> nullable) where T : struct {
    //        if (nullable.HasValue) {
    //            Append(ms, nullable.Value);
    //        } else {
    //            ms.Append(Null);
    //        }
    //    }

    //    //// Override in writers
    //    public static void Append(MutableString ms, object o) {
    //        if (o == null) {
    //            ms.Append(Null);
    //        } else {
    //            Append(ms, o, o.GetType());
    //        }
    //    }

    //    // Accommodate
    //    //      enums
    //    //      Nullables
    //    //      reference types
    //    //// Override in writers
    //    public static void Append(MutableString ms, object o, Type type) {
    //        // Is it null?
    //        if (o == null) {
    //            ms.Append(Null);
    //        }

    //        // Is it a value type? (simple types, well-known structs)
    //        if (type.IsValueType) {
    //            // Most common first
    //            //// replace with dictionary
    //            if (type == typeof(int)) {
    //                Append(ms, (int)o);
    //            } else if (type == typeof(bool)) {
    //                Append(ms, (bool)o);
    //            } else if (type == typeof(long)) {
    //                Append(ms, (long)o);
    //            } else if (type.IsEnum) {
    //                Append(ms, o, Enum.GetUnderlyingType(type));
    //            } else if (type == typeof(decimal)) {
    //                Append(ms, (decimal)o);
    //            } else if (type == typeof(sbyte)) {
    //                Append(ms, (sbyte)o);
    //            } else if (type == typeof(byte)) {
    //                Append(ms, (byte)o);
    //            } else if (type == typeof(short)) {
    //                Append(ms, (short)o);
    //            } else if (type == typeof(ushort)) {
    //                Append(ms, (ushort)o);
    //            } else if (type == typeof(uint)) {
    //                Append(ms, (uint)o);
    //            } else if (type == typeof(ulong)) {
    //                Append(ms, (ulong)o);
    //            } else if (type == typeof(float)) {
    //                Append(ms, (float)o);
    //            } else if (type == typeof(ulong)) {
    //                Append(ms, (ulong)o);
    //            } else if (type == typeof(char)) {
    //                Append(ms, (char)o);
    //            } else if (type == typeof(DateTime)) {
    //                Append(ms, (DateTime)o);
    //            } else if (type == typeof(DateTimeOffset)) {
    //                Append(ms, (DateTimeOffset)o);
    //            } else if (type == typeof(Guid)) {
    //                Append(ms, (Guid)o);
    //            } else {
    //                // If it's nullable, re-call this method with the basic type
    //                Type underlyingType;
    //                if (Utilities.IsNullable(type, out underlyingType)) {
    //                    object underlyingValue = Utilities.GetNullableValue(o);
    //                    if (underlyingValue == null) {
    //                        ms.Append(Null);
    //                    }
    //                    Append(ms, underlyingValue, underlyingType);
    //                }

    //                // Otherwise it's a struct and should be done as an object
    //                AppendObject(ms, o, type);
    //            }
    //        } else {
    //            // It's a reference type: String, string, or non-simple class
    //            if (type == typeof(String)) {
    //                Append(ms, (String)o);
    //            } else if (type == typeof(string)) {
    //                Append(ms, (string)o);
    //            } else {
    //                AppendObject(ms, o, type);
    //            }
    //        }
    //    }

    //    public static void AppendEscaped(MutableString ms, char ch) {
    //        if (ch == '\\' || ch == '"') {
    //            ms.Append('\\');
    //            ms.Append(ch);
    //        }
    //    }

    //    [WhatItDoes("Appends a string that is the original text, but with the quote backslash characters prefixed by a backslash")]
    //    public static void AppendEscaped(MutableString ms, String s) {
    //        int i = 0;
    //        while (true) {
    //            if (i >= s.Length) {
    //                break;
    //            }
    //            if (s[i] == '\\' || s[i] == '"') {
    //                ms.Append('\\');
    //                ++i;
    //            }
    //            ms.Append(s[i]);
    //            ++i;
    //        }
    //    }

    //    // Appends a string surrounded with { }
    //    //// Needs to be owned by some strategy object like a JsonWriter
    //    public static void AppendObject(MutableString ms, object o, Type type) {
    //        Map<String, object> map;
    //        if (typeof(Map<String, object>).IsAssignableFrom(type)) {
    //            map = (Map<String, object>)o;
    //        } else {
    //            //// Also see if it's any Map<{string|String}, [any]> or IDictionary<{string|String, [any]> or IReadOnlyDictionary<...>
    //            map = MapFromObject(o, type);
    //        }

    //        ms.Append("{");
    //        bool first = true;
    //        foreach (String key in map.Domain.Sorted()) {
    //            if (first) {
    //                first = false;
    //            } else {
    //                ms.Append(",");
    //            }
    //            ms.Append("\"");
    //            AppendEscaped(ms, key);
    //            ms.Append("\":");
    //            Append(ms, map[key]);
    //        }
    //        ms.Append("}");
    //    }

    //    //        Type type = o.GetType();
    //    //        if (type == typeof(bool)) {
    //    //            s.Append((bool)o ? "true" : "false");
    //    //        } else if (type == typeof(string)) {
    //    //            s.Append("\"");
    //    //            s.Append(Text.Escape((string)o));
    //    //            s.Append("\"");
    //    //        } else if (type.IsValueType) {
    //    //            if (type == typeof(DateTime)) {
    //    //                s.Append(((DateTime)o).ToString("Z"));
    //    //            } else {
    //    //                s.Append(o.ToString());
    //    //            }
    //    //        } else {
    //    //            if (type.IsArray) {
    //    //                s.Append("[");
    //    //                bool first = true;
    //    //                foreach (object item in (IEnumerable)o) {
    //    //                    if (first) {
    //    //                        first = false;
    //    //                    } else {
    //    //                        s.Append(",");
    //    //                    }
    //    //                    Append(s, item);
    //    //                }
    //    //                s.Append("]");
    //    //            } else if (type == typeof(Node)) {
    //    //                s.Append("{");
    //    //                bool first = true;
    //    //                foreach (KeyValuePair<String, Node> pair in ((Node)o).Sorted((p1, p2) => string.Compare(p1.Key, p2.Key))) {
    //    //                    if (first) {
    //    //                        first = false;
    //    //                    } else {
    //    //                        s.Append(",");
    //    //                    }
    //    //                    s.Append("\"");
    //    //                    s.Append(pair.Key);
    //    //                    s.Append("\":");
    //    //                    Append(s, pair.Value);
    //    //                }
    //    //                s.Append("}");
    //    //            } else {
    //    //                throw new Exception("BLAH");
    //    //            }
    //    //        }
    //    //    }
    //    //}

    //    public static new Json From(object o) {
    //        MutableString ms = new MutableString();
    //        Append(ms, o);
    //        return new Json(ms.Current);
    //    }

    //    //public static RecognitionTree Parse(String json) { ////, out Array<Json> children) {

    //    //    if (!JsonValueRecognizer.TryRecognize(json, out tree)) {
    //    //        throw new Exception("Invalid JSON.");
    //    //    }
    //    //    return 
    //    //}

    //    // Takes apart the string and remembers where it found what
    //    public static object Parse(String json) { ////, out Array<Json> children) {
    //        //// 
    //        long i = 0;
    //        object value = ReadValue(json, i, out i);
    //        i = SkipWhitespace(json, i);
    //        if (i != json.Length) {
    //            throw new Exception("Invalid JSON.");
    //        }
    //        return value;
    //    }

    //    ////public static object Parse(String json) {
    //    ////    long i = 0;
    //    ////    object value = ReadValue(json, i, out i);
    //    ////    i = SkipWhitespace(json, i);
    //    ////    if (i != json.Length) {
    //    ////        throw new Exception("Invalid JSON.");
    //    ////    }
    //    ////    return value;
    //    ////}

    //    // Reads a value of any kind: string, number, object, array, Boolean, null
    //    public static object ReadValue(String json, long i, out long iNew) {
    //        i = SkipWhitespace(json, i);
    //        object value;
    //        String s;
    //        double r;
    //        bool b;
    //        Map<String, object> node;
    //        Array<object> array;
    //        if (TryReadString(json, i, out s, out iNew)) {
    //            value = s;
    //        } else if (TryReadNumber(json, i, out r, out iNew)) {
    //            value = r;
    //        } else if (TryReadBoolean(json, i, out b, out iNew)) {
    //            value = b;
    //        } else if (TryReadNull(json, i, out iNew)) {
    //            value = null;
    //        } else if (TryReadObject(json, i, out node, out iNew)) {
    //            value = node;
    //        } else if (TryReadArray(json, i, out array, out iNew)) {
    //            value = array;
    //        } else {
    //            throw new Exception(System.String.Format("No value found in JSON starting at character {0}.", i));
    //        }
    //        return value;
    //    }

    //    ////// Reads the next value
    //    ////public static Node Read(TextReader reader, out Json json) {
    //    ////    Node nodeRead;

    //    ////    // Skip whitespace
    //    ////    SkipWhitespace(reader);

    //    ////    // Read next: null, true, false, <number>, ", {, [
    //    ////    int ch = reader.Peek();
    //    ////    if (ch < 0) {
    //    ////        throw new Exception("No value was found.");
    //    ////    }
    //    ////    if (ch == '"') {
    //    ////        nodeRead = ReadString(reader).ToNode();
    //    ////    } else if (ch == '{') {
    //    ////        return ReadNode(reader);
    //    ////    } else if (ch == '[') {
    //    ////        return ReadArrayNode(reader);
    //    ////    } else {
    //    ////        // Get the next word
    //    ////        List<char> charactersSoFar = new List<char>();
    //    ////        while (true) {
    //    ////            ch = reader.Peek();
    //    ////            if (ch < 0 ||
    //    ////                ch == ',' ||
    //    ////                ch == '}' ||
    //    ////                Char.IsWhiteSpace((char)ch)) {
    //    ////                break;
    //    ////            }
    //    ////            ch = reader.Read();
    //    ////            charactersSoFar.Add((char)ch);
    //    ////        }
    //    ////        string word = new string(charactersSoFar.ToArray());
    //    ////        string wordToLowerTrimmed = word.ToLowerInvariant().Trim();
    //    ////        if (wordToLowerTrimmed == "null") {
    //    ////            return NullNode.Only;
    //    ////        } else if (wordToLowerTrimmed == "true") {
    //    ////            return ObjectNode.True;
    //    ////        } else if (wordToLowerTrimmed == "false") {
    //    ////            return ObjectNode.False;
    //    ////        } else {
    //    ////            return new ObjectNode(Double.Parse(word));
    //    ////        }
    //    ////    }

    //    ////    children = childrenSoFar.Current;
    //    ////}

    //    //public static String ReadString(TextReader reader) {
    //    //    int ch = reader.Read();
    //    //    if (ch != '"') {
    //    //        throw new Exception("Bad parameter.");
    //    //    }
    //    //    List<char> charactersSoFar = new List<char>();
    //    //    while (true) {
    //    //        ch = reader.Read();
    //    //        if (ch < 0)
    //    //            throw new Exception("Unmatched quotation mark.");
    //    //        if (ch == '\\') {
    //    //            ch = reader.Read();
    //    //            if (ch < 0) {
    //    //                throw new Exception("Unfinished escape sequence.");
    //    //            }
    //    //        } else if (ch == '"')
    //    //            break;
    //    //        charactersSoFar.Add((char)ch);
    //    //    }
    //    //    return String.From(charactersSoFar.ToArray());
    //    //}

    //    ////public static Node ReadNode(TextReader reader) {
    //    ////    int ch = reader.Read();
    //    ////    if (ch != '{') {
    //    ////        throw new Exception("Bad parameter.");
    //    ////    }
    //    ////    MutableNode mapSoFar = new MutableNode();
    //    ////    while (true) {
    //    ////        SkipWhitespace(reader);
    //    ////        string name = ReadString(reader);
    //    ////        SkipWhitespace(reader);
    //    ////        ch = reader.Peek();
    //    ////        if (ch != ':') {
    //    ////            throw new Exception("Expected a colon after the property name.");
    //    ////        }
    //    ////        ch = reader.Read();
    //    ////        SkipWhitespace(reader);
    //    ////        object value = Read(reader);
    //    ////        mapSoFar.Set(name, value);
    //    ////        SkipWhitespace(reader);

    //    ////        // Check for end of the object
    //    ////        ch = reader.Read();
    //    ////        if (ch < 0) {
    //    ////            throw new Exception("Unfinished hash.");
    //    ////        }
    //    ////        if (ch == '}') {
    //    ////            break;
    //    ////        }
    //    ////        if (ch != ',') {
    //    ////            throw new Exception("Expected a comma.");
    //    ////        }
    //    ////    }
    //    ////    return mapSoFar.Current;
    //    ////}

    //    ////public static ArrayNode ReadArrayNode(TextReader reader) {
    //    ////    int ch = reader.Read();
    //    ////    if (ch != '[') {
    //    ////        throw new Exception("Bad parameter.");
    //    ////    }
    //    ////    List<object> objectsSoFar = new List<object>();
    //    ////    while (true) {
    //    ////        SkipWhitespace(reader);

    //    ////        // Check for end of array
    //    ////        if (reader.Peek() == ']') {
    //    ////            reader.Read();
    //    ////            break;
    //    ////        }

    //    ////        // If it's not the first, skip the comma
    //    ////        if (objectsSoFar.Count > 0) {
    //    ////            ch = reader.Read();
    //    ////            if (ch != ',') {
    //    ////                throw new Exception("Expected a comma between array values.");
    //    ////            }
    //    ////        }

    //    ////        object o = Read(reader);
    //    ////        objectsSoFar.Add(o);
    //    ////    }
    //    ////    return objectsSoFar.ToNodeArray();
    //    ////}

    //    [Example("\"\"", 0, 2, "")]
    //    [Example(" \"123\" ", 1, 6, "123")]
    //    [Example(" \"1\"3\" ", 1, 6, "1\"3")]
    //    public static String ReadString(
    //        [Equals("First", '\"')][Equals("Last", '\"')] String s,
    //        [Less("s.Length")] long i,
    //        [True(". >= 0 && . <= s.Length")] out long iNew) {
    //        String sRead;
    //        if (!TryReadString(s, i, out sRead, out iNew)) {
    //            throw new Exception("No string found.");
    //        }
    //        return sRead;
    //    }

    //    //// Converts the object to a Node, then to JSON
    //    //public static Json ToJson(this object value) {
    //    //    return value.ToJson();
    //    //}

    //    //public static void Test() {
    //    //}

    //    public override bool TryGetCharacter(long i, out char ch) {
    //        if (i >= Length) {
    //            ch = default(char);
    //            return false;
    //        }
    //        ch = contents[i];
    //        return true;
    //    }

    //    public static bool TryReadArray(String s, long i, out Array<object> array, out long iNew) {
    //        // End of overall string
    //        long length = s.Length;
    //        if (i >= length) {
    //            goto notfound;
    //        }

    //        // Doesn't start with left bracket
    //        if (s[i] != '[') {
    //            goto notfound;
    //        }

    //        MutableArray<object> arraySoFar = new MutableArray<object>();
    //        while (true) {
    //            i = SkipWhitespace(s, i);
    //            if (i >= length) {
    //                throw new Exception("No value found.");
    //            }
    //            object value = ReadValue(s, i, out i);
    //            arraySoFar.Append(value);
    //            i = SkipWhitespace(s, i);
    //            if (i >= length) {
    //                throw new Exception("No right bracket found.");
    //            }

    //            // Check for end of the array
    //            char ch = s[i];
    //            if (ch == ']') {
    //                ++i;
    //                break;
    //            }
    //            if (ch != ',') {
    //                throw new Exception("No ',' found.");
    //            }
    //        }
    //        array = arraySoFar.Current;
    //        iNew = i;
    //        return true;

    //    notfound:
    //        array = null;
    //        iNew = 0L;
    //        return false;
    //    }

    //    public static bool TryReadBoolean(String s, long i, out bool b, out long iNew) {
    //        if (String.TryRead(s, i, False, false, out iNew)) {
    //            b = false;
    //            return true;
    //        }
    //        if (String.TryRead(s, i, True, false, out iNew)) {
    //            b = true;
    //            return true;
    //        }
    //        b = false;
    //        iNew = 0;
    //        return false;
    //    }

    //    public static bool TryReadNull(String s, long i, out long iNew) {
    //        if (String.TryRead(s, i, Null, false, out iNew)) {
    //            return true;
    //        }
    //        iNew = 0;
    //        return false;
    //    }

    //    public static bool TryReadNumber(String s, long i, out double number, out long iNew) {
    //        ///// Eventually use own IEEE-754
    //        ////if (String.TryReadNumber(s, i, out number, out iNew)) {
    //        ////    return true;
    //        ////}
    //        // For now, get next word and parse that
    //        String word;
    //        if (TryReadWord(s, i, out word, out iNew)) {
    //            if (double.TryParse(word, out number)) {
    //                return true;
    //            }
    //        }
    //        number = 0.0;
    //        iNew = 0;
    //        return false;
    //    }

    //    // Must be queued up to the beginning of the map, i.e. not whitespace.
    //    public static bool TryReadObject(String s, long i, out Map<String, object> map, out long iNew) {
    //        // End of overall string
    //        long length = s.Length;
    //        if (i >= length) {
    //            goto notfound;
    //        }

    //        // Doesn't start with left brace
    //        if (s[i] != '{') {
    //            goto notfound;
    //        }

    //        MutableMap<String, object> mapSoFar = new MutableMap<String, object>();
    //        while (true) {
    //            i = SkipWhitespace(s, i);
    //            String name;
    //            if (!TryReadString(s, i, out name, out i)) {
    //                throw new Exception("No name found.");
    //            }
    //            i = SkipWhitespace(s, i);
    //            if (i >= length) {
    //                throw new Exception("No ':' found.");
    //            }
    //            i = String.Skip(s, i, ':');
    //            i = SkipWhitespace(s, i);
    //            if (i >= length) {
    //                throw new Exception("No value found.");
    //            }
    //            object value = ReadValue(s, i, out i);
    //            i = SkipWhitespace(s, i);
    //            mapSoFar.Add(name, value);

    //            // Check for end of the object
    //            char ch = s[i];
    //            if (ch == '}') {
    //                ++i;
    //                break;
    //            }
    //            if (ch != ',') {
    //                throw new Exception("No ',' found.");
    //            }
    //        }
    //        map = mapSoFar.Current;
    //        iNew = i;
    //        return true;

    //    notfound:
    //        map = null;
    //        iNew = 0L;
    //        return false;
    //    }

    //    public static bool TryReadString(String s, long i, out String sRead, out long iNew) {
    //        // End of overall string
    //        long length = s.Length;
    //        if (i >= length) {
    //            goto notfound;
    //        }

    //        // Doesn't start with quote character
    //        if (s[i] != '\"') {
    //            goto notfound;
    //        }

    //        // Started with quote. Move on to the next
    //        ++i;
    //        MutableString readSoFar = new MutableString();
    //        while (true) {
    //            // End of overall string
    //            if (i >= length) {
    //                throw new Exception("Unmatched quotation mark.");
    //            }

    //            // End of string value
    //            char ch = s[i];
    //            if (ch == '"') {
    //                ++i;
    //                break;
    //            }
    //            if (ch == '\\') {
    //                ++i;
    //                if (i >= length) {
    //                    throw new Exception("Unfinished escape sequence.");
    //                }
    //                ch = s[i];
    //                char properCharacter;
    //                if (escapeCodesToCharacters.TryGetValue(ch, out properCharacter)) {
    //                    readSoFar.Append(properCharacter);
    //                } else if (ch == 'u') {
    //                    ++i;
    //                    char unicode = ReadUnicode(s, i, out i);
    //                    readSoFar.Append(unicode);
    //                } else {
    //                    throw new Exception("Illegal escape sequence.");
    //                }
    //            } else {
    //                readSoFar.Append(ch);
    //                ++i;
    //            }
    //        }
    //        sRead = readSoFar.Current;
    //        iNew = i;
    //        return true;

    //    notfound:
    //        sRead = null;
    //        iNew = 0L;
    //        return false;
    //    }

    //    ////public static bool TryReadString(String s, long i, out Node map, out long iNew) {
    //    ////    String sRead;
    //    ////    if (TryReadString(s, i, out sRead, out iNew)) {
    //    ////        map = sRead.ToNode();
    //    ////        return true;
    //    ////    }
    //    ////    map = null;
    //    ////    iNew = 0;
    //    ////    return false;
    //    ////}

    //    public static void WriteFile(string path, object o) {
    //        System.IO.File.WriteAllText(path, Json.From(o));
    //    }

    //    public override string ToString() {
    //        return contents.ToString();
    //    }

    //    protected static Map<String, object> MapFromObject(object o, Type type) {
    //        MutableMap<String, object> mapSoFar = new MutableMap<String,object>();

    //        // Add public properties that are settable and gettable
    //        foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
    //            if (propertyInfo.GetGetMethod() != null && propertyInfo.GetSetMethod() != null) {
    //                mapSoFar.Add(propertyInfo.Name, propertyInfo.GetValue(o, null));
    //            }
    //        }

    //        // Add public fields that aren't calculated
    //        foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)) {
    //            mapSoFar.Add(fieldInfo.Name, fieldInfo.GetValue(o));
    //        }

    //        return mapSoFar.Current;
    //    }

    //    protected static char ReadUnicode(String s, long i, out long iNew) {
    //        if (i >= s.Length - 4) {
    //            throw new Exception("Unfinished Unicode value.");
    //        }
    //        int unicode = adecimal.ToNumber(s[i]);
    //        ++i;
    //        unicode <<= 4;
    //        unicode |= adecimal.ToNumber(s[i]);
    //        ++i;
    //        unicode <<= 4;
    //        unicode |= adecimal.ToNumber(s[i]);
    //        ++i;
    //        unicode <<= 4;
    //        unicode |= adecimal.ToNumber(s[i]);
    //        ++i;
    //        iNew = i;
    //        return (char)unicode;
    //    }

    //    // Different than String.SkipWhitespace because it uses only JSON-legal whitespace.
    //    protected static new long SkipWhitespace(String s, long i) {
    //        long length = s.Length;
    //        while (i < length) {
    //            if (whitespaceCharacters.Contains(s[i])) {
    //                break;
    //            }
    //            ++i;
    //        }
    //        return i;
    //    }

    //    // Reads to just past the word that starts at the Current location. Sets word to the 
    //    // characters preceding that and returns true. If the reader is at the end of the s, returns false.
    //    // If characters are found at the end of s without a newline, those characters are returned.
    //    protected static bool TryReadWord(String s, long i, out String word, out long iNew) {
    //        long length = s.Length;
    //        iNew = i;
    //        while (true) {
    //            if (iNew >= length || whitespaceCharacters.Contains(s[iNew])) {
    //                break;
    //            }
    //            ++iNew;
    //        }
    //        if (iNew == i) {
    //            word = null;
    //            return false;
    //        }
    //        word = s.Substring(i, iNew - i);
    //        return true;
    //    }

    //    //protected override char GetCharacter(long i) {
    //    //    return contents[i];
    //    //}
    //}

    //public static partial class ObjectExtensions {
    //    public static Json ToJson(this object o) {
    //        MutableString s = new MutableString();
    //        Json.Append(s, o);
    //        return new Json(s.Current);
    //    }
    //}


    //public partial class Node {
    //    ////public static Node FromJson(String json) {
    //    ////    return NullNode.Only; //////
    //    ////}

    //    public Json ToJson(this Node node, bool pretty = true) {
    //        MutableString jsonSoFar = new MutableString();
    //        node.AppendJson(jsonSoFar, true);
    //        return new Json(jsonSoFar.Current, node);
    //    }

    //    public virtual void AppendJson(MutableString json, bool pretty);
    //}

    //public partial class ArrayNode {
    //    public override void AppendJson(MutableString json, bool pretty) {
    //        json.Append("[");
    //        bool first = true;
    //        foreach (Node item in this) {
    //            if (first) {
    //                first = false;
    //            } else {
    //                json.Append(",");
    //            }
    //            item.AppendJson(json);
    //        }
    //        json.Append("]");
    //    }
    //}

    //public partial class File {
    //    // Takes a JSON file and creates a representation of it
    //    public static Json ReadJson(String path) {
    //        using (StreamReader reader = new StreamReader(path)) {
    //            return Json.From(String.From(reader));
    //        }
    //    }
    //}

    //public class PrettyJson : Json {
    //    public static new Json From(object o) {
    //        Json json = new AppendJson(o).DoAll();
    //        return json;
    //    }
    //}


    //// https://datatracker.ietf.org/doc/html/rfc8259#section-3
    //public static OrRecognizer JsonValueRecognizer = new OrRecognizer(
    //    "value",
    //    Array<Recognizer>.From(
    //        new SpecificStringRecognizer("false", False, rt => false),
    //        new SpecificStringRecognizer("null", Null, rt => null),
    //        new SpecificStringRecognizer("true", True, rt => true),

    //        // object = begin-object [ member *( value-separator member ) ] end-object
    //        new SequenceRecognizer(
    //            "object",
    //            Array<Recognizer>.From(
    //                new NamedRecognizer("begin-object"),
    //                new OptionalRecognizer(new SequenceRecognizer(
    //                    new NamedRecognizer("member"),
    //                    new AnyRepetitionsRecognizer(new SequenceRecognizer(
    //                        new NamedRecognizer("value-separator"),
    //                        new NamedRecognizer("member"))))),
    //                new NamedRecognizer("end-object")),
    //    // member = string name-separator value
    //            Array<Recognizer>.From(
    //                new SequenceRecognizer(
    //                    "member",
    //                    new NamedRecognizer("string"),
    //                    new NamedRecognizer("name-separator"),
    //                    new NamedRecognizer("value"))),
    //            rt => {
    //                MutableMap<String, object> membersSoFar = new MutableMap<String, object>();
    //                RecognitionTree membersTree = rt.Children[1];
    //                if (membersTree.Children.Length > 0) {
    //                    RecognitionTree firstMemberTree = membersTree.Children[0];
    //                    membersSoFar.Add((String)firstMemberTree.Children[0].Data, firstMemberTree.Children[2].Data);
    //                    RecognitionTree otherMembersTree = membersTree.Children[1];
    //                    foreach (RecognitionTree separatedMemberTree in otherMembersTree.Children) {
    //                        RecognitionTree memberTree = separatedMemberTree.Children[1];
    //                        membersSoFar.Add((String)memberTree.Children[0].Data, memberTree.Children[2].Data);
    //                    }
    //                }
    //                return Glom.From(membersSoFar.Current);
    //            }),

    //        // array = begin-array [ value *( value-separator value ) ] end-array
    //        new SequenceRecognizer(
    //            "array",
    //            Array<Recognizer>.From(
    //                new NamedRecognizer("begin-array"),
    //                new OptionalRecognizer(new SequenceRecognizer(
    //                    new NamedRecognizer("value"),
    //                    new AnyRepetitionsRecognizer(new SequenceRecognizer(
    //                        new NamedRecognizer("value-separator"),
    //                        new NamedRecognizer("value"))))),
    //                new NamedRecognizer("end-array")),
    //            null,
    //            rt => {
    //                MutableArray<object> valuesSoFar = new MutableArray<object>();
    //                RecognitionTree valuesTree = rt.Children[1];
    //                if (valuesTree.Children.Length > 0) {
    //                    RecognitionTree firstValueTree = valuesTree.Children[0];
    //                    valuesSoFar.Append(firstValueTree.Data);
    //                    RecognitionTree otherValuesTree = valuesTree.Children[1];
    //                    foreach (RecognitionTree separatedValuesTree in otherValuesTree.Children) {
    //                        valuesSoFar.Append(separatedValuesTree.Children[1]);
    //                    }
    //                }
    //                return Glom.From(valuesSoFar.Current);
    //            }),

    //        // number = [ minus ] int [ frac ] [ exp ]
    //        //JsonNumberRecognizer.Only,
    //        new SequenceRecognizer(
    //            "number",
    //            new OptionalRecognizer(new SpecificCharacterRecognizer('-')),
    //            new OrRecognizer(
    //                "int",
    //                new SpecificCharacterRecognizer('0', rt => 0),
    //                new SequenceRecognizer(
    //                    new NonzeroDigitRecognizer(),
    //                    new AnyRepetitionsRecognizer(new DigitRecognizer()))),
    //            new OptionalRecognizer(new SequenceRecognizer(
    //                "frac",
    //                new SpecificCharacterRecognizer('.'),
    //                new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9')))),
    //            new OptionalRecognizer(new SequenceRecognizer(
    //                "exp",
    //                new OrRecognizer(
    //                    new SpecificCharacterRecognizer('e'),
    //                    new SpecificCharacterRecognizer('E')),
    //                new OrRecognizer(
    //                    new SpecificCharacterRecognizer('+'),
    //                    new SpecificCharacterRecognizer('-')),
    //                new OneOrMoreRepetitionsRecognizer(new CharacterRangeRecognizer('0', '9'))))),

    //        // quotation-mark *char quotation-mark
    //        JsonStringRecognizer.Only
    //        //new SequenceRecognizer(
    //        //    "string",
    //        //    new SpecificCharacterRecognizer('"'),
    //        //    new AnyRepetitionsRecognizer(new OrRecognizer(
    //        //        "char",
    //        //        new OrRecognizer(
    //        //            "unescaped",
    //        //            new CharacterRangeRecognizer('\u0020', '\u0021'),
    //        //            new CharacterRangeRecognizer('\u0023', '\u005B'),
    //        //            new CharacterRangeRecognizer('\u005D', '\uFFFF')),
    //        //        new OrRecognizer(
    //        //            "escaped",
    //        //            new SpecificStringRecognizer("\\\""),
    //        //            new SpecificStringRecognizer("\\\\"),
    //        //            new SpecificStringRecognizer("\\/"),
    //        //            new SpecificStringRecognizer("\\b"),
    //        //            new SpecificStringRecognizer("\\f"),
    //        //            new SpecificStringRecognizer("\\n"),
    //        //            new SpecificStringRecognizer("\\r"),
    //        //            new SpecificStringRecognizer("\\t"),
    //        //            new SequenceRecognizer(
    //        //                new SpecificStringRecognizer("\\u"),
    //        //                new RepetitionsRangeRecognizer(new NamedRecognizer("hex-digit"), 4, 4))))),
    //        //    new SpecificCharacterRecognizer('"'))
    //    ),
    //    Array<Recognizer>.From(
    //        new SequenceRecognizer(
    //            "begin-array",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer('['),
    //            new NamedRecognizer("ws")),
    //        new SequenceRecognizer(
    //            "begin-object",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer('{'),
    //            new NamedRecognizer("ws")),
    //        new SequenceRecognizer(
    //            "end-array",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer(']'),
    //            new NamedRecognizer("ws")),
    //        new SequenceRecognizer(
    //            "end-object",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer('}'),
    //            new NamedRecognizer("ws")),
    //        new SequenceRecognizer(
    //            "name-separator",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer(':'),
    //            new NamedRecognizer("ws")),
    //        new SequenceRecognizer(
    //            "value-separator",
    //            new NamedRecognizer("ws"),
    //            new SpecificCharacterRecognizer(','),
    //            new NamedRecognizer("ws")),
    //        new CharacterSetRecognizer("ws", ' ', '\t', '\n', '\r'))
    //    );

    //private static HierarchicalRecognizer jsonRecognizer;

    //static Json() {
    //   jsonRecognizer = new HierarchicalRecognizer(
    //    "JSON",
    //}

    // The real character, to the escape codes

}
