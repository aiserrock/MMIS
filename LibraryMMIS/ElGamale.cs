﻿using System;
namespace LibraryMMIS
{
    public class ElGamale
    {
        private Random r; 
        private long p { get; set; } 
        private long g { get; set; }
        private long x { get; set; }
        private long Y { get; set; }
        private long m { get; set; }
        private long k { get; set; }
        private long A { get; set; }
        private long B { get; set; }
        private long DecodeM { get; set; }


        public ElGamale(long p, long g, long X, long K ,long Message)
        {
            try
            {
                if(!Supporting.Simple(p))
                   throw new Exception("p should be simple");
                    
                r = new Random(DateTime.Now.Second); 
                if (X == 0)
                    x = r.Next(1, (int)p);
                else
                {
                    x = X;
                }
                this.p = p;
                this.g = g;
                Y = Supporting.bin_pow(g, x, p);
                m = Message;
                if (K == 0)
                {
                    while (!Supporting.MutuallySimple(k, p - 1))
                    {
                        k = r.Next(1, (int)p - 1);
                    }
                }
                    
                else
                {
                    k = K;
                   if (!(1 < k && k < p - 1) &&! Supporting.MutuallySimple(k,p-1))
                     throw new Exception("k - сессионный ключ обязан придерживаться условия 1<k<p-1");
                }
                A = Supporting.bin_pow(g, k, p);
                    
                B = (Supporting.bin_pow(Y, k, p)*(m%p))%p;
                    
                DecodeM = ((B % p) * Supporting.divide_pow(A, x,p)) % p;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public static string DecodeMessageGet(long a, long b, long x,long p)
        {
            string resolve = "";
            long Decode = ((b % p) * Supporting.divide_pow(a, x,p)) % p;
            resolve += $"Решение для закодированного сообщения :\n" +
                       $"Получена пара чисел: a={a} и b={b} - это и есть зашифрованное сообщение. А также мы имеем секретный ключ x = {x}\n" +
                       $"Для того чтобы раскодировать сообщение воспользуемся формулой" +
                       $"M = b/a^x mod p = b*a^(-x) mod p= {b}*{a}^(-{x}) mod {p}= (({b}mod{p})*({a}^(-{x}) mod p)) mod p = (({(b % p)})*({Supporting.divide_pow(a, x, p)})) mod {p} = {Decode}\n" +
                       $"Таким образом было полученно расшифрованное сообщение с помощью секретного ключа {x}, сообщение M = {Decode}\n";
            return resolve;
        }

        public override string ToString()
        {
            return "Решение: \n" +
                   $"p = {p}\n" +
                   $"q = {g}\n" +
                   $"x = {x}\n" +
                   $"Y = {Y}\n" +
                   $"m = {m}\n" +
                   $"k = {k}\n" +
                   $"A = {A}\n" +
                   $"B = {B}\n" +
                   $"Decodem = {DecodeM}\n";
        }
    }
}