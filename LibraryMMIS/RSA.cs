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
                if(!Supporting.Simple(p)||!Supporting.Simple(q))
                    throw new Exception("p и q обязаны быть простыми для метода RSA");
                this.p = p;
                this.q = q;
                n = p*q;
                fi = (p-1)*(q-1);
                e = Supporting.GetMutuallySimple(fi);
                d = Supporting.ResolveModuleEquation(1,n-1,e,fi,1);
                if(!(0<x && x<n))
                    throw new Exception("x должен быть больше нуля и меньше n");
                this.x = x;
                C = Supporting.bin_pow(x, e, n);
                if(!(0<C && C<n))
                    throw new Exception("C должен быть больше нуля и меньше n");
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
            return $"({p},{q},{d})";
        }
        
        public static string DecodeMessageGet(long c, long d, long n)
        {
            string resolve = "Расшифруем сообщение и получим X = c^d mod n = {c}^{d} mod {n} = {Supporting.bin_pow(c, d, n)}";
            return resolve;
        }
        public override string ToString()
        {
            return $"p = {p} , q = {q}\n"
                   + $"n=p*q={n}\n"
                   + $"fi(n)=(p-1)(q-1)={fi}\n"
                   + $"Возьмем взаимнопростое число e = {e}\n"
                   + $"Публичный ключ: {PublicKey()}\n"
                   + $"Из уравнения ed = 1 mod fi(n), получаем d = {d}\n"
                   + $"Приватный ключ: {PrivateKey()}\n"
                   + $"Зашифровываем сообщение x = {x}\n"
                   + $"C = x^e mod n = {C}\n"
                   + $"Расшифруем сообщение и получим обратно x =C^d mod n={X}\n";
        }
    }
}