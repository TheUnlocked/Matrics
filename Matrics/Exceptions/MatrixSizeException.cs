using System;
using System.Runtime.Serialization;

namespace Matrics.Exceptions{
    [Serializable]
    public class MatrixSizeException : Exception
    {
        public MatrixSizeException() { }
        public MatrixSizeException(string message) : base(message) { }
        public MatrixSizeException(string message, Exception inner) : base(message, inner) { }
        protected MatrixSizeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}