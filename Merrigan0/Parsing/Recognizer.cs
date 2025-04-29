//using System;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//namespace Merrigan0.Parsing {
//    [Untested]
//    public class DictionaryBuilder<K, V> {
//        private List<K> keys;
//        private List<V> values;

//        public void Add(K key, V value) {
//            if (keys == null) {
//                keys = new List<K>();
//                values = new List<V>();
//            }
//            keys.Add(key);
//            values.Add(value);
//        }

//        public Dictionary<K, V> ToDictionary() {
//            Dictionary<K, V> entriesSoFar = new Dictionary<K, V>();
//            for (int i = 0; i < keys.Count; ++i) {
//                entriesSoFar.Add(keys[i], values[i]);
//            }
//            return entriesSoFar;
//        }
//    }

//    [Untested]
//    public class RecognizerResult {
//        public Dictionary<string, RecognizerResult> ChildrenByName { get; private set; }
//        public RecognizerResult[] Children { get; private set; }
//        public int Begin { get; private set; }
//        //public Set<int> ValidLengths { get; private set; }
//        public int Length { get; private set; }
//        public string Name { get; private set; }
//        public Recognizer Recognizer { get; private set; }
//        public string Text { get; private set; }
        
//        public RecognizerResult(string s, Recognizer recognizer) {
//            Recognizer = recognizer;
//            Text = s;
//        }

//        public RecognizerResult(string s, int begin, int length, Recognizer recognizer, Dictionary<string, RecognizerResult> childrenByName) {
//            Begin = begin;
//            ChildrenByName = childrenByName;
//            Length = length;
//            Recognizer = recognizer;
//            Text = s;
//        }
//    }

//    [Untested]
//    public abstract class Recognizer {
//        public virtual RecognizerResult Read(string s) {
//            int i = 0;
//            RecognizerResult result;
//            bool succeeded = TryRead(s, ref i, out  result);
//            if (!succeeded) {
//                throw new Exception("Did not recognize the value.");
//            }
//            return result;
//        }

//        public virtual bool Peek(string s, int i, out int length) {
//            RecognizerResult dummy;
//            int iOriginal = i;
//            bool found = TryRead(s, ref i, out dummy);
//            length = i - iOriginal;
//            return found;
//        }

//        public void Skip(string s, ref int i) {
//            int length;
//            if (Peek(s, i, out length)) {
//                i += length;
//            }
//        }

//        public abstract bool TryRead(string s, ref int i, out RecognizerResult result);
//    }

//    [Untested]
//    public class RegexRecognizer : Recognizer {
//        protected Regex StartRegex {
//            get {
//                if (startRegex == null) {
//                    string regex = Regex;
//                    if (regex.Length == 0 || regex[0] != '^') {
//                        regex = "^" + regex;
//                    }
//                    if (regex.Length >= 1 && regex[regex.Length - 1] == '$') {
//                        regex = regex.Substring(0, regex.Length - 1);
//                    }
//                    startRegex = new Regex(regex);
//                }
//                return startRegex;
//            }
//        }
//        private Regex startRegex;

//        public string Regex { get; private set; }

//        public RegexRecognizer(string regex) {
//            Regex = regex;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            Match match = StartRegex.Match(s, i);
//            if (match.Success) {
//                result = new RecognizerResult(null, match.Value, null);
//                i += match.Value.Length;
//                return true;
//            }
//            result = null;
//            return false;
//        }
//    }

//    [Untested]
//    public class ListRecognizer : Recognizer {
//        public string[] PossibleValues { get; private set; }

//        public ListRecognizer(string[] possibleValues) {
//            PossibleValues = possibleValues;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            foreach (string value in PossibleValues) {
//                if (string.Equals(value, s, StringComparison.OrdinalIgnoreCase)) {
//                    result = new RecognizerResult(null, s.Substring(i, value.Length), null);
//                    return true;
//                }
//            }
//            result = null;
//            return false;
//        }
//    }

//    [Untested]
//    public class WhiteSpaceSkippingRecognizer : Recognizer {
//        public Recognizer Target { get; private set; }

//        public WhiteSpaceSkippingRecognizer(Recognizer target) {
//            Target = target;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            // Skip all white space
//            int iBeforeRead = i;
//            while (true) {
//                if (i >= s.Length) {
//                    goto fail;
//                }

//                if (!Char.IsWhiteSpace(s[i])) {
//                    break;
//                }

//                ++i;
//            }

//            // If the following part is not recognized, fail
//            if (!Target.TryRead(s, ref i, out result)) {
//                goto fail;
//            }
//            return true;

//        fail:
//            i = iBeforeRead;
//            result = null;
//            return false;
//        }
//    }

//    [Untested]
//    public class AnythingExceptRecognizer : Recognizer {
//        public Recognizer Exception { get; private set; }

//        public AnythingExceptRecognizer(Recognizer exception) {
//            Exception = exception;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            int iBeforeRead = i;
//            int iBeforeLookahead;
//            while (true) {
//                // Look ahead to see if the exception is lurking here
//                RecognizerResult dummy;
//                iBeforeLookahead = i;
//                if (Exception.TryRead(s, ref i, out dummy)) {
//                    // If so, set i back to the end of the "anything" segment
//                    i = iBeforeLookahead;
//                    break;
//                }

//                // Move on to the next character
//                ++i;

//                // End if there is no next character
//                if (i >= s.Length) {
//                    break;
//                }
//            }
//            result = new RecognizerResult(null, s.Substring(iBeforeRead, i - iBeforeRead), null);
//            return true;
//        }
//    }

//    [Untested]
//    public class LineEndingRecognizer : Recognizer {
//        public static LineEndingRecognizer Default = new LineEndingRecognizer();

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            // Could be \r\n, \r, \n, end, or anything else
//            int iBeforeRead;

//            // End
//            if (i >= s.Length) {
//                goto notFound;
//            }

//            // \r or \r\n
//            char firstChar = s[i];
//            if (firstChar == '\r') {
//                iBeforeRead = i;
//                ++i;
//                if (i >= s.Length && s[i] == '\n') {
//                    ++i;
//                }

//                goto found;
//            }

//            // \n
//            if (firstChar == '\n') {
//                iBeforeRead = i;
//                ++i;
//                goto found;
//            }

//            // Anything else
//            goto notFound;

//        found:
//            result = new RecognizerResult(null, s.Substring(iBeforeRead, i - iBeforeRead), null);
//            return true;
//        notFound:
//            result = null;
//            return false;
//        }
//    }

//    [Untested]
//    public class LineRecognizer : AnythingExceptRecognizer {
//        public static LineRecognizer Default = new LineRecognizer();

//        public LineRecognizer() :
//            base(LineEndingRecognizer.Default) {
//        }
//    }

//    // Not thread-safe
//    [Untested]
//    public class OptionsRecognizer : Recognizer {
//        public List<Recognizer> MostEfficientOptionRecognizers {
//            get {
//                if (mostEfficientOptionRecognizers == null)
//                    mostEfficientOptionRecognizers = new List<Recognizer>();
//                return MostEfficientOptionRecognizers;
//            }
//        }
//        private List<Recognizer> mostEfficientOptionRecognizers;

//        public Recognizer[] OptionRecognizers { get; private set; }

//        protected void SetMostEfficientOptionRecognizer(Recognizer recognizer) {
//            MostEfficientOptionRecognizers.Remove(recognizer);
//            MostEfficientOptionRecognizers.Insert(0, recognizer);
//        }
//    }

//    [Untested]
//    public class AndRecognizer : OptionsRecognizer {
//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            int iBeforeReading = i;
//            DictionaryBuilder<string, RecognizerResult> resultsByNameSoFar = new DictionaryBuilder<string,RecognizerResult>();
//            foreach (Recognizer optionRecognizer in MostEfficientOptionRecognizers) {
//                i = iBeforeReading;
//                RecognizerResult optionResult;
//                if (!optionRecognizer.TryRead(s, ref i, out optionResult)) {
//                    SetMostEfficientOptionRecognizer(optionRecognizer);
//                    result = null;
//                    return false;
//                }
//                resultsByNameSoFar.Add(result.Name, result);
//            }
//            return new RecognizerResult(s);
//        }
//    }

//    [Untested]
//    public class OrRecognizer : OptionsRecognizer {
//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            int iBeforeReading = i;
//            DictionaryBuilder<string, RecognizerResult> resultsByNameSoFar = new DictionaryBuilder<string,RecognizerResult>();
//            foreach (Recognizer optionRecognizer in MostEfficientOptionRecognizers) {
//                i = iBeforeReading;
//                RecognizerResult optionResult;
//                if (optionRecognizer.TryRead(s, ref i, out optionResult)) {
//                    SetMostEfficientOptionRecognizer(optionRecognizer);
//                    result = new RecognizerResult(s, iBeforeReading, i - iBeforeReading);
//                    return false;
//                }
//                resultsByNameSoFar.Add(result.Name, result);
//            }
//            return new RecognizerResult(s);
//        }
//    }

//    [Untested]
//    public class CharacterRecognizer : Recognizer {
//        public Char Character { get; private set; }
//        public CharacterRecognizer(char character) {
//            Character = character;
//        }

//        public override bool Peek(string s, int i, out int length) {
//            if (i >= s.Length && s[i] == Character) {
//                length = 1;
//                return true;
//            }
//            length = 0;
//            return false;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            if (i >= s.Length) {
//                goto fail;
//            }

//            if (s[i] != Character) {
//                goto fail;
//            }

//            ++i;
//            result = new RecognizerResult(s.Substring(i, 1), null);
//            return true;

//        fail:
//            result = null;
//            return false;
//        }
//    }

//    [Untested]
//    public class GroupRecognizer : Recognizer {
//        public Char BeginCharacter { get; private set; }
//        public Char EndCharacter { get; private set; }
//        public Char EscapeCharacter { get; private set; }

//        public GroupRecognizer(char beginCharacter, char endCharacter, char escapeCharacter) {
//            BeginCharacter = beginCharacter;
//            EndCharacter = endCharacter;
//            EscapeCharacter = escapeCharacter;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            int iToTry = i;
//            if (iToTry >= s.Length) {
//                goto fail;
//            }
//            if (s[iToTry] != BeginCharacter) {
//                goto fail;
//            }

//            while (true) {
//                char chToTry = s[iToTry];

//                // End if the end character was found
//                if (chToTry == EndCharacter) {
//                    break;
//                }

//                // Skip escape character if present
//                if (chToTry == EscapeCharacter) {
//                    ++iToTry;
//                }

//                // Skip any bytes
//                ++iToTry;
//                if (iToTry >= s.Length) {
//                    goto fail;
//                }
//            }

//            result = new RecognizerResult(s.Substring(i, iToTry - i), null);
//            i = iToTry;
//            return true;

//        fail:
//            result = null;
//            return false;
//        }
//    }

//    public class NotRecognizer : Recognizer {
//        public Recognizer Base { get; private set; }

//        public NotRecognizer(Recognizer baseString) {
//            Base = baseString;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            int iBeforeTry = i;
//            RecognizerResult dummy;
//            bool baseSucceeded = Base.TryRead(s, ref i, out dummy);
//            if (baseSucceeded) {
//                result = null;
//                return false;
//            }
//            result = new RecognizerResult(s, this);
//            return true;
//        }
//    }



//    [Untested]
//    //public class RecognizerResultBuilder {
//    //    private int i;
//    //    private int length;
//    //    private List<string> names;
//    //    private List<RecognizerResult> results;
//    //    private string s;

//    //    public void AddChild(string name, RecognizerResult result) {
//    //        if (names == null) {
//    //            names = new List<string>();
//    //            results = new List<RecognizerResult>();
//    //        }
//    //        names.Add(name);
//    //        results.Add(result);
//    //    }

//    //    public void SetSubstring(string s, int i, int length) {
//    //        this.s = s;
//    //        this.i = i;
//    //        this.length = length;
//    //    }

//    //    public Dictionary<string, RecognizerResult> GetChildrenByName() {
//    //        Dictionary<string, RecognizerResult> childrenByNameSoFar = new Dictionary<string, RecognizerResult>();
//    //        for (int i = 0; i < names.Count; ++i) {
//    //            childrenByNameSoFar.Add(names[i], results[i]);
//    //        }
//    //        return childrenByNameSoFar;
//    //    }

//    //    public string GetText() {
//    //        return s.Substring(i, length);
//    //    }
//    //}
//}
