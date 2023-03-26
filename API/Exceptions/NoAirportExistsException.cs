using System;

namespace API.Exceptions
{
	public class NoAirportExistsException : Exception
	{
		public NoAirportExistsException()
		{
		}

		public NoAirportExistsException(string message) : base(message)
		{
		}

		public NoAirportExistsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
