using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace LibraryMMIS
{
    public static class Supporting
    {
        static long Min(long x, long y)
        {
            return x < y ? x : y;
        }

        static long Max(long x, long y)
        {
            return x > y ? x : y;
        }

        public static bool Simple(long n) //проверка на простоту числа
        {
            for (long i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0)
                    return false;
            return true;
        }


        public static long GetNOD(long a, long b) //Поиск NOD
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);
                //вызываем метод с новыми аргументами
                return GetNOD(max % min, min);
            }
        }
        public static void gnd(long a, long b, out string output) //Поиск NOD
        {
            long q, r;
            output = "";
            if (b == 0)
            {
                output = $"НОД({a},{b}) = {a}\n";
                
            }
            int count = 0;
            output = "НОД\nИтерация   q   r   a   b\n";
            while (b>0)
            {
                q = a / b;
                r = a - q * b;
                a = b;
                b = r;
                output += $"{count}          {q}   {r}   {a}   {b}\n";
                count++;
            }

            output +=$"НОД = {a}\n" ;
        }

        public static void extendedEuclid(long a, long b, out long x, out long y, out long d, out string outputEuclid)
        {
            long q, r, x0, x1, y0, y1;
            long tempa = a;
            long tempb = b;
            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
                outputEuclid = $"Т.к один из входных параметров = 0, то НОД({a},{b}) = {d}, x = {x}, y = {y}\n";
            }

            x0 = 1;
            x1 = 0;
            y0 = 0;
            y1 = 1;
            outputEuclid = "";
            outputEuclid += $"x0 = {x0}\nx1 = {x1}\ny0 = {y0}\ny1 ={y1}\nИтерация q  r  a  b  x  y  x0  x1  y0  y1\n";
            int count = 0;
            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                a = b;
                b = r;
                x = x0 - q * x1;
                y = y0 - q * y1;
                x0 = x1;
                x1 = x;
                y0 = y1;
                y1 = y;
                outputEuclid += $"{count}        {q}  {r}  {a}  {b}  {x}  {y}  {x0}  {x1}  {y0}  {y1}\n";
                count++;
            }

            d = a; //NOD
            x = x0;
            y = y0;
            outputEuclid += $"NOD = a * x + b*y = d" +
                            $"Получаем d = {d} = {tempa} * {x} + {tempb} * {y}\nНОД = {d}\nx = {x}\ny = {y}\n";
        }

        /// <summary>
        /// Метод ищет обратный элемент к числу и хранит решение в message
        /// </summary>
        /// <param name="number"> число для которого ище</param>
        /// <param name="mod">мод числа</param>
        /// <param name="output"></param>
        /// <exception cref="Exception"></exception>
        public static void getMultiplicative(long number, long mod, out string output) //Нод - мультипликативный элемент
        {
            long d, x, y;
            var outputEuclid = "";
            output = "";
            extendedEuclid(number, mod, out x, out y, out d, out outputEuclid);
            if (d == 1)
            {
                x = (x % mod + mod) % mod;
                output += outputEuclid;
                output += $"x - обратное число к a\nт.к НОД = 1 , то x = (x % m + m) % m = {x}\n";
            }
            else
            {
                throw new Exception(
                    "Числа a и n должны быть взаимно простыми, не существует решения для входных параметров");
            }
        }

        public static bool MutuallySimple(long val1, long val2) //проверка на взаимную простоту чисел.
        {
            return GetNOD(val1, val2) == 1;
        }


        public static long
            GetMutuallySimple(long val1) //получить первое взаимное простое число с числом поданным на вход.
        {
            long i = 2;

            while (i < val1)
            {
                if (MutuallySimple(val1, i))
                    return i;
                i++;
            }

            return val1;
        }

        public static long
            ResolveModuleEquation(long start, long end, long y, long a, long b) //уравнение типа yx mod a = b 
        {
            if (b >= a)
                throw new Exception("b should be less a");
            for (var i = start; i < end; i++)
            {
                if (y * i % a == b)
                    return i;
            }

            throw new Exception("Equation haven`t resolve");
            return 0;
        }

        public static long ResolveDeofantovoEquation(ref long x, ref long y, long a, long b, long c)
        {
            for (int i = -100; i <= 100; i++)
            {
                for (int j = -100; j <= 100; j++)
                {
                    if (i * a + j * b == c)
                    {
                        x = i;
                        y = j;
                        return 0;
                    }
                }
            }

            for (int i = -1000; i <= 1000; i++)
            {
                for (int j = -1000; j <= 1000; j++)
                {
                    if (i * a + j * b == c)
                    {
                        x = i;
                        y = j;
                        return 0;
                    }
                }
            }

            return 0;
        }

        public static long bin_pow(long b, long p, long MOD)
        {
            //a^n mod  2^125  
            if (p == 1)
            {
                return b; //Выход из рекурсии.
            }

            if (p % 2 == 0)
            {
                long t = bin_pow(b, p / 2, MOD);
                //if (t < 0)
                //{
                //    t += MOD;
                //}
                return t * t % MOD;
            }
            else
            {
                long t = bin_pow(b, p - 1, MOD) * b % MOD;
                //if (t < 0)
                //{
                //    t += MOD;
                //}
                return t;
            }
        }

        public static long inverse_element(long x, long MOD)
        {
            return bin_pow(x, MOD - 2, MOD);
        }

        //(a / b) mod m
        public static long divide(long a, long b, long MOD)
        {
            // a/b mod k 
            return a * inverse_element(b, MOD) % MOD;
        }

        public static long divide_pow(long a, long pow, long MOD) /// 1/b^k mod 
        {
            long t = 1;
            for (int i = 0; i < pow; i++)
            {
                t *= inverse_element(a, MOD);
                t %= MOD;
            }
            return t % MOD;
        }

        //поиск примитивных элементов , возращаетс первый примитивный элемент.
        public static long PrimitivsMultiGroupZ(out string list, long n)
        {
            list = "";
            long first_primitive = 0;
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (bin_pow(i, j, n) == 1) //значит является примитивом
                    {
                        if (j == n - 1)
                            list += $"{i} ";
                        break;
                    }
                }
            }

            return Convert.ToInt64(list.Split(" ")[0]);
        }
    }
}