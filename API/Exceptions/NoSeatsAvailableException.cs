using System;

namespace API.Exceptions
{
	public class NoSeatsAvailableException : Exception
	{
		public NoSeatsAvailableException()
		{
		}

		public NoSeatsAvailableException(string message) : base(message)
		{
		}

		public NoSeatsAvailableException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
