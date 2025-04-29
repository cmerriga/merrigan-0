using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ExecutionsInternal;
using Merrigan0.RecognizersInternal;

namespace Merrigan0.NmlInternal {
    public class NmlRecognizer : HierarchicalRecognizer {
        public static readonly NmlRecognizer Only;

        private static Array<HierarchicalRecognizer.Island> nmlIslands;
        private static Array<HierarchicalRecognizer.Layer> nmlLayers;
        private static Array<HierarchicalRecognizer.Prefix> nmlPrefixes;
        private static Array<Recognizer> nmlMostSpecificUnitRecognizers;
        private static Array<Recognizer> nmlMostSpecificTokenRecognizers;
        private static HierarchicalRecognizer.Layer nmlTokenSequenceLayer;

        static NmlRecognizer() {
            // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/Operator_Precedence
            nmlTokenSequenceLayer = new HierarchicalRecognizer.Layer(
                "sequence", 
                Recognizer.WellKnownRecognizer("white-space"), 
                100, //// maybe?
                true,
                (rt1, rt2) => new SequenceAppendExpression((Expression)rt1.Data, (Expression)rt2.Data));
            nmlLayers = Array<HierarchicalRecognizer.Layer>.From(
                new HierarchicalRecognizer.Layer(
                    "list", 
                    ",", 
                    1, 
                    true,
                    (rt1, rt2) => new ListAppendExpression((Expression)rt1.Data, (Expression)rt2.Data),
                    (rt1, rt2) => new PairExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("assignment", "=", 1, false, (rt1, rt2) => new ContextAppendExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("logical or", "||", 3, true, (rt1, rt2) => new LogicalOrExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("logical and", "&&", 4, true, (rt1, rt2) => new LogicalAndExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("bitwise or", "|", 5, true, (rt1, rt2) => new BitwiseOrExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("bitwise xor", "^", 6, false, (rt1, rt2) => new BitwiseXorExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("bitwise and", "&", 7, true, (rt1, rt2) => new BitwiseAndExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("equal", "==", 8, false, (rt1, rt2) => new EqualsExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("not equal", "!=", 8, false, (rt1, rt2) => new NotEqualsExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("less", "<", 9, false, (rt1, rt2) => new LessExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("less or equal", "<=", 9, false, (rt1, rt2) => new LessOrEqualsExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("greater or equal", ">=", 9, false, (rt1, rt2) => new GreaterOrEqualsExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("greater", ">", 9, false, (rt1, rt2) => new GreaterExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("shift left", "<<", 10, false, (rt1, rt2) => new LeftShiftExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("shift right", ">>", 10, false, (rt1, rt2) => new RightShiftExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("plus", "+", 11, true, (rt1, rt2) => new PlusExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("minus", "-", 11, false, (rt1, rt2) => new MinusExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("times", "*", 14, true, (rt1, rt2) => new TimesExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("divided by", "/", 14, false, (rt1, rt2) => new DividedByExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("modulo", "%", 14, false, (rt1, rt2) => new RemainderExpression((Expression)rt1.Data, (Expression)rt2.Data)),
                new HierarchicalRecognizer.Layer("filter", ".", 101, true, (rt1, rt2) => new SequenceAppendExpression((Expression)rt1.Data, (Expression)rt2.Data)));
            //,
            //    nmlTokenSequenceLayer);
            nmlMostSpecificUnitRecognizers = Array<Recognizer>.From(
                new SpecificStringRecognizer("current context", ".", rt => Expression.CurrentContext),
                new TransformingRecognizer("number", DoubleRecognizer.Only, o => new ConstantExpression<double>((double)o)));

            nmlMostSpecificTokenRecognizers = Array<Recognizer>.From(
                new SpecificStringRecognizer("false", "false", rt => Expression.False),
                new SpecificStringRecognizer("true", "true", rt => Expression.True),
                new SpecificStringRecognizer("null", "null", rt => Expression.Null),
                //new TransformingRecognizer("integer", IntegerRecognizer.Only, o => new ConstantExpression<long>((long)o)),
                new TransformingRecognizer("identifier", Recognizer.True, o => new IdentifierExpression((String)o)));
            nmlPrefixes = Array<HierarchicalRecognizer.Prefix>.From(
                new HierarchicalRecognizer.Prefix("bitwise not", "~", o => new BitwiseNotExpression((Expression)o)),
                new HierarchicalRecognizer.Prefix("negate", "-", o => new NegateExpression((Expression)o)),
                new HierarchicalRecognizer.Prefix("logical not", "!", o => new NotExpression((Expression)o)));
            Recognizer stringWithExpressionsRecognizer = new NamedRecognizer("string-with-expressions");
            Recognizer sequenceWithGroupEndRecognizer = new NamedRecognizer("sequence-with-group-end");
            Recognizer sequenceWithExpressionEndRecognizer = new NamedRecognizer("sequence-with-expression-end");
            nmlIslands = Array<HierarchicalRecognizer.Island>.From(
                new HierarchicalRecognizer.Island(
                    "string literal",
                    new SpecificCharacterRecognizer('"'), 
                    stringWithExpressionsRecognizer,
                    new SpecificCharacterRecognizer('"'), 
                    rt => (Expression)rt.Data),
                new HierarchicalRecognizer.Island(
                    "group",
                    new SpecificCharacterRecognizer('('),
                    sequenceWithGroupEndRecognizer,
                    new SpecificCharacterRecognizer(')'), 
                    rt => (Expression)rt.Data),
                new HierarchicalRecognizer.Island(
                    "expression",
                    new SpecificCharacterRecognizer('['),
                    sequenceWithExpressionEndRecognizer,
                    new SpecificCharacterRecognizer(']'), 
                    rt => (Expression)rt.Data));
            Only = new NmlRecognizer(stringWithExpressionsRecognizer, sequenceWithGroupEndRecognizer, sequenceWithExpressionEndRecognizer);
        }

        protected NmlRecognizer(params Recognizer[] recognizersToParent) : base(
            "nml",
            nmlLayers,
            nmlTokenSequenceLayer,
            nmlMostSpecificUnitRecognizers,
            nmlMostSpecificTokenRecognizers,
            nmlPrefixes,
            nmlIslands) 
        {
            Array<Recognizer>.From(recognizersToParent).Each(r => r.SetParent(this));

            SequenceWithEndRecognizer sequenceWithGroupEndRecognizer = new SequenceWithEndRecognizer("sequence-with-group-end", this, new SpecificCharacterRecognizer(')'));
            SequenceWithEndRecognizer sequenceWithExpressionEndRecognizer = new SequenceWithEndRecognizer("sequence-with-expression-end", this, new SpecificCharacterRecognizer(']'));
            StringWithExpressionsRecognizer stringWithExpressionsRecognizer = new StringWithExpressionsRecognizer(NamedRecognizer("expression"), new SpecificCharacterRecognizer('"'));
            RegisterNamedRecognizers(sequenceWithGroupEndRecognizer, sequenceWithExpressionEndRecognizer, stringWithExpressionsRecognizer);
        }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Recognizer recognizer = NmlRecognizer.Only;
            /*
             * token1
             *  token1 \
             * token1 token2
             * (tokena tokenb)
             * (tokena tokenb) \
             * token1 (tokena tokenb)
             * token1(tokena tokenb)
             * token1 (tokena tokenb) token2
             * -token1
             * -(tokena tokenb)
             * token1 + token2
             * token1+token2
             * token1 + token2 + token3
             * token1 + token2 * token3
             * (token1 + token2) * token3
             * "string1"
             * "string1" \
             * "str[a]ing1"
             * "string1" token2
             * token1 "string2"
             */

            recognizer.TestTryReadOrRecognize("token1", true, true);
            recognizer.TestTryReadOrRecognize(" token1 ", true, true);
            recognizer.TestTryReadOrRecognize("token1 token2", true, true);
            recognizer.TestTryReadOrRecognize("(tokena tokenb)", true, true);
            recognizer.TestTryReadOrRecognize("(tokena tokenb) ", true, true);
            recognizer.TestTryReadOrRecognize("token1 (tokena tokenb)", true, true);
            recognizer.TestTryReadOrRecognize("token1(tokena tokenb)", true, true);
            recognizer.TestTryReadOrRecognize("token1 (tokena tokenb) token2", true, true);
            recognizer.TestTryReadOrRecognize("-token1", true, true);
            recognizer.TestTryReadOrRecognize("-(tokena tokenb) ", true, true);
            recognizer.TestTryReadOrRecognize("token1 + token2", true, true);
            recognizer.TestTryReadOrRecognize("token1+token2", true, true);
            recognizer.TestTryReadOrRecognize("token1 + token2 + token3", true, true);
            recognizer.TestTryReadOrRecognize("token1 + token2 * token3", true, true);
            recognizer.TestTryReadOrRecognize("(token1 + token2) * token3", true, true);
            recognizer.TestTryReadOrRecognize("\"string1\"", true, true);
            recognizer.TestTryReadOrRecognize("\"string1\" ", true, true);
            recognizer.TestTryReadOrRecognize("\"str[a]ing1\"", true, true);
            recognizer.TestTryReadOrRecognize("\"string1\" token2", true, true);
            recognizer.TestTryReadOrRecognize("token1 \"string2\"", true, true);
            recognizer.TestTryReadOrRecognize("false", true, true);
            recognizer.TestTryReadOrRecognize("true", true, true);
            recognizer.TestTryReadOrRecognize("null", true, true);
            recognizer.TestTryReadOrRecognize("12345", true, true);
            recognizer.TestTryReadOrRecognize("12345.6789", true, true);
            recognizer.TestTryReadOrRecognize("1, 2, 3", true, true);
        }

        [WhatItIs("A singleton that recognizes a C-style string literal, including recognizing all escaped characters " +
            "according to the JSON standard, as well as skipping all white space from the beginning of any line " +
            "to accommodate indentation.")]
        [Untested]
        protected class StringWithExpressionsRecognizer : Recognizer {
            private Recognizer expressionRecognizer;
            private Recognizer endRecognizer;

            public StringWithExpressionsRecognizer(Recognizer expressionRecognizer, Recognizer endRecognizer) :
                base("string-with-expressions") 
            {
                this.endRecognizer = endRecognizer;
                this.expressionRecognizer = expressionRecognizer;
            }

            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                // Tree data will be of an expression that is a PlusExpression. It will add string constant expressions for segments and
                // arbitrary expressions
                long iToTry = i;
                long length = s.Length;

                Expression topExpression = null;
                MutableString segmentSoFar = new MutableString();

                // Go until another unescaped " is found. If the string has run out of characters, fail
                bool beginningOfLine = false;
                while (true) {
                    // See if there's an expression island here: [<nml>]
                    RecognitionTree expressionTree;
                    if (expressionRecognizer.TryRead(s, ref iToTry, out expressionTree)) {
                        // First, if we were working on any string segment, add it
                        if (segmentSoFar.Current.Length > 0) {
                            ConstantExpression segmentExpression = new ConstantExpression(segmentSoFar.Current);
                            if (topExpression == null) {
                                topExpression = segmentExpression;
                            } else {
                                topExpression = new PlusExpression(topExpression, segmentExpression);
                            }

                            // Start a new one
                            segmentSoFar = new MutableString();
                        }

                        // Then add the expression found
                        Expression expression = (Expression)expressionTree.Data;
                        if (topExpression == null) {
                            topExpression = expression;
                        } else {
                            topExpression = new PlusExpression(topExpression, expression);
                        }
                    } else {
                        char ch = s[iToTry];

                        // Ignore white space that begins a line
                        if (beginningOfLine) {
                            if (ch == ' ' || ch == '\t') {
                                ++iToTry;
                                continue;
                            }
                        }
                        beginningOfLine = false;

                        // End found
                        long iEnd = iToTry;
                        if (endRecognizer.TryRead(s, ref iEnd)) {
                            break;
                        }

                        // End of lines: \n, \r\n, or \r
                        if (ch == '\n') {
                            ++iToTry;
                            segmentSoFar.Append('\n');
                            beginningOfLine = true;
                            continue;
                        } else if (ch == '\r') {
                            if (iToTry < length) {
                                ch = s[iToTry];
                                if (ch == '\n') { ++iToTry; }
                                segmentSoFar.Append('\n');
                                beginningOfLine = true;
                                continue;
                            }
                        }

                        // Escape character found
                        if (ch == '\\') {
                            ++iToTry;
                            if (iToTry >= length) { goto fail; }
                            ch = s[iToTry];
                            if (ch == 'n') {
                                ch = '\n';
                            } else if (ch == 'r') {
                                ch = '\r';
                            } else if (ch == 't') {
                                ch = '\t';
                            } else if (ch == 'b') {
                                ch = '\b';
                            } else if (ch == 'f') {
                                ch = '\f';
                            } else if (ch != '\"' && ch != '\\' && ch != '/') {
                                throw new Exception("Illegally escaped character " + ch);
                            }
                        }

                        // Legit character is added to the string so far
                        segmentSoFar.Append(ch);
                        ++iToTry;
                    }

                    if (iToTry >= length) { break; }
                }

                // If there was an unfinished segment or no top expression, add it
                if (segmentSoFar.Current.Length > 0 || topExpression == null) {
                    ConstantExpression segmentExpression = new ConstantExpression(segmentSoFar.Current);
                    if (topExpression == null) {
                        topExpression = segmentExpression;
                    } else {
                        topExpression = new PlusExpression(topExpression, segmentExpression);
                    }
                }

                tree = new RecognitionTree(this, s, i, iToTry - i, topExpression);
                i = iToTry;
                return true;

            fail:
                tree = null;
                return false;
            }
        }
    }
}
