using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.NmlInternal;

namespace Merrigan0 {
    [Untested]
    public static class Nml {
        public static readonly Recognizer Recognizer = NmlRecognizer.Only;

        private static Map<char, char> escapeCodesToCharacters = Map<char, char>.From(
               '"', '"',
               '\\', '\\',
               '/', '/',
               'b', '\b',
               'f', '\f',
               'n', '\n',
               'r', '\r',
               't', '\t');

        public static Expression Expression(String nml) {
            /*
             * Sequential ones
             *      named (to be evaluated) one (either via . or space) A.1 A 1
             *      specifier groups like indented list or parenthetical sequential group A (1 2 3)
             *      
             * Binary ones
             *      + - * / ^ && || & | < <= == != >= >
             *      
             * Unary ones
             *      - ! ~
             *      
             * Grouping ones
             *      ()
             *      
             * Escaping ones
             *      []
             *      
             * Literal ones
             *      "" d*.d* true false
             */
            RecognitionTree tree;
            if (!NmlRecognizer.Only.TryRecognize(nml, out tree)) {
                throw new Exception();
            }
            Expression expression = (Expression)tree.Data;
            return expression;
        }

        public static Glom Glom(Expression expression) {
            object evaluation = Merrigan0.Glom.Global.Evaluate(expression);
            Glom glom = Merrigan0.Glom.From(evaluation);
            return glom;
        }

        public static Glom Glom(String nml) {
            Expression expression = Expression(nml);
            return Glom(expression);
        }

        public static Glom Glom(Glom context, String nml) {
            Expression expression = Expression(nml);
            object evaluation = context.Evaluate(expression);
            Glom glom = Merrigan0.Glom.From(evaluation);
            return glom;
        }

        public static object Object(String nml, Type type) {
            Glom glom = Glom(nml);
            object o = Conversion.Convert(glom, type);
            return o;
        }

        //// ...expression...
        //private static bool TryReadExpression(String nml, ref long i, out Expression expression) {
        //    RecognitionTree tree;
        //    if (!NmlRecognizer.Only.TryRecognize(nml, ref i, out tree)) {
        //        expression = null;
        //        return false;
        //    }
        //    expression = (Expression)tree.Data;
        //    return true;
        //}

        [DiagnosticOnly]
        [Test]
        private static void Test() {
            Testing.Test("nml 1", true);
            Testing.Test("creates context-based expressions from predicates", () => {
                Expression result = Nml.Expression(". < 1");
                Testing.TestImplements(result, typeof(LessExpression));
                LessExpression lessExpression = (LessExpression)result;
                Testing.TestImplements(lessExpression.Left, typeof(CurrentContextExpression));
            });
        }
    }
}

//private static Recognizer recognizer = new OrRecognizer(
//    "glom",
//    Array<Recognizer>.From(
//        new SeparatedRepetitionsRecognizer(
//            "level-3",
//            new NamedRecognizer("glom"),
//            new NamedRecognizer("level-3-separator")),
//        new SeparatedRepetitionsRecognizer(
//            "level-4",
//            new NamedRecognizer("glom"),
//            new NamedRecognizer("level-4-separator")),
//        //new SeparatedRepetitionsRecognizer(
//        //    "level-5",
//        //    new NamedRecognizer("glom"),
//        //    new NamedRecognizer("level-5-separator")),
//        new SeparatedRepetitionsRecognizer(
//            "level-6",
//            new NamedRecognizer("glom"),
//            new NamedRecognizer("level-6-separator")),
//        //new SeparatedRepetitionsRecognizer(
//        //    "level-7",
//        //    new NamedRecognizer("glom"),
//        //    new NamedRecognizer("level-7-separator")),
//        new SeparatedRepetitionsRecognizer(
//            "level-8",
//            new NamedRecognizer("glom"),
//            new NamedRecognizer("level-8-separator")),
//        new SeparatedRepetitionsRecognizer(
//            "level-9",
//            new NamedRecognizer("glom"),
//            new NamedRecognizer("level-9-separator")),
//        new NamedRecognizer("token")
//        ),
//    Array<Recognizer>.From(
//        new SequenceRecognizer(
//            "level-3-separator",
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//            new CharacterRecognizer('|'),
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        new SequenceRecognizer(
//            "level-4-separator",
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//            new CharacterRecognizer('&'),
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        //new SequenceRecognizer(
//        //    "level-5-separator",
//        //    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//        //    new CharacterRecognizer('|'),
//        //    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        new SequenceRecognizer(
//            "level-6-separator",
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//            new CharacterRecognizer('^'),
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        //new SequenceRecognizer(
//        //    "level-7-separator",
//        //    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//        //    new CharacterRecognizer('&'),
//        //    new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        new SequenceRecognizer(
//            "level-8-separator",
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//            new OrRecognizer(new StringRecognizer("=="), new StringRecognizer("!=")),
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space"))),
//        new SequenceRecognizer(
//            "level-9-separator",
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")),
//            new OrRecognizer(
//                new CharacterRecognizer('<'),
//                new StringRecognizer("<="),
//                new StringRecognizer(">="),
//                new CharacterRecognizer('<')),
//            new AnyRepetitionsRecognizer(new NamedRecognizer("white-space")))
//        ));


//// Returns either a simple constant string expression, or a concatenation of two string expressions
//public static bool TryReadString(String s, ref long i, out Expression stringExpression) {
//    // End of overall string
//    long length = s.Length;
//    if (i >= length) {
//        goto notfound;
//    }

//    // Doesn't start with quote character
//    if (s[i] != '\"') {
//        goto notfound;
//    }

//    // Started with quote. Move on to the next
//    long iToTry = i;
//    ++iToTry;

//    //// Don't recopy all characters if there are no escaped characters
//    //// Probably just concatenate non-escaped with translated characters
//    // Pinch off an ongoing string whenever needed. It becomes the 
//    MutableString currentString = new MutableString();
//    Expression currentExpression = null;
//    Expression embeddedExpression;
//    long iBeforeRead;
//    long iCurrentStringStart = iToTry;
//    String latestSegment;
//    Expression currentStringExpression;
//    while (true) {
//        iBeforeRead = iToTry;

//        // End of overall string
//        if (iToTry >= length) {
//            throw new Exception("Unmatched quotation mark.");
//        }

//        // End of string value
//        char ch = s[iToTry];
//        if (ch == '"') {
//            ++i;
//            break;
//        } else if (ch == '\\') {
//            ++iToTry;
//            if (iToTry >= length) {
//                throw new Exception("Unfinished escape sequence.");
//            }
//            ch = s[iToTry];
//            char properCharacter;
//            if (escapeCodesToCharacters.TryGetValue(ch, out properCharacter)) {
//                currentString.Append(s.Substring(iCurrentStringStart, iBeforeRead - iCurrentStringStart));
//                currentString.Append(properCharacter);
//                iCurrentStringStart = iToTry + 1;
//            } else {
//                throw new Exception("Illegal escape sequence.");
//            }
//        } else if (TryReadGroup(s, '[', ']', ref iToTry, out embeddedExpression)) {
//            // Embedded expression.
//            // Pinch off latest string and make it the right side of a concatenated string expression (or a standalone constant string
//            // if there is no accumulating expression). Then add the just-found embedded expression as well
//            latestSegment = s.Substring(iCurrentStringStart, iBeforeRead - iCurrentStringStart);
//            currentString.Append(latestSegment);
//            currentStringExpression = new ConstantExpression<String>(currentString.Current);
//            if (currentExpression == null) {
//                currentExpression = currentStringExpression;
//            } else {
//                currentExpression = new CastExpression(new PlusExpression(currentExpression, currentStringExpression), typeof(String));
//            }
//            currentExpression = new PlusExpression(currentExpression, new CastExpression(embeddedExpression, typeof(String)), typeof(String));
//            currentString = new MutableString();
//            iCurrentStringStart = iToTry + 1;
//        }
//    }

//    // Pinch off latest string and make it the right side of a concatenated string expression (or a standalone constant string
//    // if there is no accumulating expression)
//    latestSegment = s.Substring(iCurrentStringStart, iBeforeRead - iCurrentStringStart);
//    currentString.Append(latestSegment);
//    currentStringExpression = new ConstantExpression<String>(currentString.Current);
//    if (currentExpression == null) {
//        currentExpression = currentStringExpression;
//    } else {
//        currentExpression = new CastExpression(new PlusExpression(currentExpression, currentStringExpression), typeof(String));
//    }
//    stringExpression = currentExpression;
//    i = iToTry;
//    return true;

//notfound:
//    stringExpression = null;
//    return false;
//}

//// Returns the next contiguous alphanumeric string (note no
//// punctuation is allowed, including _)
//private static bool TryReadToken(String s, ref long i, out String token) {
//    long iToTry = i;
//    long length = s.Length;
//    long iToken = iToTry;
//    while (iToTry < length) {
//        if (!Char.IsLetterOrDigit(s[iToTry])) {
//            break;
//        }
//        ++iToTry;
//    }

//    // Didn't find any letters or digits
//    if (iToTry == iToken) {
//        token = null;
//        return false;
//    }
//    token = s.Substring(i, iToTry - i);
//    i = iToTry;
//    return true;
//}

//private static void SkipToWhiteSpace(String s, ref long i) {
//    long length = s.Length;
//    while (i < length) {
//        if (Char.IsWhiteSpace(s[i])) {
//            ++i;
//            break;
//        }
//        ++i;
//    }
//}

//private static void SkipWhiteSpace(String s, ref long i) {
//    long length = s.Length;
//    while (i < length) {
//        if (!Char.IsWhiteSpace(s[i])) {
//            break;
//        }
//        ++i;
//    }
//}

//////// Expression embedded in a string, e.g. the bracket-delineated par in "Answer is [x + 5].". It must output a string. 
//////private static bool TryReadEmbeddedExpression(String s, ref long i, out Expression<object> embeddedExpression) {
//////    return TryReadGroup(String
//////    // End of overall string
//////    long length = s.Length;
//////    if (i >= length) {
//////        goto notfound;
//////    }

//////    // Doesn't start with left bracket
//////    if (s[i] != '[') {
//////        goto notfound;
//////    }

//////    // Started with left bracket. Move on to the next
//////    long iToTry = i;
//////    ++iToTry;
//////    if (!TryReadExpression(s, ']', ref iToTry, out embeddedExpression)) {
//////        throw new Exception("No embedded expression found.");
//////    }

//////    // Skip the right bracket
//////    ++iToTry;

//////    i = iToTry;
//////    return true;

//////notfound:
//////    embeddedExpression = null;
//////    return false;
//////}

//// Skips this character if it is the next character
//private static bool TryReadChar(String s, ref long i, char ch) {
//    if (i >= s.Length) {
//        return false;
//    }
//    if (s[i] == ch) {
//        ++i;
//        return true;
//    }
//    return false;
//}

//// Skips this string if it is the next characters
//private static bool TryReadString(String s, ref long i, String sToRead) {
//    long iToTry = i;
//    long lengthToRead = sToRead.Length;
//    if (i + lengthToRead >= s.Length) {
//        return false;
//    }
//    for (long j = 0; j < lengthToRead; ++j) {
//        if (s[iToTry] != sToRead[j]) {
//            return false;
//        }
//        ++iToTry;
//    }
//    i = iToTry;
//    return true;
//}

//// 0 or [1-9] [0-9]*
//private static bool TryReadInt(String s, ref long i, out long n) {
//    long iToTry = i;
//    long nSoFar = 0;
//    while (i >= s.Length) {
//        int digit = s[i] - '0';
//        if (digit < 0 || digit > 10) {
//            n = 0;
//            return false;
//        }
//        nSoFar *= 10;
//        nSoFar += digit;
//    }
//    n = nSoFar;
//    return true;
//}

//// Returns the next contiguous alphanumeric string (note no
//// punctuation is allowed, including _)
//private static bool TryReadNumber(String s, ref long i, out object number) {
//    long iNew;
//    double r;
//    bool succeeded = Json.Json.TryReadNumber(s, i, out r, out iNew);
//    if (succeeded) {
//        number = r;
//        i = iNew;
//        return true;
//    }
//    number = null;
//    return false;
//}
