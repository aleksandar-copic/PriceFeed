using Microsoft.AspNetCore.Mvc;
using PriceFeed.API.Controllers.Base;
using PriceFeed.API.DTOs;
using PriceFeed.Application.Prices.Queries;

namespace PriceFeed.API.Controllers;

[ApiController]
public class PricesController : BaseController
{
    [HttpGet("{symbol}")]
    public async Task<ActionResult<PriceDTO>> GetStar(string symbol, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetPriceQuery(symbol), cancellationToken);

        return Ok(Mapper.Map<PriceDTO>(response));
    }
}
