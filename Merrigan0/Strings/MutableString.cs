using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;
using Merrigan0.MetaInternal; // ChangeTracker
using Merrigan0.StringsInternal;

namespace Merrigan0 {
    [Untested]
    public class MutableString /*: String */ {
        public static MutableString operator +(MutableString s, char ch) {
            s.Append(ch);
            return s;
        }

        public static MutableString operator +(MutableString s, string systemString) {
            s.Append(systemString);
            return s;
        }

        public static MutableString operator +(MutableString s1, String s2) {
            s1.Append(s2);
            return s1;
        }

        public static MutableString operator +(MutableString s, object o) {
            if (o == null) {
                s.Append(String.NullRepresentation);
            } else {
                s.Append(o.ToString());
            }
            return s;
        }

        private static Change<String> Root = new Change<String>() { Value = String.Empty };

        private ChangeTracker<String> changeTracker;

        public virtual String Current { get { return changeTracker.Current; } }
        public virtual Change<String> History { get { return changeTracker.History; } }

        public MutableString(String s) {
            changeTracker = new ChangeTracker<String>(new Change<String>() { Value = s });
        }

        public MutableString() : this(String.Empty) { }

        public void Append(char ch) { changeTracker.Change(new AppendString(Current, ch)); }
        public void Append(string s) { changeTracker.Change(new AppendSystemStringString(Current, s)); }
        public void Append(String s) { changeTracker.Change(new ConcatenateString(Current, s)); }
        public void Append(object o) { Append(o.ToString()); }
        public void Insert(long i, char ch) { changeTracker.Change(new InsertCharacterString(Current, i, ch)); }
        public void Insert(long i, string s) { Insert(i, (String)s); }
        public void Insert(long i, String s) { changeTracker.Change(new InsertString(Current, i, s)); }
        public void Prepend(char ch) { changeTracker.Change(new PrependString(Current, ch)); }
        public void Prepend(string s) { new PrependSystemStringString(Current, s); }
        public void Prepend(String s) { changeTracker.Change(new ConcatenateString(s, Current)); }
        public void Remove(long i, int length) { changeTracker.Change(new RemoveString(Current, i, length)); }
        public void Replace(string unwanted, string wanted) { Replace((String)unwanted, (String)wanted); }

        public void Replace(String unwanted, String wanted) {
            throw new NotImplementedException();
            //MutableString newStringSoFar = new MutableString();

            //// We need these things: one, where we are in the string
            //long i = 0;

            //// Two, where the start of the most recent preservable part of the string is
            //long? iPreservable = null;

            //// Three, where the next deletable part of the string is
            //long iNeedingDelete = -1;
            
            //while (i < Length) {
            //    if (!TryGetIndex(unwanted, i, out iWanted) {
            //        break;
            //    }



            //}
        }

        public override string ToString() { return Current; } //// should cache for awhile
    }
}
