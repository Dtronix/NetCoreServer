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

    protected ServerConfigBase(int sendReceiveBufferSize, int maxBufferCount) 
        : base(sendReceiveBufferSize, maxBufferCount)
    {
    }
}
