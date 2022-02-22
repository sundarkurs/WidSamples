using FrontifyApiConsumer.Common;
using FrontifyApiConsumer.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrontifyApiConsumer.Services
{
    public class Queries
    {
        readonly GraphQLHttpClient _graphQLHttpClient;

        public Queries()
        {
            var graphQLHttpClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://echo.wsa.com/graphql", UriKind.Absolute),
            };

            _graphQLHttpClient = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer());
            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer FJLJkMJQZSrwV8ckfXzk7MhFPw7MRaZL88GQn46i");
        }

        public async Task<GraphQLResponse<ProjectAssets>> GetAllAssetsAsync(int pageIndex, int limitValue)
        {
            GraphQLResponse<ProjectAssets> graphQLResponse = null;
            try
            {
                var request = new GraphQLRequest
                {
                    Query = Constants.Queries.ProjectAssets
                };

                graphQLResponse = await _graphQLHttpClient.SendQueryAsync<ProjectAssets>(request);
            }
            catch (Exception ex)
            {
            }

            return graphQLResponse;
        }
    }
}
