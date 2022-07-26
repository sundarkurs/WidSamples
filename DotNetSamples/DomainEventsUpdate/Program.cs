using DomainEventsUpdate.Model;
using System;
using System.Linq;

namespace DomainEventsUpdate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new tstmsplmwsasdbContext())
            {
                var brand = context.Brands.FirstOrDefault();

            }

        }
    }
}
