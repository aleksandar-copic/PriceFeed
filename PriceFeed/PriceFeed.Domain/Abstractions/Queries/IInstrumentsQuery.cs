using PriceFeed.Domain.Abstractions.Queries.Base;
using PriceFeed.Domain.Models;

namespace PriceFeed.Domain.Abstractions.Queries;

public interface IInstrumentsQuery : IBaseQuery<Instrument> { }
