
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/k5zEl0
    /// 
    /// Verify that the eye icon toggles password visibility.
    /// 
    /// </summary>
    public class TogglePAsswordVisibility : BaseTest
    {
        [Test]
        public async Task TestTogglePAsswordVisibility()
        {
            //precondition and init var's
            var email = "z.timgranger@gmail.com";
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);
            Random random = new();
            string password = $"Abcdefg{random.Next(9999)}";


            // Navigate to Https://stampinup.com
            await _page.GotoAsync("https://www.stampinup.com");

            // Home page loads without issues
            await _homePage.VerifyOnHomePageAsync();


            // From Header bar user locates "Sign In" button and selects it 
            await _headerPage.VerifyHeaderLoadedAsync();
            await _homePage.OpenSignInModalAsync();

            // Enter a password into the password filed (any mix of 4 - 10 characters)
            await ElementSendTextToAsync(_signInModal.PasswordInput, password);


            // input is obfuscated with *****
            var inputType = await _signInModal.PasswordInput.GetAttributeAsync("type");
            if (inputType != "password")
            {
                throw new Exception("Password field is not masked properly.");
            }

            // Identify the Eye icon in the password field (usually indicating to a user as the SHOW PASSWORD button and select that icon)
            await ElementToBeVisibleAsync(_signInModal.PasswordVisibilityToggle);
            // and user selects that icon
            await ElementClickAsync(_signInModal.PasswordVisibilityToggle);

            // Password switches between masked **** and visible.
            await ElementToHaveValueAsync(_signInModal.PasswordInput, password);

        }
    }
}