using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0.JsonInternal {
    //// An execution of writing from an object to a JSON 
    //[Untested]
    //internal class AppendJson { //// : Merrigan0.MetaInternal.Execution {
    //    private MutableString jsonSoFar = new MutableString();

    //    public bool Done { get; protected set; }
    //    public object Value { get; private set; }

    //    protected MutableArray<Chunk> ChunksInProgress { get; private set; }

    //    public AppendJson(object value) {
    //        Value = value;
    //        ChunksInProgress = new MutableArray<Chunk>();
    //        ChunksInProgress.Append(new Chunk() { Value = value });
    //    }

    //    public virtual void AppendName(String name) {
    //        jsonSoFar.Append(Merrigan0.Json.Quote);
    //        Merrigan0.Json.AppendEscaped(jsonSoFar, name);
    //        jsonSoFar.Append(Merrigan0.Json.Quote);
    //        jsonSoFar.Append(Merrigan0.Json.Colon);
    //    }

    //    public virtual void AppendObject(object o) {
    //    }

    //    public virtual void AppendObject(object o, Type type) {
    //    }

    //    public virtual void AppendObjectBegin() {
    //        jsonSoFar.Append('{');
    //    }

    //    public virtual void AppendObjectEnd() {
    //        jsonSoFar.Append('}');
    //    }

    //    public virtual void AppendPropertySeparator() {
    //        jsonSoFar.Append(Merrigan0.Json.Comma);
    //    }

    //    public Merrigan0.Json DoAll() {
    //        while (!Done) {
    //            DoChunk();
    //        }
    //        return ChunksInProgress.Current.First().Json;
    //    }

    //    public void DoChunk() {
    //        // Could be in these situations:
    //        //      haven't done anything yet
    //        //      need to write value
    //        //      need to make progress on Current array
    //        //      need to make progress on Current object
    //        Chunk currentChunk = ChunksInProgress.Current.Last();
    //        if (currentChunk.PropertyEnumerator != null) {
    //            Tuple<String, object> property;
    //            if (currentChunk.PropertyEnumerator.MoveNext()) {
    //                property = currentChunk.PropertyEnumerator.Current;
    //                if (currentChunk.EnumeratorStarted) {
    //                    // Write property separator
    //                    AppendPropertySeparator();
    //                } else {
    //                    currentChunk.EnumeratorStarted = true;
    //                }
    //                AppendName(property.Item1);
    //            } else {
    //                // Wrap up
    //                AppendObjectEnd();
    //                ChunksInProgress.Truncate(1);
    //            }
    //        }

    //    }

    //    public void DoUntilIndex(long i) {
    //        while (!Done && i >= jsonSoFar.Current.Length) {
    //            DoChunk();
    //        }
    //    }

    //    //protected MutableString StringSoFar = new MutableString();

    //    //public void AppendJson(Node node) {
    //    //}

    //    //protected void AppendJsonString(String s) {

    //    //}

    //    //protected void AppendJsonNumber(double number) {
    //    //}

    //    //protected void AppendJsonNode(Node node) {
    //    //}

    //    //protected void AppendJsonArray(ArrayNode nodeArray) {
    //    //}

    //    //protected void AppendJsonBoolean(bool b) {
    //    //    StringSoFar.Append(b ? "true" : "false");
    //    //}

    //    //protected void AppendJsonNull() {
    //    //    StringSoFar.Append("null");
    //    //}

    //    protected struct Chunk {
    //        public bool EnumeratorStarted;
    //        public Merrigan0.Json Json;
    //        public object Value;
    //        public IEnumerator<Tuple<String, object>> PropertyEnumerator;
    //        //public IEnumerator<object> ItemEnumerator;
    //    }

    //    /*
    //     * chunks
    //     * 
    //     * AppendValue
    //     *      write
    //     *      end
    //     * AppendArray
    //     *      AppendArrayBegin
    //     *          write
    //     *          end
    //     *      foreach value AppendValue, maybe separator
    //     *      AppendArrayEnd
    //     * AppendObject
    //     *      AppendObjectBegin
    //     *      foreach property AppendName, colon AppendValue, maybe separator
    //     *      AppendObjectEnd
    //     */


    //}

    ////internal class AppendPrettyJson : AppendJson {
    ////}

    ////internal class NodePrettyJsonText : Text {
    ////    public Node Node { get; private set; }

    ////    public NodePrettyJsonText(Node node) {
    ////        Node = node;
    ////    }

    ////}

    ////// When you have a map but you need a JSON string
    ////internal class NodeJsonString : String {
    ////    private Stack<Node> nodesBeingWritten = new Stack<Node>();
    ////    private bool done;
    ////    private MutableString jsonSoFar = new MutableString();

    ////    public override long Length {
    ////        get {
    ////            if (!done) {
    ////                WriteUntilEndUnchecked();
    ////            }
    ////            return jsonSoFar.Length;
    ////        }
    ////    }
    ////    public Node Node { get; private set; }

    ////    public NodeJsonString(Node node, JsonWriter writer) {
    ////        Node = node;
    ////        nodesBeingWritten.Push(node);
    ////    }

    ////    protected override char GetCharacter(long i) {
    ////        if (!done && i >= jsonSoFar.Length) {
    ////            WriteUntilUnchecked(i);
    ////        }
    ////        return jsonSoFar[i];
    ////    }

    ////    protected bool WriteNode(Node node) {
    ////        Type type = node.GetType(); 
    ////        if (type == typeof(StringNode)) {
    ////            jsonSoFar.Append("null");
    ////            return false;
    ////        }
    ////        BooleanNode booleanNode = node as BooleanNode;
    ////        if (node is BooleanNode) {
    ////            jsonSoFar.Append((Boolea
    ////    }

    ////    // Presumes not done, and i is not already done
    ////    protected void WriteUntilUnchecked(long i) {
    ////        do {
    ////            if (nodesBeingWritten.Count == 0) {
    ////                done = true;
    ////                if (i >= jsonSoFar.Length) {
    ////                    throw new IndexOutOfRangeException();
    ////                }
    ////            }
    ////        } while (i >= jsonSoFar.Length);

    ////    }

    ////    protected void WriteUntilEndUnchecked() {
    ////        done = true;
    ////    }
    ////}
}
