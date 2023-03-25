using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;

namespace API.Mapping
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderViewModel>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => src.FlightId))
               .ForMember(dest => dest.FlightRateId, opt => opt.MapFrom(src => src.FlightRateId))
			   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
		}
    }
}

