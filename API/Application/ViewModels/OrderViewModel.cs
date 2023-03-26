using System;
using Domain.Common;

namespace API.Application.ViewModels
{
    public class OrderViewModel
    {
		public Guid OrderId { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
        public Guid FlightId { get; set; }
        public Guid FlightRateId { get; set; }
        public int NoOfSeats { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }   
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}