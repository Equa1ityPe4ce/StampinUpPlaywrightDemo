
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.CreateAccount
{
    /// <summary>
    /// Automated test Case covering test case: https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/xDL2Vm
    /// 
    /// Verify that required field validation appears when submitting an empty form.
    /// 
    /// </summary>
    public class SubmitWithBlankFields : BaseTest
    {
        [Test]
        public async Task SubmitCreateAccountWithBlankFields()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            
            await _homePage.OpenCreateAccount();

            // Leave all fields empty → Click Create Account button
            await ElementClickAsync(_createAccountModal.CreateAccountButton);

            // Each field highlights in red with validation messages:
            await _createAccountModal.VerifyAllCreateAccountErrorMessagesAreDisplayed();        }
    }
}