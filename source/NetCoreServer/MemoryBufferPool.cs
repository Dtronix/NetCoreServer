using System;
using System.Collections.Concurrent;
using System.Threading;

#if DTRONIX_IPC
namespace DtronixIpc.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

internal class MemoryBufferPool<T>
    where T : struct
{
    private readonly int _bufferSize;
    private readonly int _maxBuffers;

    private readonly ConcurrentBag<MemoryBuffer<T>> _freeBuffers = new ConcurrentBag<MemoryBuffer<T>>();

    private readonly T[] _buffer;

    private int consumedBufferIndex = -1;

    public MemoryBufferPool(int bufferSize, int maxBuffers)
    {
        _bufferSize = bufferSize;
        _maxBuffers = maxBuffers;

        _buffer = GC.AllocateUninitializedArray<T>((bufferSize * maxBuffers), true);
    }

    public bool TryRent(out MemoryBuffer<T>? buffer)
    {
        if (!_freeBuffers.TryTake(out buffer))
        {
            var index = Interlocked.Increment(ref consumedBufferIndex);
            if (index > _maxBuffers)
                return false;

            buffer = new MemoryBuffer<T>(this, new Memory<T>(_buffer, index * _bufferSize, _bufferSize));
        }

        return true;
    }

    public void Return(MemoryBuffer<T> memoryBuffer)
    {
        _freeBuffers.Add(memoryBuffer);
    }
}
