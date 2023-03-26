using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositores
{
	public class OrderRepository : IOrderRepository
	{
		private readonly FlightsContext _context;

		public IUnitOfWork UnitOfWork
		{
			get { return _context; }
		}

		public OrderRepository(FlightsContext context)
		{
			_context = context;
		}

		public Order Add(Order order)
		{
			return _context.Orders.Add(order).Entity;
		}

		public void Update(Order order)
		{
			_context.Orders.Update(order);
		}

		public async Task<Order> GetAsync(Guid orderId)
		{
			return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
		}

		public async Task<Order> ConfirmOrder(Order order)
		{
			//Reduce seats availability
			await MutateSeatsAvailablity(order.Name, order.FlightRateId, order.NoOfSeats);

			//Order confirmation
			order.Status = OrderStatusEnum.Confirmed;
			order.UpdatedDate = DateTimeOffset.UtcNow;

			Update(order);

			await UnitOfWork.SaveEntitiesAsync();
			return order;
		}

		private async Task MutateSeatsAvailablity(string name, Guid flightRateId, int seats)
		{
			var flightRate = await _context.FlightRates.FirstOrDefaultAsync(o => o.Id == flightRateId);

			var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Rates.Any(r => r.Id == flightRate.Id));

			flight.MutateRateAvailability(flightRate.Id, -seats, name);
			_context.Flights.Update(flight);
		}
	}
}