using System;

namespace API.Exceptions
{
	public class InvalidFlightRateException : Exception
	{
		public InvalidFlightRateException()
		{
		}

		public InvalidFlightRateException(string message) : base(message)
		{
		}

		public InvalidFlightRateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
