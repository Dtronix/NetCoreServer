using System.Net.Sockets;

#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

public sealed class UdsClientConfig : ConfigBase
{
    public required UnixDomainSocketEndPoint UdsEndpoint { get; init; }


    public UdsClientConfig(int sendReceiveBufferSize = 1024 * 16)
        : base(sendReceiveBufferSize, 3)
    {
    }
}

public sealed class UdsServerConfig : ConfigBase
{
    /// <summary>
    /// Option: acceptor backlog size
    /// </summary>
    /// <remarks>
    /// This option will set the listening socket's backlog size
    /// </remarks>
    public int AcceptorBacklog { get; init; } = 1024;

    public required UnixDomainSocketEndPoint UdsEndpoint { get; init; }

    public UdsServerConfig(int sendReceiveBufferSize = 1024 * 16, int maxBufferCount = 300) 
        : base(sendReceiveBufferSize, maxBufferCount)
    {
    }
}
