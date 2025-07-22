
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Automated test Case covering test case
    /// https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/ymwD29
    /// 
    /// Ensure the Sign In modal shows the correct error message when a user submits invalid login credentials.
    /// </summary>
    public class SubmitWithInvalidCredentials : BaseTest
    {
        [Test]
        public async Task TestSubmitWithInvalidCredentials()
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

            // Enter an invalid email (eg QWerTY@@co.com)
            await ElementSendTextToAsync(_signInModal.EmailInput, "QWerTY@@co.com");

            // Enter a 1 character password (eg 1)
            await ElementSendTextToAsync(_signInModal.PasswordInput, "1");

            // select Sign In button
            await ElementClickAsync(_signInModal.SubmitSignIn);

            // Authentication fails and user is not logged in
            await ElementToBeInvisibleAsync(_headerPage.FirstNameButton);

            // A clear red error message appears at the top of the modal:
            // Displaying: “Your email or password was incorrect.”
            await ElementToBeVisibleAsync(_signInModal.InvalidLoginMessage);

            // Review Email and password fields
            // Email and password fields retain entered values
            // Focus may remain in the password field for re-entry
            await ElementToHaveValueAsync(_signInModal.EmailInput, "QWerTY@@co.com");
            var inputType = await _signInModal.PasswordInput.GetAttributeAsync("type");
            if (inputType != "password")
            {
                throw new Exception("Password field is not masked properly.");
            }


        }
    }
}