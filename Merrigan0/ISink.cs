using System;
using System.Collections;
using System.Collections.Generic;

namespace Merrigan0 {
    // A passive sink.
    public interface ISink<T> : IDisposable {
        // Faster than TryAdd but will throw an exception if unable to be completed.
        // Exceptions:
        //      SinkUnavailableException, when the sink is temporarily unable to receive items
        //      ObjectDisposedException, when the sink is permanently unable to receive items
        void Add(T item);

        // Faster than TryAdd but will throw an exception if unable to be completed.
        // Exceptions:
        //      SinkUnavailableException, when the sink is temporarily unable to receive items
        //      ObjectDisposedException, when the sink is permanently unable to receive items
        void Add(Array<T> items);

        bool TryAdd(T item);

        bool TryAdd(Array<T> items);
    }

    // A passive sink.
    public class ExampleSink<T> : ISink<T> {
        // Faster than TryAdd but will throw an exception if unable to be completed.
        // Exceptions:
        //      SinkUnavailableException, when the sink is temporarily unable to receive items
        //      ObjectDisposedException, when the sink is permanently unable to receive items
        public void Add(T item) {
            ////
        }

        // Faster than TryAdd but will throw an exception if unable to be completed.
        // Exceptions:
        //      SinkUnavailableException, when the sink is temporarily unable to receive items
        //      ObjectDisposedException, when the sink is permanently unable to receive items
        public void Add(Array<T> items) {
            ////
        }

        public void Dispose() {
            ////
        }

        public bool TryAdd(T item) {
            ////
            return true; ////
        }

        public bool TryAdd(Array<T> items) {
            ////
            return true; ////
        }
    }
}
