using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Events;
using MediatR;

namespace API.EventNotification
{
	public class FlightRateAvailabilityChangedEventNotification : INotificationHandler<FlightRateAvailabilityChangedEvent>
	{
		public Task Handle(FlightRateAvailabilityChangedEvent notification, CancellationToken cancellationToken)
		{
			//Convert mutation to positive number 
			int numberOfSeats = notification.Mutation * -1;

			Console.WriteLine($"Notification: Thank You! Your order has been succesfully confirmed. Order details: Name: {notification.Name}, Flight: {notification.Flight.Id}. No of seats {numberOfSeats}");
			return Task.CompletedTask;
		}
	}
}
