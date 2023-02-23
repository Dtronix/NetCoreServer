using System;
using System.Net.Sockets;

namespace NetCoreServer.Configs;

internal class ServerEvents<TSession>
{
    internal readonly Action? OnStartingInternal;
    internal readonly Action? OnStartedInternal;
    internal readonly Action? OnStoppingInternal;
    internal readonly Action? OnStoppedInternal;
    internal readonly OnErrorDelegate? OnErrorInternal;
    internal readonly OnErrorDelegate? OnSendErrorInternal;
    internal readonly SessionDelegate? OnSessionConnectingInternal;
    internal readonly SessionDelegate? OnSessionConnectedInternal;
    internal readonly SessionDelegate? OnSessionDisconnectingInternal;
    internal readonly SessionDelegate? OnSessionDisconnectedInternal;
    internal readonly SessionDelegate? OnSessionEmptyInternal;
    internal readonly OnSessionErrorDelegate? OnSessionErrorInternal;
    internal readonly OnSessionErrorDelegate? OnSessionSendErrorInternal;
    internal readonly SessionCreateDelegate? CreateSessionInternal;
    internal readonly Received OnSessionReceivedInternal;

    /// <summary>
    /// Handle buffer received notification
    /// </summary>
    /// <remarks>
    /// Notification is called when another chunk of buffer was received from the server
    /// </remarks>
    public delegate void Received(TSession session, ReadOnlyMemory<byte> readOnlyMemory);

    public delegate void Disconnect(SocketError error);
    
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

    public delegate void SessionDelegate(TSession session);

    public delegate TSession SessionCreateDelegate();

    /// <summary>
    /// Handle error notification
    /// </summary>
    /// <param name="error">Socket error code</param>
    public delegate void OnErrorDelegate(SocketError error);
    public delegate void OnSessionErrorDelegate(TSession session, SocketError error);

    /// <summary>
    /// Handle error notification
    /// </summary>
    /// <param name="error">Socket error code</param>
    public delegate void Error(SocketError error);

    /// <summary>
    /// Handle server starting notification
    /// </summary>
    public Action? OnStarting
    {
        get => OnStartingInternal;
        init => OnStartingInternal = value;
    }

    /// <summary>
    /// Handle server started notification
    /// </summary>
    public Action? OnStarted
    {
        get => OnStartedInternal;
        init => OnStartedInternal = value;
    }

    /// <summary>
    /// Handle server stopping notification
    /// </summary>
    public Action? OnStopping
    {
        get => OnStoppingInternal;
        init => OnStoppingInternal = value;
    }

    /// <summary>
    /// Handle server stopped notification
    /// </summary>
    public Action? OnStopped
    {
        get => OnStoppedInternal;
        init => OnStoppedInternal = value;
    }

    /// <summary>
    /// Handle server stopped notification
    /// </summary>
    public OnErrorDelegate? OnError
    {
        get => OnErrorInternal;
        init => OnErrorInternal = value;
    }

    /// <summary>
    /// Handle session connecting notification
    /// </summary>
    public SessionDelegate? OnSessionConnecting
    {
        get => OnSessionConnectingInternal;
        init => OnSessionConnectingInternal = value;
    }

    /// <summary>
    /// Handle session connected notification
    /// </summary>
    public SessionDelegate? OnSessionConnected
    {
        get => OnSessionConnectedInternal;
        init => OnSessionConnectedInternal = value;
    }

    /// <summary>
    /// Handle session disconnecting notification
    /// </summary>
    public SessionDelegate? OnSessionDisconnecting
    {
        get => OnSessionDisconnectingInternal;
        init => OnSessionDisconnectingInternal = value;
    }

    /// <summary>
    /// Handle session disconnected notification
    /// </summary>
    public SessionDelegate? OnSessionDisconnected
    {
        get => OnSessionDisconnectedInternal;
        init => OnSessionDisconnectedInternal = value;
    }

    public SessionDelegate? OnSessionEmpty
    {
        get => OnSessionEmptyInternal;
        init => OnSessionEmptyInternal = value;
    }

    /// <summary>
    /// Handle server stopped notification
    /// </summary>
    public OnErrorDelegate? OnSendError
    {
        get => OnSendErrorInternal;
        init => OnSendErrorInternal = value;
    }

    public OnSessionErrorDelegate? OnSessionError
    {
        get => OnSessionErrorInternal;
        init => OnSessionErrorInternal = value;
    }

    public OnSessionErrorDelegate? OnSessionSendError
    {
        get => OnSessionSendErrorInternal;
        init => OnSessionSendErrorInternal = value;
    }

    public Received OnSessionReceived
    {
        get => OnSessionReceivedInternal;
        init => OnSessionReceivedInternal = value;
    }

    public SessionCreateDelegate? SessionCreate
    {
        get => CreateSessionInternal;
        init => CreateSessionInternal = value;
    }
}
