using System;
using InvertedTomato.Serialization.HeliumSerialization.Buffers;

namespace InvertedTomato.Serialization.HeliumSerialization
{
    public class Helium
    {
        public Byte[] Encode<T>(T value)
        {
#if DEBUG
            if (null == value)
            {
                throw new ArgumentNullException(nameof(value));
            }
#endif

            // Assume root is a class
            var coder = new HeliumClass(0, false);
            coder.Prepare(typeof(T));

            // Invoke coder
            var output = coder.Encode(value);

            // Flattern into to byte array
            return output.Flattern();
        }

        public T Decode<T>(Byte[] input)
        {
            return Decode<T>(new ArraySegment<Byte>(input));
        }


        public T Decode<T>(ArraySegment<Byte> input)
        {
#if DEBUG
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }
#endif

            // Assume root is a class
            var coder = new HeliumClass(0, false);
            coder.Prepare(typeof(T));

            // Invoke root serializer
            var value = coder.Decode(new DecodeBuffer(input));
            return (T)value;
        }

        public static Byte[] Serialize<T>(T value)
        {
            var helium = new Helium();
            return helium.Encode(value);
        }

        /// <summary>
        ///     Deserialize an object from a byte array.
        /// </summary>
        public static T Deserialize<T>(Byte[] payload)
        {
            var helium = new Helium();
            return helium.Decode<T>(payload);
        }
    }

}