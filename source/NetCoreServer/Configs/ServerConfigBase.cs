using System.Threading;

namespace NetCoreServer.Configs;

internal abstract class ServerConfigBase<TSession> : ConfigBase
{
    internal ServerEvents<TSession> Events;

    /// <summary>
    /// Option: acceptor backlog size
    /// </summary>
    /// <remarks>
    /// This option will set the listening socket's backlog size
    /// </remarks>
    public int AcceptorBacklog { get; init; } = 1024;
}

internal abstract class ConfigBase
{
    /// <summary>
    /// Send & receive buffer size
    /// </summary>
    public int SendReceiveBufferSize { get; init; } = 1024 * 16;

    public MemoryBufferPool<byte> MemoryPool { get; set; }

    private long _sessionCounter = 0;

    public long GetNewSessionId()
    {
        return Interlocked.Increment(ref _sessionCounter);
    }
}