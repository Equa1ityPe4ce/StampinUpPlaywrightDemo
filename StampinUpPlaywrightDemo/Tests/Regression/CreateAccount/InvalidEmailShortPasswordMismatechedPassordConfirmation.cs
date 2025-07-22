
using StampinUpPlaywrightDemo.Models;
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.CreateAccount
{
    /// <summary>
    /// Automated test Case covering test case https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/ExzBLb
    /// Verify that the Create Account form correctly detects and displays error messages for:

    //Invalid email format
    //Password shorter than 8 characters
    //Mismatched password confirmation
    /// </summary>
    public class InvalidEmailShortPasswordMismatechedPassordConfirmation : BaseTest
    {
        [Test]
        public async Task TestForMissMatchedPasswordsETC()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            await _homePage.OpenCreateAccount();

            Random random = new();
            int randomInt = random.Next(99999999);
            string firstName = "qw";
            string lastName = "qw";
            string email = $"qw";
            string password = "qw";

            // Fill in the Create Account form with the following values:
            //First Name: qw
            //Last Name: qw
            //Email: qw
            //Password: qw
            //Confirm Password: qw
            await _createAccountModal.FillAllCreateAccountFields(firstName, lastName, email, password);

            // Upon entering QW for email field "Email field must be a valid email" error displayed
            await ElementToBeVisibleAsync(_createAccountModal.EmailInvalidFormatError);

            // upon entering qw in password field error is displayed informing user password entered is too short
            await ElementToBeVisibleAsync(_createAccountModal.PasswordTooShortError);

            // no error is thrown or displayed for comfirmation as the passwords match
            await ElementToBeInvisibleAsync(_createAccountModal.PasswordConfirmationMismatchError);

            //select Create account button
            await ElementClickAsync(_createAccountModal.CreateAccountButton);

            // account is not created and create account modal is still displayed
            await _createAccountModal.VerifyCreateAccountModalAsync();

            // edit Confirmpassword field to not match qw in the password field
            await ElementSendTextToAsync(_createAccountModal.ConfirmPasswordInput, "qwe");
            await ElementClickAsync(_createAccountModal.CreateAccountButton);

            // error is now displayed for confirm password field that the passwords do not match
            await ElementToBeVisibleAsync(_createAccountModal.PasswordConfirmationMismatchError);
        }
    }
}