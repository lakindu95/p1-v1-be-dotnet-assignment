using System;

namespace API.Exceptions
{
	public class InvalidFlightException : Exception
	{
		public InvalidFlightException()
		{
		}

		public InvalidFlightException(string message) : base(message)
		{
		}

		public InvalidFlightException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
