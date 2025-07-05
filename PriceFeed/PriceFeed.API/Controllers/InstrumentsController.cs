using Microsoft.AspNetCore.Mvc;
using PriceFeed.API.Controllers.Base;
using PriceFeed.API.DTOs;
using PriceFeed.Application.Instruments.Queries.GetInstruments;
using PriceFeed.Domain.Models;

namespace PriceFeed.API.Controllers
{
    [ApiController]
    public class InstrumentsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstrumentDTO>>> GetInstruments(CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new GetInstrumentsQuery(), cancellationToken);

            return Ok(Mapper.Map<IEnumerable<Instrument>>(response));
        }
    }
}
