﻿using System.Threading;
using NetCoreServer;

#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

public abstract class ConfigBase
{
    private long _sessionCounter = 0;

    /// <summary>
    /// Send & receive buffer size
    /// </summary>
    public int SendReceiveBufferSize { get; }

    internal MemoryBufferPool<byte> MemoryPool { get; }

    protected ConfigBase(int sendReceiveBufferSize, int maxBufferCount)
    {
        SendReceiveBufferSize = sendReceiveBufferSize;
        MemoryPool = new MemoryBufferPool<byte>(sendReceiveBufferSize, maxBufferCount);
    }

    public long GetNewSessionId()
    {
        return Interlocked.Increment(ref _sessionCounter);
    }
}