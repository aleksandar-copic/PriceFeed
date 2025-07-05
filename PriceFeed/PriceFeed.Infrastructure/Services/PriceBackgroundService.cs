using Microsoft.Extensions.Hosting;
using PriceFeed.Domain.Abstractions.MarketData;
using PriceFeed.Domain.Seed;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using PriceFeed.API.Services;
using Microsoft.Extensions.Logging;

namespace PriceFeed.Infrastructure.Services;

public class PriceBackgroundService : BackgroundService
{
    private readonly IPriceStore _priceStore;
    private readonly ILogger<PriceBackgroundService> _logger;

    public PriceBackgroundService(IPriceStore priceStore, ILogger<PriceBackgroundService> logger)
    {
        _priceStore = priceStore;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var symbols = InstrumentSeed.Instruments.Select(x => x.Id.ToLower()).ToList();
        var socket = new ClientWebSocket();
        var uriString = "wss://stream.binance.com:9443/ws";
        var uri = new Uri(uriString);

        _logger.LogInformation($"Connecting to Binance WebSocket on {uriString}...");
        await socket.ConnectAsync(uri, stoppingToken);
        _logger.LogInformation("Successfully connected to Binance WebSocket.");

        var subscribeRequest = new
        {
            method = "SUBSCRIBE",
            @params = symbols.Select(s => $"{s}@aggTrade").ToArray(),
            id = 1
        };

        var message = JsonSerializer.Serialize(subscribeRequest);

        _logger.LogInformation("Subscribing to {Count} symbol(s): {Symbols}", symbols.Count, string.Join(", ", symbols));
        await socket.SendAsync(
            Encoding.UTF8.GetBytes(message),
            WebSocketMessageType.Text,
            true,
            stoppingToken
        );

        var buffer = new byte[8192];
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            _logger.LogDebug("Received WebSocket message: {Message}", json);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("s", out var symbolProp) && root.TryGetProperty("p", out var priceProp))
            {
                var symbol = symbolProp.GetString();
                var price = decimal.Parse(priceProp.GetString() ?? "0");

                _priceStore.Set(symbol!, price);
                
                _logger.LogDebug("Updated price for {Symbol}: {Price}", symbol, price);

                await WebSocketHandler.BroadcastAsync(json);
            }
        }
    }
}
