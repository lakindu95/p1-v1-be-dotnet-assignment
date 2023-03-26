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
            CreateMap<SearchFlightResponseDto, FlightResponse>();
        }
    }
}

