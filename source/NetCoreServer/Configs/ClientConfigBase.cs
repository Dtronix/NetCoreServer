using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreServer.Configs;
internal abstract class ClientConfigBase
{
    internal ClientEvents Events;

    internal MemoryBufferPool<byte> MemoryPool;

    /// <summary>
    /// Option: receive buffer size
    /// </summary>
    public int SendReceiveBufferSize { get; init; } = 1024 * 16;
}
