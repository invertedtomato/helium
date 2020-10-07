using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumBoolean : HeliumCoder
    {
        private const Byte Zero = 0x00;
        private const Byte One = 0x01;
        private const Byte Two = 0x02;

        public HeliumBoolean(Byte index, Boolean nullable) : base(index, nullable)
        {
        }

        public override void Prepare(Type underlyingType)
        {
            // Error if unsupported data type
            if (underlyingType != typeof(Boolean) &&
                underlyingType != typeof(Boolean?))
            {

                throw new UnsupportedDataTypeException($"Boolean does not support {underlyingType}.");
            }
        }

        public override EncodeBuffer Encode(Object value)
        {
            switch (value)
            {
                case false: return Precomputed.Zero;
                case true: return Precomputed.One;
                default: return Precomputed.Two;
            }
        }

        public override Object Decode(DecodeBuffer input)
        {
            var v = input.ReadByte();
            switch (v)
            {
                case Zero: return false;
                case One: return true ;
                case Two: return null;
                default: throw new SchemaMismatchException($"Boolean value must be either {Zero}, {One} or {Two}, but {v} given.");
            }
        }
    }
}
