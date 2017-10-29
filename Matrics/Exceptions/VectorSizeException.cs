using System;
using System.Runtime.Serialization;

namespace Matrics.Exceptions{
    [Serializable]
    public class VectorSizeException : Exception
    {
        public VectorSizeException() { }
        public VectorSizeException(string message) : base(message) { }
        public VectorSizeException(string message, Exception inner) : base(message, inner) { }
        protected VectorSizeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}