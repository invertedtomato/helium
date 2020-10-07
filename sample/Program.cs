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
                B = new RecordB()
                {
                    A = 72
                }
            });

            
            var decoded = Helium.Deserialize<Record>(encoded);

            Console.WriteLine($"A={decoded.A}, B.A={decoded.B.A}");
        }
    }


    class Record
    {
        [HeliumIntegerUnsigned(0, false, 5, 40)]
        public UInt32 A;

        [HeliumClass(1, true)]
        public RecordB B;
    }

    class RecordB
    {
        [HeliumIntegerUnsigned(0, false, 5, 40)]
        public UInt32 A;
    }
}
