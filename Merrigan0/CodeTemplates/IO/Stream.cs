namespace Merrigan0.Internal.CodeTemplates.IO {
    /// <summary>
    /// Provides a generic view of a sequence of bytes. This is an abstract class.
    /// </summary>
    public abstract class Stream {
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <value><c>true</c> if the stream supports reading; otherwise, <c>false</c>.</value>
        public abstract bool CanRead { get; }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <value><c>true</c> if the stream supports seeking; otherwise, <c>false</c>.</value>
        public abstract bool CanSeek { get; }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value><c>true</c> if the stream supports writing; otherwise, <c>false</c>.</value>
        public abstract bool CanWrite { get; }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <value>A long value representing the length of the stream in bytes.</value>
        /// <exception cref="System.NotSupportedException">A class derived from <see cref="Stream"/> does not support seeking.</exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract long Length { get; }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <value>The current position within the stream.</value>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.NotSupportedException">A class derived from <see cref="Stream"/> does not support seeking.</exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract long Position { get; set; }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and 
        /// causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        public abstract void Flush();

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current
        /// stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified
        /// byte array with the values between offset and (offset + count - 1) replaced
        /// by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in buffer at which to begin storing the data
        /// read from the current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the
        /// number of bytes requested if that many bytes are not currently available,
        /// or zero (0) if the end of the stream has been reached.
        /// </returns>
        /// <exception cref="System.ArgumentException">The sum of <c>offset</c> and <c>count</c> is larger than the buffer length.</exception>
        /// <exception cref="System.ArgumentNullException"><c>buffer</c> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><c>offset</c> or <c>count</c> is negative.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract int Read(byte[] buffer, int offset, int count);

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <c>origin parameter</c>.</param>
        /// <param name="origin">
        /// A value of type <see cref="System.IO.SeekOrigin"/> indicating the reference point used
        /// </param>
        /// <returns>The new position within the current stream.</returns>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.NotSupportedException">
        /// The stream does not support seeking, such as if the stream is constructed
        /// from a pipe or console output.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract long Seek(long offset, System.IO.SeekOrigin origin);

        /// <summary>When overridden in a derived class, sets the length of the current stream.</summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.NotSupportedException">
        /// The stream does not support both writing and seeking, such as if the stream 
        /// is constructed from a pipe or console output.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract void SetLength(long value);

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number
        /// of bytes written.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. This method copies count bytes from <c>buffer</c> to the current
        /// stream.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in <c>buffer</c> at which to begin copying bytes to the
        /// current stream.
        /// </param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        /// <exception cref="System.ArgumentException">The sum of <c>offset</c> and <c>count</c> is larger than the buffer length.</exception>
        /// <exception cref="System.ArgumentNullException"><c>buffer</c> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><c>offset</c> or <c>count</c> is negative.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occured, such as the specified file cannot be found.</exception>
        /// <exception cref="System.NotSupportedException">The stream does not support writing.</exception>
        /// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public abstract void Write(byte[] buffer, int offset, int count);
    }
}
