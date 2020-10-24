using System;

namespace LibraryMMIS
{
    public class ElGamaleSignature
    {
        static Random r = new Random(DateTime.Now.Second);

        public PublicKey pk { get; set; }
        
        public SecretKey sk { get; set; }

        public string message;

        public string decodemessage;

        //Реализация публичный и секретного ключа.
        #region PublicAndSecretKey
        public struct PublicKey
        {
            public long p { get; set; }
            public long g { get; set; }
            public long y { get; set; }

            public PublicKey(long p, long g, SecretKey sk)
            {
                try
                {
                    if(!Supporting.Simple(p))
                        throw new Exception("p should be simple");
                    this.p = p;
                    if (!(1 < g && g<p))
                        throw new Exception("Condition 1<g<p is not met");
                    this.g = g;
                    this.y = Supporting.bin_pow(g, sk.x, p);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            public override string ToString()
            {
                return $"Public key: ({nameof(p)}: {p}, {nameof(g)}: {g}, {nameof(y)}: {y})";
            }
        }
        public struct SecretKey
        {
            public long x { get; set; }

            public SecretKey(long x,long p)
            {
                if (x == 0)
                    x = r.Next(1, (int) p);
                if (!(1<x && x<p))
                    throw new Exception("Condition 1<x<p is not met");
                this.x = x;
            }

            public override string ToString()
            {
                return $"Secret key: ({nameof(x)}: {x})";
            }
        }
        #endregion 
        
        
        
    }
}