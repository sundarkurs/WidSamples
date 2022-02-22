
using FrontifyApiConsumer.Services;

Console.WriteLine("Hello, World!");

var queries = new Queries();
var commands = new Commands();
//var assets = await queries.GetAssetsAsync(0, 5);
var res = commands.CreateAssetAsync();

Console.ReadLine();