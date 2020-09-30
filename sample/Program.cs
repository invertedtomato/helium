using System;

namespace sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoded = Helium.Serialize(new Record()
            {
                A = "Lala",
                B = 42,
                C = new Record()
                {
                    A = "La",
                    B = 72,
                    C = null
                }
            });

            
            var decoded = Helium.Deserialize<Record>(encoded);
        }
    }


    class Record
    {
        [HeliumString(0, false)]
        public String A { get; set; }

        [HeliumIntegerSigned(1, false, 40, 5, 100)]
        public Int32 B { get; set; }

        [HeliumClass(2, true)]
        public Record C { get; set; }
    }
}
