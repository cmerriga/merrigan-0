using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Merrigan0.ArraysInternal;
using Merrigan0.StringsInternal;

namespace Merrigan0 {
    //// consider not inheriting from Array<char>, to prevent ambiguous possible implicit casts to char[]
    [Untested]
    [DebuggerDisplay("{debuggerDisplay}")]
    public abstract class String : /*Array<char>, */ IEnumerable<char>, IEquatable<String>, IEquatable<string>, IComparable<String>, IComparable<string>, IComparable {
        [ExtenderIndex]
        private const int SYSTEM_STRING_EXTENSION = 0;

        [ExtenderIndex]
        private const int BLOCK_EXTENSION = 1;

        ////public static Map<char, char> EscapableCharactersToCodes = Map<char, char>.From(
        ////    '"', '"',
        ////    '\\', '\\',
        ////    '/', '/',
        ////    '\b', 'b',
        ////    '\f', 'f',
        ////    '\n', 'n',
        ////    '\r', 'r',
        ////    '\t', 't');

        public static String NullRepresentation = new SystemStringString("[null]");

        public static implicit operator string(String s) {
            if (s == null) {
                return null;
            }
            return s.ToString();
        }

        public static implicit operator String(string s) {
            if (s == null) {
                return null;
            }
            return new SystemStringString(s); 
        } //// or ToString()?

        public static implicit operator String(char ch) {
            return new SingleCharacterString(ch);
        }

        public static String operator +(String s1, String s2) {
            return new ConcatenateString(s1, s2);
        }

        public static String operator +(String s1, string s2) {
            return new AppendSystemStringString(s1, s2);
        }

        public static String operator +(string s1, String s2) {
            return new PrependSystemStringString(s1, s2);
        }

        public static String operator +(String s, object o) {
            if (o == null) {
                return s;
            }
            return new ConcatenateString(s, new SystemStringString(o.ToString()));
        }

        public static bool operator ==(String s1, object s2) {
            if (Object.ReferenceEquals(s1, s2)) {
                return true;
            }

            if (((object)s1 == null) || s2 == null) {
                return false;
            }

            return s1.Equals(s2);
        }

        ////public static bool operator ==(String s1, string s2) {
        ////    if (((object)s1 == null) || ((object)s2 == null)) {
        ////        return false;
        ////    }

        ////    return s1.Equals(s2);
        ////}

        //public static bool operator ==(string s1, String s2) {
        //    if (((object)s1 == null) || ((object)s2 == null)) {
        //        return false;
        //    }

        //    return s2.Equals(s1);
        //}

        public static bool operator !=(String s1, object s2) {
            return !(s1 == s2);
        }

        ////public static bool operator !=(String s1, string s2) {
        ////    return !(s1 == s2);
        ////}

        //public static bool operator !=(string s1, String s2) {
        //    return !(s1 == s2);
        //}
        
        public static Array<String> DivideAtFirst(String s, char ch) {
            long i;
            if (s.TryGetIndex(ch, 0, out i)) {
                return Array<String>.From(
                    s.Before(i),
                    s.After(i + 1));
            }
            return Array<String>.From(s);
        }

        public static String Empty { get { return EmptyString.Only; } }

        private Extender extender;

        public char this[long i] { get { return GetCharacter(i); } }

        [Equals("Length", "instance.Length")]
        [Untested]
        public char[] Block {
            get {
                char[] block = (char[])extender[BLOCK_EXTENSION];
                if (block == null) {
                    block = ToBlock();
                    extender[BLOCK_EXTENSION] = block;
                }
                return block;
            }
        }

        public abstract long Length { get; }

        [Equals("Length", "instance.Length")]
        [Untested]
        public string SystemString {
            get {
                string s = (string)extender[SYSTEM_STRING_EXTENSION];
                if (s == null) {
                    s = SystemStringify();
                    extender[SYSTEM_STRING_EXTENSION] = s;
                }
                return s;
            }
        }

        [DiagnosticOnly]
        private String debuggerDisplay { get { return ToString(); } }

        public static String Concatenate(params object[] values) {
            return new ArrayConcatenateString(((Array<object>)values).Transform(v => String.From(v)));
        }

        //// Returns a condensed array of
        //public static Array<String> Fold(Array<String> strings, char separator) {
        //}

        public static String From(char ch) { return new SingleCharacterString(ch); }
        public static String From(params char[] characters) { return new CharacterBlockString(characters); }
        public static String From(char[] characters, long length) { return new TruncatedCharacterBlockString(characters, length); }
        public static String From(char[] characters, long i, long length) { return new CharacterBlockSubstring(characters, i, length); }

        public static String From(object value) {
            if (value == null) {
                return String.Empty;
            }
            String s = value as String;
            if (s != null) {
                return s;
            }
            string systemString = value.ToString();
            return (String)systemString;
        }

        public static String Join<T>(Array<T> values, String separator) {
            bool first = true;
            MutableString resultSoFar = new MutableString();
            foreach (object value in values) {
                if (first) {
                    first = false;
                } else {
                    resultSoFar.Append(separator);
                }
                if (value != null) {
                    String valueString = String.From(value);
                    resultSoFar.Append(valueString);
                } else {
                    resultSoFar.Append(NullRepresentation);
                }
            }
            String result = resultSoFar.Current;
            return result;
        }
            
        public static long Skip(String s, long i, char ch) {
            if (i >= s.Length) {
                goto fail;
            }
            if (s[i] != ch) {
                goto fail;
            }
            return i + 1;

        fail:
            throw new Exception("Character not found.");
        }

        [WhatItDoes("Advances a text reader to the first character after any whitespace, as determined by Char.IsWhiteSpace")]
        public static void SkipWhitespace(TextReader reader) {
            while (true) {
                int ch = reader.Peek();
                if (ch < 0 || !Char.IsWhiteSpace((char)ch)) {
                    return;
                }
                ch = reader.Read();
            }
        }

        [WhatItDoes("Finds the index of the next character after any whitespace, as determined by Char.IsWhiteSpace")]
        [return: NotNegative]
        public static long SkipWhitespace(String s, [NotNegative] long i) {
            long length = s.Length;
            while (i < length) {
                char ch = s[i];
                if (!Char.IsWhiteSpace(ch)) {
                    break;
                }
                ++i;
            }
            return i;
        }

        [WhatItDoes("Advances a text reader to the first character after any whitespace, or a new line if that appears first, as determined by Char.IsWhiteSpace")]
        public static void SkipIntralineWhitespace(TextReader reader) {
            while (true) {
                int ch = reader.Peek();
                if (ch < 0) {
                    return;
                }
                char character = (char)ch;
                if (!(character == ' ' || character == '\t')) {
                    return;
                }
                ch = reader.Read();
            }
        }

        [WhatItDoes("Advances a text reader to the first character after any whitespace, or a new line if that appears first, as determined by Char.IsWhiteSpace")]
        [Example("hel   lo", 3, 6)]
        [Example("hel  \n lo", 3, 5)]
        [Example("hel  \n lo", 1, 1)]
        [Example("hel  \n lo", 1, 1)]
        public static void SkipIntralineWhiteSpace(String s, ref long i) {
            long length = s.Length;
            while (i < length) {
                char ch = s[i];
                if (!Char.IsWhiteSpace(ch) || ch == '\n' || ch == '\r') {
                    break;
                }
                ++i;
            }
        }

        public static String Substring(string s, long i, long length) {
            return String.From(s).Substring(i, length);
        }

        [WhatItDoes("Tries to read a floating-point number from text, per the rules of IEEE-754")]
        [Concept("IEEE-754", "https://en.wikipedia.org/wiki/IEEE_754")]
        public static bool TryReadNumber(String s, ref long i, out double r) {
            object number;
            if (!TryReadNumber(s, ref i, out number)) {
                r = default(double);
                return false;
            }
            if (number is long) {
                r = (double)(long)number;
            } else {
                r = (double)number;
            }
            return true;
        }

        [WhatItDoes("Tries to read a floating-point number from text, per the rules of IEEE-754")]
        [Concept("IEEE-754", "https://en.wikipedia.org/wiki/IEEE_754")]
        public static bool TryReadNumber(String s, ref long i, out /*[Note("May be double or long")] */object number) {
            bool negative = false;
            long length = s.Length;
            if (i >= length) {
                goto notfound;
            }
            long iToTry = i;
            if (s[i] == '-') {
                negative = true;
                ++iToTry;
            }
            long beforeDecimal = 0L;
            double afterDecimal = 0.0;

            // Maybe 0. something?
            char ch = s[iToTry];
            if (ch == '0') {
                // Do nothing. The beforeDecimal value is already 0
                ++iToTry;
                if (iToTry >= length) {
                    number = 0L;
                    i = iToTry;
                    return true;
                }
                ch = s[iToTry];
            } else if (ch < '0' || ch > '9') {
                // These characters are not legal
                goto notfound;
            } else {
                // Accumulate number until digits run out
                while (true) {
                    beforeDecimal *= 10;
                    beforeDecimal += (ch - '0');
                    ++iToTry;

                    // Reached end of string before decimal. Return the value so far
                    if (iToTry >= length) {
                        if (negative) {
                            beforeDecimal = -beforeDecimal;
                        }
                        number = beforeDecimal;
                        i = iToTry;
                        return true;
                    }
                    ch = s[iToTry];
                    if (ch < '0' || ch > '9') {
                        // We've reached either . e E or white space
                        break;
                    }
                }
            }

            // Is it .?
            double combined;
            if (ch == '.') {
                // Accumulate number until digits run out
                ++iToTry;
                double placeValue = 0.1;
                while (true) {
                    if (iToTry >= length) {
                        if (negative) {
                            afterDecimal = -afterDecimal;
                        }
                        combined = beforeDecimal + afterDecimal;
                        number = combined;
                        i = iToTry;
                        return true;
                    }
                    ch = s[iToTry];
                    if (ch < '0' || ch > '9') {
                        break;
                    }
                    afterDecimal += (ch - '0') * placeValue;
                    ++iToTry;
                    placeValue /= 10.0;
                }
            }

            // Is it E?
            if (ch == 'e' || ch == 'E') {
                //// Implement
                throw new Exception("Exponential notation not implemented yet.");
            }

            ////// Is it whitespace? If not it is a problem
            ////if (!Char.IsWhiteSpace(ch)) {
            ////    goto notfound;
            ////}

            if (negative) {
                afterDecimal = -afterDecimal;
            }
            combined = beforeDecimal + afterDecimal;
            number = combined;
            i = iToTry;
            return true;

        notfound:
            number = 0.0;
            i = 0L;
            return false;
        }

        public static bool TryRead(String s, long i, String sToRead, bool caseInsensitive, out long iNew) {
            if (i + sToRead.Length > s.Length) {
                goto notfound;
            }
            for (long iToRead = 0; iToRead < sToRead.Length; ++iToRead) {
                if (caseInsensitive) {
                    if (Char.ToLowerInvariant(s[i + iToRead]) != Char.ToLowerInvariant(sToRead[iToRead])) {
                        goto notfound;
                    }
                } else {
                    if (s[i + iToRead] != sToRead[iToRead]) {
                        goto notfound;
                    }
                }
            }
            iNew = i + sToRead.Length;
            return true;

        notfound:
            iNew = 0;
            return false;
        }

        public virtual String After(long i) { return Substring(i, Length - i); }

        public virtual String Before(long i) { return Substring(0, i); }

        public virtual String BeforeLast(char ch) {
            long i;
            if (TryGetLastIndex(ch, 0, out i)) {
                return Before(i);
            }
            return EmptyString.Only;
        }

        [Example("", "", 0)]
        [Example("", "a", -1)]
        [Example("a", "a", 0)]
        [Example("a", "", 1)]
        [Example("ab", "ac", -1)]
        [Example("ab", "ab", 0)]
        [Example("ac", "ab", 1)]
        public virtual int CompareTo(object other) {
            String otherString = Conversion.Convert<String>(other);
            return CompareTo(otherString);
        }

        [Example("", "", 0)]
        [Example("", "a", -1)]
        [Example("a", "a", 0)]
        [Example("a", "", 1)]
        [Example("ab", "ac", -1)]
        [Example("ab", "ab", 0)]
        [Example("ac", "ab", 1)]
        public virtual int CompareTo(String other) {
            long length = Length;
            long otherLength = other.Length;
            for (long i = 0; i < length; ++i) {
                if (otherLength <= i) {
                    return 1;
                }
                char thisChar = this[i];
                char otherChar = other[i];
                if (thisChar < otherChar) {
                    return -1;
                }
                if (thisChar > otherChar) {
                    return 1;
                }
            }
            if (otherLength > length) {
                return -1;
            }
            return 0;
        }

        [Example("", "", 0)]
        [Example("", "a", -1)]
        [Example("a", "a", 0)]
        [Example("a", "", 1)]
        [Example("ab", "ac", -1)]
        [Example("ab", "ab", 0)]
        [Example("ac", "ab", 1)]
        public virtual int CompareTo(string other) {
            long length = Length;
            long otherLength = other.Length;
            for (long i = 0; i < length; ++i) {
                if (otherLength <= i) {
                    return 1;
                }
                char thisChar = this[i];
                char otherChar = other[(int)i];
                if (thisChar < otherChar) {
                    return -1;
                }
                if (thisChar > otherChar) {
                    return 1;
                }
            }
            if (otherLength > length) {
                return -1;
            }
            return 0;
        }

        public virtual bool Contains(String substring) {
            long dummy;
            return TryGetIndex(substring, 0, out dummy);
        }

        //// add one taking generic recognizer function, regex, dumbex
        //public virtual bool Contains(string text) { return Contains((String)text); } //// add one taking generic recognizer function, regex, dumbex
        //public virtual bool ContainsAny(String s) { return this.ContainsAny((Array<char>)s); } //// add one taking generic recognizer function, regex, dumbex
        //public virtual bool ContainsAny(string s) { return this.ContainsAny(new SystemStringString(s)); } //// add one taking generic recognizer function, regex, dumbex

        public virtual bool ContainsAt(String substring, long i) {
            long substringLength = substring.Length;
            if (i + substringLength > Length) {
                return false;
            }
            for (int j = 0; j < substringLength; ++j) {
                if (GetCharacter(i + j) != substring[j]) {
                    return false;
                }
            }
            return true;
        }

        public virtual void CopyTo(char[] block, int arrayIndex) {
            long length = Length;
            for (long i = 0; i < length; ++i) {
                block[arrayIndex + i] = GetCharacter(i);
            }
        }

        [WhatItDoes("determines whether this string ends with a given string")]
        //[return: False("s.Length > Length")]
        [Example("abc", "c", true)]
        [Example("abc", "b", false)]
        [Example("abc", "abc", true)]
        [Example("abc", "abcd", false)]
        public virtual bool EndsWith(String s) {
            long sLength = s.Length;
            long iSuffix = Length - sLength;
            if (iSuffix < 0) { return false; }
            for (long i = 0; i < sLength; ++i) {
                if (GetCharacter(iSuffix + i) != s[i]) {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object o) {
            String stringObject = o as String;
            if (stringObject != null) {
                return Equals(stringObject);
            }
            return Equals(o as string);
        }

        public bool Equals(String s) {
            if (s == null) { return false; }
            long stringLength = s.Length;
            if (s.Length != Length) { return false; }
            for (long i = 0; i < stringLength; ++i) {
                if (s[i] != GetCharacter(i)) {
                    return false;
                }
            }
            return true;
        }

        public bool Equals(string s) {
            if (s == null) { return false; }
            int stringLength = s.Length;
            if (s.Length != Length) { return false; }
            for (int i = 0; i < stringLength; ++i) {
                if (s[i] != GetCharacter(i)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<char> GetEnumerator() { return new StringEnumerator(this); }

        public override int GetHashCode() {
            unchecked
            {
                int hash = 17;
                long length = Length;
                for (long i = 0; i < length; ++i) {
                    hash = hash * 23 + (int)GetCharacter(i);
                }
                return hash;
            }
        }

        //public override int GetHashCode() {
        //    return this.ToString().GetHashCode();
        //}

        public virtual String Left([LessOrEqual("Length")] long length) { return Substring(0L, length); }

        public String Replaced(params object[] recognizersAndReplacementStrings) {
            return new ReplaceString(this, recognizersAndReplacementStrings);
        }

        public virtual String Reverse() {
            return new ReverseString(this);
        }

        public virtual String Right([LessOrEqual("Length")] long length) { return Substring(0L, length); }

        [WhatItDoes("determines whether this string starts with a given string")]
        //[return: False("s.Length > Length")]
        [Example("abc", "a", true)]
        [Example("abc", "b", false)]
        [Example("abc", "abc", true)]
        [Example("abc", "abcd", false)]
        public virtual bool StartsWith(String s) {
            long sLength = s.Length;
            long iSuffix = Length - sLength;
            if (iSuffix < 0) { return false; }
            for (long i = 0; i < sLength; ++i) {
                if (GetCharacter(i) != s[i]) {
                    return false;
                }
            }
            return true;
        }

        public String Substring(long i) { return Substring(i, Length - i); }

        public virtual String Substring(long i, long length) {
            return new Substring(this, i, length);
        }

        public virtual Array<char> ToArray() {
            return Block;
        }

        public virtual char[] ToBlock() {
            char[] block = new char[Length];
            CopyTo(block, 0);
            return block;
        }

        public virtual IEnumerable<char> ToIEnumerable() { return this; }

        public override string ToString() {
            return SystemString;
        }

        public virtual String Truncated(long maxLength) {
            // 20 or less
            if (Length <= maxLength) {
                return this;
            }

            return Substring(0, maxLength - 3) + "...";
        }

        public abstract bool TryGetCharacter(long i, out char ch);

        public bool TryGetIndex(char ch, out long i) { return TryGetIndex(ch, 0L, out i); }

        public virtual bool TryGetIndex(char ch, long iBegin, out long i) {
            long length = Length;
            for (long iToTry = iBegin; iToTry < length; ++iToTry) {
                if (GetCharacter(iToTry) == ch) {
                    i = iToTry;
                    return true;
                }
            }
            i = -1L;
            return false;
        }

        public bool TryGetIndex(String substring, out long i) { return TryGetIndex(substring, 0L, out i); }

        public virtual bool TryGetIndex(String substring, long iBegin, out long i) {
            //// Depending on size, use Boyer-Moore
            long length = Length;
            long substringLength = substring.Length;
            for (long iToTry = iBegin; iToTry < length; ++iToTry) {
                if (iToTry + substringLength > length) {
                    substringLength = length - iToTry;
                }
                bool found = true;
                for (int jToTry = 0; jToTry < substringLength; ++jToTry) {
                    if (!object.Equals(GetCharacter(iToTry + jToTry), substring[jToTry])) {
                        found = false;
                        break;
                    }
                }
                if (found) {
                    i = iToTry;
                    return true;
                }
            }
            i = -1L;
            return false;
        }

        public virtual bool TryGetLastIndex(char ch, out long i) { return TryGetLastIndex(ch, 0L, out i); }

        public virtual bool TryGetLastIndex(char ch, long iBegin, out long i) {
            for (long iToTry = Length - 1; iToTry >= 0; --iToTry) {
                if (GetCharacter(iToTry) == ch) {
                    i = iToTry;
                    return true;
                }
            }
            i = -1L;
            return false;
        }

        // Reads to just past the next newline character combination (CR, LF, or CR/LF). Sets line to the 
        // characters preceding that and returns true. If the reader is at the end of the s, returns false.
        // If characters are found at the end of s without a newline, those characters are returned.
        public bool TryReadLine(long i, out String line, out long iNew) {
            // If the line had no characters and we were out of room in the string, return nothing
            long length = Length;
            if (i >= length) {
                line = null;
                iNew = 0;
                return false;
            }

            // Read all characters until encountering a CR, LF, or CR/LF pair
            long iStart = i;
            long iToTry = i;
            while (true) {
                // Stop if found LF (Unix)
                char ch = GetCharacter(i);
                if (ch == '\n') {
                    ++i;
                    break;
                }

                // Stop if found CR (Mac) or CR/LF (TTY/Windows)
                if (ch == '\r') {
                    ++i;

                    // Skip any \n found
                    if (i < length && GetCharacter(i) == '\n') {
                        ++i;
                    }

                    break;
                } else {
                    ++i;
                    if (i >= length) {
                        break;
                    }
                }
            }
            line = Substring(iStart, i - iStart);
            iNew = i;
            return true;
        }

        //public virtual String WithSubstitution(String s) { return new SubstitutionString(this, s); } //// add one taking generic recognizer function, regex, dumbex
        //public virtual String WithSubstitution(string s) { return new SubstitutionString(this, (String)s); } //// add one taking generic recognizer function, regex, dumbex

        public virtual String Until(String sentinel) {
            long iSentinel;
            if (TryGetIndex(sentinel, out iSentinel)) {
                return Left(iSentinel);
            }
            return this;
        }

        protected char GetCharacter(long i) {
            char ch;
            if (!TryGetCharacter(i, out ch)) {
                Utilities.ThrowIndexOutOfRangeException(i, Length);
            }
            return ch;
        }

        protected virtual string SystemStringify() {
            long length = Length;
            char[] block = new char[length];
            CopyTo(block, 0);
            return new string(block);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
