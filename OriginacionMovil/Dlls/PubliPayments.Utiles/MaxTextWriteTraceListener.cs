using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Text;

namespace PubliPayments.Utiles
{
    // 20150826 - Maximiliano Jesús Nazareno Silva
    // Modificación al TextWriteTraceListener de MS para que incluya un rolling diario 

    /// <devdoc> 
    ///    <para>Directs tracing or debugging output to
    ///       a <see cref="T:System.IO.TextWriter"> or to a <see cref="T:System.IO.Stream">,
    ///       such as <see cref="F:System.Console.Out"> or <see cref="T:System.IO.FileStream">.</see></see></see></see></para>
    /// </devdoc> 
    [HostProtection(Synchronization = true)]
    public class MaxTextWriteTraceListener : TraceListener
    {
        internal TextWriter writer;
        private String _fileName;
        private string _rollingFileName;

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref="System.Diagnostics.TextWriterTraceListener"> class with
        /// <see cref="System.IO.TextWriter">
        /// as the output recipient.</see></see></para> 
        /// </devdoc>
        public MaxTextWriteTraceListener()
        {
        }

        /// <devdoc> 
        /// <para>Initializes a new instance of the <see cref="System.Diagnostics.TextWriterTraceListener"> class, using the
        ///    stream as the recipient of the debugging and tracing output.</see></para>
        /// </devdoc>
        public MaxTextWriteTraceListener(Stream stream)
            : this(stream, string.Empty)
        {
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref="System.Diagnostics.TextWriterTraceListener"> class with the 
        ///    specified name and using the stream as the recipient of the debugging and tracing output.</see></para>
        /// </devdoc>
        public MaxTextWriteTraceListener(Stream stream, string name)
            : base(name)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            writer = new StreamWriter(stream);
        }

        /// <devdoc> 
        /// <para>Initializes a new instance of the <see cref="System.Diagnostics.TextWriterTraceListener"> class using the
        ///    specified writer as recipient of the tracing or debugging output.</see></para>
        /// </devdoc>
        public MaxTextWriteTraceListener(TextWriter writer)
            : this(writer, string.Empty)
        {
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref="System.Diagnostics.TextWriterTraceListener"> class with the 
        ///    specified name and using the specified writer as recipient of the tracing or
        ///    debugging
        ///    output.</see></para>
        /// </devdoc> 
        public MaxTextWriteTraceListener(TextWriter writer, string name)
            : base(name)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            this.writer = writer;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc> 
        [ResourceExposure(ResourceScope.Machine)]
        public MaxTextWriteTraceListener(string fileName)
        {
            _fileName = fileName;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        public MaxTextWriteTraceListener(string fileName, string name)
            : base(name)
        {
            _fileName = fileName;
        }

        /// <devdoc> 
        ///    <para> Indicates the text writer that receives the tracing
        ///       or debugging output.</para>
        /// </devdoc>
        public TextWriter Writer
        {
            get
            {
                EnsureWriter();
                return writer;
            }

            set { writer = value; }
        }

        /// <devdoc> 
        /// <para>Closes the <see cref="System.Diagnostics.TextWriterTraceListener.Writer"> so that it no longer 
        ///    receives tracing or debugging output.</see></para>
        /// </devdoc> 
        public override void Close()
        {
            if (writer != null)
            {
                try
                {
                    writer.Close();
                }
                catch (ObjectDisposedException)
                {
                }
            }

            writer = null;
        }

        /// <internalonly>
        /// <devdoc>
        /// </devdoc></internalonly> 
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    Close();
                }
                else
                {
                    // clean up resources
                    if (writer != null)
                        try
                        {
                            writer.Close();
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    writer = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <devdoc> 
        /// <para>Flushes the output buffer for the <see cref="System.Diagnostics.TextWriterTraceListener.Writer">.</see></para> 
        /// </devdoc>
        public override void Flush()
        {
            if (!EnsureWriter()) return;
            try
            {
                writer.Flush();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <devdoc> 
        ///    <para>Writes a message
        ///       to this instance's <see cref="System.Diagnostics.TextWriterTraceListener.Writer">.</see></para> 
        /// </devdoc>
        public override void Write(string message)
        {
            if (!EnsureWriter()) return;
            if (NeedIndent) WriteIndent();
            try
            {
                writer.Write(message);
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <devdoc>
        ///    <para>Writes a message
        ///       to this instance's <see cref="System.Diagnostics.TextWriterTraceListener.Writer"> followed by a line terminator. The
        ///       default line terminator is a carriage return followed by a line feed (\r\n).</see></para> 
        /// </devdoc>
        public override void WriteLine(string message)
        {
            if (!EnsureWriter()) return;
            if (NeedIndent) WriteIndent();
            try
            {
                writer.WriteLine(message);
                NeedIndent = true;
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private static Encoding GetEncodingWithFallback(Encoding encoding)
        {
            // Clone it and set the "?" replacement fallback
            Encoding fallbackEncoding = (Encoding) encoding.Clone();
            fallbackEncoding.EncoderFallback = EncoderFallback.ReplacementFallback;
            fallbackEncoding.DecoderFallback = DecoderFallback.ReplacementFallback;

            return fallbackEncoding;
        }

        // This uses a machine resource, scoped by the fileName variable. 
        [ResourceExposure(ResourceScope.None)]
        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        internal bool EnsureWriter()
        {
            bool ret = true;

            if ((_rollingFileName == null && _fileName != null)
                || (_fileName != null && _rollingFileName !=
                    Path.GetDirectoryName(_fileName) + "\\" +
                    Path.GetFileNameWithoutExtension(_fileName) + "_" +
                    DateTime.Now.ToString("yyyyMMdd") +
                    Path.GetExtension(_fileName)))
            {
                if (writer != null)
                    try
                    {
                        writer.Flush();
                        writer.Close();
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                writer = null;
                _rollingFileName = Path.GetDirectoryName(_fileName) + "\\" +
                                   Path.GetFileNameWithoutExtension(_fileName) + "_" +
                                   DateTime.Now.ToString("yyyyMMdd") +
                                   Path.GetExtension(_fileName);
            }

            if (writer == null)
            {
                ret = false;

                if (_fileName == null || _rollingFileName == null)
                    return false;

                // StreamWriter by default uses UTF8Encoding which will throw on invalid encoding errors.
                // This can cause the internal StreamWriter's state to be irrecoverable. It is bad for tracing
                // APIs to throw on encoding errors. Instead, we should provide a "?" replacement fallback
                // encoding to substitute illegal chars. For ex, In case of high surrogate character 
                // D800-DBFF without a following low surrogate character DC00-DFFF
                // NOTE: We also need to use an encoding that does't emit BOM whic is StreamWriter's default 
                Encoding noBoMwithFallback = GetEncodingWithFallback(new UTF8Encoding(false));


                // To support multiple appdomains/instances tracing to the same file,
                // we will try to open the given file for append but if we encounter
                // IO errors, we will prefix the file name with a unique GUID value
                // and try one more time 
                string fullPath = Path.GetFullPath(_rollingFileName);
                string dirPath = Path.GetDirectoryName(fullPath);
                string fileNameOnly = Path.GetFileName(fullPath);

                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        writer = new StreamWriter(fullPath, true, noBoMwithFallback, 4096);
                        ret = true;
                        break;
                    }
                    catch (IOException)
                    {

                        // Should we do this only for ERROR_SHARING_VIOLATION?
                        //if (InternalResources.MakeErrorCodeFromHR(Marshal.GetHRForException(ioexc)) == InternalResources.ERROR_SHARING_VIOLATION) { 

                        fileNameOnly = Guid.NewGuid().ToString() + fileNameOnly;
                        if (dirPath != null) fullPath = Path.Combine(dirPath, fileNameOnly);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        //ERROR_ACCESS_DENIED, mostly ACL issues 
                        break;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                if (!ret)
                {
                    // Disable tracing to this listener. Every Write will be nop. 
                    // We need to think of a central way to deal with the listener
                    // init errors in the future. The default should be that we eat 
                    // up any errors from listener and optionally notify the user
                    _fileName = null;
                }
            }
            return ret;
        }
    }
}
