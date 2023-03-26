using System.Net;
using System;
using System.Threading.Tasks;
using API.Application.Commands;
using API.Application.ViewModels;
using API.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.ApiResponses;

namespace API.Controllers
{
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public OrderController(
				ILogger<OrderController> logger,
				IMediator mediator,
				IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("Order")]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
		{
			try
			{
				var order = await _mediator.Send(command);
				return new JsonResult(_mapper.Map<OrderViewModel>(order)){ StatusCode = (int)HttpStatusCode.Created };
			}

			catch (InvalidNumberOfSeatsException ex)
			{
				_logger.LogError(ex, $"CreateOrder: Number of seats must be more than 0.");
				return new JsonErrorResult(new { message = $"Invalid Order. Number of seats must be more than 0." }, HttpStatusCode.BadRequest);

			}
			catch (NoSeatsAvailableException ex)
			{
				_logger.LogError(ex, $"CreateOrder: No seats available. Flight Rate ID: {command.FlightRateId}");
				return new JsonErrorResult(new { message = $"Sorry, the requested seats count is not available. Flight Rate ID: {command.FlightRateId}" }, HttpStatusCode.BadRequest);
			}
			catch (InvalidFlightException ex)
			{
				_logger.LogError(ex, $"CreateOrder: Invalid Flight ID {command.FlightId}");
				return new JsonErrorResult(new { message = $"Invalid Flight ID {command.FlightId}" }, HttpStatusCode.NotFound);

			}
			catch (InvalidFlightRateException ex)
			{
				_logger.LogError(ex, $"CreateOrder: Invalid Flight Rate ID {command.FlightRateId}");
				return new JsonErrorResult(new { message = $"Invalid Flight Rate ID {command.FlightRateId}" }, HttpStatusCode.NotFound);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, $"CreateOrder: One of more values are not valid.");
				return new JsonErrorResult(new { message = $"One of more values are not valid." }, HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex, $"CreateOrder: Internal error occurred.");
				return new JsonErrorResult(new { message = "Internal error occurred." });
			}
		}

		[HttpPut]
		[Route("Confirm")]
		public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrderCommand command)
		{
			try
			{
				var confirmedOrder = await _mediator.Send(command);
				return Ok(_mapper.Map<OrderViewModel>(confirmedOrder));
			}

			catch (NoSeatsAvailableException ex)
			{
				_logger.LogError(ex, $"ConfirmOrder: No seats available. Order ID: {command.Id}");
				return new JsonErrorResult(new { message = $"Sorry, the requested seats count is available. Order ID: {command.Id}" }, HttpStatusCode.BadRequest);
			}
			catch (OrderAlreadyConfirmedException ex)
			{
				_logger.LogError(ex, $"ConfirmOrder: A confirmed order already exists. Order ID: {command.Id}");
				return new JsonErrorResult(new { message = $"A confirmed order already exists. Order ID: {command.Id}" }, HttpStatusCode.Conflict);
			}
			catch (OrderNotFoundException ex)
			{
				_logger.LogError(ex, $"ConfirmOrder: Order not found. Order ID: {command.Id}");
				return new JsonErrorResult(new { message = $"Order not found. Order ID: {command.Id}" }, HttpStatusCode.NotFound);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, $"ConfirmOrder: One of more values are not valid.");
				return new JsonErrorResult(new { message = $"One of more values are not valid." }, HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"ConfirmOrder: Internal error occured.");
				return new JsonErrorResult(new { message = "Internal error occurred." });
			}
		}
	}

}