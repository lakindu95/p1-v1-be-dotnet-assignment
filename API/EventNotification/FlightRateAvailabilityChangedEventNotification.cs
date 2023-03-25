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
			Console.WriteLine($"Notification: Thank You! Your order has been succesfully confirmed. Order details: Name: {notification.Name}, Flight: {notification.Flight.Id}. No of seats {notification.Mutation}");
			return Task.CompletedTask;
		}
	}
}
