using System;
using LibraryMMIS;
namespace MMIS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Методы математической защиты информации:\nRSA - 1,\nРэббина - 2\nЭль-Гамаль - 3\nЭлектронноцифровая подпись Гамаля -4\n ");
            string methods = Console.ReadLine();
            long p, q, x;
            switch (methods)
            {
                case "1":
                    
                    Console.WriteLine("Введите простые числа p и q, и число x которое надо зашифровать по публичкому ключу и расшифровать по секретному ключу");
                    p = Convert.ToInt64(Console.ReadLine());
                    q = Convert.ToInt64(Console.ReadLine());
                    x = Convert.ToInt64(Console.ReadLine());
                    RSA rsa = new RSA(p,q,x);
                    Console.WriteLine("Решение:\n" +rsa.ToString());
                    break;
                case "2":
                    Console.WriteLine("Введите простые числа p и q(по модулю 4 равные трём), и число x которое надо зашифровать по публичкому ключу и расшифровать по секретному ключу");
                    p = Convert.ToInt64(Console.ReadLine());
                    q = Convert.ToInt64(Console.ReadLine());
                    x = Convert.ToInt64(Console.ReadLine());
                    Rabin rabin = new Rabin(p,q,x);
                    Console.WriteLine("Решение:\n" +rabin.ToString());
                    break;
                case "3":
                    
                    p = Convert.ToInt64(Console.ReadLine());
                    q = Convert.ToInt64(Console.ReadLine());
                    x = Convert.ToInt64(Console.ReadLine());
                    ElGamale elGamale= new ElGamale(p,q,8,7,x);
                    Console.WriteLine(elGamale.ToString());
                    break;
                default:
                        break;
            }
           
            
        }
    }
}