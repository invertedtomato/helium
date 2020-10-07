using System;
using System.Collections;
using System.Reflection;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumList : HeliumCoder
    {
        public HeliumList(Byte index, Boolean nullable) : base(index, nullable)
        {
        }

        public override void Prepare(Type underlyingType)
        {
            // This explicitly does not support arrays (otherwise they could get matched with the below check)

            // Error if unsupported data type
            if (underlyingType.IsArray || typeof(IList).GetTypeInfo().IsAssignableFrom(underlyingType)) { 
                throw new UnsupportedDataTypeException($"{nameof(HeliumList)} does not support {underlyingType}.");
            }
        }

        public override EncodeBuffer Encode(Object value)
        {
            throw new NotImplementedException();
            /*
            // Handle nulls
            if (null == value)
            {
                return Null;
            }

            // Serialize elements
            var output = new EncodeBuffer();
            foreach (var element in value)
            {
                output.Append((EncodeBuffer)valueEncoder.DynamicInvokeTransparent(element));
            }

            // Encode length
            output.SetFirst(UnsignedVlq.Encode((UInt64)value.Count + 1));

            return output;*/
        }

        public override Object Decode(DecodeBuffer input)
        {
            throw new NotImplementedException();
            /*  // Read header
                var header = UnsignedVlq.Decode(input);

                // Handle nulls
                if (header == 0)
                {
                    return null;
                }

                // Determine length
                var count = (Int32)header - 1;

                // Instantiate list
                var output = (IList)Activator.CreateInstance(type); //typeof(List<>).MakeGenericType(type.GenericTypeArguments)

                // Deserialize until we reach length limit
                for (var i = 0; i < count; i++)
                {
                    // Deserialize element
                    var element = valueDecoder.DynamicInvokeTransparent(input);

                    // Add to output
                    output.Add(element);
                }

                return output;*/
        }
    }
}
