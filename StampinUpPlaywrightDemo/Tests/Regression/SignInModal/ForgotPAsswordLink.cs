
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Covering this manual test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/1rBvX0
    ///  Ensure "Forgot Password" link opens reset flow or page. 
    /// </summary>
    public class ForgotPAsswordLink : BaseTest
    {
        [Test]
        public async Task TestForgotPAsswordLink()
        {
            //precondition and init var's
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);


            // Navigate to Https://stampinup.com
            await _page.GotoAsync("https://www.stampinup.com");

            // Home page loads without issues
            await _homePage.VerifyOnHomePageAsync();


            // From Header bar user locates "Sign In" button and selects it 
            await _headerPage.VerifyHeaderLoadedAsync();
            await _homePage.OpenSignInModalAsync();

            // Click Forgot Password.
            await ClickByTextAsync(_page, "Forgot Password");


            // User is redirected to password recovery screen or modal appears.
            await ElementToBeInvisibleAsync(_signInModal.PasswordInput);

        }
    }
}