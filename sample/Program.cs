using System;
using InvertedTomato.Serialization.HeliumSerialization;

namespace InvertedTomato.Serialization.HeliumSerialization.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoded = Helium.Serialize(new Record()
            {
                A = 42,
                B = new Record()
                {
                    A = 72,
                    B = null
                }
            });

            
            var decoded = Helium.Deserialize<Record>(encoded);
        }
    }


    class Record
    {
        [HeliumIntegerUnsigned(1, false, 40, 5)]
        public UInt32 A { get; set; }

        [HeliumClass(2, true)]
        public Record B { get; set; }
    }
}
