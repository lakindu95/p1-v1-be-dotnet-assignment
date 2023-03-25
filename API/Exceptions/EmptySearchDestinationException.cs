using System;

namespace API.Exceptions
{
	public class EmptySearchDestinationException : Exception
	{
		public EmptySearchDestinationException()
		{
		}

		public EmptySearchDestinationException(string message) : base(message)
		{
		}

		public EmptySearchDestinationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
