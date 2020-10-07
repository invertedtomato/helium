using System;
using InvertedTomato.Serialization.HeliumSerialization;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;
using Xunit;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumStringTests
    {
        [Fact]
        public void StringSerializeNullbleNull()
        {
            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var encoded = coder.Encode((String)null);

            Assert.Equal("00", encoded.ToHexString());
        }

        [Fact]
        public void StringSerializeNullableEmpty()
        {
            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var encoded = coder.Encode(String.Empty);

            Assert.Equal("01", encoded.ToHexString());
        }

        [Fact]
        public void StringSerializeNullable1()
        {
            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var encoded = coder.Encode("a");

            Assert.Equal("0261", encoded.ToHexString());
        }

        [Fact]
        public void StringSerializeNull()
        {
            var coder = new HeliumString(0, false);
            coder.Prepare(typeof(String));
            Assert.Throws<UnexpectedNullValueException>(() =>
            {
                coder.Encode((String)null);
            });
        }

        [Fact]
        public void StringSerializeEmpty()
        {
            var coder = new HeliumString(0, false);
            coder.Prepare(typeof(String));
            var encoded = coder.Encode(String.Empty);

            Assert.Equal("00", encoded.ToHexString());
        }

        [Fact]
        public void StringSerializeOne()
        {
            var coder = new HeliumString(0, false);
            coder.Prepare(typeof(String));
            var encoded = coder.Encode("a");

            Assert.Equal("0161", encoded.ToHexString());
        }


        [Fact]
        public void StringDeserializeNullableNull()
        {
            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x00
            }));

            Assert.Equal((String)null, decoded);
        }

        [Fact]
        public void StringDeserializeNullableEmpty()
        {
            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x01
            }));

            Assert.Equal(String.Empty, decoded);
        }

        [Fact]
        public void StringDeserializeNullableOne()
        {

            var coder = new HeliumString(0, true);
            coder.Prepare(typeof(String));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x02,
                (Byte)'a'
            }));

            Assert.Equal("a", decoded);
        }

        [Fact]
        public void StringDeserializeEmpty()
        {
            var coder = new HeliumString(0, false);
            coder.Prepare(typeof(String));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x00
            }));

            Assert.Equal(String.Empty, decoded);
        }

        [Fact]
        public void StringDeserializeOne()
        {
            var coder = new HeliumString(0, false);
            coder.Prepare(typeof(String));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x01,
                (Byte)'a'
            }));

            Assert.Equal("a", decoded);
        }
    }
}
