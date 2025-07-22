
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

        private void SetHeaders(HttpRequestMessage request, string cookie = null)
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

        public async Task<Account> GetFullAccountAsync(string cookie)
        {
            var getResponse = await GetAsync("/account", cookie);
            Assert.That(getResponse.Code, Is.EqualTo(200), "GET /account failed");
            return JsonConvert.DeserializeObject<Account>(getResponse.Data);
        }

        public Contact ExtractContactFromAccount(Account account)
        {
            return new Contact
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                email = account.Email,
                NewEmail = account.Email,
                PhoneNumber = account.PhoneNumber,
                PreferredContact = account.PreferredContact,
                Birthday = account.Birthday
            };
        }

        public Contact GenerateSanitizedContact(Contact original)
        {
            var rand = new Random();
            var newFirst = $"Test{rand.Next(100, 999)}";
            var newLast = $"User{rand.Next(100, 999)}";

            var phoneBase = "(435) 659-";
            var newLast4 = rand.Next(1000, 9999).ToString();
            var newPhone = phoneBase + newLast4;

            var contactMethods = new[] { "email", "text", "phone" };

            return new Contact
            {
                FirstName = newFirst,
                LastName = newLast,
                email = original.email,
                NewEmail = original.email,
                PhoneNumber = newPhone,
                PreferredContact = contactMethods[rand.Next(contactMethods.Length)],
                Birthday = original.Birthday ?? "01 May"
            };
        }

        public async Task<HttpResponse> PutContactInfoAsync(Contact contact, string cookie)
        {
            Console.WriteLine("PUT /account/contact with sanitized contact data");
            Console.WriteLine(JsonConvert.SerializeObject(contact, Formatting.Indented));
            return await PutAsync("/account/contact", contact, cookie);
        }

        public async Task<bool> SanitizeContactInfoAsync(string cookie)
        {
            Console.WriteLine("Start SanitizeContactInfoAsync");

            var account = await GetFullAccountAsync(cookie);
            var currentContact = ExtractContactFromAccount(account);
            var sanitizedContact = GenerateSanitizedContact(currentContact);

            var response = await PutContactInfoAsync(sanitizedContact, cookie);
            Assert.That(response.Code, Is.EqualTo(200), "PUT /account/contact failed");

            var account2 = await GetFullAccountAsync(cookie);
            var currentContact2 = ExtractContactFromAccount(account2);
            Console.WriteLine($"\n\n");
            currentContact2.PrintData();
            Console.WriteLine($"\n\n");

            Console.WriteLine("End SanitizeContactInfoAsync");
            return true;
        }


    }
}
