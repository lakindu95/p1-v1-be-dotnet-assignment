using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Exceptions;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Application.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly ILogger<CreateOrderCommandHandler> _logger;

		public CreateOrderCommandHandler(IOrderRepository orderRepository, IFlightRepository flightRepository, ILogger<CreateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_flightRepository = flightRepository;
			_logger = logger;
		}

		async Task<Order> IRequestHandler<CreateOrderCommand, Order>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var flight = await _flightRepository.GetAsync(request.FlightId);

				if (flight == null)
				{
					throw new InvalidFlightException();
				}
				var selectedFlightsWithRates = await _flightRepository.GetFlightsWithRatesAsync(flight.Id);

				var flightRate = selectedFlightsWithRates.Select(f => f.Rates.FirstOrDefault(fr => fr.Id == request.FlightRateId)).FirstOrDefault();

				if (flightRate == null)
				{
					throw new InvalidFlightRateException();
				}

				Price price = new Price(flightRate.Price.Value * request.NoOfSeats, flightRate.Price.Currency);

				var order = _orderRepository.Add(new Order
				{
					Name = request.Name,
					Email = request.Email,
					FlightId = request.FlightId,
					FlightRateId = request.FlightRateId,
					NoOfSeats = request.NoOfSeats,
					Price = price.Value,
					Currency = price.Currency,
					Status = OrderStatus.Pending
				});

				await _orderRepository.UnitOfWork.SaveEntitiesAsync();
				_logger.LogInformation($"CreateOrderCommandHandler: Successfully added Order {request.FlightId}");

				return order;
			}
			//TODO Exception handling
			catch (InvalidFlightException ex)
			{
				_logger.LogError(ex, $"CreateOrderCommandHandler: Invalid Flight ID {request.FlightId}");
				return null;
			}
			catch (InvalidFlightRateException ex)
			{
				_logger.LogError(ex, $"CreateOrderCommandHandler: Invalid Flight Rate ID {request.FlightRateId}");
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex, $"CreateOrderCommandHandler: General Exception");
				return null;
			}
		}
	}
}
