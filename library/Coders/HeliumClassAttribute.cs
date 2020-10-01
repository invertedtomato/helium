using System;
using System.Reflection;
using System.Collections.Generic;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumClassAttribute : HeliumAttribute
    {
        private Record[] Records { get; set; }
        private Type UnderlyingType { get; set; }

        private class Record
        {
            public FieldInfo Field { get; set; }
            public HeliumAttribute Coder { get; set; }
        }

        public HeliumClassAttribute(Byte index, Boolean nullable) : base(index, nullable) { }

        public override void Prepare(Type underlyingType)
        {
            // Abort if already prepared
            if (null != Records)
            {
                return;
            }

            UnderlyingType = underlyingType;

            var records = new Record[Byte.MaxValue];

            // Find all properties decorated with LightWeightProperty attribute
            var properties=underlyingType.GetRuntimeFields();
            
            var fieldCount = -1;
            foreach (var property in properties) 
            {
                // Get property attribute which tells us the properties' index
                var coder = property.GetCustomAttribute<HeliumAttribute>(true);
                if (null == coder)
                {
                    // No attribute found, skip
                    continue;
                }

                // Check for duplicate index
                if (null != records[coder.Index])
                {
                    throw new DuplicateIndexException($"The index {coder.Index} is already used and cannot be reused.");
                }

                // Note the max index used
                if (coder.Index > fieldCount)
                {
                    fieldCount = coder.Index;
                }

                // Store property in lookup
                records[coder.Index] = new Record()
                {
                    Field = property,
                    Coder = coder
                };

                // Recurse preparation
                coder.Prepare(property.FieldType);
            }

            // Check that no indexes have been missed
            for (var i = 0; i < fieldCount; i++)
            {
                if (null == records[i])
                {
                    throw new MissingIndexException($"Indexes must not be skipped, however missing index {i}."); // TODO: Make so indexes can be skipped for easier versioning
                }
            }

            // Trim array to size
            Array.Resize(ref records, fieldCount + 1);

            Records = records;
        }

        public override EncodeBuffer Encode(Object value)
        {
#if DEBUG
            if (null == Records)
            {
                throw new InvalidOperationException("Coder has not yet been prepared.");
            }
#endif

            // Handle nulls
            if (null == value)
            {
                if (!Nullable)
                {
                    throw new UnexpectedNullValueException();
                }
                return EncodeBuffer.Zero;
            }

            var output = new EncodeBuffer();

            for (Byte i = 0; i < Records.Length; i++)
            {
                var record = Records[i];

                // Extract value
                var v = record.Field.GetValue(value);

                // Add to output
                output.Append(record.Coder.Encode(v));
            }

            // Increment length if nullable, to allow space for null representation
            var length = (UInt64)output.TotalLength;
            if (Nullable)
            {
                length++;
            }

            // Encode length
            output.SetFirst(UnsignedVlq.Encode(length));

            return output;
        }

        public override Object Decode(DecodeBuffer input)
        {
#if DEBUG
            if (null == Records)
            {
                throw new InvalidOperationException("Coder has not yet been prepared.");
            }
#endif

            // Read the length header
            var header = (Int32)UnsignedVlq.Decode(input);

            // Handle nulls
            if (Nullable)
            {
                if (header == 0)
                {
                    return null;
                }
                header--;
            }

            // Instantiate output
            var output = Activator.CreateInstance(UnderlyingType);

            // Extract an inner buffer so that if fields are added to the class in the future we ignore them, being backwards compatible
            var innerInput = input.Extract(header);

            // Isolate bytes for body
            foreach (var record in Records)
            {
                // Deserialize value
                var v = record.Coder.Decode(innerInput);

                // Set it on property
                record.Field.SetValue(output, v);
            }

            return output;
        }
    }
}