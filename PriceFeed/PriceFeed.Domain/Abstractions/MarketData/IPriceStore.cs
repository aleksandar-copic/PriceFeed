namespace PriceFeed.Domain.Abstractions.MarketData;

public interface IPriceStore
{
    void Set(string symbol, decimal price);
    bool TryGet(string symbol, out decimal price);
}
