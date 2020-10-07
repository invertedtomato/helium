using System;
using System.Text;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumString : HeliumCoder
    {

        public HeliumString(Byte index, Boolean nullable) : base(index, nullable)
        {
        }

        public override void Prepare(Type underlyingType)
        {
            // Error if unsupported data type
            if (underlyingType != typeof(String))
            {

                throw new UnsupportedDataTypeException($"IntegerUnsigned does not support {underlyingType}.");
            }
        }

        public override EncodeBuffer Encode(Object value)
        {
            var v = value as String;
            if (v == null && IsNullable)
            {
                return Precomputed.Zero;
            }

            var length = (UInt64)v.Length;
            if (IsNullable)
            {
                length++;
            }

            // Encode value
            var output = new EncodeBuffer(2);
            output.Append(new ArraySegment<Byte>(Encoding.UTF8.GetBytes(v)));

            // Prepend length
            output.SetFirst(UnsignedVlq.Encode(length)); // Offset by one, since 0 is NULL

            return output;
        }

        public override Object Decode(DecodeBuffer input)
        {
            // Decode header
            var header = UnsignedVlq.Decode(input);

            // Handle nulls
            if (IsNullable)
            {
                if (header == 0)
                {
                    return null;
                }
                header--;
            }

            // Decode value
            return Encoding.UTF8.GetString(input.Underlying, input.GetIncrementOffset((Int32)header), (Int32)header);
        }
    }
}
