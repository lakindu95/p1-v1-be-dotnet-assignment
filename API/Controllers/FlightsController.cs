using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ApiRequests;
using API.ApiResponses;
using API.Application.Queries;
using API.Dtos.Flights;
using API.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
	private readonly ILogger<FlightsController> _logger;
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public FlightsController(
			ILogger<FlightsController> logger,
			IMediator mediator,
			IMapper mapper)
	{
		_logger = logger;
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet]
    [Route("Search")]
    public async Task<IEnumerable<FlightResponse>> GetAvailableFlights([FromQuery] string destination)
    {
		try
		{
			SearchFlightsRequest query = new SearchFlightsRequest()
			{
				Destination = destination
			};

			List<SearchFlightResponseDto> availableFlights = await _mediator.Send(new GetFlightsByDestinationQuery()
			{
				SearchFlightsRequest = query
			});
			return _mapper.Map<IEnumerable<FlightResponse>>(availableFlights);

		}
		catch (EmptySearchDestinationException ex)
		{
			_logger.LogError(ex, $"GetAvailableFlights: Empty destination.");
			return Enumerable.Empty<FlightResponse>();
		}
		catch (NoAirportExistsException ex)
		{
			_logger.LogError(ex, $"GetAvailableFlights: No airports available. Destination: {destination}");
			return Enumerable.Empty<FlightResponse>();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"GetAvailableFlights: Internal error occured.");
			throw new Exception();
		}
	}
}
