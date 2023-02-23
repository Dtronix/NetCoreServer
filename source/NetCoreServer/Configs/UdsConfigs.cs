using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreServer.Configs;

internal sealed class UdsClientContext : ContextBase
{
    private readonly UnixDomainSocketEndPoint _udsEndpoint;

    public UnixDomainSocketEndPoint UdsEndpoint
    {
        get => _udsEndpoint;
        init => _udsEndpoint = value;
    }


    public UdsClientContext(int sendReceiveBufferSize)
        : base(sendReceiveBufferSize, 3)
    {
    }
}

internal sealed class UdsServerContext : ContextBase
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

    public UdsServerContext(int sendReceiveBufferSize, int maxBufferCount) 
        : base(sendReceiveBufferSize, maxBufferCount)
    {
    }
}
