using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.SeedWork;

namespace Domain.Aggregates.AirportAggregate
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Airport Add(Airport airport);

        void Update(Airport airport);

        Task<Airport> GetAsync(Guid airportId);

		Task<Airport> GetAirportByDestinationAsync(string destination);
	}
}