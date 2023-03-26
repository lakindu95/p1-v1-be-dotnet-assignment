using System;

namespace API.Exceptions
{
	public class InvalidNumberOfSeatsException : Exception
	{
		public InvalidNumberOfSeatsException()
		{
		}

		public InvalidNumberOfSeatsException(string message) : base(message)
		{
		}

		public InvalidNumberOfSeatsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
