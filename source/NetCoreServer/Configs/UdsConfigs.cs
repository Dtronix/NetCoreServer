using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreServer.Configs;

internal sealed class UdsClientConfig : ClientConfigBase
{
    private readonly UnixDomainSocketEndPoint _udsEndpoint;

    public UnixDomainSocketEndPoint UdsEndpoint
    {
        get => _udsEndpoint;
        init => _udsEndpoint = value;
    }
}

internal sealed class UdsServerConfig : ServerConfigBase<UdsSession>
{
    private readonly UnixDomainSocketEndPoint _udsEndpoint;

    public UnixDomainSocketEndPoint UdsEndpoint
    {
        get => _udsEndpoint;
        init => _udsEndpoint = value;
    }
}