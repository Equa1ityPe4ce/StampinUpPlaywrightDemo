
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.CreateAccount
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/ek6Zqn
    /// 
    /// Verify that the user can navigate to the Create Account form from the Sign In modal.
    /// 
    /// 
    /// </summary>
    public class OpenCreateAccountModal : BaseTest
    {
        [Test]
        public async Task CanUserOpenCreateAccountModal()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);

            //Click Sign In icon
            // Sign In modal is displayed and contains Create Account option
            // User selects "Create Account" option
            // Create account Modal is displayed
            await _homePage.OpenCreateAccount();


            // Review Create Account modal
            // Create Account modal appears with 5 fields:
            //  First Name, Last Name, Email, Password, Confirm Password
            // Create Account button is displayed under fields
            // Verify header of the Modal
            await _createAccountModal.VerifyCreateAccountModalAsync();

            // Header: “CREATE ACCOUNT” with subtext:
            // “Welcome! Thank you for creating an account.”
            await ElementToBeVisibleAsync(_createAccountModal.WelcomeMessage);

            // ﻿Close icon (X) in the top-right corner
            await ElementToBeVisibleAsync(_createAccountModal.CloseDialogButton);

            // Verify footer fo the modal
            // Footer text: “Already have an account? Welcome back!” with clickable Sign In link
            await ElementToBeVisibleAsync(_createAccountModal.AlreadyHaveAccountMessage);

            // User selects the Sign In option in the footer
            await ElementClickAsync(_createAccountModal.BottomSignInButton);

            // user is navigated back to the sign in modal
            await _signInModal.VerifySignInModalAsync();

            // user reselects the create account option then selects the X from the create account modal
            await ElementClickAsync(_signInModal.CreateAccountButton);
            await ElementClickAsync(_createAccountModal.CloseDialogButton);

            //use is back on home page focused in a logged out state
            await ElementToBeVisibleAsync(_headerPage.SignInButton);
        }
    }
}