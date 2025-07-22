
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using NUnit.Framework.Interfaces;
using StampinUpPlaywrightDemo.Helpers;
using System.Diagnostics;
using StampinUpPlaywrightDemo.Drivers;

namespace StampinUpPlaywrightDemo
{
    [TestFixture]
    public abstract class BaseTest : Constants
    {
        BrowserOptionsFactory browserOptionsFactory = new();
        public static IReadOnlyList<Microsoft.Playwright.BrowserContextCookiesResult> SessionCookies { get; set; }
        protected IPlaywright _playwright => TestDriver.playwright;
        protected IBrowser _browser => TestDriver.browser;
        protected IBrowserContext _context => TestDriver.context;
        protected IPage _page => TestDriver.page;

        /// <summary>
        /// Global setup for a test class
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.WriteLine("\n_________====== DEBUG Test started ======_________\n");

            Console.WriteLine("☼☼ Test Started!");

        }

        /// <summary>
        /// Global teardown after a test class
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            String className = TestContext.CurrentContext.Test.ClassName;
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            if (result == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                //TestTracker.FailedTestClasses.Add(className);
            }
            else if (result == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                //TestTracker.PassedTestClasses.Add(className);
            }
            Console.WriteLine("☼☼ Test Completed!");
            Debug.WriteLine("\n_________====== DEBUG Test Completed ======_________\n");
        }

        /// <summary>
        /// Global SetUp before test method
        /// </summary>
        [SetUp]
        public async Task TestMethodSetup()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            string className = TestContext.CurrentContext.Test.ClassName;
            Console.WriteLine($"\n< * >< * >< * > Starting test method: {testName}  in {className}");

            //Build driver
            await TestDriver.BuildDriver(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50,
                Args = new[] { "--start-maximized" } // Makes sure window isn't a small tablet
            });
            await TestDriver.page.SetViewportSizeAsync(1920, 1080);
        }

        /// <summary>
        /// Global TearDown after test method
        /// </summary>
        [TearDown]
        public async Task TestMethodTearDown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                Console.WriteLine("test failed, taking screenshot");
                await TestDriver.page.ScreenshotAsync(GetScreenshotOptions(testName));
                Console.WriteLine("screenshot saved");
            }
            else if (status == TestStatus.Passed)
            {
                Console.WriteLine($"Test Result Passed for {testName}");
            }
            else
            {
                Console.WriteLine("Test Result unknown");
            }

            Console.WriteLine($"\n Ending test method: {testName} < * >< * >< * >\n");

            // Clean up Playwright browser instance
            await TestDriver.page.CloseAsync();
            await TestDriver.context.CloseAsync();
            await TestDriver.browser.CloseAsync();
        }

        public static PageScreenshotOptions GetScreenshotOptions(String testName)
        {
            PageScreenshotOptions options = new PageScreenshotOptions { Path = $"Screenshots/{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png" };
            return options;
        }

        public void PrintSummary()
        {
            Console.WriteLine("\n--- Test Class Summary ---");
            //Console.WriteLine($"Passed Tests: {classPassList}");
            // Console.WriteLine($"Failed Tests: {classFailList}");
        }
        // ################################################
        //         Actions Helpers
        // ################################################
        // Clicks an element after ensuring it's visible and attached
        public static async Task ElementClickAsync(ILocator locator, string elementName = "", int timeoutMs = 5000, int pollIntervalMs = 500)
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddMilliseconds(timeoutMs);
            Exception lastException = null;

            Console.WriteLine($"Clicking element: {locator}");

            while (DateTime.UtcNow < endTime)
            {
                try
                {
                    if (await locator.IsVisibleAsync() && await locator.IsEnabledAsync())
                    {
                        await locator.ClickAsync();
                        Console.WriteLine($"Clicked element: {locator}");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    // retry
                }

                await Task.Delay(pollIntervalMs);
            }

            Console.WriteLine($"Failed to click element: {locator} after {timeoutMs}ms");
            throw new TimeoutException($"Element click failed for: {locator}", lastException);
        }

        // ElementIsCheckedAsync
        public static async Task ElementIsCheckedAsync(ILocator locator, bool checkedTrueOrFalse)
        {
            Console.WriteLine($"\nLooking for element to be checked {checkedTrueOrFalse}: {locator}");
            if (checkedTrueOrFalse)
            {
                await Expect(locator).ToBeCheckedAsync();
            }
            else
            {
                await Expect(locator).Not.ToBeCheckedAsync();
            }


            Console.WriteLine($"Element was confirmed checked {checkedTrueOrFalse}\n");
        }

        //click by text
        public static async Task ClickByTextAsync(IPage page, string text, int timeoutMs = 5000)
        {
            Console.WriteLine($"Looking for element with exact text: '{text}'");

            var locator = page.Locator($"text=\"{text}\"");

            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeoutMs
                });

                await locator.ClickAsync();
                Console.WriteLine($"Clicked element with text: '{text}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to click element with text: '{text}'");
                throw;
            }
        }

        // Sends text to an input element
        public static async Task ElementSendTextToAsync(ILocator locator, string text, string elementName = "")
        {
            try
            {
                Console.WriteLine($"Sending text: {text} to element: {locator}");

                await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await locator.FillAsync(text);

                Console.WriteLine($"Text sent to: {locator}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send text to element: {locator}");
                throw;
            }
        }

        public static async Task ElementClearAndSendTextToAsync(ILocator locator, string text, string elementName = "")
        {
            try
            {
                Console.WriteLine($"Clearing and sending text to: {locator}");

                await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await locator.ClearAsync();
                await locator.FillAsync(text);

                Console.WriteLine($"Text cleared and sent to: {locator}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clear/send text to: {locator}");
                throw;
            }
        }

        // ################################################
        //         Assertion Helpers
        // ################################################

        //ElementBool
        public static async Task<bool> ElementBoolVisible(ILocator locator)
        {
            Console.WriteLine($"Boolean return looking for element {locator}");

            bool isVisible = await locator.IsVisibleAsync();

            Console.WriteLine($"Element bool was: {isVisible}");
            return isVisible;
        }

        // ToBeCheckedAsync
        public static async Task ElementToBeCheckedAsync(ILocator locator)
        {
            Console.WriteLine($"\nLooking for element to be checked: {locator}");

            await Expect(locator).ToBeCheckedAsync();

            Console.WriteLine($"Element was found \n");
        }

        // ToBeEnabledAsync
        public static async Task ElementToBeEnabledAsync(ILocator locator)
        {
            Console.WriteLine($"\nLooking for element to be enabled: {locator}");

            await Expect(locator).ToBeEnabledAsync();

            Console.WriteLine($"Element was found \n");
        }


        // ToBeVisibleAsync
        public static async Task ElementToBeVisibleAsync(ILocator locator, int timeoutMs = 5000)
        {
            try
            {
                Console.WriteLine($"Waiting for: {locator}");

                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeoutMs
                });

                Console.WriteLine($"Element is now visible: {locator}");
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"Timeout: Element {locator} was not visible after {timeoutMs}ms.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        // ToContainTextAsync
        public static async Task ElementToContainTextAsync(ILocator locator, string textToAssert)
        {
            Console.WriteLine($"\nLooking for element: {locator} to contain text: {textToAssert}");

            await Expect(locator).ToContainTextAsync(textToAssert);

            Console.WriteLine($"Element was found with text \n");
        }

        // ToHaveTextAsync
        public static async Task ElementToHaveTextAsync(ILocator locator, string textToAssert)
        {
            Console.WriteLine($"\nLooking for element: {locator} to have text: {textToAssert}");

            await Expect(locator).ToHaveTextAsync(textToAssert);

            Console.WriteLine($"Element was found with text \n");
        }

        // ToHaveValueAsync
        public static async Task ElementToHaveValueAsync(ILocator locator, string expectedValue)
        {
            Console.WriteLine($"\n Looking for element: {locator} to have value: {expectedValue}");

            await Expect(locator).ToHaveValueAsync(expectedValue);

            Console.WriteLine($"Element had expected value: {expectedValue}\n");
        }

        // ToNotHaveValueAsync
        public static async Task ElementToNotHaveValueAsync(ILocator locator, string unexpectedValue)
        {
            Console.WriteLine($"\n Looking for element: {locator} to NOT have value: {unexpectedValue}");

            await Expect(locator).Not.ToHaveValueAsync(unexpectedValue);

            Console.WriteLine($"Element did NOT have value: {unexpectedValue}\n");
        }

        // ToBeUnChecked
        public static async Task ElementToBeUncheckedAsync(ILocator locator)
        {
            Console.WriteLine($"\nLooking for element to be unchecked: {locator}");

            await Expect(locator).Not.ToBeCheckedAsync();

            Console.WriteLine($"Element was confirmed unchecked\n");
        }

        // ToBeDisabled
        public static async Task ElementToBeDisabledAsync(ILocator locator)
        {
            Console.WriteLine($"\nLooking for element to be disabled: {locator}");

            await Expect(locator).ToBeDisabledAsync();

            Console.WriteLine($"Element was disabled\n");
        }

        // ToBeInvisible
        public static async Task ElementToBeInvisibleAsync(ILocator locator)
        {
            Console.WriteLine($"\nLooking for element to be invisible: {locator}");

            await Expect(locator).ToBeHiddenAsync(); // "Hidden" means not visible

            Console.WriteLine($"Element was confirmed invisible\n");
        }

        // ToNotHaveText
        public static async Task ElementToNotHaveTextAsync(ILocator locator, string unexpectedText)
        {
            Console.WriteLine($"\nVerifying element: {locator} does NOT have text: {unexpectedText}");

            await Expect(locator).Not.ToHaveTextAsync(unexpectedText);

            Console.WriteLine($"Confirmed text is not present\n");
        }

        // ToNotContainText
        public static async Task ElementToNotContainTextAsync(ILocator locator, string unexpectedText)
        {
            Console.WriteLine($"\nVerifying element: {locator} does NOT contain text: {unexpectedText}");

            await Expect(locator).Not.ToContainTextAsync(unexpectedText);

            Console.WriteLine($"Confirmed text is not present in element\n");
        }
    }
}