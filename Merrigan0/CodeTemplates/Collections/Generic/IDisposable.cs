using System;

namespace Merrigan0.Internal.CodeTemplates.Collections.Generic {
    /// <summary>
    /// Provides a mechanism for releasing unmanaged resources.
    /// </summary>
    internal interface IDisposable {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void Dispose();
    }

    internal class ExampleDisposable<T> : System.IDisposable {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            ////
        }
    }
}
