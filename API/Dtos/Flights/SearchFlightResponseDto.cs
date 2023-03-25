using System;

namespace API.Dtos.Flights
{
    public class SearchFlightResponseDto
    {
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }
        public DateTimeOffset Departure { get; set; }
        public DateTimeOffset Arrival { get; set; }
        public decimal PriceFrom { get; set; }
    }
}