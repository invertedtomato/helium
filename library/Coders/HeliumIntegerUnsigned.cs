using System;
using System.Reflection;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerUnsigned : HeliumCoder
    {
        protected UInt64 Minimum { get; } = UInt64.MinValue;
        protected UInt64 Increment { get; } = 1;

        private Type UnderlyingType { get; set; }

        public HeliumIntegerUnsigned(Byte index, Boolean nullable) : base(index, nullable)
        {
        }
        public HeliumIntegerUnsigned(Byte index, Boolean nullable, UInt64 increment) : base(index, nullable)
        {
            Increment = increment;
        }
        public HeliumIntegerUnsigned(Byte index, Boolean nullable, UInt64 increment, UInt64 minimum) : base(index, nullable)
        {
            Increment = increment;
            Minimum = minimum;
        }

        public override void Prepare(Type underlyingType)
        {
            // If enum, unwrap
            var typeInfo = underlyingType.GetTypeInfo();
            if (typeInfo.IsEnum)
            {
                underlyingType = typeInfo.GetEnumUnderlyingType();
            }

            // Error if unsupported data type
            if (underlyingType != typeof(UInt16) &&
                underlyingType != typeof(UInt32) &&
                underlyingType != typeof(UInt64) &&
                underlyingType != typeof(UInt16?) &&
                underlyingType != typeof(UInt32?) &&
                underlyingType != typeof(UInt64?)) {
           
                throw new UnsupportedDataTypeException($"IntegerUnsigned does not support {underlyingType}.");
            }

            UnderlyingType = underlyingType;
        }

        public override EncodeBuffer Encode(Object value)
        {
            if (value == null && IsNullable)
            {
                return Precomputed.Zero;
            }

            var v = Convert.ToUInt64(value);
            v -= Minimum;
            v /= Increment;
            if (IsNullable)
            {
                v++;
            }

            return new EncodeBuffer(UnsignedVlq.Encode(v));
        }

        public override Object Decode(DecodeBuffer input)
        {
            var v = UnsignedVlq.Decode(input);
            if (IsNullable)
            {
                if (v == 0)
                {
                    return null;
                }
                v--;
            }
            v *= Increment;
            v += Minimum;

            return Convert.ChangeType(v, UnderlyingType);
        }
    }
}
