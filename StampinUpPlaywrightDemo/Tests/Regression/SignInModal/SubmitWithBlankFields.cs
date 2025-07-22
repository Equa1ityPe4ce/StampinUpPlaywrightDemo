
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/ExzNjB
    /// 
    /// Verify that validation messages appear when required fields are empty.
    /// 
    /// </summary>
    public class SubmitWithBlankFields : BaseTest
    {
        [Test]
        public async Task Test()
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

            // Leave both Email or ID and Password fields blank. Click Sign In.
            await ElementClickAsync(_signInModal.SubmitSignIn);

            // Red borders appear on both fields.Error messages display:
            // “The Email Address field is required.”
            // “The Password field is required.
            await ElementToBeVisibleAsync(_signInModal.EmailRequiredError);
            await ElementToBeVisibleAsync(_signInModal.PasswordRequiredError);

        }
    }
}