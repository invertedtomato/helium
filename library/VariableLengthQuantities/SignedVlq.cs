using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities
{
    public static class SignedVlq
    {
        // Based on https://developers.google.com/protocol-buffers/docs/encoding
        public const Int64 MinValue = Int64.MinValue + 1;
        public const Int64 MaxValue = Int64.MaxValue;

        public static ArraySegment<Byte> Encode(Int64 value)
        {
            return UnsignedVlq.Encode((UInt64)((value << 1) ^ (value >> 63))); // (n << 1) ^ (n >> 63)
        }

        public static Int64 Decode(Byte[] input)
        {
#if DEBUG
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }
#endif
            return Decode(new DecodeBuffer(input, 0, input.Length));
        }

        public static Int64 Decode(DecodeBuffer input)
        {
            // This was difficult to get right, translated from https://github.com/protocolbuffers/protobuf/blob/master/src/google/protobuf/wire_format_lite.h
            var n = UnsignedVlq.Decode(input);
            return (Int64)((n >> 1) ^ (~(n & 1) + 1));
        }
    }
}