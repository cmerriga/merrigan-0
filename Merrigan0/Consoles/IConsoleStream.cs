using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public interface IConsoleStream {
        int NumberOfColumns { get; }
        void Append(char ch);
        void Append(string s);
        void Append(String s);
        void Append(object o);
        void Append<T>(IParent<T> parent);
        void AppendLine(string s);
        void AppendLine(String s);
        void AppendLine(object o);
        void AppendLine<T>(IParent<T> parent);
        void EndLine();
        //void Write(char ch);
        //void Write(string s);
        //void Write(String s);
        //void WriteLine(string s);
        //void WriteLine(String s);
    }
}
