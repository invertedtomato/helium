using System;
using InvertedTomato.Serialization.HeliumSerialization;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;
using Xunit;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumBooleanTests
    {
        [Fact]
        public void BooleanSerializeFalse()
        {
            var coder = new HeliumBoolean(0, false);
            coder.Prepare(typeof(Boolean));
            var encoded = coder.Encode(false);

            Assert.Equal("00", encoded.ToHexString());
        }

        [Fact]
        public void BooleanSerializeTrue()
        {
            var coder = new HeliumBoolean(0, false);
            coder.Prepare(typeof(Boolean));
            var encoded = coder.Encode(true);

            Assert.Equal("01", encoded.ToHexString());
        }

        [Fact]
        public void BooleanSerializeNull()
        {
            var coder = new HeliumBoolean(0, false);
            coder.Prepare(typeof(Boolean));
            Assert.Throws<UnexpectedNullValueException>(() =>
            {
                coder.Encode((String)null);
            });
        }

        [Fact]
        public void BooleanSerializeNullableFalse()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean));
            var encoded = coder.Encode(false);

            Assert.Equal("00", encoded.ToHexString());
        }

        [Fact]
        public void BooleanSerializeNullableTrue()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean));
            var encoded = coder.Encode(true);

            Assert.Equal("01", encoded.ToHexString());
        }

        [Fact]
        public void BooleanSerializeNullableNull()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean?));
            var encoded = coder.Encode(null);

            Assert.Equal("02", encoded.ToHexString());
        }




        [Fact]
        public void BooleanDeserializeFalse()
        {
            var coder = new HeliumBoolean(0, false);
            coder.Prepare(typeof(Boolean));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x00
            }));

            Assert.Equal(false, decoded);
        }

        [Fact]
        public void BooleanDeserializeTrue()
        {
            var coder = new HeliumBoolean(0, false);
            coder.Prepare(typeof(Boolean));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x01
            }));

            Assert.Equal(true, decoded);
        }

        [Fact]
        public void BooleanDeserializeNullableFalse()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x00
            }));

            Assert.Equal(false, decoded);
        }

        [Fact]
        public void BooleanDeserializeNullableTrue()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x01
            }));

            Assert.Equal(true, decoded);
        }

        [Fact]
        public void BooleanDeserializeNullableNull()
        {
            var coder = new HeliumBoolean(0, true);
            coder.Prepare(typeof(Boolean));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x02
            }));

            Assert.Null(decoded);
        }
    }
}
