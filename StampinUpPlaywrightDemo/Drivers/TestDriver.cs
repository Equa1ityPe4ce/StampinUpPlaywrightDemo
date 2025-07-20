
using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Drivers
{
    public static class TestDriver
    {
        public static IPlaywright playwright { get; set; }
        public static IBrowser browser { get; set; }
        public static IBrowserContext context { get; set; }
        public static IPage page { get; set; }

        public static async Task BuildDriver(BrowserTypeLaunchOptions launchOptions)
        {
            playwright = await Playwright.CreateAsync();

            browser = await playwright.Chromium.LaunchAsync(launchOptions);

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null, // Let it use the full window size
                ScreenSize = new ScreenSize { Width = 1920, Height = 1080 },
                IsMobile = false,
                HasTouch = false,
                DeviceScaleFactor = 1,
                //UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/122.0.0.0 Safari/537.36"
            });

            page = await context.NewPageAsync();
        }
    }
}