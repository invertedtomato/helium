using System;
using System.Reflection;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerSigned : HeliumCoder
    {
        protected Int64 Increment { get; } = 1;

        private Type UnderlyingType { get; set; }

        public HeliumIntegerSigned(Byte index, Boolean nullable) : base(index, nullable)
        {
        }
        public HeliumIntegerSigned(Byte index, Boolean nullable, Int64 increment) : base(index, nullable)
        {
            Increment = increment;
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
            if (underlyingType != typeof(Int16) &&
                underlyingType != typeof(Int32) &&
                underlyingType != typeof(Int64) &&
                underlyingType != typeof(Int16?) &&
                underlyingType != typeof(Int32?) &&
                underlyingType != typeof(Int64?))
            {

                throw new UnsupportedDataTypeException($"IntegerSigned does not support {underlyingType}.");
            }

            UnderlyingType = underlyingType;
        }

        public override EncodeBuffer Encode(Object value)
        {
            if (value == null && IsNullable)
            {
                return Precomputed.Zero;
            }

            var v = Convert.ToInt64(value);
            v /= Increment;
            if (IsNullable)
            {
                v++;
            }

            return new EncodeBuffer(SignedVlq.Encode(v));
        }

        public override Object Decode(DecodeBuffer input)
        {
            var v = SignedVlq.Decode(input);
            if (IsNullable)
            {
                if (v == 0)
                {
                    return null;
                }
                v--;
            }
            v *= Increment;

            return Convert.ChangeType(v, UnderlyingType);
        }
    }
}
