using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public abstract class HeliumAttribute : Attribute
    {
        public Byte Index { get; }
        protected Boolean Nullable { get; }

        public HeliumAttribute(Byte index, Boolean nullable)
        {
            Index = index;
            Nullable = nullable;
        }

        public abstract void Prepare(Type underlyingType);

        public abstract EncodeBuffer Encode(Object value);

        public abstract Object Decode(DecodeBuffer input);
    }
}
