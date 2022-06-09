using DocCenterSample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DocCenterSample
{
    class Program
    {
        const string queryBaseUrl = "https://widextest.azure-api.net/plm-docplaceholder-query/v1";
        const string commandBaseUrl = "https://widextest.azure-api.net/plm-docplaceholder-command/v1";

        static async Task Main(string[] args)
        {
            var placeholders = await GetPlaceholders();
            placeholders?.ForEach(i => Console.WriteLine(JsonConvert.SerializeObject(i)));

            var transactionId = await BeginTransaction();
            Console.WriteLine(transactionId);

            await UploadDocuments(transactionId);

            await CommitTransaction(transactionId);

            var pageUrl = await GetUploadPageUrl(transactionId);
            Console.WriteLine(pageUrl);

            //await RollbackTransaction(transactionId);

            Console.ReadLine();
        }

        public static async Task<List<Placeholder>> GetPlaceholders()
        {
            var placeholderEndpoint = $"{queryBaseUrl}/api/placeholder";

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var response = await client.GetAsync(placeholderEndpoint);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        return JsonConvert.DeserializeObject<List<Placeholder>>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex) { }

            return null;
        }

        public static async Task<string> BeginTransaction()
        {
            var initializeEndpoint = $"{commandBaseUrl}/api/techdoc/transaction/initialize";

            var request = new TransactionInitializeRequest() { name = "Marketing Documents" };

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var response = await client.PostAsJsonAsync(initializeEndpoint, request);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex) { }

            return null;
        }

        public static async Task UploadDocuments(string transactionId)
        {
            string uploadEndpoint = $"{commandBaseUrl}/api/techdoc/transaction/{transactionId}/upload";

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var metadataStream = new StreamContent(File.OpenRead($"{AppContext.BaseDirectory}\\SampleFiles\\metadata.json"));
                    var fileStream = new StreamContent(File.OpenRead($"{AppContext.BaseDirectory}\\SampleFiles\\languagefile.pdf"));
                    var anotherFileStream = new StreamContent(File.OpenRead($"{AppContext.BaseDirectory}\\SampleFiles\\helpfile.pdf"));

                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StringContent(transactionId));
                        content.Add(metadataStream, "MetaData", "metadata.json");
                        content.Add(fileStream, "Files", "languagefile.pdf");
                        content.Add(anotherFileStream, "Files", "helpfile.pdf");

                        var response = await client.PostAsync(uploadEndpoint, content);

                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            Console.WriteLine($"Upload Success - {HttpStatusCode.OK}");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            Console.WriteLine($"Upload Failed");
        }

        public static async Task CommitTransaction(string transactionId)
        {
            string commitEndpoint = $"{commandBaseUrl}/api/techdoc/transaction/{transactionId}/commit";

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var response = await client.PutAsJsonAsync(commitEndpoint, transactionId);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        Console.WriteLine($"Commit Success - {HttpStatusCode.OK}");
                        return;
                    }
                }
            }
            catch (Exception ex) { }

            Console.WriteLine($"Commit Failed");
        }

        public static async Task RollbackTransaction(string transactionId)
        {
            string rollbackEndpoint = $"{commandBaseUrl}/api/techdoc/transaction/{transactionId}/rollback";

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var response = await client.DeleteAsync(rollbackEndpoint);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        Console.WriteLine($"Rollback - Success - {HttpStatusCode.OK}");
                        return;
                    }
                }
            }
            catch (Exception ex) { }

            Console.WriteLine($"Rollback Failed");
        }

        public static async Task<string> GetUploadPageUrl(string transactionId)
        {
            var uploadPageUrlEndpoint = $"{queryBaseUrl}/api/techdoc/{transactionId}/uploadpage";

            try
            {
                using (var client = new HttpClient())
                {
                    AddRequestHeaders(client);

                    var response = await client.GetAsync(uploadPageUrlEndpoint);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex) { }

            return null;
        }

        public static void AddRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4b584168f2d04185accbb625fac03ba4");
        }

    }
}
