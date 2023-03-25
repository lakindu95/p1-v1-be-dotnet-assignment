using System;
using System.Threading.Tasks;
using System.Threading;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using API.Exceptions;
using Domain.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands
{
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Order>
    {
		private readonly IOrderRepository _orderRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly ILogger<ConfirmOrderCommandHandler> _logger;

		public ConfirmOrderCommandHandler(IOrderRepository orderRepository, IFlightRepository flightRepository, ILogger<ConfirmOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_flightRepository = flightRepository;
			_logger = logger;
		}

		async Task<Order> IRequestHandler<ConfirmOrderCommand, Order>.Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var order = await _orderRepository.GetAsync(request.Id);

				if (order == null)
				{
					throw new OrderNotFoundException();
				}

				if (order.Status == OrderStatus.Confirmed)
				{
					throw new OrderAlreadyConfirmedException();
				}

				//Check availability before mutation
				var flightsWithRates = await _flightRepository.GetFlightsWithRatesAsync(order.FlightId);
				var flightRate = flightsWithRates.Select(f => f.Rates.FirstOrDefault(fr => fr.Id == order.FlightRateId)).FirstOrDefault();

				var availableSeatCount = flightRate.Available - order.NoOfSeats;

				if (availableSeatCount <= 0)
				{
					throw new NoSeatsAvailableException();
				}

				var confirmOrder = await _orderRepository.ConfirmOrder(order);

				await _flightRepository.UnitOfWork.SaveEntitiesAsync();
				await _orderRepository.UnitOfWork.SaveEntitiesAsync();

				_logger.LogInformation($"ConfirmOrderCommandHandler: Successfully confirmed Order {request.Id}");

				return confirmOrder;
			}
			//TODO: Exceptions
			catch (NoSeatsAvailableException ex)
			{
				return null;
			}
			catch (OrderAlreadyConfirmedException ex)
			{
				return null;
			}
			catch (OrderNotFoundException ex)
			{
				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}