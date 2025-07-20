using Newtonsoft.Json;

namespace StampinUpPlaywrightDemo.Models
{
    public class Contact
    {
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("newEmail")]
        public string NewEmail { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("preferredContact")]
        public string PreferredContact { get; set; }

        public void PrintData()
        {
            Console.WriteLine("=== Contact Information ===");
            Console.WriteLine($"Name: {FirstName ?? "N/A"} {LastName ?? "N/A"}");
            Console.WriteLine($"Email: {email ?? "N/A"}");
            Console.WriteLine($"New Email: {NewEmail ?? "N/A"}");
            Console.WriteLine($"Phone: {PhoneNumber ?? "N/A"}");
            Console.WriteLine($"Preferred Contact: {PreferredContact ?? "N/A"}");
            Console.WriteLine($"Birthday: {Birthday ?? "N/A"}");
        }
    }
}