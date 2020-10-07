using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumFloat : HeliumCoder
    {
        public HeliumFloat(Byte index, Boolean nullable) : base(index, nullable)
        {
        }

        public override void Prepare(Type underlyingType)
        {
            // Error if unsupported data type
            if (underlyingType != typeof(Single) &&
                underlyingType != typeof(Double) &&
                underlyingType != typeof(Single?) &&
                underlyingType != typeof(Double?))
            {

                throw new UnsupportedDataTypeException($"{nameof(HeliumFloat)} does not support {underlyingType}.");
            }
        }

        public override EncodeBuffer Encode(Object value)
        {
            throw new NotImplementedException();
        }

        public override Object Decode(DecodeBuffer input)
        {
            throw new NotImplementedException();
        }
    }
}
