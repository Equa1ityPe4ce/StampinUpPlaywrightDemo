
using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Drivers
{
    public static class TestDriver
    {
        public static IPlaywright playwright { get; set; }
        public static IBrowser browser { get; set; }
        public static IBrowserContext context { get; set; }
        public static IPage page { get; set; }

        public static List<string> jsErrors = new();
        public static List<string> failedRequests = new();

        public static async Task BuildDriver(BrowserTypeLaunchOptions launchOptions)
        {
            playwright = await Playwright.CreateAsync();

            browser = await playwright.Chromium.LaunchAsync(launchOptions);

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null,
                ScreenSize = new ScreenSize { Width = 1920, Height = 1080 },
                IsMobile = false,
                HasTouch = false,
                DeviceScaleFactor = 1,
            });

            page = await context.NewPageAsync();

            jsErrors = new List<string>();
            failedRequests = new List<string>();

            // Add listeners
            page.Console += (_, msg) =>
            {
                if (msg.Type == "error")
                    jsErrors.Add($"JS Error: {msg.Text}");
            };

            page.RequestFailed += (_, request) =>
            {
                failedRequests.Add($"FAILED: {request.Method} {request.Url}");
            };

            // Hook into test cleanup to dump results at the end
            AppDomain.CurrentDomain.ProcessExit += (_, __) => WriteLog(jsErrors, failedRequests);
            AppDomain.CurrentDomain.DomainUnload += (_, __) => WriteLog(jsErrors, failedRequests);
        }

        private static void WriteLog(List<string> jsErrors, List<string> failedRequests)
        {
            if (!jsErrors.Any() && !failedRequests.Any())
                return;

            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string errorsFolder = Path.Combine(downloadsPath, "WebSiteErrors");

            // Create folder if it doesn't exist
            if (!Directory.Exists(errorsFolder))
                Directory.CreateDirectory(errorsFolder);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFile = Path.Combine(errorsFolder, $"stampinup_errors_{timestamp}.log");

            using var writer = new StreamWriter(outputFile);

            if (jsErrors.Any())
            {
                writer.WriteLine("=== JavaScript Errors ===");
                foreach (var error in jsErrors)
                    writer.WriteLine(error);
            }

            if (failedRequests.Any())
            {
                writer.WriteLine("\n=== Failed Network Requests ===");
                foreach (var request in failedRequests)
                    writer.WriteLine(request);
            }

            writer.WriteLine($"\nLog saved at {outputFile}");
        }

        public static void ValidateNoSiteErrors()
        {
            if (jsErrors.Any() || failedRequests.Any())
            {
                var message = "Homepage validation failed:";

                if (jsErrors.Any())
                {
                    message += $"\nJavaScript Errors ({jsErrors.Count}):";
                    message += string.Join("\n", jsErrors);
                }

                if (failedRequests.Any())
                {
                    message += $"\nFailed Network Requests ({failedRequests.Count}):";
                    message += string.Join("\n", failedRequests);
                }

                throw new Exception(message);
            }
        }
    }
}