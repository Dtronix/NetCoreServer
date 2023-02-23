using System.Threading;

namespace NetCoreServer.Configs;

internal abstract class ConfigBase
{
    private long _sessionCounter = 0;

    /// <summary>
    /// Send & receive buffer size
    /// </summary>
    public int SendReceiveBufferSize { get; }

    public MemoryBufferPool<byte> MemoryPool { get; }

    protected ConfigBase(int sendReceiveBufferSize, int maxBufferCount)
    {
        SendReceiveBufferSize = sendReceiveBufferSize;
        MemoryPool = new MemoryBufferPool<byte>(sendReceiveBufferSize, 3);
    }

    public long GetNewSessionId()
    {
        return Interlocked.Increment(ref _sessionCounter);
    }
}
