using PriceFeed.Domain.Abstractions.Queries;

namespace PriceFeed.Domain.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    #region Queries
    IInstrumentsQuery InstrumentsQuery { get; }
    #endregion

    #region Repositories
    #endregion
}
