using System;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace API.Application.Commands
{
	public class CreateOrderCommand : IRequest<Order>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public Guid FlightId { get; set; }
		public Guid FlightRateId { get; set; }
		public int NoOfSeats {get; set;}

		public CreateOrderCommand(string name, string email, Guid flightId, Guid flightRateId, int noOfSeats)
		{
			Name = name;
			Email = email;
			FlightId = flightId;
			FlightRateId = flightRateId;
			NoOfSeats = noOfSeats;
		}
	}
}