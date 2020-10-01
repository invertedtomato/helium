using System;
using System.Reflection;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerUnsignedAttribute : HeliumAttribute
    {
        protected UInt64 Minimum { get; } = UInt64.MinValue;
        protected UInt64 Increment { get; } = 1;

        private Type UnderlyingType { get; set; }

        public HeliumIntegerUnsignedAttribute(Byte index, Boolean nullable) : base(index, nullable)
        {
        }
        public HeliumIntegerUnsignedAttribute(Byte index, Boolean nullable, UInt64 minimum) : base(index, nullable)
        {
            Minimum = minimum;
        }
        public HeliumIntegerUnsignedAttribute(Byte index, Boolean nullable, UInt64 minimum, UInt64 increment) : base(index, nullable)
        {
            Minimum = minimum;
            Increment = increment;
        }

        public override void Prepare(Type underlyingType) // TODO: correctly detect and support nullables
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
                underlyingType != typeof(UInt64)) // underlyingType != typeof(Byte) &&
            {
                throw new UnsupportedDataTypeException();
            }

            UnderlyingType = underlyingType;
        }

        public override EncodeBuffer Encode(Object value)
        {
            if (value == null && Nullable)
            {
                return EncodeBuffer.Zero;
            }

            var v = (UInt64)value;
            v -= Minimum;
            v /= Increment;
            if (Nullable)
            {
                v++;
            }

            return new EncodeBuffer(UnsignedVlq.Encode(v));
        }

        public override Object Decode(DecodeBuffer input)
        {
            var v = (UInt64)UnsignedVlq.Decode(input);
            if (Nullable)
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
