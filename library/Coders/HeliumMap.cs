using System;
using System.Collections;
using System.Reflection;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumMap : HeliumCoder
    {
        public HeliumMap(Byte index, Boolean nullable) : base(index, nullable)
        {
        }

        public override void Prepare(Type underlyingType)
        {
            // Error if unsupported data type
            if (!typeof(IDictionary).GetTypeInfo().IsAssignableFrom(underlyingType))
            {
                throw new UnsupportedDataTypeException($"{nameof(HeliumMap)} does not support {underlyingType}.");
            }
        }

        public override EncodeBuffer Encode(Object value)
        {
            throw new NotImplementedException();
            /*  // Get serializer for sub items
            var keyEncoder = recurse(type.GenericTypeArguments[0]);
            var valueEncoder = recurse(type.GenericTypeArguments[1]);

            return new Func<IDictionary, EncodeBuffer>(value =>
            {
                // Handle nulls
                if (null == value)
                {
                    return Null;
                }

                // Serialize elements   
                var output = new EncodeBuffer();
                var e = value.GetEnumerator();
                UInt64 count = 0;
                while (e.MoveNext())
                {
                    output.Append((EncodeBuffer)keyEncoder.DynamicInvokeTransparent(e.Key));
                    output.Append((EncodeBuffer)valueEncoder.DynamicInvokeTransparent(e.Value));
                    count++;
                }

                // Encode length
                output.SetFirst(UnsignedVlq.Encode(count + 1));

                return output;
            });
            */
        }

        public override Object Decode(DecodeBuffer input)
        {
            throw new NotImplementedException();
            /*// Get deserializer for sub items
            var keyDecoder = recurse(type.GenericTypeArguments[0]);
            var valueDecoder = recurse(type.GenericTypeArguments[1]);

            return new Func<DecodeBuffer, IDictionary>(input =>
            {
                // Read header
                var header = UnsignedVlq.Decode(input);

                if (header == 0)
                {
                    return null;
                }

                // Get count
                var count = (Int32)header - 1;

                // Instantiate dictionary
                var output = (IDictionary)Activator.CreateInstance(type);

                // Loop through input buffer until depleted
                for (var i = 0; i < count; i++)
                {
                    // Deserialize key
                    var keyValue = keyDecoder.DynamicInvokeTransparent(input);

                    // Deserialize value
                    var valueValue = valueDecoder.DynamicInvokeTransparent(input);

                    // Add to output
                    output[keyValue] = valueValue;
                }

                return output;
            });*/
        }
    }
}
