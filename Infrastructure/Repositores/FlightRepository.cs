using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositores
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public FlightRepository(FlightsContext context)
        {
            _context = context;
        }

        public Flight Add(Flight flight)
        {
            return _context.Flights.Add(flight).Entity;
        }

        public void Update(Flight flight)
        {
            _context.Flights.Update(flight);
        }

        public async Task<Flight> GetAsync(Guid flightId)
        {
            return await _context.Flights.FirstOrDefaultAsync(o => o.Id == flightId);
        }

        public async Task<List<Flight>> GetAvailableFlightsByAirportIdAsync(Guid airportId)
        {
            var test = await _context.Flights
                .Include(r => r.Rates)
                .Where(f => f.DestinationAirportId == airportId && f.Rates.Count > 0)
                .ToListAsync();

            return test;
        }
	}

}