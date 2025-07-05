using MediatR;
using PriceFeed.Domain.Models;
using System.Net;

namespace PriceFeed.Application.Instruments.Queries.GetInstruments;

public record GetInstrumentsQuery : IRequest<IEnumerable<Instrument>>;
