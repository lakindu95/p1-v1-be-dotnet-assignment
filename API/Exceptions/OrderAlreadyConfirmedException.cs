using System;

namespace API.Exceptions
{
	public class OrderAlreadyConfirmedException : Exception
	{
		public OrderAlreadyConfirmedException()
		{
		}

		public OrderAlreadyConfirmedException(string message) : base(message)
		{
		}

		public OrderAlreadyConfirmedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
