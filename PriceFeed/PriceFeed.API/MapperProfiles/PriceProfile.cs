using AutoMapper;
using PriceFeed.API.DTOs;
using PriceFeed.Domain.Models;

namespace PriceFeed.API.MapperProfiles;

public class PriceProfile : Profile
{
    public PriceProfile()
    {
        CreateMap<Price, PriceDTO>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Instrument, opt => opt.MapFrom(src => src.InstrumentId));
    }
}
