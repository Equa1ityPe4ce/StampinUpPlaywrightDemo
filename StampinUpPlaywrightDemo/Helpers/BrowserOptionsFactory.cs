using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Helpers
{
    public class BrowserOptionsFactory
    {
        public static BrowserTypeLaunchOptions GetStandardTestRun()
        {
            return new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 250,
                Args = new[] { "--start-maximized" }
            };
        }

        public static BrowserTypeLaunchOptions GetTestRunWithNetwork()
        {
            return new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 250,
                Args = new[] {
                    "--start-maximized",
                    "--auto-open-devtools-for-tabs"}
            };
        }

        public static BrowserTypeLaunchOptions GetSpeedyTestRun()
        {
            return new BrowserTypeLaunchOptions
            {
                Headless = true,
                SlowMo = 0,
                Args = new[] { "--start-maximized" }
            };
        }

    }
}