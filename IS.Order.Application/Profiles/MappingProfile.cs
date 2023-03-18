using AutoMapper;
using IS.Order.Application.Features.Orders;

namespace IS.Order.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Order, PlaceOrderInDto>()
            .ReverseMap();
    }
}
