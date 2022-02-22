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
    public class Commands
    {
        readonly GraphQLHttpClient _graphQLHttpClient;

        public Commands()
        {
            var graphQLHttpClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://echo.wsa.com/graphql", UriKind.Absolute),
            };

            _graphQLHttpClient = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer());
            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _graphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer FJLJkMJQZSrwV8ckfXzk7MhFPw7MRaZL88GQn46i");
        }
    }
}
