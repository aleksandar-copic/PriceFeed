using AutoMapper;
using PriceFeed.API.DTOs;
using PriceFeed.Domain.Models;

namespace PriceFeed.API.MapperProfiles;

public class InstrumentProfile : Profile
{
    public InstrumentProfile()
    {
        CreateMap<Instrument, InstrumentDTO>();
    }
}
