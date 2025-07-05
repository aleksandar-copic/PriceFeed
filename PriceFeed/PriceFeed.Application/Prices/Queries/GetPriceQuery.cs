using MediatR;
using PriceFeed.Domain.Models;

namespace PriceFeed.Application.Prices.Queries;

public record GetPriceQuery(string Symbol) : IRequest<Price>;
