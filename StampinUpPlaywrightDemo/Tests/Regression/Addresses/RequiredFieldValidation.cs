
using StampinUpPlaywrightDemo.Pages;

namespace StampinUpPlaywrightDemo.Tests.Regression.Addresses
{
    /// <summary>
    /// Automated test Case covering test case : https://projectdiablo2web.qatouch.com/case/shareable/p/38Qe/cid/pnEXM2
    /// Verify that leaving required fields blank shows validation errors.
    /// THIS TEST IS ALSO BLOCKED BY KNOWN BUG https://drive.google.com/file/d/1kjDwrktYNboBEgIGJmElmDyw5nSgd6nm/view?usp=drive_link
    /// </summary>
    public class RequiredFieldValidation : BaseTest
    {

        [Test]
        public async Task TestRequiredFieldsValidation()
        {
            //precondition
            var _homePage = new HomePage(_page);
            var _headerPage = new HeaderPage(_page);
            var _signInModal = new SignInModalPage(_page);
            var _createAccountModal = new CreateAccountModalPage(_page);
            await _homePage.OpenCreateAccount();
            // Clear required fields like First Name, Address, City, ZIP, Phone.


            // for fields other than zip code use is able to enter dummy data


            // Zip 

            //when user strays too far from entering dummy data for addresses at some point its validating the
            //address / city / zip are correct and user is unable to submit form.
            //It's hard to properly test this feature without the requirements on how this is expected to behave

        }
    }
}