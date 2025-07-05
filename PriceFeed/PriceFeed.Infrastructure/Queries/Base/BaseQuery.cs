using PriceFeed.Domain.Abstractions.Queries.Base;
using System.Linq.Expressions;

namespace PriceFeed.Infrastructure.Queries.Base;

public class BaseQuery<T> : IBaseQuery<T> where T : class
{
    private readonly IQueryable<T> _data;

    public BaseQuery(IEnumerable<T> context)
    {
        _data = context.AsQueryable();
    }

    public IQueryable<T> GetAll()
    {
        return _data;
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _data.Where(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await Task.FromResult(_data.Any(expression));
    }
}
