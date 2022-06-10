using System;
using System.IO;

namespace WebApiSecurityDemo.Utils.Streams
{
    public class RestrictedStream : Stream
    {
        private readonly Stream _stream;
        private long _length = 0L;

        public RestrictedStream(Stream stream, long maxLength)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            MaxLength = maxLength;
        }

        public long MaxLength { get; }

        public override bool CanRead => _stream.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _stream.Length;

        public override long Position
        {
            get => _stream.Position;
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var result = _stream.Read(buffer, offset, count);

            _length += result;

            if (_length > MaxLength)
                throw new Exception("Stream is larger than the maximum allowed size.");

            return result;
        }

        // TODO ReadAsync

        public override void Flush() => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            _stream.Dispose();
            base.Dispose(disposing);
        }
    }
}