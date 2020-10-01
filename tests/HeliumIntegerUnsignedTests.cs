using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;
using Xunit;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerUnsignedTests
    {
        [Fact]
        public void EncodeSimple()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 1);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)5);

            Assert.Equal("05", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMax()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 1);
            coder.Prepare(typeof(UInt64));
            var encoded = coder.Encode(UnsignedVlq.MaxValue);

            Assert.Equal("FEFEFEFEFEFEFEFEFE00", encoded.ToHexString());
        }

        [Fact]
        public void EncodeCouldBeNullable()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)5);

            Assert.Equal("06", encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullable()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32?));
            var encoded = coder.Encode((UInt32?)5);

            Assert.Equal("06", encoded.ToHexString());
        }

        [Fact]
        public void EncodeNull()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32?));
            var encoded = coder.Encode(null);

            Assert.Equal("00", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMinimum()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 1, 1);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)5);

            Assert.Equal("04", encoded.ToHexString());
        }

        [Fact]
        public void EncodeIncrement()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 10);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)111);

            Assert.Equal("0B", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMinimumIncrement1()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 1, 10);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)111);

            Assert.Equal("0B", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMinimumIncrement2()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 20, 10);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)152);

            Assert.Equal("0D", encoded.ToHexString());
        }



        [Fact]
        public void DecodeSimple()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 1);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x05
            }));

            Assert.Equal((UInt32)5, decoded);
        }


        [Fact]
        public void DecodeMax()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 1);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0xFE,
                0x00
            }));

            Assert.Equal(UnsignedVlq.MaxValue, decoded);
        }

        [Fact]
        public void DecodeCouldBeNullable()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x06
            }));

            Assert.Equal((UInt32)5, decoded);
        }

        [Fact]
        public void DecodeNullable()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32?));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x06
            }));

            Assert.Equal((UInt32?)5, decoded);
        }

        [Fact]
        public void DecodeNull()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, true, 0, 1);
            coder.Prepare(typeof(UInt32?));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x00
            }));

            Assert.Equal((UInt32?)null, decoded);
        }

        [Fact]
        public void DecodeMinimum()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 1, 1);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x04
            }));

            Assert.Equal((UInt32)5, decoded);
        }

        [Fact]
        public void DecodeIncrement()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 0, 10);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0B
            }));

            Assert.Equal((UInt32)110, decoded);
        }

        [Fact]
        public void DecodeMinimumIncrement1()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 1, 10);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0B
            }));

            Assert.Equal((UInt32)111, decoded);
        }

        [Fact]
        public void DecodeMinimumIncrement2()
        {
            var coder = new HeliumIntegerUnsignedAttribute(0, false, 20, 10);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0D
            }));

            Assert.Equal((UInt32)150, decoded);
        }
    }
}
