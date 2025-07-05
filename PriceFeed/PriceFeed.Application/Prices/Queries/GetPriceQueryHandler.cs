using MediatR;
using PriceFeed.Domain.Abstractions.MarketData;
using PriceFeed.Domain.Abstractions.UnitOfWork;
using PriceFeed.Domain.Exceptions;
using PriceFeed.Domain.Models;

namespace PriceFeed.Application.Prices.Queries;

public class GetPriceQueryHandler : IRequestHandler<GetPriceQuery, Price>
{
    private readonly IPriceStore _priceStore;

    public GetPriceQueryHandler(IUnitOfWork unitOfWork, IPriceStore priceStore)
    {
        _priceStore = priceStore;
    }

    public async Task<Price> Handle(GetPriceQuery request, CancellationToken cancellationToken)
    {
        if (_priceStore.TryGet(request.Symbol.ToUpper(), out var price))
        {
            return await Task.FromResult(new Price
            {
                InstrumentId = request.Symbol,
                Value = price.ToString()
            });
        }

        throw new BadRequestException($"Price for {request.Symbol} not found.");
    }
}
