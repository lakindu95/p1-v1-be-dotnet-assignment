using System;
using API.ApiResponses;
using API.Dtos.Flights;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace API.Mapping
{
    public class FlightsProfile : Profile
    {
        public FlightsProfile()
        {
            CreateMap<SearchFlightResponseDto, FlightResponse>()
               .ForMember(dest => dest.DepartureAirportCode, opt => opt.MapFrom(src => src.DepartureAirportCode))
               .ForMember(dest => dest.ArrivalAirportCode, opt => opt.MapFrom(src => src.ArrivalAirportCode))
               .ForMember(dest => dest.Departure, opt => opt.MapFrom(src => src.Departure))
               .ForMember(dest => dest.Arrival, opt => opt.MapFrom(src => src.Arrival))
               .ForMember(dest => dest.PriceFrom, opt => opt.MapFrom(src => src.PriceFrom));
        }
    }
}

