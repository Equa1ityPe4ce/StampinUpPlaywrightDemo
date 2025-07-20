using Newtonsoft.Json;

namespace StampinUpPlaywrightDemo.Models
{
    public class Address
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("defaultAddress")]
        public bool DefaultAddress { get; set; }

        [JsonProperty("defaultMailingAddress")]
        public bool DefaultMailingAddress { get; set; }

        public void PrintData()
        {
            Console.WriteLine("=== Address ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {FirstName ?? "N/A"} {LastName ?? "N/A"}");
            Console.WriteLine($"Line 1: {AddressLine1 ?? "N/A"}");
            Console.WriteLine($"Line 2: {AddressLine2 ?? "N/A"}");
            Console.WriteLine($"Line 3: {AddressLine3 ?? "N/A"}");
            Console.WriteLine($"City: {City ?? "N/A"}");
            Console.WriteLine($"Suburb: {Suburb ?? "N/A"}");
            Console.WriteLine($"County: {County ?? "N/A"}");
            Console.WriteLine($"Postal Code: {PostalCode ?? "N/A"}");
            Console.WriteLine($"Region: {Region ?? "N/A"}");
            Console.WriteLine($"Country: {Country ?? "N/A"}");
            Console.WriteLine($"Phone: {PhoneNumber ?? "N/A"}");
            Console.WriteLine($"Timestamp: {(Timestamp != default ? Timestamp.ToString() : "N/A")}");
            Console.WriteLine($"Default Address: {DefaultAddress}");
            Console.WriteLine($"Default Mailing Address: {DefaultMailingAddress}");
        }
    }
}