using PriceFeed.Domain.Abstractions.MarketData;
using System.Collections.Concurrent;

namespace PriceFeed.Infrastructure.MarketData;

public class PriceStore : IPriceStore
{
    private readonly ConcurrentDictionary<string, decimal> _prices = new();

    public void Set(string symbol, decimal price) => _prices[symbol] = price;

    public bool TryGet(string symbol, out decimal price) => _prices.TryGetValue(symbol, out price);
}
