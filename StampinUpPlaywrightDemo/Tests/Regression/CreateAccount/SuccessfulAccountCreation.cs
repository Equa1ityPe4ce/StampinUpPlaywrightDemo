using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.CreateAccount
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/rvrzak
    /// 
    /// Verify that submitting valid details successfully creates an account.
    /// 
    /// </summary>
    public class SuccessfulAccountCreation : BaseTest
    {
        [Test]
        public async Task TestSuccessFulAccountCreation()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            Random random = new();
            int randomInt = random.Next(99999999);
            string firstName = "Tim";
            string lastName = "Granger";
            string email = $"z.timgranger{randomInt}@gmall.co";
            string password = "Qwer1234";

            await _page.GotoAsync("https://www.stampinup.com");
            await _homePage.VerifyOnHomePageAsync();
            await ElementClickAsync(_headerPage.SignInButton);
            await _signInModal.StartCreateAccount();
            await _createAccountModal.VerifyCreateAccountModalAsync();

            // enter valid data into the first & Last name fields
            await ElementSendTextToAsync(_createAccountModal.FirstNameInput, firstName);
            await ElementSendTextToAsync(_createAccountModal.LastNameInput, lastName);


            // input is populated and retained in those fields
            // email input is also persisting in field
            await Expect(_createAccountModal.FirstNameInput).ToHaveValueAsync(firstName);
            await Expect(_createAccountModal.LastNameInput).ToHaveValueAsync(lastName);

            //NOTE NO 2FA NEEDED so a throw AWAY EMAIL IS USED using a throw away email site like"https://temp-mail.org/en/10minutemail" enter the throw away email into the email field
            await ElementSendTextToAsync(_createAccountModal.EmailInput, email);
            await Expect(_createAccountModal.EmailInput).ToHaveValueAsync(email);

            // Enter simple password for password and confirmpassword fields or complicated if you want (eg. Qwer1234)
            // input is entered and retained with an obfuscated mask to cover the password from the ui
            await ElementSendTextToAsync(_createAccountModal.PasswordInput, password);
            await ElementSendTextToAsync(_createAccountModal.ConfirmPasswordInput, password);

            Assert.That(await _createAccountModal.PasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));
            Assert.That(await _createAccountModal.ConfirmPasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));

            // select Create account button
            await ElementClickAsync(_createAccountModal.CreateAccountButton);

            // Account is created. Modal closes.
            // Join Stampin' Rewards modal is displayed => user closes that
            await ElementClickAsync(_createAccountModal.MaybeLaterButton);
            await ElementClickAsync(_createAccountModal.CloseButton);

            //User is logged in.
            //Header displays: “Hello, {userFirstName}”
            await ElementToHaveTextAsync(_headerPage.FirstNameButton,firstName);

            // when user selects "Hello {usersFirstName}" button
            //      Account menu options dropdown is displayedin dropdown should be the following menu options"Account Settings""addresses""payment""My Orders""MyLists""Subscriptions""Demonstrator
            // When user selects "Sign Out" menu option   
            await _headerPage.NavigateToPage(Enums.NavigationEnum.SIGN_OUT);

            // A quick load occurs < 2 secondsUser shall be logged outHeader button "Hello, {usersFirstName}" is replaced back to "Sign In"   
            await ElementToBeInvisibleAsync(_headerPage.FirstNameButton);

            // User then signs in with the new account credentials using the below steps
            // From Header bar user locates "Sign In" button and selects it 
            //       Sign In Modal is Displayed
            // Browser's Focus had locked user to the Modal
            // Email / Password Field's are displayed in modal
            // remember me / forgot password and sign in button are displayed in modal and enabled
            // User populates Email and Password fields with CREDENTIALS generated during create account steps&& "Qwer1234" 
            //     Fields are populated displaying to user input was acceptedpassword field is obfuscated with **** to hide user password   
            // user selects the Sign In button 
            // after call is complete the modal has closedin header the Sign in button is replaced with a "Hello {usersFirstName}"
            await _homePage.PreconditionOpenWebsiteLoginAndNavigateToPage(email,password,Enums.NavigationEnum.ACCOUNT_SETTINGS);
        }
    }
}