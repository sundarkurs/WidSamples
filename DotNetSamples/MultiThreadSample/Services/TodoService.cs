using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadSample.Services
{
    public class TodoService
    {
        public async Task<bool> ProcessAsync(string name)
        {
            Console.WriteLine($"{name} - {DateTime.Now}");

            if (name.StartsWith("A"))
            {
                //Thread.Sleep(3000);
                Console.WriteLine($"{name} - {DateTime.Now}");
                return true;
            }

            //Thread.Sleep(2000);
            Console.WriteLine($"{name} - {DateTime.Now}");
            return false;
        }
    }
}
