
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;
using System.Diagnostics;
using System.Reflection;

namespace StampinUpPlaywrightDemo.Tests.Smoke
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/ek6ZLp
    /// 
    /// After user successfully opens website, they are able to sign in and out.
    /// 
    /// </summary>
    public class SimpleSignInAndSignOut : BaseTest
    {
        [Test]
        public async Task SimpleSigninAndOutTest()
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

            // Sign In Modal is Displayed
            //Browser's Focus had locked user to the Modal
            //Email / Password Field's are displayed in modal
            //remember me / forgot password and sign in button are displayed in modal and enabled
            await _signInModal.VerifySignInModalAsync();

            //User populates Email and Password fields with CREDENTIALS "z.timgranger@gmail.com" && "Qwer1234" 
            //Fields are populated displaying to user input was acceptedpassword field is obfuscated with **** to hide user password   
            //user selects the Sign In button 
            await _signInModal.SignInAsync(email, password);

            // after call is complete the modal has closedin header the Sign in button is replaced with a "Hello {usersFirstName}"
            await BaseTest.ElementToBeVisibleAsync(_headerPage.FirstNameButton);

            //when user selects "Hello {usersFirstName}" button
            //Account menu options dropdown is displayed
            //in dropdown should be the following menu options
            //"Account Settings"
            //"addresses"
            //"payment"
            //"My Orders"
            //  "MyLists"
            //"Subscriptions"
            //"Demonstrator"
            //"Rewards"
            //"Signout"
            // When user selects "Sign Out" menu option
            await _headerPage.NavigateToPage(Enums.NavigationEnum.SIGN_OUT);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //A quick load occurs < 2 seconds
            //User shall be logged out
            //Header button "Hello, {usersFirstName}" is replaced back to "Sign In"
            await BaseTest.ElementToBeInvisibleAsync(_headerPage.FirstNameButton);
            await BaseTest.ElementBoolVisible(_headerPage.SignInButton);
            stopwatch.Stop();
            var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Logout completed in {elapsedSeconds} seconds");
            Assert.LessOrEqual(elapsedSeconds, 2, "Logout should complete in under 2 seconds");



        }
    }
}