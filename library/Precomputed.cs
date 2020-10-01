using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public static class Precomputed
    {
        public static readonly EncodeBuffer Zero = new EncodeBuffer(UnsignedVlq.Encode(0));
        public static readonly EncodeBuffer One = new EncodeBuffer(UnsignedVlq.Encode(1));
    }
}
