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
            Console.WriteLine("Start");

            var updateBrandIdService = new UpdateDomainEventBrandIdService();
            updateBrandIdService.UpdateBrandId();

            Console.Read();
        }
    }
}
