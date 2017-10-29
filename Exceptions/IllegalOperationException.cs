using System;
using System.Runtime.Serialization;

namespace Matrics
{
    [Serializable]
    public class IllegalOperationException : Exception
    {
        public IllegalOperationException() { }
        public IllegalOperationException(string message) : base(message) { }
        public IllegalOperationException(string message, Exception inner) : base(message, inner) { }
        protected IllegalOperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}