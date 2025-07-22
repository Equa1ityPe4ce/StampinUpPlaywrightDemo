
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// : https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/bkVevQ
    /// test editing phone number to invalid phone number
    /// </summary>
    public class PhoneNumberWithAlphaChars : BaseTest
    {
        [Test]
        public async Task TestPhoneNumberWithAlphaChars()
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

            // Edit Phone Number field to 123-abc-4567.
            await ElementSendTextToAsync(_contactPage.PhoneInput, "123-abc-4567");

            // Select Save button.
            await ElementClickAsync(_contactPage.SaveChangesButton);

            // Phone number field is marked invalid. Error message shown.
            await ElementToBeVisibleAsync(_contactPage.PhoneNumberLengthError);
        }
    }
}