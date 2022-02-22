
using FrontifyApiConsumer.Services;

Console.WriteLine("Hello, World!");

var queries = new Queries();
var assets = await queries.GetAllAssetsAsync(0, 5);

Console.ReadLine();