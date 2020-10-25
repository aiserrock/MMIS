using System;
using System.Net;

namespace LibraryMMIS
{
    public class ElGamaleSignature
    {
        static Random r = new Random(DateTime.Now.Second);

        public PublicKey pk { get; set; } // публичный ключ

        public SecretKey sk { get; set; } //секретный ключ

        public long M { get; set; } //сообщение , которое проверяется на подлинность. 

        public long k { get; set; } // если k==0 то выбирается случайное число, иначе на вход подается 1<k<p-1  
        public long ReverseK { get; set; } // такое k^-1 что k*k^-1 = 1 mod (p-1)
        public long A { get; set; } //числа А и B - это и есть подпись. Которая будет проверятся

        public long B { get; set; }

        //Реализация публичный и секретного ключа.
        public long ANumber1 { get; set; } //если АuthenticityNumber1 == АuthenticityNumber2 , то сообщение подлинное,
        public long ANumber2 { get; set; } //если нет , то подпись либо неподлинная , либо сообщение фальшивое.

        public bool Аuthenticity { get; set; } //подлинно ли полученное сообщение.

        #region PublicAndSecretKey

        public struct PublicKey
        {
            public long p { get; set; } //простое число
            public long g { get; set; } //число от 1<g<p
            public long y { get; set; } // y = g^x mod p

            public PublicKey(long p, long g, SecretKey sk)
            {
                try
                {
                    //if (!Supporting.Simple(p))
                      //  throw new Exception("p should be simple");
                    this.p = p;
                    //if (!(1 < g && g < p))
                      //  throw new Exception("Condition 1<g<p is not met");
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
            public long x { get; set; } // если x ==0 , то ищется рандомное число ,иначе 1<x<p

            public SecretKey(long x, long p)
            {
                if (x == 0)
                    x = r.Next(1, (int) p);
                //if (!(1 < x && x < p))
                  //  throw new Exception("Condition 1<x<p is not met");
                this.x = x;
            }

            public override string ToString()
            {
                return $"Secret key: ({nameof(x)}: {x})";
            }
        }

        #endregion

        public ElGamaleSignature(long p, long g, long x, long K, long message)
        {
            sk = new SecretKey(x, p); //Secret key: (x)
            pk = new PublicKey(p, g, sk); //Public key: (p,g,y)
            M = message;
            if (K == 0)
            {
                while (!Supporting.MutuallySimple(k, p - 1))
                {
                    k = r.Next(1, (int) p - 1);
                }
            }
            else
            {
                k = K;
                if (!(1 < k && k < p - 1) && !Supporting.MutuallySimple(k, p - 1))
                    throw new Exception("k - сессионный ключ обязан придерживаться условия 1<k<p-1");
            }


            ReverseK = Supporting.ResolveModuleEquation(1, p - 1, k, p - 1,
                1); //Поиск к^-1 через уравнение k*k^-1 mod (p-1) = 1

            A = Supporting.bin_pow(g, k, p);
            B = ((M - x * A) * ReverseK) % (p - 1); //цифровая подпись из чисел A и B,  
            if (B < 0)
                B += p - 1;
            // проверка подлинности подписи
            ANumber1 = (Supporting.bin_pow(pk.y, A, p) * Supporting.bin_pow(A, B, p)) % p;
            ANumber2 = Supporting.bin_pow(g, M, p);
            if (ANumber1 == ANumber2)
                Аuthenticity = true;
            else
            {
                Аuthenticity = false;
            }
        }

        public string SignatureVerificationGet(long a, long b, long p, long g, long y, long Message)
        {
            string resolve = "";
            PublicKey publicKey = new PublicKey()
            {
                p = p,
                g = g,
                y = y
            };
            resolve += $"Решение:" +
                       $"Получен публичный ключ: {publicKey.ToString()}\n" +
                       $"Поступило сообщение M = {M} и цифровая подпись (a = {A} и b = {B})\n" +
                       $"Получатель вычисляет два числа:\n" +
                       $"1)Number1=(y^a)*(a^b) mod p = (((y^a) mod p)*((a^b) mod p)) mod p." +
                       $"2)Number2=g^M mod p";
                       // проверка подлинности подписи
            ANumber1 = (Supporting.bin_pow(publicKey.y, a, publicKey.p) * Supporting.bin_pow(a, b, publicKey.p)) % publicKey.p;
            ANumber2 = Supporting.bin_pow(publicKey.g, Message, publicKey.p);

            if (ANumber1 == ANumber2)
            {
                Аuthenticity = true;
                resolve += $"Т.к Number1=Number2={ANumber1} ,то сообщение является подлинным ";
            }
            else
            {
                Аuthenticity = false;
                resolve += $"Т.к Number1={ANumber1} и Number2={ANumber2} - они не равны друг друга ,поэтому сообщение является фальшивым или подпись неподлинная ";
            }
            return "";
        }


        public override string ToString()
        {
            return $"Выберем числа p={pk.p} и g={pk.g} и случайный секретный ключ x={sk.x}\n"
                   + $"Вычислим значение открытого ключа y = g^x mod p = {pk.g}^{sk.x} mod {pk.p} ={pk.y}\n"
                   + $"Вычислим цифровую подпись для сообщения M = {M}\n"
                   + $"    1)Сначала выберем случайное число k = {k}, 1<k<p-1, такое, что числа k и p-1 взаимно простые - {Supporting.MutuallySimple(k, pk.p - 1)}\n"
                   + $"1.1)Найдём число k^-1 такое что выполняется условие k*k^(-1) = 1 mod(p-1) т.е {k}*k^(-1) = 1 mod {pk.p - 1}\n"
                   + $"    2)Вычислить числа a = g^k mod p = {pk.g}^{k} mod {pk.p} = {A}, и с помощью секретного ключа x вычислим b = (M-xa)k^-1 mod (p-1)={B}\n"
                   + $"Тем самым цифровая подпись представляет собой пару чисел: a = {A}, b = {B}..\n"
                   + $"    3)Проверим подпись. Приняв сообщение M = {M} и цифровую подпись (a = {A} и b = {B})\n"
                   + $"Получатель вычисляет два числа 1)(y^a)*(a^b) mod p = {ANumber1}\n "
                   + $"И 2) g^M mod p = {ANumber2}\n"
                   + $"Проверим они равны? - {Аuthenticity}\n"
                   + "Если два числа равны , то принятое получателем сообщение признается подлинным , иначе сообщение фальшивое или цифровая подпись не подлинная\n";
        }
    }
}