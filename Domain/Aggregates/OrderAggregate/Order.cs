using System;
using Domain.Common;
using Domain.SeedWork;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid FlightId { get; set; }
        public Guid FlightRateId { get; set; }
        public int NoOfSeats { get; set; }
		public decimal Price { get; set; }
		public Currency Currency { get; set; }
		public OrderStatus Status { get; set; }
	}
}