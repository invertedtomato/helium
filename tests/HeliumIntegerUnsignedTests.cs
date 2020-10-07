using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;
using Xunit;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerUnsignedTests
    {
        [Fact]
        public void EncodeMin()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 1, 0);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode(UnsignedVlq.MinValue);

            Assert.Equal(UnsignedVlq.Encode(UnsignedVlq.MinValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeMax()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 1, 0);
            coder.Prepare(typeof(UInt64));
            var encoded = coder.Encode(UnsignedVlq.MaxValue);

            Assert.Equal(UnsignedVlq.Encode(UnsignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableMin()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt32?));
            var encoded = coder.Encode((UInt32?)UnsignedVlq.MinValue);

            Assert.Equal(UnsignedVlq.Encode(UnsignedVlq.MinValue+1).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableMax()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt64?));
            var encoded = coder.Encode(UnsignedVlq.MaxValue-1);

            Assert.Equal(UnsignedVlq.Encode(UnsignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableNull()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt32?));
            var encoded = coder.Encode((UInt32?)null);

            Assert.Equal(UnsignedVlq.Encode(UnsignedVlq.MinValue).ToHexString(), encoded.ToHexString());
        }


        [Fact]
        public void EncodeIncrement()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 0);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)111);

            Assert.Equal("0B", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMinimumIncrement1()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 1);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)111);

            Assert.Equal("0B", encoded.ToHexString());
        }

        [Fact]
        public void EncodeMinimumIncrement2()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 20);
            coder.Prepare(typeof(UInt32));
            var encoded = coder.Encode((UInt32)152);

            Assert.Equal("0D", encoded.ToHexString());
        }

        [Fact]
        public void PrepareUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerUnsigned(0, false, 10, 20);
                coder.Prepare(typeof(String));
            });
        }

        [Fact]
        public void EncodeUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerUnsigned(0, false, 10, 20);
                coder.Prepare(typeof(UInt32));
                var encoded = coder.Encode("asdf");
            });
        }


        [Fact]
        public void DecodeUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerUnsigned(0, false, 10, 20);
                coder.Prepare(typeof(UInt32));
                var decoded = coder.Decode(new DecodeBuffer(  new Byte[] { 0,0,0}));
            });
        }


        [Fact]
        public void DecodeMin()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 1, 0);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(UnsignedVlq.Encode(0).ToArray()));

            Assert.Equal((UInt32)0, decoded);
        }


        [Fact]
        public void DecodeMax()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 1, 0);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(UnsignedVlq.Encode(UnsignedVlq.MaxValue).ToArray()));

            Assert.Equal(UnsignedVlq.MaxValue, decoded);
        }

        [Fact]
        public void DecodeNullableMin()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt32?));
            var decoded = coder.Decode(new DecodeBuffer(UnsignedVlq.Encode(UnsignedVlq.MinValue).ToArray()));

            Assert.Equal(UnsignedVlq.MinValue+1, decoded);
        }

        [Fact]
        public void DecodeNullableMax()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt32?));
            var decoded = coder.Decode(new DecodeBuffer(UnsignedVlq.Encode(UnsignedVlq.MaxValue).ToArray()));

            Assert.Equal(UnsignedVlq.MaxValue -1, decoded);
        }

        [Fact]
        public void DecodeNullableNull()
        {
            var coder = new HeliumIntegerUnsigned(0, true, 1, 0);
            coder.Prepare(typeof(UInt32?));
            var decoded = coder.Decode(new DecodeBuffer(UnsignedVlq.Encode(0).ToArray()));

            Assert.Equal((UInt32?)null, decoded);
        }

        [Fact]
        public void DecodeMinimum()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 1, 1);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x04
            }));

            Assert.Equal((UInt32)5, decoded);
        }

        [Fact]
        public void DecodeIncrement()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 0);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0B
            }));

            Assert.Equal((UInt32)110, decoded);
        }

        [Fact]
        public void DecodeMinimumIncrement1()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 1);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0B
            }));

            Assert.Equal((UInt32)111, decoded);
        }

        [Fact]
        public void DecodeMinimumIncrement2()
        {
            var coder = new HeliumIntegerUnsigned(0, false, 10, 20);
            coder.Prepare(typeof(UInt32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0D
            }));

            Assert.Equal((UInt32)150, decoded);
        }
    }
}
