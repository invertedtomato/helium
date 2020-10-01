using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class HeliumAttribute : Attribute
    {
        public Byte Index { get; }
        protected Boolean IsNullable { get; }

        public HeliumAttribute(Byte index, Boolean nullable)
        {
            Index = index;
            IsNullable = nullable;
        }

        public abstract void Prepare(Type underlyingType);

        public abstract EncodeBuffer Encode(Object value);

        public abstract Object Decode(DecodeBuffer input);
    }
}
