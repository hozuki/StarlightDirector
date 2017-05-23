using System;
using NAudio.Wave;

namespace StarlightDirector.Previewing.Audio {
    public sealed class NullWaveStream : WaveStream, IDisposable {

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override int Read(byte[] buffer, int offset, int count) {
            Array.Clear(buffer, offset, count);
            return count;
        }

        public override WaveFormat WaveFormat => Format;

        public override long Length => long.MaxValue;

        public override long Position { get; set; }

        public static readonly NullWaveStream Instance = new NullWaveStream();

        private NullWaveStream() {
        }

        private static readonly WaveFormat Format = new WaveFormat();

    }
}
