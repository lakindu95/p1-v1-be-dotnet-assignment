using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Events;
using Domain.SeedWork;

namespace Domain.Aggregates.FlightAggregate
{
    public class Flight : Entity, IAggregateRoot
    {
        public Guid OriginAirportId { get; private set; }
        public Guid DestinationAirportId { get; private set; }
        
        public DateTimeOffset Departure { get; private set; }
        public DateTimeOffset Arrival { get; private set; }

        private List<FlightRate> _rates;
        public IReadOnlyCollection<FlightRate> Rates => _rates;
        
        protected Flight()
        {
            _rates = new List<FlightRate>();
        }

        public Flight(DateTimeOffset departure, DateTimeOffset arrival, Guid originAirportId, Guid
            destinationAirportId) : this()
        {
            OriginAirportId = originAirportId;
            DestinationAirportId = destinationAirportId;
            Departure = departure;
            Arrival = arrival;
        }

        public void AddRate(string name, Price price, int numAvailable)
        {
            var rate = new FlightRate(name, price, numAvailable);
            _rates.Add(rate);
        }

        public void UpdateRatePrice(Guid rateId, Price price)
        {
            var rate = GetRate(rateId);
            
            rate.ChangePrice(price);
            
            AddDomainEvent(new FlightRatePriceChangedEvent(this, rate));
        }

        public void MutateRateAvailability(Guid rateId, int mutation, string name)
        {
            var rate = GetRate(rateId);
            
            rate.MutateAvailability(mutation);
            
            AddDomainEvent(new FlightRateAvailabilityChangedEvent(this, rate, mutation, name));
        }

        private FlightRate GetRate(Guid rateId)
        {
            var rate = _rates.SingleOrDefault(o => o.Id == rateId);

            if (rate == null)
            {
                throw new ArgumentException("This flight does not contain a rate with the provided rateId");
            }

            return rate;
        }
	}
}