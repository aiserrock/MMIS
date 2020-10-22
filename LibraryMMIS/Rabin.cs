using System;
using System.Linq;

namespace LibraryMMIS
{
    public class Rabin
    {
        private long p { get; set; }
        private long q { get; set; }
        private long n { get; set; }
        private long r { get; set; }
        private long s { get; set; }
        private long d { get; set; }
        public long a { get; set; }
        public long b { get; set; }
        
        private long m1 { get; set; }
        private long m2 { get; set; }
        private long m3 { get; set; }
        private long m4 { get; set; }
        private long x { get; set; }//число которое хотим зашифровать

        private long c { get; set; }
        
        private long X { get; set; }//число которое расшифровали.
        
        public Rabin(long p,long q,long x)
        {
            long a = 0;
            long b = 0;
            try
            {
                if(!(Supporting.Simple(p)&&Supporting.Simple(q)&&p%4==3&&q%4==3))
                    throw new Exception("p и q обязаны быть простыми для метода Rabbin ,а также для удобства просчёта p и q по модулю 4 обязаны равняться трём");
                this.p = p;
                this.q = q;
                this.x = x;

                
                n = p * q;
                c = Supporting.bin_pow(x, 2, n);
                    //зашифрование
                    
                r = Supporting.bin_pow(c, (p + 1) / 4, p);
                s = Supporting.bin_pow(c, (q + 1) / 4, q);
                
                Supporting.ResolveDeofantovoEquation(ref a, ref b, p, q, 1);
                this.a = a;
                this.b = b;
                m1 = +((a * p * s + b * q * r) % n);
                m2 = -((a * p * s + b * q * r) % n);
                m3 = +((a * p * s - b * q * r) % n);
                m4 = -((a * p * s - b * q * r) % n);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
        }

        public override string ToString()
        {
            return $"Выбрали два простых числа p={p} и q={q},которые по модулю 4 равны 3\n"
                   + $"Cекретный ключ n = pq = {n}\n"
                   + $"Зашифруем сообщение m = {x}\n"
                   + $"Для зашифрования c=m^2 mod n ={x*x} mod {n} ={c}\n"
                   + $"Для расшифрования найдем вспомогательные переменные: \n"
                   + $"1) r = c^(p+1/4) mod p = {r}\n"
                   + $"2) s = c^(p+1/4) mod q = {s}\n"
                   + $" Найдём целые числа a и b такие что ap + bq = 1, a{p} + b{q} = 1\na = {a}, b = {b}\n"
                   + $" Тогда одно из этих 4ех сообщений будет нашим зашифрованным:\n"
                   + $"m1 = +(aps+bqr)={m1}\n"
                   + $"m2 = -(aps+bqr)={m2}\n"
                   + $"m3 = +(aps-bqr)={m3}\n"
                   + $"m4 = -(aps-bqr)={m4}\n";
        }
    }
}