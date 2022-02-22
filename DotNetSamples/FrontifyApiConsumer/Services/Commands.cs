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
    public class Commands
    {
        readonly GraphQLHttpClient _httpClient;
        private const int maxRetryAttempts = 3;
        private TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(2);

        public Commands()
        {
            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("https://echo.wsa.com/graphql", UriKind.Absolute),
            };

            _httpClient = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
            _httpClient.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer FJLJkMJQZSrwV8ckfXzk7MhFPw7MRaZL88GQn46i");
        }

        public async Task<bool> CreateAssetAsync()
        {
            GraphQLResponse<FrontifyCreateAsset> graphQLResponse = null;
            try
            {
                var request = new GraphQLRequest
                {
                    Query = Constants.Mutations.CreateAssetMutation,
                    Variables = new
                    {
                        input = new { projectId = Constants.ApprovedLibraryId, fileId = "TEST-ID", title = "TEST-NAME" }
                    }
                };

                Helper.RetryOnException(maxRetryAttempts, pauseBetweenFailures, () =>
                {
                    graphQLResponse = AsyncHelper.RunSync(async () => await _httpClient.SendMutationAsync<FrontifyCreateAsset>(request));
                });
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        
    }
}
