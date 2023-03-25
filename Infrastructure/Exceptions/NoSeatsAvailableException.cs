using System;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    [Serializable]
    internal class NoSeatsAvailableException : Exception
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

        protected NoSeatsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}