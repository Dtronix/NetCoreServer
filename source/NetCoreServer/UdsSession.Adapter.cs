using System;
using System.Net.Sockets;

#if BUILD_NEXNET
namespace NexNet.Transports.Foundation;
#else
namespace NetCoreServer;
#endif

internal partial class UdsSession
{
    private readonly UdsServerConfig _config;
    private MemoryBuffer<byte> _receiveBuffer;
    private MemoryBuffer<byte> _sendBufferMain;
    private MemoryBuffer<byte> _sendBufferFlush;

    /// <summary>
    /// Session Id
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Option: receive buffer limit
    /// </summary>
    public int OptionReceiveBufferLimit => _config.SendReceiveBufferSize;

    /// <summary>
    /// Option: receive buffer size
    /// </summary>
    public int OptionReceiveBufferSize => _config.SendReceiveBufferSize;
    /// <summary>
    /// Option: send buffer limit
    /// </summary>
    public int OptionSendBufferLimit => _config.SendReceiveBufferSize;
    /// <summary>
    /// Option: send buffer size
    /// </summary>
    public int OptionSendBufferSize => _config.SendReceiveBufferSize;

    public UdsSession(UdsServerConfig config, UdsServer server)
    {
        _config = config;
        Server = server;
        Id = config.GetNewSessionId();

        if (!_config.MemoryPool.TryRent(out _receiveBuffer) ||
            !_config.MemoryPool.TryRent(out _sendBufferMain) ||
            !_config.MemoryPool.TryRent(out _sendBufferFlush))
            throw new InvalidOperationException("Memory pool depleted.");
    }

    /// <summary>
    /// Initialize the session with a given server
    /// </summary>
    /// <param name="server">Unix Domain Socket server</param>
    public UdsSession(UdsServer server)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Send data to the server (asynchronous)
    /// </summary>
    /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
    public bool SendAsync(Action<ITransportBufferWriter<byte>> writerAction)
    {
        if (!IsConnected)
            return false;

        lock (_sendLock)
        {
            writerAction.Invoke(_sendBufferMain);

            // Update statistic
            BytesPending = _sendBufferMain.Size;

            // Avoid multiple send handlers
            if (_sending)
                return true;
            else
                _sending = true;

            // Try to send the main buffer
            TrySend();
        }

        return true;
    }
}

