using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

#if DTRONIX_IPC
namespace DtronixIpc.Transports.Foundation;
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


    public UdsClientConfig(int sendReceiveBufferSize)
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

    public UdsServerConfig(int sendReceiveBufferSize, int maxBufferCount) 
        : base(sendReceiveBufferSize, maxBufferCount)
    {
    }
}
