
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/Z1DmLp
    /// test is created to see if a user and remove critical data from account details like email / name fields
    /// </summary>
    public class BlankRequiredFields : BaseTest
    {
        [Test]
        public async Task TestBlankRequiredFields()
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

            // Clear First Name and Email fields.
            await ElementClearAndSendTextToAsync(_contactPage.FirstNameInput, "");
            await ElementClearAndSendTextToAsync(_contactPage.EmailInput, "");

            // select Save button.
            await ElementClickAsync(_contactPage.SaveChangesButton);


            // as soon as email and name fields are removed red error text is displayed informing user those are required fields
            await ElementToBeVisibleAsync(_contactPage.FirstNameRequiredError);
            await ElementToBeVisibleAsync(_contactPage.EmailRequiredError);

            HttpClient httpClient = new();
            HttpClientHelper apiHelper = new(httpClient);
            var cookieHeader = string.Join("; ", SessionCookies.Select(c => $"{c.Name}={c.Value}"));
            var response = await apiHelper.GetAsync("/account", cookieHeader);
            Assert.That(response.Code.Equals(200));

            //proccess the response data
            var contactInfo = JsonConvert.DeserializeObject<Account>(response.Data);
            contactInfo.PrintData();
            // as soon as save button is selected form is not submitted and users details are not updated
            Assert.That(!contactInfo.FirstName.Equals(""));
            Assert.That(!contactInfo.Email.Equals(""));

        }
    }
}