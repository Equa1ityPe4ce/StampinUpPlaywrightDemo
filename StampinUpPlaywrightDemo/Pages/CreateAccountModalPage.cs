
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace StampinUpPlaywrightDemo.Pages
{
    public class CreateAccountModalPage : BaseTest
    {
        private readonly IPage _page;

        public CreateAccountModalPage(IPage page)
        {
            _page = page;
        }

        // === FORM CONTAINER ELEMENTS ===
        public ILocator AuthenticationModal => _page.Locator("[data-testid='authentication']");
        public ILocator RegistrationModal => _page.Locator("[data-testid='registration']");
        public ILocator Form => _page.Locator("[data-testid='form-reg']");
        public ILocator CloseDialogButton => _page.Locator("[data-testid='close-dialog']");
        public ILocator ConfirmDialog => _page.Locator("[data-testid='confirm-dialog']");

        // === INPUT FIELDS ===
        public ILocator FirstNameInput => _page.Locator("[data-testid='reg-first-name']");
        public ILocator LastNameInput => _page.Locator("[data-testid='reg-last-name']");
        public ILocator EmailInput => _page.Locator("[data-testid='reg-email']");
        public ILocator PasswordInput => _page.Locator("input[data-testid='reg-password']");
        public ILocator ConfirmPasswordInput => _page.Locator("input[data-testid='reg-password-confirmation']");

        // === BUTTONS ===
        public ILocator CreateAccountButton => _page.Locator("button[data-testid='reg-submit']");
        public ILocator SubmitButton => _page.Locator("[data-testid='reg-submit']");
        public ILocator SignInInsteadButton => _page.Locator("[data-testid='reg-btn-sign-in']");
        public ILocator CloseButton => _page.Locator("button:has(span:has-text('Close'))");
        public ILocator MaybeLaterButton => _page.Locator("button:has-text('Maybe Later')");

        // === ERROR MESSAGES
        // Validation Error Messages
        public ILocator FirstNameRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The First Name field is required." });
        public ILocator LastNameRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Last Name field is required." });
        public ILocator EmailRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Email Address field is required." });
        public ILocator EmailInvalidFormatError =>
            _page.Locator("div.v-messages__message", new() { HasTextString = "The Email Address field must be a valid email" });
        public ILocator PasswordRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field is required." });
        public ILocator PasswordTooShortError =>
            _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field must be at least 8 characters long." });
        public ILocator PasswordConfirmationMismatchError =>
            _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field confirmation does not match." });

        // hack for when both are displayed
        public ILocator PasswordRequiredErrors => _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field is required." });
        public ILocator PasswordFieldRequiredError => PasswordRequiredErrors.Nth(0);
        public ILocator ConfirmPasswordFieldRequiredError => PasswordRequiredErrors.Nth(1);


        // === MISC ===
        public ILocator Chat => _page.Locator("[data-testid='chatDiv']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='demo-highlight']");
        public ILocator Nav => _page.Locator("[data-testid='nav']");
        public ILocator NameLabel => _page.Locator("[data-testid='name']");
        public ILocator WelcomeMessage =>
            _page.Locator("p.font14.mb-2", new() { HasTextString = "Welcome! Thank you for creating an account." });

        public ILocator AlreadyHaveAccountMessage =>
            _page.Locator("p.ma-1.font14", new() { HasTextString = "Already have an account?" });

        public ILocator BottomSignInButton =>
            _page.Locator("[data-testid='reg-btn-sign-in']");

        public async Task FillAllCreateAccountFields(string firstName, string lastName, string email, string password)
        {
            // enter valid data into the first & Last name fields
            await ElementSendTextToAsync(FirstNameInput, firstName);
            await ElementSendTextToAsync(LastNameInput, lastName);


            // input is populated and retained in those fields
            // email input is also persisting in field
            await Expect(FirstNameInput).ToHaveValueAsync(firstName);
            await Expect(LastNameInput).ToHaveValueAsync(lastName);

            //NOTE NO 2FA NEEDED so a throw AWAY EMAIL IS USED using a throw away email site like"https://temp-mail.org/en/10minutemail" enter the throw away email into the email field
            await ElementSendTextToAsync(EmailInput, email);
            await Expect(EmailInput).ToHaveValueAsync(email);

            // Enter simple password for password and confirmpassword fields or complicated if you want (eg. Qwer1234)
            // input is entered and retained with an obfuscated mask to cover the password from the ui
            await ElementSendTextToAsync(PasswordInput, password);
            await ElementSendTextToAsync(ConfirmPasswordInput, password);

            Assert.That(await PasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));
            Assert.That(await ConfirmPasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));
        }

        public async Task VerifyCreateAccountModalAsync()
        {
            Console.WriteLine("Start VerifyCreateAccountModalAsync");

            // Modal container
            await BaseTest.ElementToBeVisibleAsync(AuthenticationModal);
            await BaseTest.ElementToBeVisibleAsync(RegistrationModal);
            await BaseTest.ElementToBeVisibleAsync(Form);
            await BaseTest.ElementToBeVisibleAsync(CloseDialogButton);

            // Input fields
            await BaseTest.ElementToBeVisibleAsync(FirstNameInput);
            await BaseTest.ElementToBeVisibleAsync(LastNameInput);
            await BaseTest.ElementToBeVisibleAsync(EmailInput);
            await BaseTest.ElementToBeVisibleAsync(PasswordInput);
            await BaseTest.ElementToBeVisibleAsync(ConfirmPasswordInput);

            // Buttons
            await BaseTest.ElementToBeVisibleAsync(CreateAccountButton);
            await BaseTest.ElementToBeVisibleAsync(SubmitButton);
            await BaseTest.ElementToBeVisibleAsync(SignInInsteadButton);

            Console.WriteLine("End VerifyCreateAccountModalAsync");
        }

        public async Task VerifyAllCreateAccountErrorMessagesAreDisplayed()
        {
            await ElementToBeVisibleAsync(FirstNameRequiredError);
            await ElementToBeVisibleAsync(LastNameRequiredError);
            await ElementToBeVisibleAsync(EmailRequiredError);
            await ElementToBeVisibleAsync(PasswordFieldRequiredError);
            await ElementToBeVisibleAsync(ConfirmPasswordFieldRequiredError);
        }

        public async Task CreateanAccount(string nameFirst, string nameLast, string email, string password)
        {
            HeaderPage _headerPage = new(_page);
            await ElementSendTextToAsync(FirstNameInput, nameFirst);
            await ElementSendTextToAsync(LastNameInput, nameLast);

            await Expect(FirstNameInput).ToHaveValueAsync(nameFirst);
            await Expect(LastNameInput).ToHaveValueAsync(nameLast);

            //NOTE NO 2FA NEEDED so a throw AWAY EMAIL IS USED using a throw away email site like"https://temp-mail.org/en/10minutemail" enter the throw away email into the email field
            await ElementSendTextToAsync(EmailInput, email);
            await Expect(EmailInput).ToHaveValueAsync(email);

            // Enter simple password for password and confirmpassword fields or complicated if you want (eg. Qwer1234)
            // input is entered and retained with an obfuscated mask to cover the password from the ui
            await ElementSendTextToAsync(PasswordInput, password);
            await ElementSendTextToAsync(ConfirmPasswordInput, password);

            Assert.That(await PasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));
            Assert.That(await ConfirmPasswordInput.GetAttributeAsync("type"), Is.EqualTo("password"));

            // select Create account button
            await ElementClickAsync(CreateAccountButton);

            // Account is created. Modal closes.
            // Join Stampin' Rewards modal is displayed => user closes that
            await ElementClickAsync(MaybeLaterButton);
            await ElementClickAsync(CloseButton);

            //User is logged in.
            //Header displays: “Hello, {userFirstName}”
            await ElementToHaveTextAsync(_headerPage.FirstNameButton, nameFirst);
        }
    }
}