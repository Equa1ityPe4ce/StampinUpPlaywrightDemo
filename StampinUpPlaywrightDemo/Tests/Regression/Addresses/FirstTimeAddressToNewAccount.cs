
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.Addresses
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/GbnMqQ
    /// test validate the workflow of adding addresses as a first time user on a new account
    ///  NOTE THIS TEST CANT BE COMPLETED DUE TO BUG IDENTIFIED https://drive.google.com/file/d/1kjDwrktYNboBEgIGJmElmDyw5nSgd6nm/view?usp=drive_link
    /// </summary>
    public class FirstTimeAddressToNewAccount : BaseTest
    {

        [Test]
        public async Task TestFirstTimeAddress()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            var _addressPage = new AddressesPage(_page);
            Random random = new();
            int randomInt = random.Next(99999999);
            string firstName = "Tim";
            string lastName = "Granger";
            string email = $"z.timgranger{randomInt}@gmall.co";
            string password = "Qwer1234";


            await _homePage.OpenCreateAccount();

            // from home screen after accoutn creation select account menu options button "Hello {userFirstName}"
            await _createAccountModal.CreateanAccount(firstName, lastName, email, password);

            // and select addresses from the menu options
            await _headerPage.NavigateToPage(Enums.NavigationEnum.SIGN_OUT);
            await ElementToBeInvisibleAsync(_headerPage.FirstNameButton);
            await _homePage.PreconditionOpenWebsiteLoginAndNavigateToPage(email, password, Enums.NavigationEnum.ADDRESSES);



            // Add new address page is displayed with black fields
            // First / Last name
            await ElementToNotHaveTextAsync(_addressPage.FirstNameInput, "Shelby");
            await ElementToNotHaveTextAsync(_addressPage.LastNameInput, "Proctor");

            // address
            // address line 2
            // city
            // state
            // zipcode
            await ElementToNotHaveTextAsync(_addressPage.AddressLine1Input, "21377 Magnolia st");

            // phone number

            // enter first and last name for user or someone else
            await ElementSendTextToAsync(_addressPage.FirstNameInput, "Shelby");
            await ElementSendTextToAsync(_addressPage.LastNameInput, "Proctor");


            // ﻿enter address into the address field
            // ﻿upon user entering address numbers and street names auto address options are suggested
            // select a valid address option from the address suggestion picker
            // City state and Zipcode are auto populated with the correct data from the picker object
            await ElementSendTextToAsync(_addressPage.AddressLine1Input, "21377 Magnolia st");
            await ClickByTextAsync(_page, "21377 Magnolia St, Huntington Beach, CA 92646-6326");

            // enter a phone number (10 digits) into the phone number field
            await ElementSendTextToAsync(_addressPage.PhoneInput, "4356590826");

            // Observe name fields retain the input data
            // phone field successfully populated with data input

            // leaving both default address check box's unchecked 

            // select save address button
            // select Save Address Button
            await ElementClickAsync(_addressPage.SaveButton);

            // Address is saved successfully.
            await ElementToBeInvisibleAsync(_addressPage.SaveButton);

            // form is submitted and is successful Default Shipping address section is populated
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

            // repeat steps for default mailing address using a different address 
            //2239 S 1800 E, Salt Lake City, UT 84106-4128
            await ClickByTextAsync(_page, "Use My Shipping Address");
            // default mailing address is populated with the data that matchies the form that was submitted
            // repeat steps for updating mailing to match shipping
            // user is able to successfully update fields to match

            await ElementClickAsync(_addressPage.MailingEditButton);
            await ElementSendTextToAsync(_addressPage.AddressLine1Input, "2239 S 1800");
            await ClickByTextAsync(_page, "2239 S 1800 E, Salt Lake City, UT 84106-4128");
            // select save address button
            // select Save Address Button
            await ElementClickAsync(_addressPage.SaveButton);

            // Address is saved successfully.
            await ElementToBeInvisibleAsync(_addressPage.SaveButton);
            // form is submitted and is successful Default Shipping address section is populated
            var response2 = await apiHelper.GetAsync("/address", cookieHeader);
            Assert.That(response.Code.Equals(200));

            //proccess the response data
            var addresses2 = JsonConvert.DeserializeObject<List<Address>>(response.Data);
            Console.WriteLine(addresses.First().FirstName);
            Assert.That(addresses2.Last().FirstName.Equals("Shelby"));
            Assert.That(addresses2.Last().LastName.Equals("Proctor"));
            Console.WriteLine(addresses2.Last().AddressLine1);
            Assert.That(addresses2.Last().AddressLine1.Equals("2239 S 1800 E"));
            Assert.That(addresses2.Last().City.Equals("Salt Lake City"));
            Assert.That(addresses2.Last().Region.Equals("UT"));
            Assert.That(addresses2.Last().PostalCode.Equals("84106-4128"));

        }
    }
}