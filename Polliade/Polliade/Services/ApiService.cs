using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Polliade.Services
{
    public interface IApiService
    {
        [Get("")]
        Task<string> Get();
    }

    public class ApiService
    {
        private readonly IApiService _restClient;

        public ApiService()
        {
            var client = new HttpClient { BaseAddress = new Uri("") };
            _restClient = RestService.For<IApiService>(client);
        }
    }
}
