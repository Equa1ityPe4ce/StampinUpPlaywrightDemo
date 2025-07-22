
using Microsoft.Playwright;
using Newtonsoft.Json;
using StampinUpPlaywrightDemo.Drivers;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests
{
    public class HybridPOCTest : BaseTest
    {

        [Test]
        public async Task UIAuthThenGetAddressesViaApi()
        {
            //Precondition
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";
            await _page.GotoAsync("https://www.stampinup.com");
            var homePage = new HomePage(_page);
            var signInModal = new SignInModalPage(_page);
            var headerPage = new HeaderPage(_page);

            //Verify home and header pages load
            await homePage.VerifyOnHomePageAsync();
            await headerPage.VerifyHeaderLoadedAsync();

            // Open sign-in modal
            await homePage.OpenSignInModalAsync();
            await signInModal.VerifySignInModalAsync();

            //Log user in
            await signInModal.SignInAsync(email, password);

            //Extract cookies from netwrok response of header
            var cookieHeader = string.Join("; ", SessionCookies.Select(c => $"{c.Name}={c.Value}"));

            //prep the api client
            HttpClient httpClient = new();
            HttpClientHelper apiHelper = new(httpClient);

            //GET the users Address
            var response = await apiHelper.GetAsync("/address", cookieHeader);
            Assert.That(response.Code.Equals(200));

            //proccess the response data
            var addresses = JsonConvert.DeserializeObject<List<Address>>(response.Data);
            Console.WriteLine($"response: {addresses}");

            //gather last 4 digits of users phone number
            var originalPhone = addresses.FirstOrDefault()?.PhoneNumber ?? "";
            var originalLast4 = originalPhone.Length >= 4 ? originalPhone[^4..] : "";

            //get a new random 4 digits and make sure it doesnt randomly land on the same 4 digits
            string newLast4;
            do
            {
                newLast4 = new Random().Next(1000, 9999).ToString();
            } while (newLast4 == originalLast4);


            // swap phone numbers and prep payload
            var newPhone = (originalPhone.Length > 4)
                ? originalPhone.Substring(0, originalPhone.Length - 4) + newLast4
                : newLast4;
            addresses.First().PhoneNumber = newPhone;

            //update users phone number
            var putResponse = await apiHelper.PutAsync("/address", addresses.First(), cookieHeader);

            // verify update request was successful
            Assert.AreEqual(200, putResponse.Code);

            //pull new user address data
            var verifyResponse = await apiHelper.GetAsync("/address", cookieHeader);
            var updatedAddresses = JsonConvert.DeserializeObject<List<Address>>(verifyResponse.Data);

            //verify generated phone number matches what the database has for our users address
            Assert.AreEqual(newPhone, updatedAddresses.First().PhoneNumber);

            //print the old and new numbers to visually see it working
            Console.WriteLine($"phone number changes via api from {originalPhone} to {newPhone}");

            //after logging in we will navigate to the address page
            await headerPage.navigateToAddressesPage();

            //we want to verify in the ui that the api and the ui are show the correct data for our users phone number
            AddressesPage addressPage = new(_page);
            await addressPage.VerifyAddressPageAsync();
            await addressPage.VerifyAddressPhoneNumber(newPhone);
        }
    }
}