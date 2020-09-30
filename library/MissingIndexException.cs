using System;

namespace InvertedTomato.Serialization.Helium
{
    public class MissingIndexException : Exception
    {
        public MissingIndexException() { }
        public MissingIndexException(String message) : base(message) { }
        public MissingIndexException(String message, Exception inner) : base(message, inner) { }
    }
}