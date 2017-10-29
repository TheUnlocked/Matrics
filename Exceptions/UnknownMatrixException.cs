using System;
using System.Runtime.Serialization;

namespace Matrics.Exceptions
{
    [Serializable]
    internal class UnknownMatrixException : Exception
    {
        public UnknownMatrixException()
        {
        }

        public UnknownMatrixException(string message) : base(message)
        {
        }

        public UnknownMatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownMatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}