using System;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class UnexpectedNullValueException : Exception
    {
        public UnexpectedNullValueException() { }
        public UnexpectedNullValueException(String message) : base(message) { }
        public UnexpectedNullValueException(String message, Exception inner) : base(message, inner) { }
    }
}