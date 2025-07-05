using System.Net.WebSockets;
using System.Text;

namespace PriceFeed.API.Services;

public static class WebSocketHandler
{
    // Thread-safe list to hold active WebSocket clients
    private static readonly List<WebSocket> _clients = new();
    private static readonly object _lock = new();

    /// <summary>
    /// Registers and manages the lifecycle of a WebSocket client.
    /// </summary>
    public static async Task HandleClientAsync(WebSocket socket)
    {
        lock (_lock)
        {
            _clients.Add(socket);
        }

        var buffer = new byte[1024 * 4];

        // Client stays alive until explicitly closed by client or error
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                lock (_lock)
                {
                    _clients.Remove(socket);
                }

                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
            }
        }
    }

    /// <summary>
    /// Broadcasts a message to all connected WebSocket clients.
    /// Efficiently sends to all the clients using async tasks.
    /// </summary>
    public static async Task BroadcastAsync(string message)
    {
        // Convert once to bytes to avoid repeated encoding for each client
        var messageBuffer = Encoding.UTF8.GetBytes(message);

        List<Task> sendTasks = new();

        lock (_lock)
        {
            foreach (var client in _clients.Where(c => c.State == WebSocketState.Open))
            {
                sendTasks.Add(client.SendAsync(
                    new ArraySegment<byte>(messageBuffer),
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    CancellationToken.None));
            }
        }

        // Efficient broadcast with parallel async sending
        await Task.WhenAll(sendTasks);
    }
}
