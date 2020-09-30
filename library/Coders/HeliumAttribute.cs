using System;
using InvertedTomato.Serialization.Helium.Buffers;

namespace InvertedTomato.Serialization.Helium.Coders
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
