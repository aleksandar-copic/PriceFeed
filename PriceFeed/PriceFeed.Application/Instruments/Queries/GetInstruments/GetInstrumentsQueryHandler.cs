using MediatR;
using PriceFeed.Domain.Abstractions.UnitOfWork;
using PriceFeed.Domain.Models;
using System.Net.Http;
using System.Text.Json;

namespace PriceFeed.Application.Instruments.Queries.GetInstruments;

public class GetInstrumentsQueryHandler : IRequestHandler<GetInstrumentsQuery, IEnumerable<Instrument>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _httpClient;

    public GetInstrumentsQueryHandler(IUnitOfWork unitOfWork, HttpClient httpClient)
    {
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Instrument>> Handle(
        GetInstrumentsQuery request,
        CancellationToken cancellationToken)
    {
        // Option 1 - Get it from ex. DB (Currently using Seed to simplify)
        var result = _unitOfWork.InstrumentsQuery.GetAll().ToList();

        return await Task.FromResult(result);


        // Option 2 - Get all directly from API

        //var response = await _httpClient.GetAsync("https://api.binance.com/api/v3/exchangeInfo", cancellationToken);
        //response.EnsureSuccessStatusCode();

        //using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        //using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        //var symbols = json.RootElement
        //    .GetProperty("symbols")
        //    .EnumerateArray()
        //    .Select(e => new Instrument
        //    {
        //        Id = e.GetProperty("symbol").GetString() ?? string.Empty
        //    })
        //    .ToList();

        //return symbols;
    }
}
