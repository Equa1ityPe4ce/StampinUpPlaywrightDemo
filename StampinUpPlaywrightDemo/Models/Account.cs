using Newtonsoft.Json;

namespace StampinUpPlaywrightDemo.Models
{
    public class Account
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("culture")]
        public string Culture { get; set; }

        [JsonProperty("defaultCulture")]
        public string DefaultCulture { get; set; }

        [JsonProperty("receiveCommunications")]
        public bool ReceiveCommunications { get; set; }

        [JsonProperty("loyaltyOptIn")]
        public bool LoyaltyOptIn { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("address")]
        public List<Address> Addresses { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }

        [JsonProperty("preferredContact")]
        public string PreferredContact { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("isoBirthday")]
        public string IsoBirthday { get; set; }

        [JsonProperty("isBetaUser")]
        public bool IsBetaUser { get; set; }

        [JsonProperty("isCloseToMyHeartDemo")]
        public bool IsCloseToMyHeartDemo { get; set; }

        public void PrintData()
        {
            Console.WriteLine("=== Account Information ===");
            Console.WriteLine($"Name: {FirstName ?? "N/A"} {LastName ?? "N/A"}");
            Console.WriteLine($"Email: {Email ?? "N/A"}");
            Console.WriteLine($"Phone: {PhoneNumber ?? "N/A"}");
            Console.WriteLine($"Culture: {Culture ?? "N/A"}");
            Console.WriteLine($"Default Culture: {DefaultCulture ?? "N/A"}");
            Console.WriteLine($"Receive Communications: {ReceiveCommunications}");
            Console.WriteLine($"Loyalty Opt-In: {LoyaltyOptIn}");
            Console.WriteLine($"Role: {Role ?? "N/A"}");
            Console.WriteLine($"Preferred Contact: {PreferredContact ?? "N/A"}");
            Console.WriteLine($"Birthday: {Birthday ?? "N/A"}");
            Console.WriteLine($"ISO Birthday: {IsoBirthday ?? "N/A"}");
            Console.WriteLine($"Is Beta User: {IsBetaUser}");
            Console.WriteLine($"Is CTMH Demo: {IsCloseToMyHeartDemo}");
            Console.WriteLine($"Errors: {Errors ?? "None"}");

            if (Addresses != null && Addresses.Any())
            {
                Console.WriteLine("\n-- Addresses --");
                foreach (var addr in Addresses)
                {
                    addr?.PrintData();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("-- No Addresses --");
            }
        }
    }
}