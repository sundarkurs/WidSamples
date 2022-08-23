using DomainEventsUpdate.Model;
using DomainEventsUpdate.Services;
using System;
using System.Linq;


namespace DomainEventsUpdate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the last read event number, or it will start from the beginning");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                var updateBrandIdService = new UpdateDomainEventBrandIdService();
                updateBrandIdService.UpdateBrandId();
            }
            else
            {
                int last = Convert.ToInt32(Console.ReadLine());
                var updateBrandIdService = new UpdateDomainEventBrandIdService();
                updateBrandIdService.UpdateBrandId(last);
            }

            Console.Read();
        }
    }
}
