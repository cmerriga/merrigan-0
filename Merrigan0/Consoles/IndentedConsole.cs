using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [WhatItIs("A fixed-width text grid that allows infinite appends of objects, using the product of their " +
        "ToString functions.")]
    //// deal with tabs
    public class IndentedConsole : BaseConsole {
        public static IndentedConsole Ambient = new IndentedConsole();

        private int nIndentSpaces;
        private IConsoleStream output;
        private int currentDepth;
        //private bool indentPrintedOnCurrentLine;
        private int nCharsAppendedOnCurrentLine;
        private int maxChildren;
        private int maxDepth;

        public override int NumberOfColumns {
            get { return output.NumberOfColumns; }
        }

        public IndentedConsole([MayBeNull] IConsoleStream output = null, int? nIndentSpaces = null, int? maxDepth = null, int? maxChildren = null) {
            this.maxChildren = maxChildren.GetValueOrDefault(Int32.MaxValue);
            this.maxDepth = maxDepth.GetValueOrDefault(Int32.MaxValue);
            this.nIndentSpaces = nIndentSpaces.GetValueOrDefault(4);
            this.output = output ?? SystemConsole.Ambient;
        }

        public override void Append(string s) {
            if (nCharsAppendedOnCurrentLine == 0) {
                AppendIndent();
            }
            int columnsAvailable = output.NumberOfColumns - nCharsAppendedOnCurrentLine;
            if (s.Length < columnsAvailable) {
                output.Append(s);
                nCharsAppendedOnCurrentLine += s.Length;
            } else if (s.Length == columnsAvailable) {
                output.Append(s);
                nCharsAppendedOnCurrentLine = 0;
            } else {
                int i = 0;
                while (i < s.Length) {
                    Append(String.Substring(s, i, System.Math.Min(s.Length - i, columnsAvailable)));
                    i += columnsAvailable;
                }
            }
        }

        public override void Append<T>(IParent<T> parent) {
            Append((object)parent);
            if (parent != null && currentDepth < maxDepth) {
                In();
                int nLinesWrittenSoFar = 0;
                foreach (T child in parent.Children) {
                    AppendLine(child);
                    ++nLinesWrittenSoFar;
                    if (nLinesWrittenSoFar >= maxDepth) {
                        break;
                    }
                }
                Out();
            }
        }

        public override void EndLine() {
            output.EndLine();
            nCharsAppendedOnCurrentLine = 0;
        }

        public virtual void In() {
            if (nCharsAppendedOnCurrentLine > 0) {
                EndLine();
            }
            ++currentDepth;
        }

        public virtual void Out() {
            if (nCharsAppendedOnCurrentLine > 0) {
                EndLine();
            }
            --currentDepth;
        }

        protected void AppendIndent() {
            int spacesToAppend = currentDepth * nIndentSpaces;
            for (int c = 0; c < spacesToAppend; ++c) {
                output.Append(' ');
            }
            nCharsAppendedOnCurrentLine += spacesToAppend;
        }
    }
}
