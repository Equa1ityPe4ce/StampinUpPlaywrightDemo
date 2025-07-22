
using StampinUpPlaywrightDemo.Drivers;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/2v8bMN
    /// 
    /// nsure that when the "Remember Me" checkbox is not selected, the site does not retain login credentials after session ends.
    /// 
    /// </summary>
    public class RemeberMeNotChecked : BaseTest
    {
        [Test]
        public async Task TestRemeberMeNotChecked()
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
            // Navigate to the Sign In screen.
            // singin modal is displayed
            await _homePage.OpenSignInModalAsync();

            // Enter a valid email and password.
            // email and password populate in the appropriate fields
            await ElementSendTextToAsync(_signInModal.EmailInput, email);
            await ElementSendTextToAsync(_signInModal.PasswordInput, password);

            // Leave the “Remember me” checkbox unchecked.



            // Click SIGN IN.
            await ElementClickAsync(_signInModal.SubmitSignIn);

            // After successful login, perform some basic actions
            await _headerPage.NavigateToPage(Enums.NavigationEnum.ACCOUNT_SETTINGS);

            // Sign out and close the browser completely.
            await _headerPage.NavigateToPage(Enums.NavigationEnum.SIGN_OUT);

            await _homePage.VerifyOnHomePageAsync();

            // Email and password fields should be empty.
            await _homePage.OpenSignInModalAsync();

            // No stored account info should appear pre-filled.
            await ElementToNotHaveTextAsync(_signInModal.EmailInput, email);
            await ElementToNotHaveTextAsync(_signInModal.PasswordInput, password);

            // User must enter credentials again to log in.
            await ElementSendTextToAsync(_signInModal.EmailInput, email);
            await ElementSendTextToAsync(_signInModal.PasswordInput, password);
            await ElementClickAsync(_signInModal.SubmitSignIn);
            await ElementToBeVisibleAsync(_headerPage.FirstNameButton);
        }
    }
}