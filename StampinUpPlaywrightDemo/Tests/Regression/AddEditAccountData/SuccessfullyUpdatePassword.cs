
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/8rZJE1
    /// Test to update and change password from a logged in state
    /// </summary>
    public class SuccessfullyUpdatePassword : BaseTest
    {
        [Test]
        public async Task TestSuccessfullyUpdatePassword()
        {

            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            var _contactPage = new ContactPage(_page);
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";

            // Navigate to the Edit Shipping Address screen.
            await _homePage.PreconditionOpenWebsiteLoginAndNavigateToPage(email, password, Enums.NavigationEnum.ACCOUNT_SETTINGS);

            // Click Edit on Password.
            // Click Edit next to the Password field.
            await ElementClickAsync(_contactPage.PasswordEditButton);

            // edit password fields expand out and are displayed
            await ElementToBeVisibleAsync(_contactPage.CurrentPasswordInput);
            await ElementToBeVisibleAsync(_contactPage.NewPasswordInput);
            await ElementToBeVisibleAsync(_contactPage.ConfirmNewPasswordInput);

            // select the save changes button
            await ClickByTextAsync(_page, "Save Changes");


            // all fields display error messages
            await ElementToBeVisibleAsync(_contactPage.NewPasswordRequiredError);
            await ElementToBeVisibleAsync(_contactPage.ConfirmPasswordRequiredError);


            // Enter valid current password
            await ElementSendTextToAsync(_contactPage.CurrentPasswordInput, password);


            // new password (min 8 chars) and duplicate entry in confirm password field
            // all fields successfully populate the inputs entered from user
            await ElementSendTextToAsync(_contactPage.NewPasswordInput, password);
            await ElementSendTextToAsync(_contactPage.ConfirmNewPasswordInput, password);

            // select the save changes button
            await ElementClickAsync(_contactPage.SaveChangesButton);

            // no error messages are displayed
            await ElementToBeInvisibleAsync(_contactPage.NewPasswordRequiredError);
            await ElementToBeInvisibleAsync(_contactPage.ConfirmPasswordRequiredError);


            // password update flow completed and password fields are collapsed
            await ElementToBeInvisibleAsync(_contactPage.ConfirmNewPasswordInput);


            //       Account menu options dropdown is displayedin dropdown should be the following menu options
            //       "Account Settings""addresses""payment""My Orders""MyLists""Subscriptions""Demonstrator""Rewards""Signout"
            // When user selects "Sign Out" menu option   
            // A quick load occurs < 2 secondsUser shall be logged outHeader button "Hello, {usersFirstName}" is replaced back to "Sign In"   
            // attempt to sign in using old password
            // user is unable to sign in using old password
            // when user selects "Hello {usersFirstName}" button
            await _headerPage.NavigateToPage(Enums.NavigationEnum.SIGN_OUT);

            // complete sign in flow using valid password
            await ElementToBeVisibleAsync(_headerPage.SignInButton);
            await _homePage.OpenSignInModalAsync();
            await _signInModal.SignInAsync(email, password);

            // user successfully signs in using new password
            await ElementToBeVisibleAsync(_headerPage.FirstNameButton);
        }
    }
}