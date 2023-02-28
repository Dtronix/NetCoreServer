using System.Net.Sockets;

#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

public sealed class UdsClientConfig : ConfigBase
{
    private readonly UnixDomainSocketEndPoint _udsEndpoint;

    public UnixDomainSocketEndPoint UdsEndpoint
    {
        get => _udsEndpoint;
        init => _udsEndpoint = value;
    }


    public UdsClientConfig(int sendReceiveBufferSize = 1024 * 16)
        : base(sendReceiveBufferSize, 3)
    {
    }
}

public sealed class UdsServerConfig : ConfigBase
{
    private readonly UnixDomainSocketEndPoint _udsEndpoint;

    /// <summary>
    /// Option: acceptor backlog size
    /// </summary>
    /// <remarks>
    /// This option will set the listening socket's backlog size
    /// </remarks>
    public int AcceptorBacklog { get; init; } = 1024;

    public UnixDomainSocketEndPoint UdsEndpoint
    {
        get => _udsEndpoint;
        init => _udsEndpoint = value;
    }

    public UdsServerConfig(int sendReceiveBufferSize = 1024 * 16, int maxBufferCount = 300) 
        : base(sendReceiveBufferSize, maxBufferCount)
    {
    }
}
