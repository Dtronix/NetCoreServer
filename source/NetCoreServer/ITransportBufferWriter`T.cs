using System;
using System.Buffers;

#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

public interface ITransportBufferWriter<T> : IBufferWriter<T>
{
    public int Remaining { get; }

    /// <summary>
    /// Append the single T
    /// </summary>
    /// <param name="value">Byte value to append</param>
    /// <returns>Count of append bytes</returns>
    int Append(T value);

    /// <summary>
    /// Append the given span of bytes
    /// </summary>
    /// <param name="buffer">Buffer to append as a span of bytes</param>
    /// <returns>Count of append bytes</returns>
    int Append(in ReadOnlySpan<T> buffer);

    /// <summary>
    /// Append the given span of bytes
    /// </summary>
    /// <param name="buffer">Buffer to append as a span of bytes</param>
    /// <returns>Count of append bytes</returns>
    int Append(in ReadOnlySequence<T> buffer);
}
