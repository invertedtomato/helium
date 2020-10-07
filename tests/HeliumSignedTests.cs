using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;
using InvertedTomato.Serialization.HeliumSerialization.VariableLengthQuantities;
using Xunit;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class HeliumIntegerSignedTests
    {
        [Fact]
        public void EncodeZero()
        {
            var coder = new HeliumIntegerSigned(0, false, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode((Int32)0);

            Assert.Equal(SignedVlq.Encode(0).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeMax()
        {
            var coder = new HeliumIntegerSigned(0, false, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode(SignedVlq.MaxValue);

            Assert.Equal(SignedVlq.Encode(SignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeMin()
        {
            var coder = new HeliumIntegerSigned(0, false, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode(SignedVlq.MinValue);

            Assert.Equal(SignedVlq.Encode(SignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }



        [Fact]
        public void EncodeNullableZero()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode((Int32)0);

            Assert.Equal(SignedVlq.Encode(0+1).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableMax()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode(SignedVlq.MaxValue-1);

            Assert.Equal(SignedVlq.Encode(SignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableMin()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode(SignedVlq.MinValue+1);

            Assert.Equal(SignedVlq.Encode(SignedVlq.MaxValue).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeNullableNull()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(Int32?));
            var encoded = coder.Encode(null);

            Assert.Equal(SignedVlq.Encode(0).ToHexString(), encoded.ToHexString());
        }

        [Fact]
        public void EncodeIncrement()
        {
            var coder = new HeliumIntegerSigned(0, false, 10);
            coder.Prepare(typeof(Int32));
            var encoded = coder.Encode((Int32)111);

            Assert.Equal("0B", encoded.ToHexString());
        }


        [Fact]
        public void PrepareUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerSigned(0, false, 10);
                coder.Prepare(typeof(String));
            });
        }

        [Fact]
        public void EncodeUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerSigned(0, false, 10);
                coder.Prepare(typeof(Int32));
                var encoded = coder.Encode("asdf");
            });
        }


        [Fact]
        public void DecodeUnsupported()
        {
            Assert.Throws<UnsupportedDataTypeException>(() => {
                var coder = new HeliumIntegerSigned(0, false, 10);
                coder.Prepare(typeof(Int32));
                var decoded = coder.Decode(new DecodeBuffer(new Byte[] { 0, 0, 0 }));
            });
        }


        [Fact]
        public void DecodeZero()
        {
            var coder = new HeliumIntegerSigned(0, false);
            coder.Prepare(typeof(Int32));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(0).ToArray()));

            Assert.Equal((Int32)0, decoded);
        }

        [Fact]
        public void DecodeMax()
        {
            var coder = new HeliumIntegerSigned(0, false, 1);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(SignedVlq.MaxValue).ToArray()));

            Assert.Equal(UnsignedVlq.MaxValue, decoded);
        }

        [Fact]
        public void DecodeMin()
        {
            var coder = new HeliumIntegerSigned(0, false, 1);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(SignedVlq.MinValue).ToArray()));

            Assert.Equal(UnsignedVlq.MinValue, decoded);
        }

        [Fact]
        public void DecodeNullableZero()
        {
            var coder = new HeliumIntegerSigned(0, true);
            coder.Prepare(typeof(Int32));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(1).ToArray()));

            Assert.Equal((Int32)0, decoded);
        }

        [Fact]
        public void DecodeNullableMax()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(SignedVlq.MaxValue).ToArray()));

            Assert.Equal(UnsignedVlq.MaxValue-1, decoded);
        }

        [Fact]
        public void DecodeNullableMin()
        {
            var coder = new HeliumIntegerSigned(0, true, 1);
            coder.Prepare(typeof(UInt64));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(SignedVlq.MinValue).ToArray()));

            Assert.Equal(UnsignedVlq.MinValue+1, decoded);
        }


        [Fact]
        public void DecodeNullableNull()
        {
            var coder = new HeliumIntegerSigned(0, true);
            coder.Prepare(typeof(Int32));
            var decoded = coder.Decode(new DecodeBuffer(SignedVlq.Encode(0).ToArray()));

            Assert.Equal((Int32?)null, decoded);
        }


        [Fact]
        public void DecodeIncrement()
        {
            var coder = new HeliumIntegerSigned(0, false, 10);
            coder.Prepare(typeof(Int32));
            var decoded = coder.Decode(new DecodeBuffer(new byte[]{
                0x0B
            }));

            Assert.Equal((Int32)110, decoded);
        }
    }
}
