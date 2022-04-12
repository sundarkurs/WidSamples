using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadSample.Services
{
    internal class PrimeService
    {
        public IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, async number =>
            {
                if (await IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });
            
            return primeNumbers.ToList();
        }

        public async Task<bool> IsPrime(int number)
        {
            Console.WriteLine(number);
            Thread.Sleep(1000);
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
