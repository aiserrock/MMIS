using System;

namespace LibraryMMIS
{
    public class RSA
    {
        private long p { get; set; }
        private long q { get; set; }
        private long n { get; set; }
        private long e { get; set; }
        private long fi { get; set; }
        private long d { get; set; }

        private long x { get; set; }//число которое хотим зашифровать

        private long C { get; set; }
        
        private long X { get; set; }//число которое расшифровали.

        public RSA(long p, long q,long x)
        {
            try
            {

                this.p = p;
                this.q = q;
                n = p*q;
                fi = (p-1)*(q-1);
                e = Supporting.GetMutuallySimple(fi);
                d = Supporting.ResolveModuleEquation(1,n-1,e,fi,1);

                this.x = x;
                C = Supporting.bin_pow(x, e, n);

                X = Supporting.bin_pow(C, d, n);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public string PublicKey()
        {
            return $"({n},{e})";
        }
        
        public string PrivateKey()
        {
            return $"({d},{n})";
        }
        
        public static string DecodeMessageGet(long c, long d, long n)
        {
            string resolve = "Расшифруем сообщение и получим X = c^d mod n = {c}^{d} mod {n} = {Supporting.bin_pow(c, d, n)}";
            return resolve;
        }
        public override string ToString()
        {
            string outputGND;
            Supporting.gnd(e,fi, out outputGND);
            return $"p = {p} , q = {q}\n"
                   + $"n=p*q={n}\n"
                   + $"fi(n)=(p-1)(q-1)={fi}\n"
                   + $"Возьмем взаимнопростое число e = {e}\n"
                   + $"Публичный ключ: {PublicKey()}\n"
                   + $"{outputGND}"
                   + $"Из уравнения ed = 1 mod fi(n), получаем d = {d}\n"
                   + $"Приватный ключ: {PrivateKey()}\n"
                   + $"Зашифровываем сообщение x = {x}\n"
                   + $"C = x^e mod n = {C}\n"
                   + $"Расшифруем сообщение и получим обратно x =C^d mod n={X}\n";
        }
    }
}