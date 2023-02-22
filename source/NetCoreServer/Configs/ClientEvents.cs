using System;
using System.Net.Sockets;

namespace NetCoreServer.Configs;

internal class ClientEvents
{
    internal readonly Action? OnConnectingInternal;
    internal readonly Action? OnConnectedInternal;
    internal readonly Action? OnDisconnectingInternal;
    internal readonly Disconnect? OnDisconnectedInternal;
    internal readonly Received? OnReceivedInternal;
    internal readonly Sent? OnSentInternal;
    internal readonly Error? OnErrorInternal;
    internal readonly Action? OnEmptyInternal;
    public readonly Error? OnSendErrorInternal;

    /// <summary>
    /// Handle buffer received notification
    /// </summary>
    /// <remarks>
    /// Notification is called when another chunk of buffer was received from the server
    /// </remarks>
    public delegate void Received(ReadOnlyMemory<byte> readOnlyMemory);

    public delegate void Disconnect();

    /// <summary>
    /// Handle buffer sent notification
    /// </summary>
    /// <param name="sent">Size of sent buffer</param>
    /// <param name="pending">Size of pending buffer</param>
    /// <remarks>
    /// Notification is called when another chunk of buffer was sent to the server.
    /// This handler could be used to send another buffer to the server for instance when the pending size is zero.
    /// </remarks>
    public delegate void Sent(long sent, long pending);

    /// <summary>
    /// Handle error notification
    /// </summary>
    /// <param name="error">Socket error code</param>
    public delegate void Error(SocketError error);

    /// <summary>
    /// Handle client connecting notification
    /// </summary>
    public Action? OnConnecting
    {
        get => OnConnectingInternal;
        init => OnConnectingInternal = value;
    }

    /// <summary>
    /// Handle client connected notification
    /// </summary>
    public Action? OnConnected
    {
        get => OnConnectedInternal;
        init => OnConnectedInternal = value;
    }

    /// <summary>
    /// Handle client disconnecting notification
    /// </summary>
    public Action? OnDisconnecting
    {
        get => OnDisconnectingInternal;
        init => OnDisconnectingInternal = value;
    }

    /// <summary>
    /// Handle client disconnected notification
    /// </summary>
    public Disconnect? OnDisconnected
    {
        get => OnDisconnectedInternal;
        init => OnDisconnectedInternal = value;
    }

    /// <summary>
    /// Handle buffer received notification
    /// </summary>
    /// <remarks>
    /// Notification is called when another chunk of buffer was received from the server
    /// </remarks>
    public Received? OnReceived
    {
        get => OnReceivedInternal;
        init => OnReceivedInternal = value;
    }

    /// <summary>
    /// Handle buffer sent notification
    /// </summary>
    public Sent? OnSent
    {
        get => OnSentInternal;
        init => OnSentInternal = value;
    }

    /// <summary>
    /// Handle error notification
    /// </summary>
    public Error? OnError
    {
        get => OnErrorInternal;
        init => OnErrorInternal = value;
    }

    public Error? OnSendError
    {
        get => OnSendErrorInternal;
        init => OnSendErrorInternal = value;
    }

    /// <summary>
    /// Handle empty send buffer notification
    /// </summary>
    /// <remarks>
    /// Notification is called when the send buffer is empty and ready for a new data to send.
    /// This handler could be used to send another buffer to the server.
    /// </remarks>
    public Action? OnEmpty
    {
        get => OnEmptyInternal;
        init => OnEmptyInternal = value;
    }
}