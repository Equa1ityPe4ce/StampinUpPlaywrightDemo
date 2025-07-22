
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/RqLPZ1
    /// testing if user can update their passowrd is an unsuccessful confirmation password
    /// </summary>
    public class PasswordMissMAtchOnUpdate : BaseTest
    {
        [Test]
        public async Task TestPasswordMissMAtchOnUpdate()
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
            await ElementClickAsync(_contactPage.PasswordEditButton);


            // edit password fields are displayed
            await ElementToBeVisibleAsync(_contactPage.CurrentPasswordInput);
            await ElementToBeVisibleAsync(_contactPage.NewPasswordInput);
            await ElementToBeVisibleAsync(_contactPage.ConfirmNewPasswordInput);


            // Enter new password newpassword123 and confirmation newpass456.
            await ElementSendTextToAsync(_contactPage.NewPasswordInput, "newpassword123");
            await ElementSendTextToAsync(_contactPage.ConfirmNewPasswordInput, "newpass456");


            // Click Save.
            await ClickByTextAsync(_page, "Save Changes");

            // as soon as password mismatch is detected in confirmation field red error text is displayed indicating to user there is an issue with updating password
            await ElementToBeVisibleAsync(_contactPage.ConfirmPasswordMismatchError);



            // password is not updated, form is not submitted and password edit fields are not collapsed
            await ElementToBeVisibleAsync(_contactPage.NewPasswordInput);
        }
    }
}