
using StampinUpPlaywrightDemo.Drivers;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Smoke
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/Db3GPP
    /// 
    /// Verify that the Stampin' Up! homepage loads successfully without client-side JavaScript errors, console warnings, or failed network requests. This serves as a gateway check before proceeding with account-related tests.
    /// 
    /// </summary>
    public class NavigateToHomepageAndValidateForErrors : BaseTest
    {
        [Test]
        public async Task CheckHomePAgeForErrors()
        {
            //precondition and init var's
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);


            // User is to Navigate to Stampin Up homepage in Chrome
            await _page.GotoAsync("https://www.stampinup.com");

            // Observe the UI for visual glitches (e.g., broken images, misaligned elements).
            // Homepage renders cleanly. All banners, navigation, and CTA buttons (e.g., Sign In, Shop Now) are visible and responsive.
            _homePage.VerifyOnHomePageAsync();

            // Page loads successfully with no red console errors (`404's`, `500's`, `Type Errors`, UnCaught Exceptions... etc ).
            // Switch to the Network tab and reload the page (Ctrl+R).
            // No failed (red) requests under Network tab. All resources load successfully (200, 204 or 304 status codes).
            TestDriver.ValidateNoSiteErrors();


            //NOTE THESE TESTS CANT BE AUTOMATED UNLESS I  MAKE OpenCV /EMGU Wrapper and due to time constraints we will not be doing that
            // Manually trigger OS/browser dark mode and refresh the site in a new tab.
            // Site detects system preference and adjusts or ignores mode switching. Text and buttons remain legible; no color bleed or overlap.
            // Switch Dark Light Mode a few times
            // Verify site's UI elements visual stability persists and text, Images and buttons remain legible.
        }
    }
}