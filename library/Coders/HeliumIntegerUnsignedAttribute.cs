using System;
using System.Reflection;
using InvertedTomato.Serialization.Helium.Buffers;
using InvertedTomato.Serialization.Helium.VariableLengthQuantities;

namespace InvertedTomato.Serialization.Helium.Coders
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

        public override void Prepare(Type underlyingType)
        {
            // If enum, unwrap
            var typeInfo = underlyingType.GetTypeInfo();
            if (typeInfo.IsEnum)
            {
                underlyingType = typeInfo.GetEnumUnderlyingType();
            }

            // Error if unsupported data type
            if (underlyingType != typeof(Byte) &&
                underlyingType != typeof(UInt16) &&
                underlyingType != typeof(UInt32) &&
                underlyingType != typeof(UInt64))
            {
                throw new UnsupportedDataTypeException();
            }

            UnderlyingType = underlyingType;
        }

        public override EncodeBuffer Encode(Object value)
        {
            throw new NotImplementedException("Missing nullable, min and increment");
            if (UnderlyingType == typeof(UInt64))
            {
                return new EncodeBuffer(UnsignedVlq.Encode((UInt64)value));
            }
            else if (UnderlyingType == typeof(UInt32))
            {
                return new EncodeBuffer(UnsignedVlq.Encode((UInt32)value));
            }
            else if (UnderlyingType == typeof(UInt16))
            {
                return new EncodeBuffer(UnsignedVlq.Encode((UInt16)value));
            }
            else if (UnderlyingType == typeof(Byte))
            {
                return new EncodeBuffer(new ArraySegment<Byte>(new Byte[] { (Byte) value }));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public override Object Decode(DecodeBuffer input)
        {
            if (UnderlyingType == typeof(UInt64))
            {
                return UnsignedVlq.Decode(input);
            }
            else if (UnderlyingType == typeof(UInt32))
            {
                return (UInt32)UnsignedVlq.Decode(input);
            }
            else if (UnderlyingType == typeof(UInt16))
            {
                return (UInt16)UnsignedVlq.Decode(input);
            }
            else if (UnderlyingType == typeof(Byte))
            {
                return input.ReadByte();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
