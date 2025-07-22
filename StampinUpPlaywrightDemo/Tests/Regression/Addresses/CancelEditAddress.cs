
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.Addresses
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/4BDRwa
    /// Verify the Cancel button discards changes.
    /// </summary>
    public class CancelEditAddress : BaseTest
    {

        [Test]
        public async Task TestCancelEditAddress()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            var _addressPage = new AddressesPage(_page);
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";

            // Navigate to the Edit Shipping Address screen.
            await _homePage.PreconditionOpenWebsiteLoginAndNavigateToPage(email, password, Enums.NavigationEnum.ADDRESSES);



            // Change one or more fields.
            await ElementClickAsync(_addressPage.ShippingEditButton);
            await ElementSendTextToAsync(_addressPage.FirstNameInput, "Leon");

            // Select or opt to cancel changes
            await ElementClickAsync(_addressPage.CancelButton);

            // Changes are not saved.
            //prep the api client
            HttpClient httpClient = new();
            HttpClientHelper apiHelper = new(httpClient);

            //GET the users Address
            //Extract cookies from netwrok response of header
            var cookieHeader = string.Join("; ", SessionCookies.Select(c => $"{c.Name}={c.Value}"));
            var response = await apiHelper.GetAsync("/address", cookieHeader);
            Assert.That(response.Code.Equals(200));

            //proccess the response data
            var addresses = JsonConvert.DeserializeObject<List<Address>>(response.Data);
            Console.WriteLine(addresses.First().FirstName);
            Assert.That(!addresses.First().FirstName.Equals("Leon"));

        }
    }
}