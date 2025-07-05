using PriceFeed.Domain.Abstractions.Queries;
using PriceFeed.Domain.Models;
using PriceFeed.Infrastructure.Queries.Base;

namespace PriceFeed.Infrastructure.Queries;

public class InstrumentsQuery : BaseQuery<Instrument>, IInstrumentsQuery
{
    public InstrumentsQuery(IEnumerable<Instrument> context) : base(context) { }
}
