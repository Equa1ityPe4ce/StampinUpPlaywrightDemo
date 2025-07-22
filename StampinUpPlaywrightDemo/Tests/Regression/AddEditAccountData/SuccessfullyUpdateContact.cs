
using StampinUpPlaywrightDemo.Enums;
using StampinUpPlaywrightDemo.Helpers;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.AddEditAccountData
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/9wRPzd
    /// test to verify user is able to successfuly update contact info
    /// </summary>
    public class SuccessfullyUpdateContact : BaseTest
    {
        [Test]
        public async Task TestSuccessfullyUpdateContact()
        {

            //Precondition
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);


            //Openwebsite and begin login
            await _page.GotoAsync("https://www.stampinup.com");
            await _homePage.OpenSignInModalAsync();

            //completelogin
            await _signInModal.SignInAsync(email, password);
            await BaseTest.ElementToBeVisibleAsync(_headerPage.FirstNameButton);

            //do the api stuff to make sure we have fields to update rather than verify we are updating fields to the same thing.
            var cookieHeader = string.Join("; ", BaseTest.SessionCookies.Select(c => $"{c.Name}={c.Value}"));
            HttpClient httpClient = new();
            var apiHelper = new HttpClientHelper(httpClient);
            await apiHelper.SanitizeContactInfoAsync(cookieHeader);
            await _page.ReloadAsync();

            //navigatetodesired page
            await _headerPage.NavigateToPage(NavigationEnum.ACCOUNT_SETTINGS);

            await _page.WaitForTimeoutAsync(3000);

            // Click Edit next to the Contact section.
            ContactPage _contactPAge = new(_page);
            await ElementClickAsync(_contactPAge.ContactEditButton);
            await _contactPAge.VerifyContactFieldsAreInEditMode();

            //Enter new valid values for: First Name: Sarah Last Name: Doe Phone Number: 8015557777
            await _contactPAge.EditContactFieldAsync(ContactField.FIRST_NAME, "Tim");
            await _contactPAge.EditContactFieldAsync(ContactField.LAST_NAME, "Granger");
            await _contactPAge.EditContactFieldAsync(ContactField.EMAIL, "z.timgranger@gmail.com");


            // Select Preferred method of contact picker and choose each option and select save
            // contact method is saved each time
            await _contactPAge.EditContactFieldAsync(ContactField.PREFERRED_CONTACT_METHOD, "Email");
            await _contactPAge.EditContactFieldAsync(ContactField.PREFERRED_CONTACT_METHOD, "Text Message");
            await _contactPAge.EditContactFieldAsync(ContactField.PREFERRED_CONTACT_METHOD, "Phone Call");

            // All values are saved and displayed correctly. Success message is shown (if applicable).

            //user selects the birthday picker and walks through the birthday picker options for year then month, then day
            // birthday is formated m/dd/yyyy   or mm/dd/yyyy if there are two digits in month
            await _contactPAge.EditContactFieldAsync(ContactField.BIRTHDAY, "7/31/1984");

            // select "Save Changes" button
            await ElementClickAsync(_contactPAge.SaveChangesButton);


            // all field updates are saved
            await ElementToHaveTextAsync(_contactPAge.GetContactFieldValueSpan("Email"), "z.timgranger@gmail.com");
            await ElementToHaveTextAsync(_contactPAge.GetContactFieldValueSpan("First Name"), "Tim");
            await ElementToHaveTextAsync(_contactPAge.GetContactFieldValueSpan("Last Name"), "Granger");

            //when user selects "Hello {usersFirstName}" button
            // Account menu options dropdown is displayedin dropdown should be the following menu options"Account Settings""addresses""payment""My Orders""MyLists""Subscriptions""Demonstrator""Rewards""Signout"
            // When user selects "Sign Out" menu option   
            // A quick load occurs < 2 secondsUser shall be logged outHeader button "Hello, {usersFirstName}" is replaced back to "Sign In"   
            await _headerPage.NavigateToPage(NavigationEnum.SIGN_OUT);


            // sign back into account using the credentials for this user using these steps provided below
            await _homePage.OpenSignInModalAsync();
            await _signInModal.SignInAsync(email, password);


            // From Header bar user locates "Sign In" button and selects it 
            await ElementToHaveTextAsync(_headerPage.FirstNameButton, "Tim");
        }
    }
}