
using Microsoft.Playwright;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.SignInModal
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/a0xL94
    /// //TEST CAN NOT PROCEED DUE TO REMEBER ME APPEARING TO NOT REALLY WORK AS EXPECTED: https://projectdiablo2web.qatouch.com/v2/defects/view/p/38Qe/did/eJdLr
    /// Confirm that selecting “Remember me” persists session.
    /// 
    /// </summary>
    public class UseRemeberMe : BaseTest
    {
        [Test]
        public async Task TestUseRemeberMe()
        {
            //precondition and init var's
            var email = "z.timgranger@gmail.com";
            var password = "Qwer1234";
            var _homePage = new HomePage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _headerPage = new HeaderPage(_page);


            // Navigate to Https://stampinup.com
            await _page.GotoAsync("https://www.stampinup.com");

            // Home page loads without issues
            await _homePage.VerifyOnHomePageAsync();


            // From Header bar user locates "Sign In" button and selects it 
            await _headerPage.VerifyHeaderLoadedAsync();
            await _homePage.OpenSignInModalAsync();

            // in email and password fields enter valid user credentials (eg z.timgranger@gmail.com && Qwer1234)
            // input fields are populated and password field is masked
            await ElementSendTextToAsync(_signInModal.EmailInput, email);
            await ElementSendTextToAsync(_signInModal.PasswordInput, password);

            // Identify the remember me checkbox
            // select the remember me checkbox element
            // remember box is marked as checked
            await ElementClickAsync(_signInModal.RemeberMe);

            // select the Sign in button	
            await ElementClickAsync(_signInModal.SubmitSignIn);


            // user is now signed in and singin button in the header is replaced with "Hello, {userFirstName}"
            await ElementToBeVisibleAsync(_headerPage.FirstNameButton);


            // === Step 7: Close browser and reopen with persistent session ===
            Directory.CreateDirectory("storage");
            await _page.Context.StorageStateAsync(new() { Path = "storage/rememberme.json" }); // optional: save for inspection/logging
            await _page.Context.CloseAsync();

            // Use a persistent user data directory
            var userDataDir = "storage/user_data";
            Directory.CreateDirectory(userDataDir);

            // Create new context using persistent user profile
            var newContext = await _playwright.Chromium.LaunchPersistentContextAsync(userDataDir, new BrowserTypeLaunchPersistentContextOptions
            {
                Headless = false
            });

            var newPage = newContext.Pages.First();
            var _headerPageNew = new HeaderPage(newPage);

            // Navigate and verify session persists
            await newPage.GotoAsync("https://www.stampinup.com");
            await ElementToBeVisibleAsync(_headerPageNew.FirstNameButton);

            //TEST CAN NOT PROCEED DUE TO REMEBER ME APPEARING TO NOT REALLY WORK AS EXPECTED: https://projectdiablo2web.qatouch.com/v2/defects/view/p/38Qe/did/eJdLr

            // when user selects "Hello {usersFirstName}" button

            //Account menu options dropdown is displayedin dropdown should be the following menu options
            //"Account Settings""addresses""payment""My Orders""MyLists""Subscriptions""Demonstrator""Rewards""Signout"

            // When user selects "Sign Out" menu option   

            // A quick load occurs < 2 secondsUser shall be logged outHeader button "Hello, {usersFirstName}" is replaced back to "Sign In"

            // Close and reopen browser.

            // User session does not persists and header does not have "Hello, {userFirstName}" in the header

            // Select the "Sign In" button

            // Sign in modal is displayed

            // Review state of "Remeber Me" checkbox	

            // verify checkbox is not checked
        }
    }
}