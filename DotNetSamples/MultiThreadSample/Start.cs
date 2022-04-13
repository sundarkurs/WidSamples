using MultiThreadSample.Services;
using System.Collections.Concurrent;

namespace MultiThreadSample
{
    static class Start
    {
        public static void PrimeStart()
        {
            var limit = 200;
            var numbers = Enumerable.Range(0, limit).ToList();

            var primeService = new PrimeService();

            Console.WriteLine($"Start - {DateTime.Now.ToLongTimeString()}");

            var aa = primeService.GetPrimeListWithParallel(numbers);

            Console.WriteLine($"End - {DateTime.Now.ToLongTimeString()}");
        }

        public static async Task NameStartAsync()
        {
            var names = new List<string>()
            {
                "Apple","Ball","Ant", "Bat"
            };

            var bag = new ConcurrentBag<object>();

            var service = new MultiThreadSample.Services.TodoService();

            var tasks = names.Select(async name =>
            {
                var response = await service.ProcessAsync(name);
                bag.Add(response ? $"{name} - Success" : $"{name} - Failed");
            });

            await Task.WhenAll(tasks);

            var count = bag.Count;
        }
    }
}
