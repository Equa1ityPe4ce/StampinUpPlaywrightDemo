
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.Addresses
{
    /// <summary>
    /// Automated test Case covering test case:https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/j53J4P
    /// Verify user can edit and save a shipping address with valid inputs.
    /// </summary>
    public class EditShippingAddress : BaseTest
    {

        [Test]
        public async Task TestEditShippingAddress()
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
            await _addressPage.VerifyAddressPageAsync();
            await ElementClickAsync(_addressPage.ShippingEditButton);

            // Update all fields (First Name, Last Name, Address, City, State, ZIP, Phone).
            await _addressPage.VerifyAddressIsNowInEditMode();

            await ElementSendTextToAsync(_addressPage.FirstNameInput, "Shelby");
            await ElementSendTextToAsync(_addressPage.LastNameInput, "Proctor");
            await ElementSendTextToAsync(_addressPage.AddressLine1Input, "21377 Magnolia st");
            await ClickByTextAsync(_page, "21377 Magnolia St, Huntington Beach, CA 92646-6326");
            //await ElementSendTextToAsync(_addressPage.CityInput, "Huntington Beach");
            //await ElementSendTextToAsync(_addressPage.StateField, "California");
            //await ElementSendTextToAsync(_addressPage.PostalCodeInput, "92646-6326");

            // Ensure “Make this my default shipping address” is checked.
            await ElementClickAsync(_addressPage.ShippingAddressDefaultCheckbox);
            await ElementClickAsync(_addressPage.ShippingAddressDefaultCheckbox);

            // select Save Address Button
            await ElementClickAsync(_addressPage.SaveButton);

            // Address is saved successfully.
            await ElementToBeInvisibleAsync(_addressPage.SaveButton);

            // User is returned to the address list.
            await ElementToBeVisibleAsync(_addressPage.ShippingEditButton);

            // Updated address appears in the address display objects.
            HttpClient httpClient = new();
            HttpClientHelper apiHelper = new(httpClient);
            var cookieHeader = string.Join("; ", SessionCookies.Select(c => $"{c.Name}={c.Value}"));
            var response = await apiHelper.GetAsync("/address", cookieHeader);
            Assert.That(response.Code.Equals(200));

            //proccess the response data
            var addresses = JsonConvert.DeserializeObject<List<Address>>(response.Data);
            Console.WriteLine(addresses.First().FirstName);
            Assert.That(addresses.First().FirstName.Equals("Shelby"));
            Assert.That(addresses.First().LastName.Equals("Proctor"));
            Assert.That(addresses.First().AddressLine1.Equals("21377 Magnolia St"));
            Assert.That(addresses.First().City.Equals("Huntington Beach"));
            Assert.That(addresses.First().Region.Equals("CA"));
            Assert.That(addresses.First().PostalCode.Equals("92646-6326"));
        }
    }
}