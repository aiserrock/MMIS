using System;

namespace LibraryMMIS
{
    public class DiffieHellman
    {
        private readonly Random _random = new Random(DateTime.Now.Second);
        public long n { get; set; }//большое простое число
        
        public long g { get; set; }//примитивный элемент(мультипликативный)
                                   //образующий мультипликативную группу Zn*= {1,...,n-1}
        
        public long x { get; set; }//случайное число 
        public long y { get; set; }//случайное число
                                   
        public long X { get; set; }// X = g^x mod n;
        public long Y { get; set; }// Y = g^y mod n;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="Exception"></exception>
        public DiffieHellman(long n, long g , long x,long y)
        {
            try
            {
                this.n = n;
                if(!Supporting.Simple(n))
                    throw new Exception("N should be simple");
                if (g == 0)
                {
                    g = Supporting.PrimitivsMultiGroupZ(out string temp  ,n);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
    }
    
}