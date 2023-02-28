#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

internal partial class UdsServer
{
    private readonly UdsServerConfig _config;

    /// <summary>
    /// Option: acceptor backlog size
    /// </summary>
    /// <remarks>
    /// This option will set the listening socket's backlog size
    /// </remarks>
    public int OptionAcceptorBacklog => _config.AcceptorBacklog;
    /// <summary>
    /// Option: receive buffer size
    /// </summary>
    public int OptionReceiveBufferSize => _config.SendReceiveBufferSize;
    /// <summary>
    /// Option: send buffer size
    /// </summary>
    public int OptionSendBufferSize => _config.SendReceiveBufferSize;

    /// <summary>
    /// Initialize Unix Domain Socket client.
    /// </summary>
    public UdsServer(UdsServerConfig config)
        : this(config.UdsEndpoint)
    {
        _config = config;
    }
}

