//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace Merrigan0.Parsing {
//    /// <summary>
//    /// A dumbex is a recognizer that has a s pattern to follow, plus named child recognizers.
//    /// </summary>
//    [Untested]
//    public class Dumbex : Recognizer {
//        protected Dictionary<string, Dumbex> childrenByName = new Dictionary<string, Dumbex>();

//        public static readonly Dumbex Common = new Dumbex(
//            null,
//            null,
//            new RegexDumbex("space", "\\s*"),
//            new RegexDumbex("word", "\\w+"),
//            new RegexDumbex("anything", ".*"),
//            new RegexDumbex("integer", "-?\\d+"),
//            new RegexDumbex("argument", "\"(?:[^\"\\\\]|\\\\.)*\"|\\S*")); // like command-line arguments

//        public Dumbex[] Children { get; protected set; }
//        public string Format { get; private set; }
//        public string Name { get; private set; }
//        public Dumbex Parent { get; private set; }

//        protected Recognizer Recognizer { 
//            get {
//                if (recognizer == null) {
//                    int i = 0;
//                    RecognizerResult result;
//                    if (!new FormatRecognizer().TryRead(Format, ref i, out result)) {
//                        throw new Exception("Format string was unreadable.");
//                    }
//                }
//                return recognizer;
//            }
//        }
//        private Recognizer recognizer;

//        public Dumbex(Dumbex parent, string name, string format, params Dumbex[] children) {
//            if (children != null) {
//                Children = children;
//                foreach (Dumbex child in children) {
//                    child.Parent = this;
//                }
//            }
//            Format = format;
//            Name = name;
//            Parent = parent;
//        }
        
//        public Dumbex(string name, string format, params Dumbex[] children) :
//            this(null, name, format, children) {
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            return Recognizer.TryRead(s, ref i, out result);
//        }

//        protected LookForTree ExpandAsAnd(LookForTree firstLookForTree, string format, ref int i) {
//            AndLookForTree andLookForTree = new AndLookForTree();
//            andLookForTree.Children.Add(firstLookForTree);
//        }

//        protected LookForTree ExpandAsOr(LookForTree firstLookForTree, string format, ref int i) {
//            OrLookForTree orLookForTree = new OrLookForTree();
//            orLookForTree.Children.Add(firstLookForTree);
//        }

//        //protected enum OperatorLevel {
//        //    Group = 0,

//        //}

//        protected class LookForTree {
//            public List<LookForTree> Children { get; private set; }
//            // Like null, space, or ...
//            public Milieu Milieu { get { return MilieuOverride.HasValue ? ((Parent == null) ? Milieu.WhiteSpace : Parent.Milieu) : MilieuOverride.Value; } }
//            public Milieu? MilieuOverride { get; private set; }
//            public LookForTree Parent { get; private set; }
//            public abstract bool TryLookFor(string s, ref int i, out RecognizerResult result);
//            public LookForTree(LookForTree parent = null, Milieu? milieuOverride = null) {
//                Children = new List<LookForTree>();
//                MilieuOverride = milieuOverride;
//                Parent = parent;
//            }
//        }

//        protected class LiteralLookForTree : LookForTree {
//            public string Literal { get; private set; }

//            public LiteralLookForTree(string literal) {
//                Literal = literal;
//            }
//        }

//        protected class OrLookForTree : LookForTree {
//            public OrLookForTree(LookForTree parent = null, Milieu? milieuOverride = null) :
//                base(parent, milieuOverride) {
//            }
//        }

//        protected class AndLookForTree : LookForTree {
//            public List<LookForTree> Children { get; private set; }
//            public AndLookForTree(LookForTree parent = null, Milieu? milieuOverride = null) :
//                base(parent, milieuOverride) {
//            }
//        }

//        protected class FormatRecognizer : Recognizer {
//            // | vs.  | & ~ ... [] () \ * : \s \w
//            //        | | | |   |  |  | | : |  |
//            // & vs.  | & ~ ... [] () \ * : \s \w
//            //        | & & &   &  &  & & : &  &
//            // \s vs. | & ~ ... [] () \ * : \s \w
//            //        | & s -   s  s  s s : s  s
//            // ... v  | & ~ ... [] () \ * : \s \w
//            //        | & ~ ... .  .  . . : -  .
//            // ~ vs.  | & ~ ... [] () \ * : \s \w
//            //        | & ~ ~   ~  ~  ~ ~ : \s ~

//            // asking for result: set of lengths allowed
//            // result:
//            //    ? no at indexes
//            //    ? yes indexes
            
//            // | options: first if possible, then next
//            //    result = result from first: no, yes/more, yes/end
//            //    when coming back for more, and first can not anymore, others can be tried

//            // & options: first that fits but can be more
//            //    result = no, yes at length/can be more, yes at length/can't be more

//            // ~ means the child recognizer rejects it at that index

//            protected static readonly Operator AliasOperator = new Operator("alias", ':', 50);
//            protected static readonly Operator AndOperator = new Operator("and", '&', 20);
//            protected static readonly Operator AnythingOperator = new Operator("anything", "...", 40);
//            protected static readonly Operator EscapeOperator = new Operator("escape", '\\', 0);
//            protected static readonly Operator ContextOperator = new Operator("context", '[', ']', 10);
//            protected static readonly Operator GroupOperator = new Operator("group", '(', ')', 10);
//            protected static readonly Operator NotOperator = new Operator("not", '~', 30);
//            protected static readonly Operator OrOperator = new Operator("or", '|', 10);
//            protected static readonly Operator PlusOperator = new Operator("plus", '+', 100);
//            protected static readonly Operator QuoteOperator = new Operator("quote", '"', '"', 10);
//            protected static readonly Operator SpaceOperator = new Operator("space", "[ \t]", 40);
//            protected static readonly Operator StarOperator = new Operator("star", '*', 100);
//            protected static readonly Operator WordCharacterOperator = new Operator("word-character", "\\w", 90);
//            //protected static readonly Tokenizer Tokenizer = new Tokenizer(new Operator[] {
//            //    AliasOperator,
//            //    AndOperator,
//            //    //ContextOperator,
//            //    //GroupOperator,
//            //    NotOperator,
//            //    SpaceOperator
//            //});

//            // (abcd|efgh):first-group
//            // aab:first-group
//            // aa*...c:first-group
//            public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//                TokenTree tokenTree = Tokenizer.Tokenize(s);
//            }

//            // Operators in compareResult: ~ & |
//            // Other recognized things: ... [] () \ * {conditions} :label-path
//            // Anything can be escaped with \
//            // Well-known milieus: space, anything
//            // Reads to any operator looser than the Current operator
//            protected bool TryReadLookForTree(string format, ref Operator @operator, ref int i, out LookForTree lookForTree) {
//                if (i >= format.Length) {
//                    goto fail;
//                }

//                // See what the next character is
//                lookForTree = new LookForTree();
//                int iOriginal = i;
//                char ch = format[i];
//                if (ch == '|') {
//                    ////lookForTree = ExpandAsOr(lookForTree, format, ref i);
//                } else if (ch == '&') {
//                    ////lookForTree = ExpandAsAnd(lookForTree, format, ref i);
//                } else if (ch == '~') {
//                } else if (ch == '[') {
//                } else if (ch == '(') {
//                    //////lookForTree.Children.Add(TryReadLookForTree(format, ref i
//                }
//                return true;

//            fail:
//                lookForTree = null;
//                return false;
//            }

//            protected enum Milieu {
//                None,
//                WhiteSpace,
//                Anything
//            }
//        }
//    }

//    [Untested]
//    public class RecognizerDumbex : Dumbex {
//        public Recognizer Recognizer { get; private set; }

//        public RecognizerDumbex(string name, Recognizer recognizer) :
//            base(name, null, null) {
//            Recognizer = recognizer;
//        }

//        public override bool TryRead(string s, ref int i, out RecognizerResult result) {
//            return Recognizer.TryRead(s, ref i, out result);
//        }
//    }

//    public class RegexDumbex : RecognizerDumbex {
//        public RegexDumbex(string name, string regex) :
//            base(name, new RegexRecognizer(regex)) {
//        }
//    }

//    [Untested]
//    public class Operator {
//        public int BreakingStrength { get; private set; }
//        public string Name { get; private set; }
//        public Recognizer Recognizer { get; private set; }

//        public Operator(string name, char character, int breakingStrength) {
//            BreakingStrength = breakingStrength;
//            Name = name;
//            Recognizer = new CharacterRecognizer(character);
//        }

//        public Operator(string name, string regex, int breakingStrength) {
//            BreakingStrength = breakingStrength;
//            Name = name;
//            Recognizer = new RegexRecognizer(regex);
//        }
//    }

//    [Untested]
//    public class Grouping {
//        public Char BeginCharacter { get; private set; }
//        public Char EndCharacter { get; private set; }
//        public Char EscapeCharacter { get; private set; }
//        public string Name { get; private set; }

//        public Grouping(string name, char beginCharacter, char endCharacter, char escapeCharacter) {
//            BeginCharacter = beginCharacter;
//            EndCharacter = endCharacter;
//            EscapeCharacter = escapeCharacter;
//            Name = name;
//        }

//        // Reads until first character after grouping
//        public bool TryRead(string s, ref int i) {
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

//            i = iToTry;
//            return true;

//        fail:
//            return false;
//        }
//    }

//    [Untested]
//    public class TokenTree {
//        public TokenTree[] Children { get; private set; }
//        public Operator Operator { get; private set; }
//        public string Text { get; private set; }

//        public TokenTree(string s, Operator @operator, TokenTree[] children = null) {
//            Children = (children == null) ? Array.Empty<TokenTree>() : children;
//            Operator = @operator;
//            Text = s;
//        }
//    }

//    [Untested]
//    public class Tokenizer {
//        public char EscapeCharacter { get; private set; }
//        public Grouping[] Groupings { get; private set; }
//        public Operator[] Operators { get; private set; }

//        public Tokenizer(Operator[] operators, Grouping[] groupings) {
//            Groupings = groupings;
//            Operators = operators;
//        }

//        public TokenTree Tokenize(string s, char escapeCharacter) {
//            TokenTree tree;
//            int i = 0;
//            Operator @operator = null;
//            if (!TryRead(s, ref @operator, ref i, out tree)) {
//                throw new Exception("Unable to tokenize s.");
//            }
//            return tree;
//        }

//        protected bool TryRead(string s, ref Operator @operator, ref int i, out TokenTree tree) {
//            int iToTry = i;
//            List<TokenTree> treesAtThisLevelSoFar = new List<TokenTree>();
//            int iTokenBegin = iToTry;
//            while (true) {
//                if (iToTry >= s.Length) {
//                    break;
//                }

//                // Check to see if there's a grouping here to include
//                int iBeforeGroup = iToTry;
//                foreach (Grouping grouping in Groupings) {
//                    if (grouping.TryRead(s, ref iToTry)) {
//                        // Create literal token out of the stuff read so far
//                        TokenTree literalTokenTree = new TokenTree(s.Substring(iTokenBegin, iBeforeGroup - iTokenBegin), @operator);
//                        treesAtThisLevelSoFar.Add(literalTokenTree);

//                        // Create token tree for the group found

//                        continue;
//                    }
//                }

//                // Check to see if the next character is one of the given operators
//                foreach (Operator operatorToTry in Operators) {
//                    RecognizerResult result;
//                    if (operatorToTry.Recognizer.TryRead(s, iToTry)) {
//                        // We've reached the end of the Current segment, so wrap it up
//                        ////
//                    }
//                }

//                // Skip any escape character
//                if (s[iToTry] == EscapeCharacter) {
//                    ++iToTry;
//                }

//                // Since it wasn't one of the operators, skip this character
//                ++iToTry;
//            }
//        }
//    }
//}
