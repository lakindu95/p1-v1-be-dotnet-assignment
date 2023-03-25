using API.ApiResponses;
using System.Collections.Generic;
using MediatR;
using API.ApiRequests;
using API.Dtos.Flights;

namespace API.Application.Queries
{
	public class GetFlightsByDestinationQuery : IRequest<List<SearchFlightResponseDto>>
	{
		public SearchFlightsRequest SearchFlightsRequest { get; set; }
	}
}
