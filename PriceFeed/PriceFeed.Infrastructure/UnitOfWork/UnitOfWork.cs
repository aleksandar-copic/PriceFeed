using PriceFeed.Domain.Abstractions.Queries;
using PriceFeed.Domain.Abstractions.UnitOfWork;
using PriceFeed.Domain.Seed;
using PriceFeed.Infrastructure.Queries;

namespace PriceFeed.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    #region Query Members
    private IInstrumentsQuery _instrumentQuery;
    #endregion

    #region Repository Members
    #endregion

    public UnitOfWork() { }

    #region Query Properties
    public IInstrumentsQuery InstrumentsQuery => _instrumentQuery ??= new InstrumentsQuery(InstrumentSeed.Instruments);
    #endregion

    #region Repository Properties
    #endregion
}
