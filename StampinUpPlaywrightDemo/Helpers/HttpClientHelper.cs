
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Models;
using System.Net.Http;
using System.Text;


namespace StampinUpPlaywrightDemo.Helpers
{
    public class HttpClientHelper
    {
        private readonly HttpClient _client;

        public HttpClientHelper(HttpClient client)
        {
            _client = client;
        }

        private void SetHeaders(HttpRequestMessage request,string cookie = null)
        {
            request.Headers.Clear();
            request.Headers.Add("Accept", "application/json");

            if (!string.IsNullOrWhiteSpace(cookie))
                request.Headers.Add("Cookie", cookie);
        }

        public async Task<HttpResponse> GetAsync(string endpoint, string cookie = null)
        {
            Console.WriteLine($"URL and ENDPOINT being tested: {Constants.API_BASE_URL}{endpoint}");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.API_BASE_URL}{endpoint}");
            SetHeaders(request, cookie);

            var response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return new HttpResponse((int)response.StatusCode, response.ReasonPhrase ?? "", content);
        }

        public async Task<HttpResponse> PutAsync(string endpoint, object payload, string cookie = null)
        {
            Console.WriteLine($"URL and ENDPOINT being tested: {Constants.API_BASE_URL}{endpoint}");

            var request = new HttpRequestMessage(HttpMethod.Put, $"{Constants.API_BASE_URL}{endpoint}");
            SetHeaders(request, cookie);

            if (!string.IsNullOrEmpty(cookie))
                request.Headers.Add("Cookie", cookie);

            if (payload != null)
            {
                string json = JsonConvert.SerializeObject(payload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            else
            {
                Console.WriteLine("!!! Payload was NULL !!!");
            }

            var response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return new HttpResponse((int)response.StatusCode, response.ReasonPhrase ?? "", content);
        }
    }
}
