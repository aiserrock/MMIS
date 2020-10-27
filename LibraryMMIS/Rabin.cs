﻿using System;
using System.Linq;

namespace LibraryMMIS
{
    public class Rabin
    {
        private long p { get; set; }/// <summary>
                                    /// p и q - секретный ключ
                                    /// n - открытый ключ
                                    /// r,s,a,b - вспомогательные переменные
                                    /// </summary>
        private long q { get; set; }
        private long n { get; set; }
        private long r { get; set; }
        private long s { get; set; }
        public long a { get; set; }
        public long b { get; set; }
        
        private long m1 { get; set; }/// <summary>
                                     /// одно из m1...m4 является расшифрованным сообщением.
                                     /// </summary>
        private long m2 { get; set; }
        private long m3 { get; set; }
        private long m4 { get; set; }
        private long x { get; set; }//число которое хотим зашифровать

        private long c { get; set; }//защифрованное число-сообщение
        

        private string outputExtendEuqlid;
        public Rabin(long p,long q,long x)
        {
            long a = 0;
            long b = 0;
            long d;
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
                Supporting.extendedEuclid(p,q,out a,out b,out d, out outputExtendEuqlid);
                this.a = a;
                this.b = b;
                m1 = +((a * p * s + b * q * r)) % n;
                if (m1 < 0)
                    m1 += n;
                m2 = -((a * p * s + b * q * r)) % n;
                if (m2 < 0)
                    m2 += n;
                m3 = +((a * p * s - b * q * r)) % n;
                if (m3 < 0)
                    m3 += n;
                m4 = -((a * p * s - b * q * r)) % n;
                if (m4 < 0)
                    m4 += n;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
        }
        
        //p и q - секретные ключи
        //n - открытый ключ

        public override string ToString()
        {
            return $"Выбрали два простых числа p={p} и q={q} ((p,q)- секретный ключ),которые по модулю 4 равны 3ем\n"
                   + $"Открытый ключ n = pq = {n}\n"
                   + $"Зашифруем сообщение m = {x}\n"
                   + $"Для зашифрования c=m^2 mod n ={x * x} mod {n} ={c}\n"
                   + $"Для расшифрования найдем вспомогательные переменные: \n"
                   + $"1) r = c^(p+1/4) mod p = {r}\n"
                   + $"2) s = c^(p+1/4) mod q = {s}\n"
                   + $"Найдём целые числа x и y, такие что px + qy = 1 = NOD(p,q),\n"
                   + $"{outputExtendEuqlid}"
                   + $"x{p} + y{q} = 1\nx = {a}, y = {b}\n"
                   + $"Тогда одно из этих 4ех сообщений будет нашим зашифрованным:\n"
                   + $"m1 = +(xps+yqr) mod n={m1}\n"
                   + $"m2 = -(xps+yqr) mod n={m2}\n"
                   + $"m3 = +(xps-yqr) mod n={m3}\n"
                   + $"m4 = -(xps-yqr) mod n={m4}\n";
        }
    }
}