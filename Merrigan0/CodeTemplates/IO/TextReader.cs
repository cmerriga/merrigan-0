// Summary:
//     Represents a reader that can read a sequential series of bytes.
//public abstract class TextReader : MarshalByRefObject, IDisposable {
//    // Summary:
//    //     Provides a TextReader with no data to read from.
//    public static readonly TextReader Null;

//    // Summary:
//    //     Initializes a new instance of the System.IO.TextReader class.
//    protected TextReader();

//    // Summary:
//    //     Closes the System.IO.TextReader and releases any system resources associated
//    //     with the TextReader.
//    public virtual void Close(); // don't override; override Dispose(bool)
//    //
//    // Summary:
//    //     Releases all resources used by the System.IO.TextReader object.
//    public void Dispose();
//    //
//    // Summary:
//    //     Releases the unmanaged resources used by the System.IO.TextReader and optionally
//    //     releases the managed resources.
//    //
//    // Parameters:
//    //   disposing:
//    //     true to release both managed and unmanaged resources; false to release only
//    //     unmanaged resources.
//    protected virtual void Dispose(bool disposing);
//    //
//    // Summary:
//    //     Reads the next character without changing the state of the reader or the
//    //     character source. Returns the next available character without actually reading
//    //     it from the input stream.
//    //
//    // Returns:
//    //     An integer representing the next character to be read, or -1 if no more bytes
//    //     are available or the stream does not support seeking.
//    //
//    // Exceptions:
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    public virtual int Peek();
//    //
//    // Summary:
//    //     Reads the next character from the input stream and advances the character
//    //     position by one character.
//    //
//    // Returns:
//    //     The next character from the input stream, or -1 if no more bytes are
//    //     available. The default implementation returns -1.
//    //
//    // Exceptions:
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    public virtual int Read();
//    //
//    // Summary:
//    //     Reads a maximum of count bytes from the Current stream and writes the
//    //     data to buffer, beginning at index.
//    //
//    // Parameters:
//    //   buffer:
//    //     When this method returns, contains the specified character array with the
//    //     values between index and (index + count - 1) replaced by the bytes read
//    //     from the Current source.
//    //
//    //   index:
//    //     The position in buffer at which to begin writing.
//    //
//    //   count:
//    //     The maximum number of bytes to read. If the end of the stream is reached
//    //     before count of bytes is read into buffer, the Current method returns.
//    //
//    // Returns:
//    //     The number of bytes that have been read. The number will be less than
//    //     or equal to count, depending on whether the data is available within the
//    //     stream. This method returns zero if called when no more bytes are left
//    //     to read.
//    //
//    // Exceptions:
//    //   System.ArgumentNullException:
//    //     buffer is null.
//    //
//    //   System.ArgumentException:
//    //     The buffer length minus index is less than count.
//    //
//    //   System.ArgumentOutOfRangeException:
//    //     index or count is negative.
//    //
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    public virtual int Read(char[] buffer, int index, int count);
//    //
//    // Summary:
//    //     Reads a maximum of count bytes from the Current stream, and writes the
//    //     data to buffer, beginning at index.
//    //
//    // Parameters:
//    //   buffer:
//    //     When this method returns, this parameter contains the specified character
//    //     array with the values between index and (index + count -1) replaced by the
//    //     bytes read from the Current source.
//    //
//    //   index:
//    //     The position in buffer at which to begin writing.
//    //
//    //   count:
//    //     The maximum number of bytes to read.
//    //
//    // Returns:
//    //     The position of the underlying stream is advanced by the number of bytes
//    //     that were read into buffer.The number of bytes that have been read.
//    //     The number will be less than or equal to count, depending on whether all
//    //     input bytes have been read.
//    //
//    // Exceptions:
//    //   System.ArgumentNullException:
//    //     buffer is null.
//    //
//    //   System.ArgumentException:
//    //     The buffer length minus index is less than count.
//    //
//    //   System.ArgumentOutOfRangeException:
//    //     index or count is negative.
//    //
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    public virtual int ReadBlock(char[] buffer, int index, int count);
//    //
//    // Summary:
//    //     Reads a line of bytes from the Current stream and returns the data as
//    //     a string.
//    //
//    // Returns:
//    //     The next line from the input stream, or null if all bytes have been
//    //     read.
//    //
//    // Exceptions:
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    //
//    //   System.OutOfMemoryException:
//    //     There is insufficient memory to allocate a buffer for the returned string.
//    //
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.ArgumentOutOfRangeException:
//    //     The number of bytes in the next line is larger than System.Int32.MaxValue
//    public virtual string ReadLine();
//    //
//    // Summary:
//    //     Reads all bytes from the Current position to the end of the TextReader
//    //     and returns them as one string.
//    //
//    // Returns:
//    //     A string containing all bytes from the Current position to the end of
//    //     the TextReader.
//    //
//    // Exceptions:
//    //   System.IO.IOException:
//    //     An I/O error occurs.
//    //
//    //   System.ObjectDisposedException:
//    //     The System.IO.TextReader is closed.
//    //
//    //   System.OutOfMemoryException:
//    //     There is insufficient memory to allocate a buffer for the returned string.
//    //
//    //   System.ArgumentOutOfRangeException:
//    //     The number of bytes in the next line is larger than System.Int32.MaxValue
//    public virtual string ReadToEnd();
//    //
//    // Summary:
//    //     Creates a thread-safe wrapper around the specified TextReader.
//    //
//    // Parameters:
//    //   reader:
//    //     The TextReader to synchronize.
//    //
//    // Returns:
//    //     A thread-safe System.IO.TextReader.
//    //
//    // Exceptions:
//    //   System.ArgumentNullException:
//    //     reader is null.
//    public static TextReader Synchronized(TextReader reader);
//}

//using System;

//namespace Merrigan0 {
//    public abstract class TextReader : IDisposable {
//        public void Close() { Dispose(); }
//        public abstract void Dispose();
//        public abstract int Peek();
//        public abstract int Read();
//        public abstract int Read(char[] buffer, long index, long count);
//        public abstract String ReadLine();
//        public abstract String ReadToEnd();
//    }

//    // Use when you have a System.IO.TextReader but you want a Merrigan0.TextReader.
//    public class TextReaderTextReader : IDisposable {
//        private System.IO.TextReader textReader;

//        public TextReaderTextReader(System.IO.TextReader textReader) { this.textReader = textReader; }

//        public override void Dispose() { textReader.Dispose(); }
//        public override int Peek() { return textReader.Peek(); }
//        public override int Read() { return textReader.Read(); }

//        public override int Read(char[] buffer, long index, long count) {
//            return textReader.Read(buffer, (int)index, (int)count);
//        }

//        public override String ReadLine() { return textReader.ReadLine(); }
//        public override String ReadToEnd() { return textReader.ReadToEnd(); }
//    }
//}
