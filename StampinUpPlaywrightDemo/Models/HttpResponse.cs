using Newtonsoft.Json.Linq;

namespace StampinUpPlaywrightDemo.Models
{
    public class HttpResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string Data { get; set; }

        public JObject JsonObjectResponseBody { get; private set; }

        public JArray JsonArrayResponseBody { get; private set; }

        public HttpResponse(int code, string message, string data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;

            TryParseData();
        }

        private void TryParseData()
        {
            if (string.IsNullOrWhiteSpace(Data))
            {
                Console.WriteLine("Empty or null data string.");
                return;
            }

            try
            {
                JsonObjectResponseBody = JObject.Parse(Data);
                Console.WriteLine("Parsed as JObject.");
            }
            catch
            {
                try
                {
                    JsonArrayResponseBody = JArray.Parse(Data);
                    Console.WriteLine("Parsed as JArray.");
                }
                catch
                {
                    Console.WriteLine("Failed to parse response as JSON.");
                }
            }
        }

        public bool IsJsonObject => JsonObjectResponseBody != null;
        public bool IsJsonArray => JsonArrayResponseBody != null;
    }
}