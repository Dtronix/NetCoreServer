﻿using System;
using System.Buffers;

#if DTRONIX_IPC
namespace DtronixIpc.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

/// <summary>
/// Dynamic T buffer
/// </summary>
internal class MemoryBuffer<T> : ITransportBufferWriter<T>
    where T : struct
{
    private readonly MemoryBufferPool<T> _pool;

    private Memory<T> _data;

    private int _size;

    /// <summary>
    /// Is the buffer empty?
    /// </summary>
    public bool IsEmpty => _size == 0;

    /// <summary>
    /// Bytes memory buffer capacity
    /// </summary>
    public int Capacity => _data.Length;
    /// <summary>
    /// Bytes memory buffer size
    /// </summary>
    public int Size => _size;

    public int Remaining => _data.Length - _size;

    private int _offset = 0;

    public int Offset => _offset;

    public Memory<T> Contents => _data.Slice(_offset, _size - _offset);
    public Memory<T> Data => _data;


    /// <summary>
    /// Initialize a new expandable buffer with the given data
    /// </summary>
    internal MemoryBuffer(MemoryBufferPool<T> pool, in Memory<T> data)
    {
        _pool = pool;
        _data = data;
        _size = 0;
    }

    public void AddOffset(int offset)
    {
        _offset += offset;
    }

    // Clear the current buffer and its offset
    public void Clear()
    {
        _size = 0;
        _offset = 0;
    }

    /// <summary>
    /// Append the single T
    /// </summary>
    /// <param name="value">Byte value to append</param>
    /// <returns>Count of append bytes</returns>
    public int Append(T value)
    {
        _data.Span[_size] = value;
        _size += 1;
        return 1;
    }

    /// <summary>
    /// Append the given span of bytes
    /// </summary>
    /// <param name="buffer">Buffer to append as a span of bytes</param>
    /// <returns>Count of append bytes</returns>
    public int Append(in ReadOnlySpan<T> buffer)
    {
        buffer.CopyTo(_data.Span.Slice(_size));
        _size += buffer.Length;
        return buffer.Length;
    }

    /// <summary>
    /// Append the given span of bytes
    /// </summary>
    /// <param name="buffer">Buffer to append as a span of bytes</param>
    /// <returns>Count of append bytes</returns>
    public int Append(in ReadOnlySequence<T> buffer)
    {
        var length = (int)buffer.Length;

        buffer.CopyTo(_data.Span.Slice(_size));
        _size += length;
        return length;
    }

    public void ReturnToPool()
    {
        Clear();
        _pool.Return(this);
    }

    public void Advance(int count)
    {
        if (count > Remaining)
            throw new OutOfMemoryException("Can't advance past the end of the buffer.");

        _size += count;
    }

    public Memory<T> GetMemory(int sizeHint = 0)
    {
        // Return the maximum buffer size left.
        if(sizeHint == 0)
            return _data.Slice(_size, Remaining);

        if (sizeHint > Remaining)
            throw new OutOfMemoryException("Can't advance past the end of the buffer.");

        return _data.Slice(_size, sizeHint);

    }

    public Span<T> GetSpan(int sizeHint = 0)
    {
        // Return the maximum buffer size left.
        if (sizeHint == 0)
            return _data.Span.Slice(_size, Remaining);

        if (sizeHint > Remaining)
            throw new OutOfMemoryException("Can't advance past the end of the buffer.");

        return _data.Span.Slice(_size, sizeHint);
    }
}
