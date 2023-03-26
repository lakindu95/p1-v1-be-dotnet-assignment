using System.Threading.Tasks;
using System.Threading;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using API.Exceptions;
using Domain.Common;
using System.Linq;

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
			var order = await _orderRepository.GetAsync(request.Id);

			//Check if order is valid
			if (order == null)
			{
				throw new OrderNotFoundException();
			}

			//Check if the order is already confirmed
			if (order.Status == OrderStatusEnum.Confirmed)
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
	}
}