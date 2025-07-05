using PriceFeed.Application;
using MediatR;
using PriceFeed.Domain.Abstractions.UnitOfWork;
using PriceFeed.Infrastructure.UnitOfWork;
using PriceFeed.Domain.Abstractions.MarketData;
using PriceFeed.Infrastructure.MarketData;

namespace PriceFeed.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(MediatREntryPoint).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions));
    }

    public static void RegisterPriceStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPriceStore, PriceStore>();
    }
}
