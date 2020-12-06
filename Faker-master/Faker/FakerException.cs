using System;
using System.Runtime.Serialization;

namespace Faker
{
    [Serializable]
    public class FakerException : Exception
    {
        public FakerException()
        {
        }

        public FakerException(string message) : base(message)
        {
        }

        public FakerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FakerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}