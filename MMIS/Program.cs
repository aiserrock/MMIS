﻿using System;
using LibraryMMIS;
namespace MMIS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Методы математической защиты информации:\nRSA - 1,\nРэббина - 2\nЭль-Гамаль - 3\nЭлектронноцифровая подпись Гамаля -4\nИли дополнительные решения к методам (1д ,3д,4д)\n");
            string methods = Console.ReadLine();
            long p, q, x, g, k,message;
            long c, d, n,y;
            long a, b;
            string outmessage = "";
            switch (methods)
            {
                case "1":
                    Console.WriteLine("Введите простые числа p и q, и число x которое надо зашифровать по публичкому ключу и расшифровать по секретному ключу");
                    Console.Write("p= ");
                    p = Convert.ToInt64(Console.ReadLine());
                    Console.Write("q= ");
                    q = Convert.ToInt64(Console.ReadLine());
                    Console.Write("message= ");
                    message = Convert.ToInt64(Console.ReadLine());
                    RSA rsa = new RSA(p,q,message);
                    Console.WriteLine("Решение:\n" +rsa.ToString());
                    break;
                case "1д":
                    Console.WriteLine("Введите зашифрованное сообщение c , число d полученное из уравнения ed = 1 mod fi(n),число n - секретный ключ");
                    Console.Write("c= ");
                    c = Convert.ToInt64(Console.ReadLine());
                    Console.Write("d= ");
                    d = Convert.ToInt64(Console.ReadLine());
                    Console.Write("n= ");
                    n = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine(RSA.DecodeMessageGet(c,d,n));
                    break;
                case "2":
                    Console.WriteLine("Введите простые числа p и q(по модулю 4 равные трём), и число x которое надо зашифровать по публичкому ключу и расшифровать по секретному ключу");
                    p = Convert.ToInt64(Console.ReadLine());
                    q = Convert.ToInt64(Console.ReadLine());
                    message = Convert.ToInt64(Console.ReadLine());
                    Rabin rabin = new Rabin(p, q, message);
                    Console.WriteLine("Решение:\n" +rabin.ToString());
                    break;
                case "3":
                    Console.WriteLine("Введите простое число p ,1<g<p, 1<x<p или 0 -если рандомное, 1<k<p-1(взаимно простое с (p-1)) или 0 - если рандомное, message- сообщение которое будем проверять на подлинность ");;
                    Console.Write("p= ");
                    p = Convert.ToInt64(Console.ReadLine());
                    Console.Write("g= ");
                    g = Convert.ToInt64(Console.ReadLine());
                    Console.Write("x= ");
                    x = Convert.ToInt64(Console.ReadLine());
                    Console.Write("k= ");
                    k = Convert.ToInt64(Console.ReadLine());
                    Console.Write("message= ");
                    message = Convert.ToInt64(Console.ReadLine());
                    ElGamale elGamale= new ElGamale(p,g,x,k,message);
                    Console.WriteLine(elGamale.ToString());
                    break;
                case "3д":
                    Console.WriteLine(
                        "Введите числа a и b (полученное сообщение), секретный ключ x и публичное число p");
                    Console.Write("a= ");
                    a = Convert.ToInt64(Console.ReadLine());
                    Console.Write("b= ");
                    b = Convert.ToInt64(Console.ReadLine());
                    Console.Write("x= ");
                    x = Convert.ToInt64(Console.ReadLine());
                    Console.Write("p= ");
                    p = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine(ElGamale.DecodeMessageGet(a,b,x,p));
                    break;
                case "4":
                    Console.WriteLine("Введите простое число p ,1<g<p, 1<x<p или 0 -если рандомное, 1<k<p-1(взаимно простое с (p-1)) или 0 - если рандомное, message- сообщение которое будем проверять на подлинность ");
                    Console.Write("p= ");
                    p = Convert.ToInt64(Console.ReadLine());
                    Console.Write("g= ");
                    g = Convert.ToInt64(Console.ReadLine());
                    Console.Write("x= ");
                    x = Convert.ToInt64(Console.ReadLine());
                    Console.Write("k= ");
                    k = Convert.ToInt64(Console.ReadLine());
                    Console.Write("message= ");
                    message = Convert.ToInt64(Console.ReadLine());
                    ElGamaleSignature elGamaleSignature= new ElGamaleSignature(p,g,x,k,message);
                    Console.WriteLine(elGamaleSignature.ToString());
                    break;
                case "4д":
                    Console.WriteLine(
                        "Введите числа a и b (цифровая подпись), секретный ключ x и публичное число p,g,y и message ,которое будет проверять на подлинность");
                    Console.Write("a= ");
                    a = Convert.ToInt64(Console.ReadLine());
                    Console.Write("b= ");
                    b = Convert.ToInt64(Console.ReadLine());
                    Console.Write("x= ");
                    x = Convert.ToInt64(Console.ReadLine());
                    Console.Write("p= ");
                    p = Convert.ToInt64(Console.ReadLine());
                    Console.Write("g= ");
                    g = Convert.ToInt64(Console.ReadLine());
                    Console.Write("y= ");
                    y = Convert.ToInt64(Console.ReadLine());
                    Console.Write("message= ");
                    message = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine(ElGamaleSignature.SignatureVerificationGet(a,b,p,g,y,message));
                    break;
                case "100":
                    long x1 = 0, y1=0;
                    Console.Write("Введите n мультипликативной группе Z(n)*=(1....n-1) по модулю n: ");
                    n = Convert.ToInt64(Console.ReadLine());
                    Console.Write("Введите a принадлежащий группе Z(n)* ");
                    a = Convert.ToInt64(Console.ReadLine());
                    Console.Write("Обратный элемент a^-1 поиск ...");
                    Supporting.ResolveDeofantovoEquation( ref x1, ref y1,a,n,1);
                    Console.WriteLine($"Решением уравнения ax+ny=1 т.е {a}x+{n}y = {1},являются x = {x1} , y={y1}." +
                                      $"И обратным элементом для выражения a*a^-1 mod n = 1 ,является a^-1 = {Supporting.inverse_element(a,n)}");
                    Console.WriteLine("Найдем список примитивных элементов группы (Zn)*");
                    Supporting.PrimitivsMultiGroupZ(out outmessage,n);
                    Console.WriteLine("Из определения для множества обратимых элементов Z*n, порядком элемента a- называется наименьшее s , такое что a^s = 1 mod n");
                    Console.WriteLine("Из определения элемент а принадлежащий Z*n, порядок которого равен фи(n)=n-1={n-1}");
                    Console.WriteLine(outmessage);
                    break;
                case "101":
                    break;
                    
                default:
                        break;
            }
            Console.ReadLine();

        }
    }
}