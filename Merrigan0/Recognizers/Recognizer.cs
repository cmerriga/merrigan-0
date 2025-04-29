using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.RecognizersInternal;

namespace Merrigan0 {
    /*
     * other recognizers to do
     *      line
     */      
    // Equivalent to a regular expression or subsection of a regular expression.
    // Supplies the ability to recognize all at once with a single call, or to recognize
    // incrementally with each character or a block of characters.
    // Each recognizer can generate its own regular expression and/or dumbex representation.
    [DebuggerDisplay("{DebuggerDisplay}")]
    [Untested]
    public abstract class Recognizer {
        //public static readonly Recognizer Json = new JsonRecognizer();
        public static readonly Recognizer MinimalWholeNumber = new MinimalWholeNumberRecognizer();
        public static readonly Recognizer True = new TrueRecognizer();

        // Common recognizers for use by all
        private static MutableMap<String, Recognizer> wellKnownRecognizersByName;
        private static object recognizersByNameWriteLockObject = new object();

        protected Map<String, Recognizer> localRecognizersByName;
        protected Recognizer parent;

        [Note("Takes precedence over data a recognition tree was initialized with, if it exists.")]
        [MayBeNull]
        private Func<RecognitionTree, object> getDataFunction; //// maybe should only be on StringRecognizer

        [MayBeNull]
        private String name;

        [DiagnosticOnly]
        protected virtual String DebuggerDisplay {
            get {
                if (name != null) {
                    return name;
                }
                return ToRegex();
            }
        }

        //public Func<RecognitionTree, object> GetDataFunction { get; protected set; }

        [MayBeNull]
        public String Name {
            get {
                return name;
            }
        }

        public String Regex {
            get {
                MutableString regexSoFar = new MutableString();
                AppendToRegex(regexSoFar);
                return regexSoFar.Current;
            }
        }

        static Recognizer() {
            wellKnownRecognizersByName = new MutableMap<String, Recognizer>();
            ThreadUnsafeRegisterWellKnownRecognizer(new CustomCharacterClassGroupRecognizer(
                "white-space",
                Char.IsWhiteSpace,
                "\\s+"));
            ThreadUnsafeRegisterWellKnownRecognizer(new OptionalRecognizer(
                "optional-white-space",
                WellKnownRecognizer("white-space")));
                //new CustomCharacterClassGroupRecognizer(
                //"optional-white-space",
                //Char.IsWhiteSpace,
                //"\\s*"));
            ThreadUnsafeRegisterWellKnownRecognizer(new CustomCharacterClassGroupRecognizer(
                "token",
                Char.IsLetterOrDigit,
                "[a-zA-Z0-9]+"));
            ThreadUnsafeRegisterWellKnownRecognizer(new OrRecognizer(
                "newline",
                new SpecificCharacterRecognizer('\r'),
                new SpecificCharacterRecognizer('\n'),
                new SpecificStringRecognizer("\r\n")));
            ThreadUnsafeRegisterWellKnownRecognizer(new UntilRecognizer("line", WellKnownRecognizer("newline")));
            ThreadUnsafeRegisterWellKnownRecognizer(new CharacterRangeRecognizer("digit", '0', '9'));
            ThreadUnsafeRegisterWellKnownRecognizer(new CharacterRangeRecognizer("lowercase", 'a', 'z'));
            ThreadUnsafeRegisterWellKnownRecognizer(new CharacterRangeRecognizer("uppercase", 'A', 'Z'));
            ThreadUnsafeRegisterWellKnownRecognizer(new CustomRecognizer(
                "white-space-character",
                (s, i) => {
                    if (i >= s.Length) {
                        return new CustomRecognizer.CustomRecognizerResult() {
                            Length = 0,
                            Succeeded = false
                        };
                    }
                    bool recognized = Char.IsWhiteSpace(s[i]);
                    return new CustomRecognizer.CustomRecognizerResult() {
                        Length = 1,
                        Succeeded = true
                    };
                }));
            ThreadUnsafeRegisterWellKnownRecognizer(new AnyRepetitionsRecognizer("white-space", new NamedRecognizer("white-space-character")));
            ThreadUnsafeRegisterWellKnownRecognizer(new OrRecognizer(
                "hex-digit",
                new CharacterRangeRecognizer('0', '9'),
                new CharacterRangeRecognizer('A', 'F'),
                new CharacterRangeRecognizer('a', 'f')));
            ThreadUnsafeRegisterWellKnownRecognizer(new SequenceRecognizer(
                "begin-white-space",
                BeginRecognizer.Only,
                new NamedRecognizer("white-space")));
            ThreadUnsafeRegisterWellKnownRecognizer(new SequenceRecognizer(
                "white-space-end",
                new NamedRecognizer("white-space"),
                EndRecognizer.Only));

            //ThreadUnsafeRegisterWellKnownRecognizer(new CustomRecognizer(
            //    "double",
            //    (s, i) => {
            //        double number;
            //        if (String.TryReadNumber(s, ref i, out number)) {
            //            return new CustomRecognizer.CustomRecognizerResult
        }

        public Recognizer() : this(null) { }

        public Recognizer(String name) : this(name, null, null) {
            ////GetDataFunction = rt => rt.String;
        }

        // The local recognizers must have Name property set
        public Recognizer(String name, Array<Recognizer> localRecognizers) : this(name, localRecognizers, null) { }

        // The local recognizers must have Name property set
        public Recognizer(String name, Array<Recognizer> localRecognizers, Func<RecognitionTree, object> getDataFunction) {
            this.getDataFunction = getDataFunction;
            this.name = name;
            if (localRecognizers != null && localRecognizers.Length > 0) {
                RegisterNamedRecognizers(localRecognizers);
            }
        }

        public static Recognizer WellKnownRecognizer(String name) {
            return wellKnownRecognizersByName.Current[name];
        }

        public static void RegisterWellKnownRecognizer(String name, Recognizer recognizer) {
            lock (recognizersByNameWriteLockObject) {
                wellKnownRecognizersByName[name] = recognizer;
            }
        }

        // Do not call unless you are already guaranteed to have no conflicts writing to 
        // recognizersByName. Otherwise, lock recognizersByNameWriteLockObject before calling
        private static void ThreadUnsafeRegisterWellKnownRecognizer(Recognizer recognizer) {
            wellKnownRecognizersByName[recognizer.Name] = recognizer;
        }

        public virtual void AppendToRegex(MutableString regexSoFar) {
            String name = Name;
            if (name == null) {
                name = this.GetType().Name;
            }
            regexSoFar.Append('<');
            regexSoFar.Append(name);
            regexSoFar.Append('>');
        }

        [WhatItDoes("Used to calculate any data this tree represents.")]
        public virtual object GetData(RecognitionTree tree) {
            if (getDataFunction != null) {
                return getDataFunction(tree);
            }
            return tree.String;
        }

        //public abstract RecognizerExecution BeginExecution(String s, long i);

        public Recognizer NamedRecognizer(String name) {
            if (localRecognizersByName != null) {
                Recognizer localRecognizer;
                if (localRecognizersByName.TryGetValue(name, out localRecognizer)) {
                    return localRecognizer;
                }
            }
            if (Name == name) {
                return this;
            }
            if (parent != null) {
                return parent.NamedRecognizer(name);
            }
            return wellKnownRecognizersByName.Current[name];
        }

        public void SetParent(Recognizer parent) {
            this.parent = parent;
        }

        public override string ToString() {
            return Name;
        }

        public virtual String ToRegex() {
            MutableString regexSoFar = new MutableString();
            AppendToRegex(regexSoFar);
            if (regexSoFar.Current[0] == '(' && regexSoFar.Current[regexSoFar.Current.Length - 1] == ')') {
                regexSoFar.Remove(0, 1);
                regexSoFar.Remove(regexSoFar.Current.Length - 1, 1);
            }
            return regexSoFar.Current;
        }

        // Default behavior is to accumulate a parse tree but ignore it
        public bool TryRead(String s, ref long i) {
            RecognitionTree dummy;
            return TryRead(s, ref i, out dummy);
        }

        // Default behavior is to use the sequential, step-by-step recognition
        public abstract bool TryRead(String s, ref long i, out RecognitionTree tree);

        // Default behavior is to accumulate a parse tree but ignore it
        [WhatItDoes("Tries to recognize a string but only if it the exact length of the complete string.")]
        public bool TryRecognize(String s) {
            RecognitionTree dummyTree;
            return TryRecognize(s, out dummyTree);
        }
            
        [WhatItDoes("Tries to recognize a string but only if it the exact length of the complete string.")]
        public virtual bool TryRecognize(String s, out RecognitionTree tree) {
            long i = 0;
            bool succeeded = TryRead(s, ref i, out tree);
            if (!succeeded || i != s.Length) {
                return false;
            }
            return true;
        }

        ////public virtual Type ReturnType {
        ////    get {
        ////        return typeof(ParsingTree);
        ////    }
        ////}

        protected void RegisterNamedRecognizers(params Recognizer[] recognizers) {
            RegisterNamedRecognizers(Array<Recognizer>.From(recognizers));
        }

        protected void RegisterNamedRecognizers(Array<Recognizer> recognizers) {
            MutableMap<String, Recognizer> recognizersByNameSoFar = (localRecognizersByName == null) ?
                new MutableMap<String, Recognizer>() :
                new MutableMap<String, Recognizer>(localRecognizersByName);
            Map<String, Recognizer> recognizersByName = Map<String, Recognizer>.From(recognizers.Where(r => r.Name != null), r => r.Name);
            recognizersByNameSoFar.Add(recognizersByName);
            localRecognizersByName = recognizersByNameSoFar.Current;
        }

        [DiagnosticOnly]
        [WhatItDoes("Tests everything about various pairs of values.")]
        internal void TestTryRead(String invalid, object[] validStringAndDataPairs) {
            // Test just the invalid one first
            TestTryRead(invalid, false);
            TestTryRecognize(invalid, false);

            // Now test each valid one
            Array<Tuple<String, object>> validAndDataPairs = Utilities.ToPairs<String, object>(validStringAndDataPairs);
            foreach (Tuple<String, object> pair in validAndDataPairs) {
                // Just valid
                TestTryReadOrRecognize(pair.Item1, 0, false, true, pair.Item1.Length, pair.Item2);
                TestTryReadOrRecognize(pair.Item1, 0, true, true, null, pair.Item2);

                // Invalid prefix
                String composedString = invalid + pair.Item1;
                TestTryReadOrRecognize(composedString, 0, false, false, null, null);
                TestTryReadOrRecognize(composedString, invalid.Length, false, true, invalid.Length + pair.Item1.Length, pair.Item2);
                TestTryReadOrRecognize(composedString, 0, true, false, null, null);

                //// Invalid prefix but good starting index
                //long i = invalid.Length;

                // Invalid suffix
                composedString = pair.Item1 + invalid;
                TestTryReadOrRecognize(composedString, 0, false, true, pair.Item1.Length, pair.Item2);
                TestTryReadOrRecognize(composedString, 0, true, false, null, null);
            }
        }

        [DiagnosticOnly]
        internal void TestTryRecognize(String s, bool expected, object expectedData = null) {
            TestTryReadOrRecognize(s, true, expected, expectedData);
        }

        [DiagnosticOnly]
        internal void TestTryRead(String s, bool expected, long i, object expectedData = null) {
            TestTryReadOrRecognize(s, i, false, expected, null, expectedData);
        }

        [DiagnosticOnly]
        internal void TestTryRead(String s, bool expected, object expectedData = null) {
            TestTryReadOrRecognize(s, false, expected, expectedData);
        }

        [DiagnosticOnly]
        //// change to private and change all calls
        internal void TestTryReadOrRecognize(String s, bool wholeString, bool expected, object expectedData = null) {
            TestTryReadOrRecognize(s, 0, wholeString, expected, null, expectedData);
        }

        [DiagnosticOnly]
        //// change to private and change all calls
        internal void TestTryReadOrRecognize(
            String s, 
            long i, 
            bool wholeString, 
            bool expected, 
            long? iExpected = null, 
            object expectedData = null) 
        {
            using (Merrigan0.Test.Begin(s.Replaced(
                new SpecificCharacterRecognizer('\r'), "\\r",
                new SpecificCharacterRecognizer('\n'), "\\n"))) {
                long iLocal = i;
                RecognitionTree tree;
                bool recognized = wholeString ?
                    TryRecognize(s, out tree) :
                    TryRead(s, ref iLocal, out tree);
                Testing.Test("returns " + expected, recognized == expected);

                // Check change in i
                if (!recognized) {
                    Testing.Test("i is not advanced", iLocal == i);
                } else if (iExpected.HasValue) {
                    Testing.Test("i is correct", iLocal == iExpected.Value);
                }

                // Check that the tree length is the same as the expected
                if (expected && recognized && iExpected.HasValue) {
                    Testing.Test("tree length is correct", tree.Length == (iExpected.Value - i));
                }

                // Check tree if expected value given
                if (expected && recognized && (expectedData != null)) {
                    Testing.Test("tree data correct", EqualsOperator.Only.Run(tree.Data, expectedData));
                }
            }
        }
    }
}
