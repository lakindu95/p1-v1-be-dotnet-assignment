using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.AirportAggregate;
using API.Dtos.Flights;
using System.Linq;
using API.Exceptions;
using Microsoft.Extensions.Logging;
namespace API.Application.Queries
{
	public class GetFlightsByDestinationQueryHandler : IRequestHandler<GetFlightsByDestinationQuery, List<SearchFlightResponseDto>>
	{
		private readonly IAirportRepository _airportRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly ILogger<GetFlightsByDestinationQueryHandler> _logger;


		public GetFlightsByDestinationQueryHandler(IAirportRepository airportRepository, IFlightRepository flightRepository, ILogger<GetFlightsByDestinationQueryHandler> logger)
		{
			_airportRepository = airportRepository;
			_flightRepository = flightRepository;
			_logger = logger;

		}
		public async Task<List<SearchFlightResponseDto>> Handle(GetFlightsByDestinationQuery request, CancellationToken cancellationToken)
		{
			var destinationRequest = request.SearchFlightsRequest.Destination;

			//Check if the destination is empty
			if (string.IsNullOrEmpty(destinationRequest))
			{
				throw new EmptySearchDestinationException();
			}

			var destinationAirport = await _airportRepository.GetAirportByDestinationAsync(destinationRequest.Trim());
			//Check if destination has available airports
			if (destinationAirport == null)
			{
				throw new NoAirportExistsException();
			}

			var availableFlights = await _flightRepository.GetAvailableFlightsByAirportIdAsync(destinationAirport.Id);
			List<SearchFlightResponseDto> result = new List<SearchFlightResponseDto>();

			foreach (var availableFlight in availableFlights)
			{
				var response = new SearchFlightResponseDto()
				{
					DepartureAirportCode = availableFlight.OriginAirportId.ToString(),
					ArrivalAirportCode = availableFlight.DestinationAirportId.ToString(),
					Departure = availableFlight.Departure,
					Arrival = availableFlight.Arrival,
					PriceFrom = availableFlight.Rates.Min(r => r.Price.Value)
				};
				result.Add(response);
			}
			return result;
		}
	}
}
