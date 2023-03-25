using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.SeedWork;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRepository : IRepository<Flight>
	{
        Flight Add(Flight flight);

        void Update(Flight flight);

        Task<Flight> GetAsync(Guid flightId);

		Task<List<Flight>> GetAvailableFlightsByAirportIdAsync(Guid airportId);
	}
}