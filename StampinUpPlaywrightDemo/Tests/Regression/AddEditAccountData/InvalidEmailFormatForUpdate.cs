
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/vw4Gpp
    /// verify user cant edit email to an invalid email
    /// </summary>
    public class InvalidEmailFormatForUpdate : BaseTest
    {
        [Test]
        public async Task TestInvalidEmailFormatForUpdate()
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
            await ElementClickAsync(_contactPage.ContactEditButton);
            await _contactPage.VerifyContactFieldsAreInEditMode();

            // Click Edit in Contact section. Enter invalid email like sarah@ or sarah@com.
            await ElementClearAndSendTextToAsync(_contactPage.EmailInput, "sarah@");

            // Form does not submit. Email field shows red highlight and error messag
            await ElementClickAsync(_contactPage.SaveChangesButton);
            await ElementToBeVisibleAsync(_contactPage.InvalidEmailError);
        }
    }
}