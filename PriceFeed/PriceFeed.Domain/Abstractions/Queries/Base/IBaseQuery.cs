using System.Linq.Expressions;

namespace PriceFeed.Domain.Abstractions.Queries.Base;

public interface IBaseQuery<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
}
