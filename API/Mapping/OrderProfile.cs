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
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));
		}
	}
}

